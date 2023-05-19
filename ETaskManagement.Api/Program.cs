using ETaskManagement.Api;
using ETaskManagement.Application;
using ETaskManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
builder.Services.AddHttpContextAccessor();
{
    var configuration = builder.Configuration;
    // IdentityModelEventSource.ShowPII = true;

    // add dependency injection
    services
        .AddPresentation(configuration)
        .AddApplication()
        .AddInfrastructure(configuration)
        .AddPersistence();

    // configure kestrel to listen 
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(5002);
        options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps());
    });
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
    }

    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/ETaskManagement/swagger.json", "E - Task Management");
            options.RoutePrefix = "";
        }
    );

    app.UseCors("CorsPolicy");

    // app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}