using Microsoft.EntityFrameworkCore;
using ReRailBackEnd.Contexts;
using ReRailBackEnd.Hubs;
using ReRailBackEnd.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(urls: "http://*:7089");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddSingleton<ILocationRectifierService, LocationRectifierService>();
builder.Services.AddControllers();
builder.Services.AddSignalR();
var app = builder.Build();
app.MapControllers();
app.MapHub<DataEntryHub>("/DataEntry");
app.MapHub<UIHub>("/UI");
app.MapHub<ClassificationHub>("/Classification");
app.Run();
