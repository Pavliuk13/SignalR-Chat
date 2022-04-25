import { Injectable } from "@angular/core";
import { User } from "src/models/user";


@Injectable({
    providedIn: 'root'
})

export class AppService{
    static currentUser: User = new User();

    write(): void{
        console.log(AppService.currentUser.userName + " : AppService");
    }
}