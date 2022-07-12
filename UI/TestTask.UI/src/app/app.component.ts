import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'TestTask.UI';
  public isUserAuthenticated: boolean = false;
  constructor(private authService: AuthenticationService, private router: Router) {
    this.authService.authChanged
    .subscribe(res => {
      this.isUserAuthenticated = res;})
   }

  ngOnInit(): void {
    if(this.authService.isUserAuthenticated())
    this.authService.sendAuthStateChangeNotification(true);
    //this.authService.authChanged
    //.subscribe(res => {
      //this.isUserAuthenticated = res;
    //})
  }
  
  public logout = () => {
    this.authService.logout();
    this.router.navigate(["login"]);
  }

}
