using iTunesSearchMobileV3.Data;
using Microsoft.ApplicationInsights;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace iTunesSearchMobileV3.Services
{
    public class ITunesSearchMobileService : IITunesSearchMobileService
    {
        private readonly TrackClickCountDbContext _dbContext;
        private readonly TelemetryClient _client;

        public ITunesSearchMobileService(TrackClickCountDbContext dbContext, TelemetryClient telemetryClient)
        {
            _dbContext = dbContext;
            _client = telemetryClient;
        }

        public async Task<IList<SearchResult>> GetByTerm(string term)
        {
            _client.TrackEvent("Called GetByTerm");
            HttpClient client = new HttpClient();
            IList<SearchResult> searchResults = new List<SearchResult>();

            var path = "https://itunes.apple.com/search?term=";
            HttpResponseMessage response = await client.GetAsync(path + term);
            if (response.IsSuccessStatusCode)
            {

                string JSONSearchResults = await response.Content.ReadAsStringAsync();

                APIResult? results = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResult>(JSONSearchResults);

                if ((results != null) && (results.Results != null))
                {
                    searchResults = results.Results.ToList();
                }

                //Not joining to update the click count

                foreach (var a in searchResults)
                {
                    var trackClickCount = await _dbContext.TrackClickCount.FindAsync(a.TrackId);

                    if (trackClickCount != null)
                        a.ClickCount = trackClickCount.ClickCount;

                }
            }
            _client.TrackEvent("Finish Calling GetByTerm");
            return searchResults;

        }

        public async Task Find(int id)
        {
            _client.TrackEvent("Called Find with id: " + id);
            var searchResult = await _dbContext.TrackClickCount.FindAsync(id);
            TrackClickCount tcc = new TrackClickCount();

            if (searchResult is null)
            {
                await _dbContext.Database.EnsureCreatedAsync();

                _dbContext.Add(new TrackClickCount
                {
                    TrackId = id,
                    ClickCount = 1
                });
            }
            else
                searchResult.ClickCount += 1;

            await _dbContext.SaveChangesAsync();

            _client.TrackEvent("Finish calling Find with id: " + id);
        }
    }
}
