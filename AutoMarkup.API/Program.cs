using Application.Abstractions;
using Application.Services;
using Infrastructure.DbContexts;
using Infrastructure.Interface;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Service Logger Useing Serilog
Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.Console()
           .WriteTo.File(builder.Configuration["Logger:PathFileToLogger"]!,
                            rollingInterval: RollingInterval.Day)
           .CreateLogger();
builder.Host.UseSerilog();


// Add Service DbContext
builder.Services.AddDbContext<AutoMarkupDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Service Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Service Repository to entity frame_worke
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IElementRepository, ElementRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IStyleRuleRepository, StyleRuleRepository>();

// Add Code Generation Service 
builder.Services.AddScoped<ICodeGenerationService, CodeGenerationService>();
builder.Services.AddScoped<IBuildeCode, BuildeCode>();

// Add Service Account
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IElementService, ElementService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IStyleRuleRepository, StyleRuleRepository>();
builder.Services.AddScoped<IPageService, PageService>();

//
builder.Services.AddScoped<IFileDownloadService, FileDownloadService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
