To have communication between two computers:
- on the RabbitMQ server (the best way is to use docker container with Management plugin) create new user (base user `guest` can only connect from the localhost by default) and give him permissions.

- in producer's and consumer's code put:
```
var factory = new ConnectionFactory { HostName = "<server's ip>" };
factory.Port = 5672;
factory.UserName = "user";
factory.Password = "123";
factory.VirtualHost = "/";
```