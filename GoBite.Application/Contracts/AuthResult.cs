using GoBite.Application.DTOs.Auth;

namespace GoBite.Application.Contracts;

public class AuthResult
{
    public AuthOutcome Outcome { get; set; }
    public AuthResponse? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
}
