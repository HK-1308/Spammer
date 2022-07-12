import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { JobDto } from '../models/JobDto.model';
import { JobsService } from '../services/jobs.service';

@Component({
  selector: 'app-add-new-job',
  templateUrl: './add-new-job.component.html',
  styleUrls: ['./add-new-job.component.css']
})
export class AddNewJobComponent implements OnInit {
  private params: string = '';
  private returnUrl: string = 'tasks';

  addNewJobForm: FormGroup = new FormGroup({
    name: new FormControl("", [Validators.required]),
    description: new FormControl("", [Validators.required]),
    period: new FormControl("", [Validators.required,Validators.min(1)]),
    periodFormat: new FormControl("", [Validators.required]),
    startDate: new FormControl("", [Validators.required]),
    ApiUrl: new FormControl("", [Validators.required]),
    city: new FormControl("",[Validators.required]),
    country: new FormControl("",[Validators.required]),
    min: new FormControl("",[Validators.required]),
    max: new FormControl("",[Validators.required]),
    type: new FormControl("",[Validators.required])
  });

  errorMessage: string = '';
  showError: boolean = true;
  constructor(private jobService: JobsService , private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.addNewJobForm
  }

  isWeatherApi = () => {
    return this.addNewJobForm.get('ApiUrl')?.value == 'WeatherApi';
  }

  isCovidApi = () => {
    return this.addNewJobForm.get('ApiUrl')?.value == 'CovidApi';
  }

  isNumberApi = () => {
    return this.addNewJobForm.get('ApiUrl')?.value == 'NumbersApi';
  }

  selectChange = (event : Event): void =>{
    if(this.isWeatherApi())
    {
      this.addNewJobForm.get("city")?.addValidators(Validators.required)
      this.addNewJobForm.get("country")?.clearValidators()
      this.addNewJobForm.get("min")?.clearValidators()
      this.addNewJobForm.get("max")?.clearValidators()
      this.addNewJobForm.get("type")?.clearValidators()
      this.addNewJobForm.get("city")?.updateValueAndValidity()
      this.addNewJobForm.get("country")?.updateValueAndValidity()
      this.addNewJobForm.get("min")?.updateValueAndValidity()
      this.addNewJobForm.get("max")?.updateValueAndValidity()
      this.addNewJobForm.get("type")?.updateValueAndValidity()
    }

    if(this.isCovidApi())
    {
      this.addNewJobForm.get('country')?.addValidators(Validators.required)
      this.addNewJobForm.get('city')?.clearValidators()
      this.addNewJobForm.get('min')?.clearValidators()
      this.addNewJobForm.get('max')?.clearValidators()
      this.addNewJobForm.get('type')?.clearValidators()
      this.addNewJobForm.get("city")?.updateValueAndValidity()
      this.addNewJobForm.get("country")?.updateValueAndValidity()
      this.addNewJobForm.get("min")?.updateValueAndValidity()
      this.addNewJobForm.get("max")?.updateValueAndValidity()
      this.addNewJobForm.get("type")?.updateValueAndValidity()
    }
    
    if(this.isNumberApi())
    {
      this.addNewJobForm.get('min')?.addValidators(Validators.required)
      this.addNewJobForm.get('max')?.addValidators(Validators.required)
      this.addNewJobForm.get('type')?.addValidators(Validators.required)
      this.addNewJobForm.get('country')?.clearValidators()
      this.addNewJobForm.get('city')?.clearValidators()
      this.addNewJobForm.get("city")?.updateValueAndValidity()
      this.addNewJobForm.get("country")?.updateValueAndValidity()
      this.addNewJobForm.get("min")?.updateValueAndValidity()
      this.addNewJobForm.get("max")?.updateValueAndValidity()
      this.addNewJobForm.get("type")?.updateValueAndValidity()
    }
  }

  validateControl = (controlName: string) => {
    return this.addNewJobForm.get(controlName)?.invalid && this.addNewJobForm.get(controlName)?.touched
  }
  hasError = (controlName: string, errorName: string) => {
    return this.addNewJobForm.get(controlName)?.hasError(errorName)
  }

  addNewJob = (addNewJobFormValue : any) => {
    this.showError = false;
    const jobFromValue = {... addNewJobFormValue };
    
    if(this.isWeatherApi())
      this.params = jobFromValue.city;     

    if(this.isCovidApi())
      this.params = jobFromValue.country;
    
    if(this.isNumberApi())
      this.params = jobFromValue.min + ','+ jobFromValue.max + ','+ jobFromValue.type;

    const jobDto: JobDto = {
      name: jobFromValue.name,
      description: jobFromValue.description,
      period: jobFromValue.period,
      periodFormat: jobFromValue.periodFormat,
      startDate: jobFromValue.startDate,
      apiUrlForJob: jobFromValue.ApiUrl,
      params: this.params
    }
    console.log(jobDto);
    this.jobService.AddJob(jobDto)
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
