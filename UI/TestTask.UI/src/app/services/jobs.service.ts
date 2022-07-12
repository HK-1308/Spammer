import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { JobDto } from '../models/JobDto.model';
import { JobForListDto } from '../models/JobForListDto.model';

@Injectable({
  providedIn: 'root'
})
export class JobsService {

  baseApiUrl : string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getAllUsersJobs() : Observable<JobForListDto[]>{
    return this.http.get<JobForListDto[]>(this.baseApiUrl + '/Jobs/GetUsersJobs');
  }

  getJob(id: string) : Observable<JobForListDto>{
    return this.http.get<JobForListDto>(this.baseApiUrl + '/Jobs/GetJob/'+ id);
  }

  public AddJob = (body: JobDto) => {
    return this.http.post(this.baseApiUrl + '/Jobs/AddJob', body);
  }

  public UpdateJob = (body: JobForListDto) => {
    return this.http.put(this.baseApiUrl + '/Jobs/UpdateJob', body);
  }

  public RemoveJob = (body: string) => {
    return this.http.request('delete', this.baseApiUrl + '/Jobs/RemoveJob/' + body)
  }
}
