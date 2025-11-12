namespace Repository;
using Domain;

public class PromptRepsitory : IPromptRepository
{
    private readonly DatabaseContext _context;

    public PromptRepsitory(DatabaseContext context)
    {
        _context = context;
    }
    public void CreatePrompt(Prompt prompt)
    {
        _context.Prompts.Add(prompt);
        _context.SaveChanges();
    }

    public IEnumerable<Prompt> GetAllPrompts()
    {
        return _context.Prompts.ToList();
    }

    public Prompt GetPrompt(int id)
    {
        return _context.Prompts.Find(id)!;
    }

    public void UpdatePrompt(int id, string titulo, string texto)
    {
        if (_context.Prompts is not null)
        {
            var promptExistente = _context.Prompts.FirstOrDefault(e => e.id == id);
            promptExistente!.texto = texto;
            promptExistente!.titulo = titulo;
            promptExistente.dataAlteracao = DateTime.Now;
            _context.Prompts.Update(promptExistente);
            _context.SaveChanges();
        }
    }
}

