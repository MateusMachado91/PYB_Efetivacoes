using PYBWeb.Infrastructure.Data;

public class ColaboradorService
{
    private readonly ColaboradoresDbContext _context;

    public ColaboradorService(ColaboradoresDbContext context)
    {
        _context = context;
    }

    public string ObterNomePorMatricula(string matricula)
    {
        var colaborador = _context.Colaboradores
            .FirstOrDefault(c => c.Matricula == matricula);

        return colaborador?.Nome ?? matricula; // fallback para matrícula
    }
}
