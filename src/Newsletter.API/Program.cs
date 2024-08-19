using Hangfire;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newsletter.Application.Commands_Queries.MakeReservation;
using Newsletter.Application.Extensions;
using Newsletter.Application.Jobs;
using Newsletter.Core.Interfaces;
using Newsletter.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NewsletterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfire((sp, config) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
    config.UseSqlServerStorage(connectionString);
});

// MediatR setup
builder.Services.ConfigureMediatR();

// Email service setup
builder.Services.AddScoped<IEmailService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new SendGridEmailService(
        configuration["SendGrid:ApiKey"],
        configuration["SendGrid:FromEmail"],
        configuration["SendGrid:FromName"]
    );
});

// Register other services
builder.Services.AddTransient<MakeReservationCommandHandler>();

builder.Services.AddHangfireServer();
builder.Services.AddMassTransitWithRabbitMQ();

builder.Services.AddTransient<EmailJob>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard();

app.Run();
