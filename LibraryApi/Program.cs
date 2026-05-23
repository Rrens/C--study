using LibraryApi.Data;
using LibraryApi.Repositories;
using LibraryApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LibraryConnectionString")));

builder.Services.AddScoped<IBookRepository, SQLBookRepository>();
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBookRequestQueue, BookRequestQueue>();
builder.Services.AddHostedService<BookRequestWorker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();