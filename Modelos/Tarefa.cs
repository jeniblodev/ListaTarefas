namespace ListaTarefas.Modelos;

public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public bool Concluida { get; set; }
    public DateTime? PrazoFinal { get; set; }
}
