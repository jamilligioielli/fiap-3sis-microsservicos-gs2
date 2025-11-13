using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Prompt
{
    public int id;
    public string texto;
    public string titulo;
    public string autor;
    public DateTime dataCriacao;
    public DateTime? dataAlteracao;
}
