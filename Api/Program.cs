using CrossCutting.AppDependencies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MassTransit;
using Infrastructure.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Configuração JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:8081")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();

//configuração do MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ContactLineConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var uriBuilder = new UriBuilder
        {
            Scheme = "rabbitmq",
            Host = "localhost",
            Port = 5672,
            Path = "/"
        };

        cfg.Host(uriBuilder.Uri, h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("contact_queue", e =>
        {
            e.ConfigureConsumer<ContactLineConsumer>(context);
        });
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
