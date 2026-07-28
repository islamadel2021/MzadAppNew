using Grpc.Net.Client;
using MzadService;

namespace TenderService;

public class GrpcMzadClient(ILogger<GrpcMzadClient> logger, IConfiguration configuration)
{
    private readonly ILogger<GrpcMzadClient> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    public Mzad GetMzad(string id)
    {
        _logger.LogInformation("Calling GRPC Service");
        var channel = GrpcChannel.ForAddress(_configuration["GrpcMzadUrl"]);
        var client = new GrpcMzad.GrpcMzadClient(channel);
        var request = new GetMzadRequest { Id = id };
        try
        {
            var reply = client.GetMzad(request);
            var mzad = new Mzad
            {
                ID = reply.Mzad.Id,
                MzadEnd = DateTime.Parse(reply.Mzad.MzadEnd),
                Seller = reply.Mzad.Seller,
                ReservePrice = reply.Mzad.ReservePrice
            };

            return mzad;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not call GRPC Server");
            return null;
        }
    }
}
