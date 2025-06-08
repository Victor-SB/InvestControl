using InvestControl.Infrastructure.Context;
using InvestControl.Worker;
using Microsoft.EntityFrameworkCore;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                context.Configuration.GetConnectionString("MySqlConnection"),
                ServerVersion.AutoDetect(context.Configuration.GetConnectionString("MySqlConnection"))
            )
        );
    })
    .Build();

host.Run();
