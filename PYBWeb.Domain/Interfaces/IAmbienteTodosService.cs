using PYBWeb.Domain.Entities;

namespace PYBWeb.Domain.Interfaces;

/// <summary>
/// Interface para serviços de ambientes remotos FCT
/// </summary>
public interface IAmbienteTodosService
{
    /// <summary>
    /// Obtém todos os ambientes da tabela ambientetodos
    /// </summary>
    /// <returns>Lista de ambientes</returns>
    Task<List<AmbienteTodos>> ObterTodosAmbientesAsync();
    
    /// <summary>
    /// Obtém apenas os ambientes ativos (EmTodos = 1)
    /// </summary>
    /// <returns>Lista de ambientes ativos</returns>
    Task<List<AmbienteTodos>> ObterAmbientesAtivosAsync();
    
    /// <summary>
    /// Obtém um ambiente por ID
    /// </summary>
    /// <param name="id">ID do ambiente</param>
    /// <returns>Ambiente encontrado ou null</returns>
    Task<AmbienteTodos?> ObterAmbientePorIdAsync(int id);
    
    /// <summary>
    /// Obtém um ambiente por nome
    /// </summary>
    /// <param name="nome">Nome do ambiente</param>
    /// <returns>Ambiente encontrado ou null</returns>
    Task<AmbienteTodos?> ObterAmbientePorNomeAsync(string nome);
}