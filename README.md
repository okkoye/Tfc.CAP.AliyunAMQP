# CAP 使用阿里云 AMQP 服务

## 安装依赖

```
dotnet add package DotNetCore.CAP
dotnet add package DotNetCore.CAP.RabbitMQ
dotnet add package Tfc.CAP.AliyunAMQP
```

## 修改配置

* appsettings.json 配置

```json
"Cap": {
    "RabbitMQ": {
      "HostName": "", // amqp 访问地址
      "UserName": "", // 阿里云 AccessKey
      "Password": "" // 阿里云访问 AccessSecret
    },
    "Aliyun": {
      "VirtualHost": "" // 阿里云AMQP创建
    }
  }
```

* Startup 的 ConfigureServices 添加如下配置

```
context.AddCap(capOptions =>
            {
                ......
                capOptions.UseRabbitMQ(_ =>
                {
                    _.HostName = configuration["Cap:RabbitMQ:HostName"];
                    _.UserName = configuration["Cap:RabbitMQ:UserName"];
                    _.Password = configuration["Cap:RabbitMQ:Password"];

                    _.ConnectionFactoryOptions = options =>
                    {
                        options.VirtualHost = configuration["Cap:Aliyun:VirtualHost"];
                        options.AuthMechanisms = new List<IAuthMechanismFactory>() {new AliyunMechanismFactory()};
                    };
                });
            });
```

* 支持 `Abp.EventBus.CAP`， 配置和上面保持一致
