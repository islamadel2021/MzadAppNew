using Grpc.Core;
using MzadService.Data;

namespace MzadService;

public class GrpcMzadService(MzadDbContext dbContext) : GrpcMzad.GrpcMzadBase
{
    private readonly MzadDbContext _dbContext = dbContext;
    public override async Task<GrpcMzadResponse> GetMzad(GetMzadRequest request, ServerCallContext context)
    {
        Console.WriteLine("==> Received gRPC Request for mzad");
        var mzad = await _dbContext.Mzadat.FindAsync(Guid.Parse(request.Id)) ??
            throw new RpcException(new Status(StatusCode.NotFound, "Mzad not found"));
        var response = new GrpcMzadResponse
        {
            Mzad = new GrpcMzadModel
            {
                Id = mzad.Id.ToString(),
                Seller = mzad.Seller,
                MzadEnd = mzad.MzadEnd.ToString(),
                ReservePrice = mzad.ReservePrice
            }
        };
        return response;
    }
}
