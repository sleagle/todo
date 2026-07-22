using TodoApi.Data.Repositories;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITodoRepository, JsonTodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService >();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin",
        builder =>
        {    
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowOrigin");
app.MapControllers();
app.Run();
