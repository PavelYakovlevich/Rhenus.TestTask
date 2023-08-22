export class AuthenticationConstants {
    public static readonly clientId = "frontend";

    // must be saved in a secure storage later
    public static readonly clientSecret = "secret";

    public static readonly scope = "offline_access";

    public static readonly passwordGrantType = "password";
    
    public static readonly refreshTokenGrantType = "refresh_token";
}