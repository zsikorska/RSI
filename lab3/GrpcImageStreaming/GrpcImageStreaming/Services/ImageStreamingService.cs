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


        private const int BUFFER_SIZE = 2048;
        private static String mainPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static String path = Path.Combine(mainPath, "images");
        private static String receivedPath = Path.Combine(mainPath, "received");

        public override async Task<Empty> SendImageToServer(IAsyncStreamReader<ImageData> requestStream, ServerCallContext context)
        {
            String fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            
            try
            {
                var counter = 0;
                using (var receivedImageStream = File.Create(Path.Combine(receivedPath, fileName)))
                {
                    while (await requestStream.MoveNext())
                    {
                        var imageData = requestStream.Current;
                        await receivedImageStream.WriteAsync(imageData.Data.ToByteArray());
                        counter++;
                        Console.WriteLine($"Odebrano pakiet o numerze {counter}");

                    }
                }
                Console.WriteLine("Odebrano " + counter + " pakietów.");
                Console.WriteLine("Zdjęcie odebrane pomyślnie.\n");
        }
            catch
            {
                Console.WriteLine("Nie znaleziono pliku.\n");
            }

            return new Empty();
        }


        public override async Task SendImageToClient(ImageName request, IServerStreamWriter<ImageData> responseStream, ServerCallContext context)
        {
            String fileName = request.Filename;
            //String fileName = "c.jpg";
            try
            {
                var imageBytesBuffer = new byte[BUFFER_SIZE];
                var counter = 0;
                using (var imageStream = File.OpenRead(Path.Combine(path, fileName)))
                {
                    int imageBytesRead;
                    while ((imageBytesRead = await imageStream.ReadAsync(imageBytesBuffer, 0, imageBytesBuffer.Length)) > 0)
                    {
                        var imageData = new ImageData { Data = ByteString.CopyFrom(imageBytesBuffer, 0, imageBytesRead) };
                        await responseStream.WriteAsync(imageData);
                        counter++;
                        Console.WriteLine($"Wysłano pakiet o numerze {counter}");
                    }
                }
                Console.WriteLine("Wysłano " + counter + " pakietów.");
                Console.WriteLine("Wysyłanie zakończone pomyślnie.\n");
            }
            catch
            {
                Console.WriteLine("Nie znaleziono pliku.\n");
            }
        }

    }
}