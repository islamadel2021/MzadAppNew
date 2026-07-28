using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace TenderService;

[ApiController]
[Route("api/[controller]")]
public class TenderController(IMapper mapper, IPublishEndpoint publishEndpoint, GrpcMzadClient grpcMzadClient) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly GrpcMzadClient _grpcMzadClient = grpcMzadClient;

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TenderDTO>> PlaceTender(string mzadId, int amount)
    {
        var mzad = await DB.Find<Mzad>().OneAsync(mzadId);

        if (mzad == null)
        {
            mzad = _grpcMzadClient.GetMzad(mzadId);
            if (mzad == null) return BadRequest("Cannot placed tender for non existing mzad");
        }

        if (mzad.Seller.ToLower() == User.Identity.Name)
        {
            return BadRequest("You cannot place tender on your own mzad");
        }

        var tender = new Tender
        {
            Amount = amount,
            MzadId = mzadId,
            TenderOwner = User.Identity.Name
        };

        if (mzad.MzadEnd < DateTime.UtcNow)
        {
            tender.TenderStatus = TenderStatus.Finished;
        }
        else
        {
            var highTender = await DB.Find<Tender>()
                        .Match(t => t.MzadId == mzadId)
                        .Sort(a => a.Descending(x => x.Amount))
                        .ExecuteFirstAsync();

            if (highTender != null && amount > highTender.Amount || highTender == null)
            {
                tender.TenderStatus = amount > mzad.ReservePrice
                    ? TenderStatus.Accepted
                    : TenderStatus.AcceptedBelowReserve;
            }

            if (highTender != null && tender.Amount <= highTender.Amount)
            {
                tender.TenderStatus = TenderStatus.TooLow;
            }
        }

        await DB.SaveAsync(tender);

        await _publishEndpoint.Publish(_mapper.Map<TenderPlaced>(tender));

        return Ok(_mapper.Map<TenderDTO>(tender));
    }

    [HttpGet("{mzadId}")]
    public async Task<ActionResult<List<TenderDTO>>> GetTendersForMzad(string mzadId)
    {
        var tenders = await DB.Find<Tender>()
            .Match(t => t.MzadId == mzadId)
            .Sort(a => a.Descending(b => b.TenderTime))
            .ExecuteAsync();

        return tenders.Select(_mapper.Map<TenderDTO>).ToList();
    }
}

