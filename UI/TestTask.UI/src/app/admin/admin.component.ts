import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserForAdminTableDto } from '../models/UserForAdminTableDto.model';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(private adminService: AdminService, private router: Router) { }

  users: UserForAdminTableDto[] = [];

  ngOnInit(): void {
    this.adminService.getUsersForAdminTable()
    .subscribe({
      next : (users) =>{this.users = users},
      error :( response) => {
        console.log(response)
        this.router.navigate(["access-denied"]);
      }
    })
  }

}
