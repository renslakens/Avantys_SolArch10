using ExamManagement;
using ExamManagement.Models;
using ExamManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ExamManagementDatabaseSettings>(
    builder.Configuration.GetSection("ExamManagementDatabase"));

builder.Services.AddSingleton<ExamsService>();
builder.Services.AddSingleton<ProctorsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ExamConnector>();
builder.Services.AddHostedService<ExamReceiverService>();

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

