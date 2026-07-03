namespace GoBite.Application.Contracts;

public enum AuthOutcome
{
    Authorized = 1,
    Unauthorized = 2,
    EmailAlreadyExists = 3,
    EmailNotFound = 4,
    InvalidOtp = 5,
    OtpExpired = 6,
    InvalidResetToken = 7,
    TokenNotFound = 8,
    UserNotOwnToken = 9,
    TokenExpired = 10,
    TokenRevoked = 11,
    TokenUsed = 12,
    Forbidden = 13,
}
