using Microsoft.EntityFrameworkCore;
using OleksiiHavryk.Finances.API.Data;
using OleksiiHavryk.Finances.API.Data.Interfaces;
using OleksiiHavryk.Finances.API.Domain;
using OleksiiHavryk.Finances.API.DTO;
using OleksiiHavryk.Finances.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var dbConStr = config.GetConnectionString("Default");

//Services
services.AddControllers();

services.AddDbContext<ResourcesDbContext>(
    opt =>
    {
        opt.EnableSensitiveDataLogging();
        opt.UseSqlServer(
            connectionString: dbConStr);
    });

services.AddScoped<ExceptionHandlerMiddleware>();

services.AddScoped<ICurrencyRepository, CurrencyRepository>();
services.AddScoped<IRecordRepository, RecordRepository>();
services.AddScoped<IEntryRepository, EntryRepository>();

services.AddAutoMapper(opt =>
{
    opt.CreateMap<EntryDto, Entry>();
    opt.CreateMap<Entry, EntryDto>();
    opt.CreateMap<CurrencyDto, Currency>();
    opt.CreateMap<Currency, CurrencyDto>();
    opt.CreateMap<RecordDto, Record>();
    opt.CreateMap<Record, RecordDto>();
    opt.CreateMap<EntryType, EntryTypeDto>();
    opt.CreateMap<EntryTypeDto, EntryType>();
});

services.AddCors(opt => opt.AddDefaultPolicy(p =>
{
    p.AllowAnyHeader();
    p.AllowAnyOrigin();
    p.AllowAnyMethod();
}));

var app = builder.Build();

//Middleware
app.UseRouting();
app.UseCors();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();