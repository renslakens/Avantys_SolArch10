using ScheduleManagement;
using ScheduleManagement.Models;
using ScheduleManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ScheduleManagementDatabaseSettings>(
    builder.Configuration.GetSection("ScheduleManagementDatabase"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ScheduleConnector>();
builder.Services.AddSingleton<ScheduleService>();
builder.Services.AddSingleton<LessonService>();
builder.Services.AddSingleton<ClassService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

DotNetEnv.Env.Load();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
