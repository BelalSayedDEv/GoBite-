# GoBite API Documentation

**Base URL:** `http://gobite.runasp.net`

---

## Authentication Endpoints

---

### 1. Register

Creates a new user account.

**POST** `/api/auth/register`

#### Request Body

```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "password": "string",
  "phoneNumber": "string | null"
}
```

| Field | Type | Required | Notes |
|---|---|---|---|
| firstName | string | Yes | |
| lastName | string | Yes | |
| email | string | Yes | Must be unique |
| password | string | Yes | Min 8 chars, at least 1 uppercase, 1 lowercase, 1 digit |
| phoneNumber | string | No | |

#### Response — 201 Created

```json
{
  "message": "",
  "data": {
    "accessToken": "string",
    "refreshToken": "string",
    "expiration": "2026-07-03T17:05:13Z"
  },
  "errors": null,
  "isSuccess": true
}
```

#### Response — 409 Conflict

```json
{
  "message": "Email is already registered",
  "data": null,
  "errors": null,
  "isSuccess": false
}
```

#### Response — 400 Bad Request

```json
{
  "message": "Registration failed",
  "data": null,
  "errors": ["PasswordTooShort", "PasswordRequiresUpper..."],
  "isSuccess": false
}
```

---

### 2. Login

Authenticates an existing user and returns JWT tokens.

**POST** `/api/auth/login`

#### Request Body

```json
{
  "email": "string",
  "password": "string"
}
```

| Field | Type | Required | Notes |
|---|---|---|---|
| email | string | Yes | |
| password | string | Yes | |

#### Response — 200 OK

```json
{
  "message": "",
  "data": {
    "accessToken": "string",
    "refreshToken": "string",
    "expiration": "2026-07-03T17:05:13Z"
  },
  "errors": null,
  "isSuccess": true
}
```

#### Response — 401 Unauthorized

```json
{
  "message": "Invalid email or password",
  "data": null,
  "errors": null,
  "isSuccess": false
}
```

#### Response — 403 Forbidden

```json
{
  "message": "Account is deactivated",
  "data": null,
  "errors": null,
  "isSuccess": false
}
```

---

### 3. Refresh Token

Exchanges an expired access token + valid refresh token for a new pair (token rotation).

**POST** `/api/auth/refresh`

**Requires Authorization header** (the expired access token)

#### Request Body

```json
{
  "accessToken": "string",
  "refreshToken": "string"
}
```

| Field | Type | Required | Notes |
|---|---|---|---|
| accessToken | string | Yes | The expired/current JWT |
| refreshToken | string | Yes | The refresh token from login/previous refresh |

#### Response — 200 OK

```json
{
  "message": "",
  "data": {
    "accessToken": "string",
    "refreshToken": "string",
    "expiration": "2026-07-03T17:05:13Z"
  },
  "errors": null,
  "isSuccess": true
}
```

**Note:** The old refresh token is marked as used (rotation). The new refresh token should be stored by the client.

#### Response — 401 Unauthorized (various scenarios)

```json
{ "message": "Refresh token not found", "data": null, "errors": null, "isSuccess": false }
{ "message": "Refresh token has been used", "data": null, "errors": null, "isSuccess": false }
{ "message": "Refresh token has been revoked", "data": null, "errors": null, "isSuccess": false }
{ "message": "Refresh token has expired", "data": null, "errors": null, "isSuccess": false }
{ "message": "Token does not belong to this user", "data": null, "errors": null, "isSuccess": false }
```

---

### 4. Forgot Password

Sends a 6-digit OTP code to the user's email.

**POST** `/api/auth/forgot-password`

#### Request Body

```json
{
  "email": "string"
}
```

| Field | Type | Required | Notes |
|---|---|---|---|
| email | string | Yes | Must match a registered email |

#### Response — 200 OK

```json
{
  "message": "OTP sent to your email",
  "data": null,
  "errors": null,
  "isSuccess": true
}
```

**Note:** OTP expires in 5 minutes. The email is sent via Gmail SMTP.

#### Response — 404 Not Found

