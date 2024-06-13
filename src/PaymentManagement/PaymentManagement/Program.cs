using PaymentManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register PaymentConnector as a singleton service
builder.Services.AddSingleton<PaymentConnector>();

// Register the background service
builder.Services.AddHostedService<PaymentReceiverService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

DotNetEnv.Env.Load();


app.UseAuthorization();

app.MapControllers();

app.Run();