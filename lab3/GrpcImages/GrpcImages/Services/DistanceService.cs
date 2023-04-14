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


        public double Distance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            double lat1 = (Math.PI / 180) * latitude1;
            double lon1 = (Math.PI / 180) * longitude1;
            double lat2 = (Math.PI / 180) * latitude2;
            double lon2 = (Math.PI / 180) * longitude2;


            double R = 6371;
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;

            return distance;
        }

    }
}