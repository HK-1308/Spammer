import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JobForListDto } from '../models/JobForListDto.model';
import { JobsService } from '../services/jobs.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  private returnUrl: string = 'tasks';
  
  constructor(private jobsService: JobsService,  private router: Router) { }

  usersJobs: JobForListDto[] = [];

  ngOnInit(): void {
    this.jobsService.getAllUsersJobs()
    .subscribe({
      next : (usersJobs) =>{this.usersJobs = usersJobs},
      error :( response) => {
        console.log(response)
      }
    })
  }

  removeJob = (jobId: any) => {
    this.jobsService.RemoveJob(jobId) 
    .subscribe({
      next:  ()=>{this.ngOnInit()} 
    })
  }

}
