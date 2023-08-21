export class AuthenticationConstants {
    public static readonly clientId = "frontend";

    // must be saved in a secure storage later
    public static readonly clientSecret = "secret";

    public static readonly scope = "managementAPI";

    public static readonly grantType = "password";
}

export const authenticationHost = "https://localhost:7088";