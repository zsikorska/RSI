using System;
using System.Globalization;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcImageStreaming;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;


namespace GrpcImageStreaming.Services
{
    public class ImageStreamingService : ImageStreaming.ImageStreamingBase
    {
        private readonly ILogger<ImageStreamingService> _logger;

        public ImageStreamingService(ILogger<ImageStreamingService> logger)
        {
            _logger = logger;
        }

        private String path = "E:\\STUDIA\\6sem\\rsi\\laby\\RSI\\lab3\\GrpcImageStreaming\\GrpcImageStreaming\\images\\";
        private String receivedPath = "E:\\STUDIA\\6sem\\rsi\\laby\\RSI\\lab3\\GrpcImageStreaming\\GrpcImageStreaming\\received\\";
        private Random random = new Random();

        public override async Task<Empty> SendImageToServer(IAsyncStreamReader<ImageData> requestStream, ServerCallContext context)
        {
            String fileName = random.Next(1, 100000).ToString() + ".jpg";
            try
            {
                using (var receivedImageStream = File.Create(receivedPath + fileName))
                {
                    while (await requestStream.MoveNext())
                    {
                        var imageData = requestStream.Current;
                        await receivedImageStream.WriteAsync(imageData.Data.ToByteArray());

                    }
                }
                Console.WriteLine("Zdjêcie odebrane pomyœlnie.\n");
        }
            catch
            {
                Console.WriteLine("Nie znaleziono pliku.\n");
            }

            return new Empty();
        }


        public override async Task SendImageToClient(Empty request, IServerStreamWriter<ImageData> responseStream, ServerCallContext context)
        {
            String fileName = "c.jpg";
            try
            {
                var imageBytesBuffer = new byte[256];
                using (var imageStream = File.OpenRead(path + fileName))
                {
                    int imageBytesRead;
                    while ((imageBytesRead = await imageStream.ReadAsync(imageBytesBuffer, 0, imageBytesBuffer.Length)) > 0)
                    {
                        var imageData = new ImageData { Data = ByteString.CopyFrom(imageBytesBuffer, 0, imageBytesRead) };
                        await responseStream.WriteAsync(imageData);
                    }
                }
                Console.WriteLine("Wysy³anie zakoñczone pomyœlnie.\n");
            }
            catch
            {
                Console.WriteLine("Nie znaleziono pliku.\n");
            }
        }

    }
}