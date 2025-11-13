using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PYBWeb.Domain.Interfaces;
using PYBWeb.Infrastructure.Data;
using PYBWeb.Infrastructure.Services;

namespace PYBWeb.Infrastructure;

/// <summary>
/// Configuração da injeção de dependência da infraestrutura
/// ⚡ PROJETO CONFIGURADO PARA USAR SQLITE NA PASTA DATA ⚡
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // =====================================================================
        // 🗄️ CONFIGURAÇÃO SQLITE - TODOS OS DADOS NA PASTA DATA
        // =====================================================================
        
        // Obter caminho da pasta DATA
        var pastaData = configuration.GetValue<string>("PastaData") ?? @"X:\DATA_PYB";
        var pastaDataCompleta = Path.GetFullPath(pastaData);
        
        // Garantir que a pasta DATA existe
        if (!Directory.Exists(pastaDataCompleta))
        {
            Directory.CreateDirectory(pastaDataCompleta);
        }

        // Connection strings para cada banco SQLite
        var dados2025ConnectionString = $"Data Source={Path.Combine(pastaDataCompleta, "dados2025.db")}";
        var ambienteConnectionString = $"Data Source={Path.Combine(pastaDataCompleta, "ambiente.db")}";
        var colaboradoresConnectionString = $"Data Source={Path.Combine(pastaDataCompleta, "colaboradores.db")}";

        // Log das connection strings para debug
        Console.WriteLine($"📁 Pasta DATA: {pastaDataCompleta}");
        Console.WriteLine($"📊 Dados2025 DB: {dados2025ConnectionString}");
        Console.WriteLine($"🌍 Ambiente DB: {ambienteConnectionString}");
        Console.WriteLine($"👥 Colaboradores DB: {colaboradoresConnectionString}");

        // Registro dos serviços SQLite CICS 2025 (principal)
        services.AddScoped<ISolicitacoesCics2025Service, SolicitacoesCics2025Service>();
        services.AddScoped<IAmbienteCicsService, AmbienteCicsService>();
        services.AddScoped<IAmbienteTodosService, AmbienteTodosService>();

        // Configurar connection strings no configuration para uso pelos serviços
        configuration["ConnectionStrings:Dados2025"] = dados2025ConnectionString;
        configuration["ConnectionStrings:Ambiente"] = ambienteConnectionString;
        configuration["ConnectionStrings:Colaboradores"] = colaboradoresConnectionString;

        return services;
    }
}