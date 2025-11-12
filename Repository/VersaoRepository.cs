using Domain;

namespace Repository;

public class VersaoRepository : IVersaoRepository
{

    private readonly DatabaseContext _context;

    private readonly IPromptRepository _promptRepository;

    public VersaoRepository(DatabaseContext context, IPromptRepository promptRepository)
    {
        _context = context;
        _promptRepository = promptRepository;
    }

    IEnumerable<Versao> IVersaoRepository.GetAllVersoesPrompt(int idPrompt)
    {
        var versoes = _context.Versoes
            .Where(v => v.prompt.id == idPrompt)
            .ToList();
        return versoes;
    }

    Versao IVersaoRepository.GetUltimaVersaoPrompt(int idPrompt)
    {
        var versao = _context.Versoes
            .Where(v => v.prompt.id == idPrompt)
            .OrderByDescending(v => v.dataCriacao)
            .FirstOrDefault();
        return versao;
    }

    void IVersaoRepository.CreateVersaoPrompt(Versao versao)
    {
        _context.Versoes.Add(versao);
        _context.SaveChanges();
    }
}

