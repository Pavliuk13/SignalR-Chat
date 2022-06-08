import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from './user.model';
import { LoginUser } from './loginUser.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly rootUrl = "https://localhost:5001";

  constructor(private http: HttpClient) {

  }

  registerUser(user: User){
    const body: User = {
      UserName: user.UserName,
      Password: user.Password,
      Email: user.Email,
      FirstName: user.FirstName,
      LastName: user.LastName
    }
    return this.http.post(this.rootUrl + '/api/account/register', body);
  }

  userAuthentication(login: LoginUser){
    const body: LoginUser = {
      UserName: login.UserName,
      Password: login.Password,
    }
    var reqHeader = new HttpHeaders({'content-type':'application/json'});
    return this.http.post(this.rootUrl + "/token", body, {headers: reqHeader});
  }

  getUserClaims(){
    return this.http.get(this.rootUrl + '/api/Account/GetUserClaims?userName=' + localStorage.getItem('user_name'), {headers: {"Accept": "application/json", 'Authorization': 'Bearer' + localStorage.getItem('userToken')}});
  }
}
