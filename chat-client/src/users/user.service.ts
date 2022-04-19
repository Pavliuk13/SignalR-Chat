import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { User } from "src/models/user";


@Injectable()
export class UserService{
    private url = "https://localhost:5001/api/User/";

    constructor(private http: HttpClient){}

    getUsers(){
        return this.http.get<Array<User>>(this.url);
    }
}