To have communication between two computers:
- in server's code change `appsettings.json` file to:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "https://<server's ip>:5001",
        "Protocols": "Http1AndHttp2"
      },
      "gRPC": {
        "Url": "http://<server's ip>:5000",
        "Protocols": "Http2"
      }
    }
  }
}
```

- in client's code change channel in `Program.cs` file to:
```
using var channel = GrpcChannel.ForAddress("http://<server's ip>:5000");
```
