import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { JobForListDto } from '../models/JobForListDto.model';
import { JobsService } from '../services/jobs.service';

@Component({
  selector: 'app-edit-job',
  templateUrl: './edit-job.component.html',
  styleUrls: ['./edit-job.component.css']
})
export class EditJobComponent implements OnInit {

  private returnUrl: string = 'tasks';

  private params : string = '';
  private currentJob!: JobForListDto;
  jobId: any ;


  editJobForm: FormGroup = new FormGroup({
    name: new FormControl("", [Validators.required]),
    description: new FormControl("", [Validators.required]),
    period: new FormControl("", [Validators.required]),
    periodFormat: new FormControl("", [Validators.required]),
    nextExecutionDate: new FormControl("", [Validators.required]),
    ApiUrl: new FormControl("", [Validators.required]),
    city: new FormControl("",[Validators.required]),
    country: new FormControl("",[Validators.required]),
    min: new FormControl("",[Validators.required]),
    max: new FormControl("",[Validators.required]),
    type: new FormControl("",[Validators.required])
  });

  errorMessage: string = '';
  showError: boolean = true;


  private querySubscription: Subscription;
  constructor(private jobService: JobsService , private router: Router, private route: ActivatedRoute) { 
        this.querySubscription = route.queryParams.subscribe(
            (queryParam: any) => {
                this.jobId = queryParam['id'];
            });


  }

  ngOnInit(): void {
    this.editJobForm
    console.log(this.jobId)
    this.jobService.getJob(this.jobId)
    .subscribe({
      next : (currentJob) => {console.log(currentJob);
        this.editJobForm.get('name')?.setValue(currentJob.name);
        this.editJobForm.get('description')?.setValue(currentJob.description); 
        this.editJobForm.get('period')?.setValue(currentJob.period); 
        this.editJobForm.get('nextExecutionDate')?.setValue(currentJob.nextExecutionDate); 
        this.editJobForm.get('ApiUrl')?.setValue(currentJob.apiUrlForJob);
        this.editJobForm.get('periodFormat')?.setValue(currentJob.periodFormat);
        this.selectChange(new Event(''));  
      }
    });
    console.log(this.currentJob);

  }



  isWeatherApi = () => {
    return this.editJobForm.get('ApiUrl')?.value == 'WeatherApi';
  }

  isCovidApi = () => {
    return this.editJobForm.get('ApiUrl')?.value == 'CovidApi';
  }

  isNumberApi = () => {
    return this.editJobForm.get('ApiUrl')?.value == 'NumbersApi';
  }

  selectChange = (event : Event): void =>{
    if(this.isWeatherApi())
    {
      this.editJobForm.get("city")?.addValidators(Validators.required)
      this.editJobForm.get("country")?.clearValidators()
      this.editJobForm.get("min")?.clearValidators()
      this.editJobForm.get("max")?.clearValidators()
      this.editJobForm.get("type")?.clearValidators()
      this.editJobForm.get("city")?.updateValueAndValidity()
      this.editJobForm.get("country")?.updateValueAndValidity()
      this.editJobForm.get("min")?.updateValueAndValidity()
      this.editJobForm.get("max")?.updateValueAndValidity()
      this.editJobForm.get("type")?.updateValueAndValidity()
    }

    if(this.isCovidApi())
    {
      this.editJobForm.get('country')?.addValidators(Validators.required)
      this.editJobForm.get('city')?.clearValidators()
      this.editJobForm.get('min')?.clearValidators()
      this.editJobForm.get('max')?.clearValidators()
      this.editJobForm.get('type')?.clearValidators()
      this.editJobForm.get("city")?.updateValueAndValidity()
      this.editJobForm.get("country")?.updateValueAndValidity()
      this.editJobForm.get("min")?.updateValueAndValidity()
      this.editJobForm.get("max")?.updateValueAndValidity()
      this.editJobForm.get("type")?.updateValueAndValidity()
    }
    
    if(this.isNumberApi())
    {
      this.editJobForm.get('min')?.addValidators(Validators.required)
      this.editJobForm.get('max')?.addValidators(Validators.required)
      this.editJobForm.get('type')?.addValidators(Validators.required)
      this.editJobForm.get('country')?.clearValidators()
      this.editJobForm.get('city')?.clearValidators()
      this.editJobForm.get("city")?.updateValueAndValidity()
      this.editJobForm.get("country")?.updateValueAndValidity()
      this.editJobForm.get("min")?.updateValueAndValidity()
      this.editJobForm.get("max")?.updateValueAndValidity()
      this.editJobForm.get("type")?.updateValueAndValidity()
    }
  }
  validateControl = (controlName: string) => {
    return this.editJobForm.get(controlName)?.invalid && this.editJobForm.get(controlName)?.touched
  }
  hasError = (controlName: string, errorName: string) => {
    return this.editJobForm.get(controlName)?.hasError(errorName)
  }

  updateJob = (addNewJobFormValue : any) => {
    this.showError = false;
    const jobFromValue = {... addNewJobFormValue };
    if(this.isWeatherApi())
    this.params = jobFromValue.city;     

    if(this.isCovidApi())
    this.params = jobFromValue.country;
  
    if(this.isNumberApi())
    this.params = jobFromValue.min + ','+ jobFromValue.max + ','+ jobFromValue.type;

    const jobForListDto: JobForListDto = {
      id: this.jobId,
      name: jobFromValue.name,
      description: jobFromValue.description,
      period: jobFromValue.period,
      periodFormat: jobFromValue.periodFormat,
      nextExecutionDate: jobFromValue.nextExecutionDate,
      apiUrlForJob: jobFromValue.ApiUrl,
      params: this.params
    }
    this.jobService.UpdateJob(jobForListDto)
    .subscribe({
      next: () => {
       this.router.navigate([this.returnUrl]);
    },
    error: (err: HttpErrorResponse) => {
      this.errorMessage = err.message;
      this.showError = true;
    }})
  }

}
