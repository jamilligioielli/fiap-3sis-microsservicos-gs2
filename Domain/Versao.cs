using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Versao
{
    public int idVersao;
    public Prompt prompt;
    public DateTime dataCriacao;
    public int numeroVersaoAtual;
    public int? numeroVersaoAnterior;
}
