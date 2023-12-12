using ListaTarefas.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefas.Data;

public class ListaTarefaContext: DbContext
{
    public DbSet<Tarefa> Tarefas { get; set; }

    public ListaTarefaContext(DbContextOptions<ListaTarefaContext> options) : base(options)
    {

    }
}
