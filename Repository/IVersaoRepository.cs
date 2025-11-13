using Domain;

namespace Repository;

public interface IVersaoRepository
{
    IEnumerable<Versao> GetAllVersoes();
    IEnumerable<Versao> GetAllVersoesPrompt(int idPrompt);
    Versao GetUltimaVersaoPrompt(int idPrompt);
    void CreateVersaoPrompt(Versao versao);
}

