
public class UserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        UserName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
    }

    public string UserName { get; set; }
    public string UserNameSemDominio => UserName.Replace("CORP\\", "");
}
