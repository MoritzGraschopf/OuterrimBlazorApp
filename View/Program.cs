using System.Reflection;
using Domain.Interfaces;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;
using View.ApiRoutes;
using View.Components;

var assembly = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<OuterRimContext>(options =>
{
    options.UseSqlite("Data Source=OuterRim.db", sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(assembly.FullName);
    });
});

builder.Services.AddScoped<IRepository<Aircraft>, AircraftsRepository>();
builder.Services.AddScoped<IRepository<AircraftCrew>, AircraftCrewsRepository>();
builder.Services.AddScoped<IRepository<AircraftSpecification>, AircraftSpecificationsRepository>();
builder.Services.AddScoped<IRepository<Compartment>, CompartmentsRepository>();
builder.Services.AddScoped<IRepository<CrimeSyndicate>, CrimeSyndicatesRepository>();
builder.Services.AddScoped<IRepository<Machinery>, MachineriesRepository>();
builder.Services.AddScoped<IRepository<Mercenary>, MercenariesRepository>();
builder.Services.AddScoped<IRepository<MercenaryReputation>, MercenaryReputationsRepository>();
builder.Services.AddScoped<IAllAircraftDataRepository, AllAircraftDataRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

Map.Get(app);
Map.Post(app);
Map.Put(app);
Map.Delete(app);

app.Run();