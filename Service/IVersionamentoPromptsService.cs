using DTO;

namespace Service;

public interface IVersionamentoPromptsService
{
    void CriarPrompt(PromptDTO novoPrompt);

    void AtualizarPrompt(int id, PromptDTO prompt);

    IEnumerable<PromptVersaoDTO> ListarTodosPromptsDisponiveis();
}
