using GalytixAssessment.API.Utility;
using GalytixAssessment.BLL.BusinessLogic;
using GalytixAssessment.BLL.IBusinessLogic;
using GalytixAssessment.DAL;
using GalytixAssessment.DAL.IRepository;
using GalytixAssessment.DAL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GalytixDbContext>
    (options => options.UseInMemoryDatabase("GalytixDB"));

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IGwpByCountryRepository, GwpByCountryRepository>();
builder.Services.AddScoped<IGwpByCountryBusinessLogic, GwpByCountryBusinessLogic>();
var app = builder.Build();

LoadData.AddCountryData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//builder.Services.AddScoped<>();
//builder.Services.AddScoped<>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
