using Domain;

namespace Repository;

public interface IPromptRepository
{
    IEnumerable<Prompt> GetAllPrompts();
    Prompt GetPrompt(int id);

    void CreatePrompt(Prompt prompt);
    void UpdatePrompt(int id, string titulo, string texto);
}

