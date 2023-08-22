import jwt_decode from 'jwt-decode';

export function getDecodedAccessToken(token: string): any {
    try {
        console.log(jwt_decode(token));
        return jwt_decode(token);
    } catch(Error) {
        return null;
    }
}