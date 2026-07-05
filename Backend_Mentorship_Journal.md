# Backend Mentorship Journal — GoBite (Restaurant Management System)

---

## Student Profile

### Current Level
**Strong Junior — approaching Early Mid-Level** — can build complete features independently. Understands clean layered architecture, JWT flow, EF Core basics, result patterns, and HTTP status code decision-making.

### Strengths
- Clean layered architecture (Controller → Service → Repository → DbContext)
- Proper Dependency Injection usage
- JWT authentication with role-based authorization
- DTO separation from entities
- ApiResponse&lt;T&gt; consistent response pattern
- Async/await understanding
- Independent feature implementation
- EF Core migrations and DbContext configuration
- Deployment to production (FTP, IIS configuration)

### Weak Areas
- **Fluent API / Model Builder** — hasn't learned entity configuration via Fluent API yet
- **Relationships** — foreign keys, navigation properties, cascade delete
- **Unit Testing** — not started
- **Security hardening** — XSS/CSRF, rate limiting, token theft prevention
- **Caching** — not started
- **Background Services** — not started
- **FluentValidation** — installed but not used yet
- **Logging consistency** — no ILogger used yet

---

## Project: GoBite — Restaurant Management API

### Architecture
**Multi-project Clean Architecture (single solution):**

```
GoBite.Domain/          — Entities (ApplicationUser, RefreshToken, OtpCode)
GoBite.Application/    — DTOs, Contracts (AuthResult), Interfaces, Services
GoBite.Infrastructure/ — DbContext, Repositories, Migrations, EmailService
GoBite.Presentation/   — Controllers, ApiResponse model
GoBite.API/            — Program.cs, appsettings, DI wiring
```

### Decisions Made
| Decision | Reason |
|---|---|
| Multi-project structure | Portfolio project — demonstrates Clean Architecture separation |
| Inline JWT in AuthService | Matches student's existing style from e-commerce project |
| AuthResult + AuthOutcome enum | Domain-specific result object (same pattern as AccountResult) |
| ApiResponse&lt;T&gt; wrapper | Consistent HTTP response format |
| Single IAuthRepository | Simpler than splitting into multiple repos |
| DesignTimeDbContextFactory | Enables EF Core migrations from command line across projects |
| Auto-migration on startup | Production database updates automatically on deploy |
| FTP deployment to MonsterASP | Live API at http://gobite.runasp.net |

### Database Tables
- AspNetUsers, AspNetRoles, AspNetRoleClaims, AspNetUserClaims, AspNetUserLogins, AspNetUserRoles, AspNetUserTokens (Identity)
- RefreshTokens (custom)
- OtpCodes (custom)

### API Endpoints (Live)
| Method | Route | Purpose |
|---|---|---|
| POST | /api/auth/register | Create account → returns JWT |
| POST | /api/auth/login | Authenticate → returns JWT |
| POST | /api/auth/refresh | Rotate tokens |
| POST | /api/auth/forgot-password | Send OTP to email |
| POST | /api/auth/verify-otp | Verify OTP → get reset token |
| POST | /api/auth/reset-password | Set new password |
| POST | /api/auth/logout | Revoke all refresh tokens |

---

## Learning Roadmap

### Current Topic: EF Core Fluent API + Model Builder
1. Two ways to configure entities (Data Annotations vs Fluent API)
2. Fluent API configuration methods (ToTable, Property, HasMaxLength, IsRequired)
3. Entity relationships with Fluent API (One-to-Many, Many-to-Many, One-to-One)
4. Cascade delete behavior
5. Indexes, composite keys, alternate keys

### Upcoming Topics (in order)
1. FluentValidation — input validation
2. CORS deep dive — security, credentials, origins
3. JWT hardening — token theft prevention, HttpOnly cookies
4. Unit Testing (xUnit + Moq)
5. Caching — response caching, distributed cache
6. Background Services — hosted services, scheduled tasks
7. Refactoring — dead code removal, consistent logging, performance

---

## Session History

### Session 1: Project Setup & Auth System
- Created multi-project Clean Architecture structure
- Implemented auth endpoints (register, login, refresh, forgot-password, verify-otp, reset-password, logout)
- Set up Identity with custom ApplicationUser
- JWT token generation with refresh token rotation
- OTP-based password reset via Gmail SMTP
- Deployed to MonsterASP via FTP
- Pushed to GitHub

---

## Deployment Info
- **Base URL:** http://gobite.runasp.net
- **Hosting:** MonsterASP (Free Plan)
- **Database:** MonsterASP MSSQL
- **GitHub:** https://github.com/BelalSayedDEv/GoBite-
- **FTP:** site77619.siteasp.net (site77619)
