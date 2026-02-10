using Application.UseCases.Persons;
using Data;
using Data.Repositories;
using Domain;
using Domain.Abstractions;
using WebApi.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddData(connectionString);

builder.Services.AddScoped<CreatePersonUseCase>();
builder.Services.AddScoped<DeletePersonUseCase>();
builder.Services.AddScoped<GetAllPersonsUseCase>();
builder.Services.AddScoped<GetPersonByIdUseCase>();
builder.Services.AddScoped<GetPersonByCodeUseCase>();
builder.Services.AddScoped<UpdatePersonUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapPersonsEndPoints();
app.MapVisitsEndPoints();

app.Run();

