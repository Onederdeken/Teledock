using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Teledock.Abstractions;
using Teledock.dbContext;
using Teledock.DI;
using Teledock.Repositories;
using Teledock.Services;

var builder = WebApplication.CreateBuilder(args);
//DI
builder.Services.AddMyDependencyGroup(builder.Configuration);
var app = builder.Build();
// Midleware.
app.AddMyMidleware();
app.Run();

