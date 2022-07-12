import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { AddNewJobComponent } from './add-new-job/add-new-job.component';
import { AdminComponent } from './admin/admin.component';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterUserComponent } from './authentication/register-user/register-user.component';
import { EditJobComponent } from './edit-job/edit-job.component';
import { TasksComponent } from './tasks/tasks.component';
import { UsageHistoryComponent } from './usage-history/usage-history.component';
import { UserUsageHistoryComponent } from './user-usage-history/user-usage-history.component';



const routes: Routes = [
  {
    path: "registration",
    component: RegisterUserComponent
  },
  {
    path: "login",
    component: LoginComponent
  },
  {
    path: "tasks",
    component: TasksComponent
  },
  {
    path: "admin",
    component: AdminComponent
  },
  {
    path: "admin/userUsageHistory",
    component: UserUsageHistoryComponent
  },
  {
    path: "admin/usageHistory",
    component: UsageHistoryComponent
  },
  {
    path: "tasks/addNewJob",
    component: AddNewJobComponent
  },
  {
    path: "tasks/editJob",
    component: EditJobComponent
  },
  {
    path: "access-denied",
    component: AccessDeniedComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
