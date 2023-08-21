export interface LoginModel {
    client_id: string,
    client_secret: string,

    scope: string,
    grant_type: string,
    
    username: string,
    password: string,
}