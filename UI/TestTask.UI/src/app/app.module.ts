import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { CommonModule } from '@angular/common';
import { AuthenticationModule } from './authentication/authentication.module';
import { RegisterUserComponent } from './authentication/register-user/register-user.component';
import { LoginComponent } from './authentication/login/login.component';
import { JwtModule } from "@auth0/angular-jwt";
import { TasksComponent } from './tasks/tasks.component';
import { AdminComponent } from './admin/admin.component';
import { AddNewJobComponent } from './add-new-job/add-new-job.component';
import { EditJobComponent } from './edit-job/edit-job.component';
import { AccessDeniedComponent } from './access-denied/access-denied.component';

export function tokenGetter() { 
  return localStorage.getItem('token'); 
}


@NgModule({
  declarations: [

    AppComponent,
    RegisterUserComponent,
    TasksComponent,
    AdminComponent,
    AddNewJobComponent,
    EditJobComponent,
    AccessDeniedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonModule,
    AuthenticationModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7017"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