```json
{
  "message": "Email not found",
  "data": null,
  "errors": null,
  "isSuccess": false
}
```

---

### 5. Verify OTP

Validates the OTP code sent to the user's email. Returns a reset token on success.

**POST** `/api/auth/verify-otp`

#### Request Body

```json
{
  "email": "string",
  "otp": "string"
}
```

| Field | Type | Required | Notes |
|---|---|---|---|
| email | string | Yes | |
| otp | string | Yes | 6-digit code received via email |

#### Response — 200 OK

```json
{
  "message": "resetTokenValueHere",
  "data": null,
  "errors": null,
  "isSuccess": true
}
```

**Important:** The `message` field contains the reset token. Pass this token to the reset-password endpoint.

#### Response — 400 Bad Request

```json
{
  "message": "Invalid or expired OTP",
  "data": null,
  "errors": null,
  "isSuccess": false
}
```

#### Response — 404 Not Found

```json
{
  "message": "Email not found",
  "data": null,
  "errors": null,
  "isSuccess": false
}
```

---

### 6. Reset Password

Resets the user's password using the reset token from verify-otp.

**POST** `/api/auth/reset-password`

#### Request Body

```json
{
  "email": "string",
  "resetToken": "string",
  "newPassword": "string"
}
```

| Field | Type | Required | Notes |
|---|---|---|---|
| email | string | Yes | |
| resetToken | string | Yes | The token returned from /verify-otp |
| newPassword | string | Yes | Min 8 chars, 1 uppercase, 1 lowercase, 1 digit |

#### Response — 200 OK

```json
{
  "message": "Password reset successfully",
  "data": null,
  "errors": null,
  "isSuccess": true
}
```

#### Response — 400 Bad Request

```json
{
  "message": "Password reset failed",
  "data": null,
  "errors": ["PasswordTooShort", "PasswordRequiresUpper..."],
  "isSuccess": false
}
```

#### Response — 404 Not Found

```json
{
  "message": "Email not found",
  "data": null,
  "errors": null,
  "isSuccess": false
}
```

---

### 7. Logout

Revokes all refresh tokens for the authenticated user.

**POST** `/api/auth/logout`

**Requires Authorization header** with valid Bearer JWT token.

#### Request Headers

```
Authorization: Bearer <accessToken>
```

#### Response — 200 OK

```json
{
  "message": "Logged out successfully",
  "data": null,
  "errors": null,
  "isSuccess": true
}
```

#### Response — 401 Unauthorized

If no valid access token is provided, ASP.NET Core returns a standard 401 without the wrapper.

---

## Common Response Format

All endpoints use the `ApiResponse<T>` wrapper:

```json
{
  "message": "string",
  "data": "T | null",
  "errors": ["string"] | null,
  "isSuccess": true | false
}
```

| Field | Type | Description |
|---|---|---|
| message | string | Success/error message |
| data | T or null | The response payload (null on errors) |
| errors | string[] or null | Validation/error details (null on success) |
| isSuccess | bool | true if successful, false if error |

## Token Format

### Access Token (JWT)

- **Type:** Bearer JWT
- **Expiration:** 15 minutes
- **Claims:**
  - `nameidentifier` — user ID
  - `name` — full name (firstName + lastName)
  - `jti` — unique token ID
  - `userId` — user ID
  - `role` — user roles (if assigned)

### Refresh Token

- **Type:** Opaque string (hex-encoded random bytes)
- **Expiration:** 7 days
- **Storage:** Database (with rotation and revocation support)

---

## Integration Notes for Flutter Team

1. Store `accessToken` and `refreshToken` securely after login/register
2. When access token expires (15 min), use `/api/auth/refresh` to get a new pair
3. On 401 from any endpoint → try refresh → if refresh fails → redirect to login
4. For forgot password flow:
   - Call `/forgot-password` → user receives email with OTP
   - Call `/verify-otp` with the OTP → get reset token
   - Call `/reset-password` with the reset token + new password
5. HTTPS can be enabled in MonsterASP control panel (Let's Encrypt — free)
