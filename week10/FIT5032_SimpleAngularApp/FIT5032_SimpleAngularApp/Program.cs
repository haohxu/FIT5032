using Microsoft.EntityFrameworkCore;
using FIT5032_SimpleAngularApp.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    )
);

builder.Services.AddControllers();
builder.Services.AddDbContext<UnitContext>(opt =>
    opt.UseInMemoryDatabase("UnitList"));

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
