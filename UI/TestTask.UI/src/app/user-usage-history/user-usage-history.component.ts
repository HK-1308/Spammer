import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { UserForUsageHistory } from '../models/UserForUsageHistory.model';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-user-usage-history',
  templateUrl: './user-usage-history.component.html',
  styleUrls: ['./user-usage-history.component.css']
})
export class UserUsageHistoryComponent implements OnInit {
  
  email : any ;

  private querySubscription: Subscription;

  constructor(private adminService: AdminService, private route: ActivatedRoute ) { 
        this.querySubscription = route.queryParams.subscribe(
            (queryParam: any) => {
                this.email = queryParam['email'];
            });


  }

  userHistory: UserForUsageHistory[] = [];
  
  ngOnInit(): void {
    this.adminService.getUserHistory(this.email)
    .subscribe({
      next : (userHistory) =>{this.userHistory = userHistory},
      error :( response) => {
        console.log(response)
      }
    })
  }

}
