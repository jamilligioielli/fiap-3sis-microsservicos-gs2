using Domain;
using DTO;
using Repository;

namespace Service;

public class VersionamentoPromptsService : IVersionamentoPromptsService
{
    private readonly IPromptRepository _promptRepository;
    private readonly IVersaoRepository _versaoRepository;
    public VersionamentoPromptsService(IVersaoRepository versaoRepository, IPromptRepository promptRepository)
    {
        _promptRepository = promptRepository;
        _versaoRepository = versaoRepository;
    }

    public void AtualizarPrompt(int id, PromptDTO prompt)
    {
        var dataAtual = DateTime.Now;
        var promptExistente = _promptRepository.GetPrompt(id);
        var ultimaVersaoPrompt = _versaoRepository.GetUltimaVersaoPrompt(id);
        if (promptExistente != null)
        {
            promptExistente.titulo = prompt.titulo ?? promptExistente.titulo;
            promptExistente.texto = prompt.texto ?? promptExistente.texto;

            ultimaVersaoPrompt.prompt = promptExistente;
            ultimaVersaoPrompt.dataCriacao = dataAtual;
            ultimaVersaoPrompt.numeroVersaoAnterior = ultimaVersaoPrompt.numeroVersaoAtual;
            ultimaVersaoPrompt.numeroVersaoAtual += 1;
            var newId = _versaoRepository.GetAllVersoes().Last().idVersao++;
            ultimaVersaoPrompt.idVersao = newId;

            _promptRepository.UpdatePrompt(promptExistente.id, promptExistente.titulo, promptExistente.texto);
            _versaoRepository.CreateVersaoPrompt(ultimaVersaoPrompt);
        }
        else
        {
            throw new Exception("Prompt não encontrado para atualização.");

        }
    }

    public void CriarPrompt(PromptDTO novoPrompt)
    {
        var data = DateTime.Now;
        var prompt = new Prompt
        {
            autor = novoPrompt.autor ?? "desconhecido",
            titulo = novoPrompt.titulo ?? "prompt novo",
            texto = novoPrompt.texto,
            dataCriacao = data,
            dataAlteracao = null
        };

        var newId = _versaoRepository.GetAllVersoes().Last().idVersao++;
        if(newId == null || newId <= 0)
        {
            newId = 1;
        }

        var versaoPrompt = new Versao
        {
            idVersao = newId,
            prompt = prompt,
            dataCriacao = data,
            numeroVersaoAnterior = null,
            numeroVersaoAtual = 1
        };

        _promptRepository.CreatePrompt(prompt);
        _versaoRepository.CreateVersaoPrompt(versaoPrompt);
    }

    public IEnumerable<PromptVersaoDTO> ListarTodosPromptsDisponiveis()
    {
       var prompts = _promptRepository.GetAllPrompts();
        var promptsDisponiveis = new List<PromptVersaoDTO>();
        foreach (var item in prompts)
        {
            var versao = _versaoRepository.GetUltimaVersaoPrompt(item.id);
            promptsDisponiveis.Add(new PromptVersaoDTO { 
                autor = item.autor, 
                titulo = item.titulo, 
                texto = item.texto, 
                versaoAtual = versao!.numeroVersaoAtual 
            });
        }
        return promptsDisponiveis;
    }
}
