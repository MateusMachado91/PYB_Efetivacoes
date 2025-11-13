using PYBWeb.Web.Components;
using Microsoft.EntityFrameworkCore;
using PYBWeb.Infrastructure.Data;
using PYBWeb.Infrastructure.Services;
using PYBWeb.Domain.Entities;
using PYBWeb.Infrastructure;
using PYBWeb.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ColaboradoresDbContext>(options =>
    options.UseSqlite(@"Data Source=X:\DATA_PYB\colaboradores.db"));

// =====================================================================
// ⚠️  SISTEMA DE SEGURANÇA TEMPORARIAMENTE DESABILITADO PARA TESTES ⚠️
// =====================================================================
// IMPORTANTE: Este é o sistema de autenticação Windows (Active Directory)
// que verifica se o usuário pertence a um grupo específico do AD.
// Foi comentado APENAS para permitir testes locais.
// EM PRODUÇÃO, SEMPRE MANTENHA A SEGURANÇA HABILITADA!
// Adiciona serviços de autenticação e autorização
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireGroup", policy =>
        policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid", "S-1-5-21-1181696648-2516392193-694368965-181594")); // Substitua pelo SID do seu grupo AD
});

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ColaboradorService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Adicionar infraestrutura
builder.Services.AddInfrastructure(builder.Configuration);

// Configurar logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
