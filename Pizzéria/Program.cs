using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Pizzéria.Models;
using System.Data;

var builder = WebApplication.CreateBuilder(args); 
var connectionString= builder.Configuration.GetConnectionString("Pizzas")??"Data Source=Pizzas.db";
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSqlite<PizzaATDB>(connectionString);
builder.Services.AddSwaggerGen (c => 
{ 
    c. SwaggerDoc("v1", new OpenApiInfo {
         Title = " API Pizzéria ",
         Description =  "Faire les pizzas que vous aimez ",
        Version = "v1" 
        });
}); 
var app = builder.Build(); 
app.UseSwagger();
app.UseSwaggerUI (c => 
{
    c. SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzéria API V1"); 
}); 
app.MapGet("/", () => "Ecole Supérieure Polytechnique DIT2 2024"); 
app.MapGet("/pizzas", async(PizzaATDB db)=>await db.Pizzas.ToListAsync());
app.MapPost("/pizza", async(PizzaATDB db, PizzaAT pizza)=>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.IdAT}", pizza);
});
app.MapPut("/pizza/{id}", async (PizzaATDB db, PizzaAT updatepizza, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.NomAT = updatepizza.NomAT;
    pizza.DescriptionAT = updatepizza.DescriptionAT;
    await db.SaveChangesAsync(); 
    return Results.NoContent();
});
app.MapDelete("/pizza/{id}", async(PizzaATDB db, int id)=>
{
    var pizza= await db.Pizzas.FindAsync(id);
    if(pizza is null)
    {
        return Results.NotFound();
    }
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.Run();
