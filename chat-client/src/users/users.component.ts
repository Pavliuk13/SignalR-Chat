import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/models/user';
import { UserService } from './user.service';

@Component({
  selector: 'users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  providers: [ UserService ]
})

export class UsersComponent {
  users: Array<User> = [];

  constructor(private serv: UserService){
    this.users = new Array<User>();
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  private loadUsers(){
    this.serv.getUsers().subscribe(x => this.users = x);
  }
}
