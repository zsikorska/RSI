using System;
using System.Globalization;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcImageStreaming;

var path = "E:\\STUDIA\\6sem\\rsi\\laby\\RSI\\lab3\\GrpcImageStreaming\\GrpcImageStreaming\\images\\";
var receivedPath = "E:\\STUDIA\\6sem\\rsi\\laby\\RSI\\lab3\\GrpcImageStreaming\\GrpcImageStreaming\\received\\";
var fileName = "c.jpg";


namespace GrpcImageStreaming.Services
{
    public class ImageStreamingService : ImageStreaming.ImageStreamingBase
    {
        private readonly ILogger<ImageStreamingService> _logger;

        public ImageStreamingService(ILogger<ImageStreamingService> logger)
        {
            _logger = logger;
        }

        public override async Task SendImageToServer(IAsyncStreamReader<ImageData> requestStream, ServerCallContext context)
        {
            try
            {
                using (var receivedImageStream = File.Create(receivedPath + fileName)
                    {
                    while (await requestStream.MoveNext())
                    {
                        var imageData = call.ResponseStream.Current;
                        await receivedImageStream.WriteAsync(imageData.Data.ToByteArray());

                    }
                }
                Console.WriteLine("Zdjêcie odebrane pomyœlnie.")
        }
            catch
            {
                Console.WriteLine("Nie znaleziono pliku.");
            }
        }


        public override async Task SendImageToClient(Empty request, IServerStreamWriter<ImageData> responseStream, ServerCallContext context)
        {
            try
            {
                var imageBytesBuffer = new byte[256];
                using (var imageStream = File.OpenRead(path + fileName))
                {
                    int imageBytesRead;
                    while ((imageBytesRead = await imageStream.ReadAsync(imageBytesBuffer, 0, imageBytesBuffer.Length)) > 0)
                    {
                        var imageData = new ImageData { Data = ByteString.CopyFrom(imageBytesBuffer, 0, bytesRead) };
                        await responseStream.WriteAsync(imageData);
                    }
                }
                Console.WriteLine("Wysy³anie zakoñczone pomyœlnie.");
            }
            catch
            {
                Console.WriteLine("Nie znaleziono pliku.");
            }
        }

    }
}