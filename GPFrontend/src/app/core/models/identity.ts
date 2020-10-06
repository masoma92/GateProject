import { JwtHelper } from '../jwt/jwt-helper';
import { Role } from './role';

export class Identity{
    _userName: string;
    _role: Role;
    _token: string;
    _authenticationTokenString: string;

    constructor(userName: string, token: string, role: string){
        if(token){
            this._userName = userName;
            this._role = Role[role];
            this._authenticationTokenString = token;
            this._token = JwtHelper.decodeToken(token);
        }
    }
}