using FrameworkTask1.Errors;
using FrameworkTask1.Middleware;
using FrameworkTask1.Models;
using FrameworkTask1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PerfumeStore>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<RequestIdMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<TimingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();


app.MapGet("/api/perfumes", (PerfumeStore store) =>
{
    var perfumes = store.GetAll();
    return Results.Ok(perfumes);
});


app.MapGet("/api/perfumes/{id:guid}", (Guid id, PerfumeStore store, HttpContext context) =>
{
    var perfume = store.GetById(id);
    if (perfume is null)
    {
        throw new NotFoundException($"Парфюм с идентификатором {id} не найден.");
    }
    return Results.Ok(perfume);
});


app.MapPost("/api/perfumes", (CreatePerfumeRequest request, PerfumeStore store, HttpContext context) =>
{
    // Валидация входных данных
    if (string.IsNullOrWhiteSpace(request.Name))
        throw new ValidationException("Название парфюма не может быть пустым.");

    if (request.Price <= 0)
        throw new ValidationException("Цена должна быть больше 0.");
    
    if (request.VolumeInMl <= 0)
        throw new ValidationException("Объем в мл должен быть больше 0.");

    var perfume = store.Add(request);
    return Results.Created($"/api/perfumes/{perfume.Id}", perfume);
});

app.Run();