import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from 'src/app/shared/loginUser.model';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  loginUser: LoginUser = new LoginUser();
  isLoginError : boolean = false;

  constructor(private userService : UserService, private router : Router) { }

  ngOnInit(): void {
  }

  OnSubmit(form: NgForm){
    this.userService.userAuthentication(form.value).subscribe((data: any) => {
      localStorage.setItem('userToken', data.access_token);
      localStorage.setItem('user_name', data.username);
      this.router.navigate(['/home']);
    },
    (err: HttpErrorResponse) => {
      this.isLoginError = true;
    });
  }

}
