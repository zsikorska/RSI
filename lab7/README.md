To have communication between two computers:
- in server's host code change `.vs/MyWebService/config/applicationhost.config` file to:
```
<bindings>
    <binding protocol="http" bindingInformation="*:2119:<server's ip>" />
</bindings>
```
and in project's options -> Internet -> Servers -> IIS Express change project's URL address to have there server's ip instead of localhost.

- in client's code change urls in `index.js` file to:
```
const JSON_URL = "http://<server's ip>:2119/MyRestService.svc/json";
const XML_URL = "http://<server's ip>:2119/MyRestService.svc";
```