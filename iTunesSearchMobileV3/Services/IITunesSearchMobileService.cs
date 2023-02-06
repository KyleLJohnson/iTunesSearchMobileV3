using iTunesSearchMobileV3.Data;

namespace iTunesSearchMobileV3.Services
{
    public interface IITunesSearchMobileService
    {
        Task<IList<SearchResult>> GetByTerm(string term);

        Task Find(int id);
    }
}
