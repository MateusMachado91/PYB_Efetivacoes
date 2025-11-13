using PYBWeb.Domain.Entities;

namespace PYBWeb.Domain.Interfaces;

/// <summary>
/// Interface para serviço de ambientes CICS
/// </summary>
public interface IAmbienteCicsService
{
    /// <summary>
    /// Obtém todos os ambientes ativos
    /// </summary>
    Task<IEnumerable<AmbienteCics>> ObterAmbientesAtivosAsync();

    /// <summary>
    /// Obtém ambiente por ID
    /// </summary>
    Task<AmbienteCics?> ObterAmbientePorIdAsync(int id);

    /// <summary>
    /// Obtém ambiente por nome
    /// </summary>
    Task<AmbienteCics?> ObterAmbientePorNomeAsync(string nome);

    /// <summary>
    /// Obtém os ambientes específicos para sistemas PXU/PXS (opção TODOS)
    /// </summary>
    Task<List<AmbienteCics>> ObterAmbientesPxuPxsAsync();
}
