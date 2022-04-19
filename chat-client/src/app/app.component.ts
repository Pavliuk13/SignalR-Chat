import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { User } from 'src/models/user';
import { UserService } from 'src/users/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [UserService]
})

export class AppComponent implements OnInit{
  @ViewChild('readOnlyTemplate', {static: false}) readonlyTemplate: TemplateRef<any>|null;

  users: Array<User> = [];

  constructor(private serv: UserService){
    this.users = new Array<User>();
    this.readonlyTemplate = null;
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  private loadUsers(){
    this.serv.getUsers().subscribe(x => this.users = x);
  }

  loadTemplate(){
    return this.readonlyTemplate;
  }

}
