import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../shared/user.model';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userClaims: any;
  users: any;

  constructor(private router: Router, private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUserClaims().subscribe((data: any) => {
      this.userClaims = data;
    });

    this.userService.getUsers().subscribe((data: any) => {
      this.users = data;
    });
  }

  Logout(){
    localStorage.removeItem('userToken');
    localStorage.removeItem('user_name');
    this.router.navigate(['/login']);
  }
}
