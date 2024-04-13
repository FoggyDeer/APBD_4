using APBD_4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var _animals = new List<Animal>();
var _visits = new List<Visit>();

app.MapGet("/api/animals", () => Results.Ok(_animals))
    .WithName("GetAnimals")
    .WithOpenApi();

app.MapGet("/api/animals/{id:int}", (int id) =>
    {
        var animal = _animals.FirstOrDefault(a => a.IdAnimal == id);
        return animal == null ? Results.NotFound($"Animals with id {id} was not found") : Results.Ok(animal);
    })
    .WithName("GetAnimal")
    .WithOpenApi();

app.MapPost("/api/animals", (Animal animal) =>
    {
        _animals.Add(animal);
        return Results.StatusCode(StatusCodes.Status201Created);
    })
    .WithName("AddAnimal")
    .WithOpenApi();

app.MapPut("/api/animals/{id:int}", (int id, Animal animal) =>
    {
        var animalToEdit = _animals.FirstOrDefault(a => a.IdAnimal == id);
        if (animalToEdit == null)
        {
            return Results.NotFound($"Student with id {id} was not found");
        }
        _animals.Remove(animalToEdit);
        _animals.Add(animal);
        return Results.NoContent();
    })
    .WithName("UpdateAnimal")
    .WithOpenApi();

app.MapDelete("/api/animals/{id:int}", (int id) =>
    {
        var animalToDelete = _animals.FirstOrDefault(s => s.IdAnimal == id);
        if (animalToDelete == null)
        {
            return Results.NoContent();
        }
        _animals.Remove(animalToDelete);
        return Results.NoContent();
    })
    .WithName("DeleteAnimal")
    .WithOpenApi();

app.MapGet("/api/visits/{id:int}", (int id) =>
    {
        var visits = _visits.Find(a => a.Animal.IdAnimal == id);
        return visits == null ? Results.NotFound($"Visits for animal with id {id} were not found") : Results.Ok(visits);
    })
    .WithName("GetAnimal")
    .WithOpenApi();

app.MapPost("/api/visits", (Visit visit) =>
    {
        _visits.Add(visit);
        return Results.StatusCode(StatusCodes.Status201Created);
    })
    .WithName("AddAnimal")
    .WithOpenApi();

app.Run();