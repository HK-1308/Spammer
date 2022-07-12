

import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserForRegistrationDto } from 'src/app/models/UserForRegistrationDto.model';
import { RegistrationResponseDto } from '../models/RegistrationResponse.mode';
import { UserForAuthenticationDto } from 'src/app/models/UserForAuthenticationDto.model';
import { AuthResponseDto } from '../models/AuthResponseDto.model';
import { Subject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  baseApiUrl : string = environment.baseApiUrl;

  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();

  constructor(private http: HttpClient,private jwtHelper: JwtHelperService) { }
  
  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");
 
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }

  public registerUser = (route: string, body: UserForRegistrationDto) => {
    return this.http.post<RegistrationResponseDto> (this.createCompleteRoute(route, this.baseApiUrl), body);
  }

  public loginUser = (route: string, body: UserForAuthenticationDto) => {
    return this.http.post<AuthResponseDto>(this.createCompleteRoute(route, this.baseApiUrl), body);
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
