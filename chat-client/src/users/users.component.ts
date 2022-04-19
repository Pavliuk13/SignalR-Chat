import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/models/user';

@Component({
  selector: 'users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})

export class UsersComponent {
  constructor(serviсe: HttpClient){
    serviсe.get<User[]>("https://localhost:5001/api/User/Get")
            .subscribe(x => this.users = x);
  }

  users: User[] = [];
}
