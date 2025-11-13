using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using PYBWeb.Domain.Entities;
using PYBWeb.Domain.Interfaces;

namespace PYBWeb.Infrastructure.Services;

/// <summary>
/// Serviço para gerenciar ambientes remotos FCT
/// 🗄️ Acessa diretamente a tabela ambientetodos no banco ambiente.db
/// </summary>
public class AmbienteTodosService : IAmbienteTodosService
{
    private readonly string _connectionString;

    public AmbienteTodosService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Ambiente") 
            ?? throw new InvalidOperationException("Connection string 'Ambiente' não encontrada");
    }

    public async Task<List<AmbienteTodos>> ObterTodosAmbientesAsync()
    {
        var ambientes = new List<AmbienteTodos>();

        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT id, nome, em_todos 
            FROM ambientetodos 
            ORDER BY nome";

        using var command = new SqliteCommand(sql, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            ambientes.Add(new AmbienteTodos
            {
                Id = reader.GetInt32(0), // id
                Nome = reader.GetString(1), // nome
                Em_Todos = reader.GetInt32(2) // em_todos
            });
        }

        return ambientes;
    }

    public async Task<List<AmbienteTodos>> ObterAmbientesAtivosAsync()
    {
        var ambientes = new List<AmbienteTodos>();

        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT id, nome, em_todos 
            FROM ambientetodos 
            WHERE em_todos = 1
            ORDER BY nome";

        using var command = new SqliteCommand(sql, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            ambientes.Add(new AmbienteTodos
            {
                Id = reader.GetInt32(0), // id
                Nome = reader.GetString(1), // nome
                Em_Todos = reader.GetInt32(2) // em_todos
            });
        }

        return ambientes;
    }

    public async Task<AmbienteTodos?> ObterAmbientePorIdAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT id, nome, em_todos 
            FROM ambientetodos 
            WHERE id = @id";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new AmbienteTodos
            {
                Id = reader.GetInt32(0), // id
                Nome = reader.GetString(1), // nome
                Em_Todos = reader.GetInt32(2) // em_todos
            };
        }

        return null;
    }

    public async Task<AmbienteTodos?> ObterAmbientePorNomeAsync(string nome)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT id, nome, em_todos 
            FROM ambientetodos 
            WHERE nome = @nome COLLATE NOCASE";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@nome", nome);

        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new AmbienteTodos
            {
                Id = reader.GetInt32(0), // id
                Nome = reader.GetString(1), // nome
                Em_Todos = reader.GetInt32(2) // em_todos
            };
        }

        return null;
    }
}