using DTO;
using Microsoft.AspNetCore.Mvc; 
using Service;
using System.Net;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromptsController : ControllerBase
{
    private readonly IVersionamentoPromptsService _versionamentoPromptsService;
    private readonly ILogger<PromptsController> _logger;

    public PromptsController(IVersionamentoPromptsService versionamentoPromptsService, ILogger<PromptsController> logger)
    {
        _versionamentoPromptsService = versionamentoPromptsService;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Cadastrar([FromBody] PromptDTO prompt)
    {
        try
        {
            _versionamentoPromptsService.CriarPrompt(prompt);
            return Ok("Prompt cadastrado com sucesso.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao cadastrar prompt");
            return StatusCode(500, "Erro interno do servidor ao cadastrar prompt");
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {

            var lista = _versionamentoPromptsService.ListarTodosPromptsDisponiveis();

            if (lista == null || !lista.Any())
            {
                _logger.LogInformation("Nenhum prompt encontrado no banco de dados");
                return Ok(new List<PromptVersaoDTO>());
            }

            _logger.LogInformation("Retornando {Count} prompts", lista.Count());
            return Ok(lista);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro interno ao buscar prompts");
            return StatusCode((int)HttpStatusCode.InternalServerError,
                new { message = "Erro interno do servidor ao buscar prompts", timestamp = DateTime.UtcNow });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PromptDTO prompt)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Tentativa de atualizar prompt com ID inválido: {Id}", id);
                return BadRequest(new { message = "ID do prompt deve ser maior que zero", timestamp = DateTime.UtcNow });
            }

            if (prompt == null)
            {
                _logger.LogWarning("Tentativa de atualizar prompt com dados nulos para ID: {Id}", id);
                return BadRequest(new { message = "Dados do prompt são obrigatórios", timestamp = DateTime.UtcNow });
            }

            // Validação básica dos campos obrigatórios
            if (string.IsNullOrWhiteSpace(prompt.texto))
            {
                _logger.LogWarning("Tentativa de atualizar prompt com campos obrigatórios vazios para ID: {Id}", id);
                return BadRequest(new
                {
                    message = "Texto é um campo obrigatório",
                    timestamp = DateTime.UtcNow
                });
            }

            _logger.LogInformation("Atualizando prompt ID: {Id}", id);

            _versionamentoPromptsService.AtualizarPrompt(id, prompt);
            _logger.LogInformation("Prompt atualizado com sucesso - ID: {Id}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro interno ao atualizar prompt ID: {Id}", id);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                new { message = "Erro interno do servidor ao atualizar prompt", timestamp = DateTime.UtcNow });
        }
    }
}