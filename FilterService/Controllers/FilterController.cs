using FilterService.Entities;
using FilterService.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace FilterService;

[ApiController]
[Route("api/[controller]")]
public class FilterController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Mzad>>> SearchMzadat([FromQuery] SearchParams searchParams)
    {
        var query = DB.PagedSearch<Mzad, Mzad>();

        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }
        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            query.Match(m => m.Seller == searchParams.Seller);
        }
        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            query.Match(m => m.Winner == searchParams.Winner);
        }

        query = searchParams.OrderBy switch
        {
            "name" => query.Sort(m => m.Ascending(h => h.Name))
                .Sort(m => m.Ascending(h => h.Father)),
            "new" => query.Sort(m => m.Descending(x => x.CreatedAt)),
            _ => query.Sort(m => m.Ascending(x => x.MzadEnd))
        };

        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(m => m.MzadEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(m => m.MzadEnd < DateTime.UtcNow.AddDays(1)
                && m.MzadEnd > DateTime.UtcNow),
            _ => query.Match(m => m.MzadEnd > DateTime.UtcNow)
        };

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);
        var result = await query.ExecuteAsync();
        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }

}
