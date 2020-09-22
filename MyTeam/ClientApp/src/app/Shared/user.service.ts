import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient) { }

  login(formData) {
    const host = this.BaseURI + '/fw/Users/login';
    return this.http.post(host, formData);
  }
}
