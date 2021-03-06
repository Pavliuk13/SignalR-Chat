import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

import { catchError, Observable, tap } from "rxjs";


@Injectable()
export class AuthInterceptor implements HttpInterceptor{
    constructor(private router : Router){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(req.headers.get('No-Auth') == "True"){
            return next.handle(req.clone());
        }

        if (localStorage.getItem('userToken') != null) {
            const clonedreq = req.clone({
                headers: req.headers.set("Authorization", "Bearer " + localStorage.getItem('userToken'))
            });
            return next.handle(clonedreq)
                .pipe(
                    tap(event => {
                        if(event instanceof HttpErrorResponse){
                            if(event.status === 401){
                                this.router.navigateByUrl('/login');
                            }
                        }
                    })
                );
        }
        else {
            this.router.navigateByUrl('/login');
        }

        return next.handle(req.clone());
    }
}