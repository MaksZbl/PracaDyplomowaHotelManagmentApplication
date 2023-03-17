import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Observable } from "rxjs";
import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from "@angular/core";

@Injectable()

export class AuthPanelGuard implements CanActivate {

    private jwtHelper: JwtHelperService = new JwtHelperService();

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        const token = sessionStorage.getItem("jwt");
        if (token && sessionStorage.getItem("role") === "Admin" || "Emloyee" && !this.jwtHelper.isTokenExpired(token || "")) {
            console.log("Dobrze");
            return true;
        }
        sessionStorage.removeItem("jwt");
        sessionStorage.removeItem("jwt");
        sessionStorage.removeItem("role");
        sessionStorage.removeItem("username");
        sessionStorage.removeItem("firstname");
        sessionStorage.removeItem("lastname");
        return false;
    }
}