import { Component, OnInit } from '@angular/core';
import { UserForUsageHistory } from '../models/UserForUsageHistory.model';
import { AdminService } from '../services/admin.service';


@Component({
  selector: 'app-usage-history',
  templateUrl: './usage-history.component.html',
  styleUrls: ['./usage-history.component.css']
})
export class UsageHistoryComponent implements OnInit {


  constructor(private adminService: AdminService) { }

  history: UserForUsageHistory[] = [];
  
  ngOnInit(): void {
    this.adminService.getHistory()
    .subscribe({
      next : (history) =>{this.history = history},
      error :( response) => {
        console.log(response)
      }
    })
  }

}
