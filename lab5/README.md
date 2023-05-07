To have communication between two computers:
- in server's host code change `Program.cs` file to:
```
Uri baseAddress = new Uri("http://<server's ip>:5000/WcfService");
```

- in client's code change channel in `Program.cs` file to:
```
Uri baseAddress = new Uri("http://<server's ip>:5000/WcfService/endpoint1");
```
And also in file `App.config`:
```
<client>
    <endpoint address="http://<server's ip>:5000/WcfService/endpoint1"
        binding="basicHttpBinding" bindingConfiguration="BasicHttpBinding_ICalculator"
        contract="ServiceReference1.ICalculator" name="BasicHttpBinding_ICalculator" />

    <endpoint address="http://<server's ip>:5000/WcfService/endpoint2"
        binding="wsHttpBinding" bindingConfiguration="WSHttpBinding_ICalculator"
        contract="ServiceReference1.ICalculator" name="WSHttpBinding_ICalculator" />
</client>
```