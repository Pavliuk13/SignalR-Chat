import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/models/user';
import { UserService } from './user.service';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  providers: [ UserService, AppService ]
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

  addUser(id: number): void{
    let obj = null;
    for(let i = 0; i < this.users.length; i++){
      if(this.users[i].id == id){
        obj = this.users[i];
        break;
      }
    }

    console.log(obj?.userName);
    if(obj != null)
      AppService.currentUser = obj;
  }

}
