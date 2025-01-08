using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MSCaddie.Client;
using MSCaddie.Client.Utils;
using MSCaddie.Shared.Containers;
using MSCaddie.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICompetitionService, CompetitionService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IMatchPlayService, MatchPlayService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<MatchResultContainerList>();
builder.Services.AddScoped<IHttpResponseHelper, HttpResponseHelper>();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

await builder.Build().RunAsync();
