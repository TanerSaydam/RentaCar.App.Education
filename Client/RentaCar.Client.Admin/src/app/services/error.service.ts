import { HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {FlexiToastService} from 'flexi-toast'

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  #toast = inject(FlexiToastService);

  errorHandler(err: HttpErrorResponse){
    console.log(err);
    let message = "Error!";
    if(err.status === 0){
      message = "API is not available";
    }else if(err.status === 401){
      message = "You are not authorized"
    }
    else if(err.status === 404){
      message = "API not found";
    }else if(err.status === 500){
      message = "";
      for(const e of err.error.errorMessages){
        message += e + "\n";
      }
    }

    this.#toast.showToast("Hata!",message,"error");
  }
}
