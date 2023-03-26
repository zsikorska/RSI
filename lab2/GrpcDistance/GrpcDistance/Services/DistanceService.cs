using System;
using System.Globalization;
using Grpc.Core;
using GrpcDistance;


namespace GrpcDistance.Services
{
    public class DistanceService : Distance.DistanceBase
    {
        private readonly ILogger<DistanceService> _logger;
        public DistanceService(ILogger<DistanceService> logger)
        {
            _logger = logger;
        }

        public override Task<DistanceReply> TwoCityDistance(TwoCityRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DistanceReply
            { 
                Distance = Distance(request.Lat1, request.Lon1, request.Lat2, request.Lon2)
            });
        }

        public override Task<DistanceReply> ThreeCityDistance(ThreeCityRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DistanceReply
            {
                Distance = Distance(request.Lat1, request.Lon1, request.Lat2, request.Lon2) + Distance(request.Lat2, request.Lon2, request.Lat3, request.Lon3) 
            });
        }

        public override Task<DistanceReply> WarsawDistance(WarsawRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DistanceReply
            {
                Distance = Distance(request.Lat1, request.Lon1, 52.231651, 21.006245)
            });
        }

        public double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double r = 6371;
            double distance = 2 * r * Math.Asin(
                Math.Sqrt(
                    Math.Pow(Math.Sin((lat2-lat1)/2),2) + 
                    Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin((lon2 - lon1) / 2), 2)
                    )
                );
            return distance;
        }
    }
}