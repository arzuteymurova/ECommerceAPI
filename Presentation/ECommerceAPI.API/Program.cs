using ECommerceAPI.API;
using ECommerceAPI.Application;
using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Enums;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure.Services.Identity;
using ECommerceAPI.SignalR;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using ECommerceAPI.API.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<RolePermissionFilter>();
})
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWTOptions"));
JWTOptions jwtSettings = builder.Configuration.GetSection("JWTOptions").Get<JWTOptions>();

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();

builder.Services.AddSwaggerSettings();
builder.Services.AuthenticationJwtSettings(jwtSettings);

builder.Services.AddStorage(StorageType.Local);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:7929", "https://localhost:7279").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));


var columnOpts = new ColumnOptions();

columnOpts.Store.Remove(StandardColumn.Properties);
columnOpts.Store.Add(StandardColumn.LogEvent);
columnOpts.LogEvent.DataLength = 2048;
columnOpts.PrimaryKey = columnOpts.TimeStamp;
columnOpts.TimeStamp.NonClusteredIndex = true;

columnOpts.AdditionalColumns = new Collection<SqlColumn>
{
    new SqlColumn { ColumnName = "UserName", DataType = SqlDbType.NVarChar, DataLength = 256 }
};

Logger logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.MSSqlServer(
        connectionString: @"Server=(localdb)\MSSQLLocalDB;Database=ECommerceDb;Trusted_Connection=True;",
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true
        },
        columnOptions: columnOpts
    )
    .CreateLogger();

Log.Logger = logger;

builder.Host.UseSerilog(logger);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHubs();

app.Run();
