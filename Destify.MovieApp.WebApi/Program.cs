using Destify.MovieApp.Core.Repository;
using Destify.MovieApp.DataAccess.Context;
using Destify.MovieApp.DataAccess.Entities;
using Destify.MovieApp.DataAccess.Repositories;
using Destify.MovieApp.DataAccess.UnitOfWork;
using Destify.MovieApp.WebApi.Authorization;
using Destify.MovieApp.WebApi.MappingProfiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieAppDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("AppConnString")));
builder.Services.AddTransient<IRepository<Movie>, MovieRepository>();
builder.Services.AddTransient<IRepository<Actor>, ActorRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(GeneralProfile).Assembly);
// authorization
builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddScoped<ApiKeyAuthFilter>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(o => o.AddPolicy("AllowAllPolicy", builder =>
{
    builder.SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
}));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAllPolicy");

app.MapFallbackToFile("/index.html");

//intialize database

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    serviceScope.ServiceProvider.GetRequiredService<MovieAppDbContext>().Database.Migrate();
}

app.Run();
