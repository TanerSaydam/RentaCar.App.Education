import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ErrorService } from './error.service';
import { api } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  token: string = "";

  #http = inject(HttpClient);
  #error = inject(ErrorService);

  constructor() { 
    if(localStorage.getItem("token")){
      this.token = localStorage.getItem("token") ?? "";
    }
  }

  get<T>(apiUrl:string, callBack: (res:T)=> void, errCallBack?: (err: HttpErrorResponse)=> void){
    this.#http.get<T>(`${api}/${apiUrl}`, {
      headers: {
        "Authorization": "Bearer " + this.token 
      }
    })
    .subscribe({
      next: (res=> {
        callBack(res);      
      }),
      error: ((err:HttpErrorResponse)=> {
        this.#error.errorHandler(err);

        if(errCallBack !== undefined){                    
          errCallBack(err);
        }        
      })
    })
  }

  post<T>(apiUrl:string, body:any, callBack: (res:T)=> void, errCallBack?: (err: HttpErrorResponse)=> void){
    this.#http.post<T>(`${api}/${apiUrl}`,body, {
      headers: {
        "Authorization": "Bearer " + this.token 
      }
    })
    .subscribe({
      next: (res=> {
        callBack(res);      
      }),
      error: ((err:HttpErrorResponse)=> {
        this.#error.errorHandler(err);

        if(errCallBack !== undefined){                    
          errCallBack(err);
        }        
      })
    })
  }
}
