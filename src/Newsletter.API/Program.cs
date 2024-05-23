using Hangfire;
using Microsoft.EntityFrameworkCore;
using Newsletter.Application.Jobs;
using Newsletter.Application.Services;
using Newsletter.Core.Interfaces;
using Newsletter.Infrastructure.Data;
using Newsletter.Infrastructure.Extensions;
using Newsletter.Infrastructure.Services;
using System;

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

builder.Services.AddHangfireServer();

builder.Services.AddTransient<IEmailService, SendGridEmailService>();
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
