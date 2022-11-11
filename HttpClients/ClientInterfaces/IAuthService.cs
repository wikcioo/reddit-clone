using System.Security.Claims;

namespace HttpClients.ClientInterfaces;

public interface IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
}