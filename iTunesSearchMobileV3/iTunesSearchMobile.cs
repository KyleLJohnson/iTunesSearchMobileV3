using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using iTunesSearchMobileV3.Data;
using iTunesSearchMobileV3.Services;
using Microsoft.AspNetCore.Mvc;

namespace iTunesSearchMobileV3
{
    public static class iTunesSearchMobile
    {
        public static RouteGroupBuilder MapiTunesSearchMobileApi(this RouteGroupBuilder group)
        {
            group.MapGet("/{term}", GetByTerm);
            group.MapPut("/{id}", FindById);
            return group;
        }

        public static async Task<IList<SearchResult>> GetByTerm(string term, IITunesSearchMobileService iTunesSearchMobileService)
        {
            
            return await iTunesSearchMobileService.GetByTerm(term);
        }

        public static async Task<IResult> FindById(int id, IITunesSearchMobileService iTunesSearchMobileService)
        {

             await iTunesSearchMobileService.Find(id);
            return TypedResults.Created("/");
        }
    }
}
