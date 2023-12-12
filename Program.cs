using ListaTarefas.Data;
using ListaTarefas.Modelos;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ListaTarefaContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/tarefas", async (ListaTarefaContext context) =>
{
    var tarefas = await context.Tarefas.ToListAsync();
    return Results.Ok(tarefas);
});

app.MapPost("/tarefas", async (ListaTarefaContext context, Tarefa tarefa) =>
{
    context.Tarefas.Add(tarefa);
    await context.SaveChangesAsync();
    return Results.Created($"/tarefas/tarefa.id", tarefa);
});

app.MapPut("/tarefas/{id}", async (int id, ListaTarefaContext context, Tarefa tarefa) =>
{
    var tarefaExistente = await context.Tarefas.FindAsync(id); 
    if (tarefaExistente == null)
    {
        return Results.NotFound();
    }

    tarefaExistente.Titulo = tarefa.Titulo;
    tarefaExistente.Concluida = tarefa.Concluida;
    tarefaExistente.PrazoFinal = tarefa.PrazoFinal;

    await context.SaveChangesAsync();
    return Results.Ok(tarefaExistente);
});

app.MapDelete("/tarefas/{id}", async (int id, ListaTarefaContext context) =>
{
    var tarefa = await context.Tarefas.FindAsync(id);
    if (tarefa == null) 
    {
        return Results.NotFound();
    }

    context.Tarefas.Remove(tarefa);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
