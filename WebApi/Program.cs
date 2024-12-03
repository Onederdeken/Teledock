
using Teledock.DI;

var builder = WebApplication.CreateBuilder(args);
//DI
builder.Services.AddMyDependencyGroup(builder.Configuration);
var app = builder.Build();
// Midleware.
app.AddMyMidleware();
app.Run();

