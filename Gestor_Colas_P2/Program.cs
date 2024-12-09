using Amazon.SQS;
using Gestor_Colas_P2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Registro del servicio
var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Profile = "AdminAccess"; // O el perfil que configuraste
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddSingleton<SQS_Service>();
builder.Services.AddAWSService<IAmazonSQS>(); // Configura Amazon SQS
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