import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserForAdminTableDto } from '../models/UserForAdminTableDto.model';
import { UserForUsageHistory } from '../models/UserForUsageHistory.model';


@Injectable({
  providedIn: 'root'
})
export class AdminService {

  baseApiUrl : string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getUsersForAdminTable() : Observable<UserForAdminTableDto[]>{
    return this.http.get<UserForAdminTableDto[]>(this.baseApiUrl + '/Admin/GetUsersForAdmin');
  } 

  getUserHistory(email : string) : Observable<UserForUsageHistory[]>
  {
    return this.http.get<UserForUsageHistory[]>(this.baseApiUrl + '/Admin/GetUserHistory/' + email);
  }

  getHistory() : Observable<UserForUsageHistory[]>
  {
    return this.http.get<UserForUsageHistory[]>(this.baseApiUrl + '/Admin/GetHistory');
  }


}
