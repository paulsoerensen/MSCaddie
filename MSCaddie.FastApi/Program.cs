using MSCaddie.Repository.Dtos;
using MSCaddie.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositoryServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));

var group = app.MapGroup("/api/match-repository");

// Competition
group.MapGet("/competitions", async (IMatchRepository repo) =>
    Results.Ok(await repo.GetCompetitions()));

group.MapGet("/competitions/{matchId:int}/results", async (int matchId, IMatchRepository repo) =>
    Results.Ok(await repo.GetCompetitionResults(matchId)));

group.MapPost("/competitions/results", async (CompetitionResultDto dto, IMatchRepository repo) =>
    Results.Ok(await repo.UpsertCompetitionResult(dto)));

group.MapDelete("/competitions/results/{resultId:int}", async (int resultId, IMatchRepository repo) =>
    Results.Ok(await repo.DeleteCompetitionResult(resultId)));

// NearestPin
group.MapGet("/nearest-pin/{id:int}", async (int id, IMatchRepository repo) =>
    Results.Ok(await repo.GetNearestPinResult(id)));

group.MapGet("/nearest-pin/match/{matchId:int}", async (int matchId, IMatchRepository repo) =>
    Results.Ok(await repo.GetNearestPinResults(matchId)));

group.MapPut("/nearest-pin", async (NearestPinResultDto dto, IMatchRepository repo) =>
    Results.Ok(await repo.UpdateNearestPinResult(dto)));

group.MapDelete("/nearest-pin/{id:int}", async (int id, IMatchRepository repo) =>
    Results.Ok(await repo.DeleteNearestPinResult(id)));

// Match
group.MapGet("/matches/{matchId:int}", async (int matchId, IMatchRepository repo) =>
    Results.Ok(await repo.GetMatch(matchId)));

group.MapGet("/matches", async (IMatchRepository repo) =>
    Results.Ok(await repo.GetMatchList()));

group.MapGet("/matches/range", async (DateTime start, DateTime end, IMatchRepository repo) =>
    Results.Ok(await repo.GetMatchList(start, end)));

group.MapGet("/matches/season/{season:int}", async (int season, IMatchRepository repo) =>
    Results.Ok(await repo.GetSeasonMatchList(season)));

group.MapPost("/matches", async (MatchDto dto, IMatchRepository repo) =>
    Results.Ok(await repo.MatchUpsert(dto)));

group.MapGet("/matches/{matchId:int}/results", async (int matchId, IMatchRepository repo) =>
    Results.Ok(await repo.GetMatchResults(matchId)));

group.MapGet("/matches/{matchId:int}/results/registration", async (int matchId, IMatchRepository repo) =>
    Results.Ok(await repo.GetMatchResultForRegistration(matchId)));

group.MapPost("/matches/results", async (MatchResultDto dto, IMatchRepository repo) =>
    Results.Ok(await repo.MatchResultUpsert(dto)));

group.MapPost("/matches/registration", async (MatchRegistrationDto dto, IMatchRepository repo) =>
    Results.Ok(await repo.MatchRegistrationUpsert(dto)));

group.MapDelete("/matches/results/{id:int}", async (int id, IMatchRepository repo) =>
    Results.Ok(await repo.MatchResultDelete(id)));

group.MapGet("/matches/{matchId:int}/birdies", async (int matchId, IMatchRepository repo) =>
    Results.Ok(await repo.GetMatchBirdies(matchId)));

group.MapPost("/matches/{matchId:int}/settlement", async (int matchId, IMatchRepository repo) =>
    Results.Ok(await repo.MatchResultSettlement(matchId)));

group.MapGet("/matchforms", async (IMatchRepository repo) =>
    Results.Ok(await repo.GetMatchforms()));

app.Run();
