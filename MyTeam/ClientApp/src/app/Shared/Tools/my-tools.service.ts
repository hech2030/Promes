import { Injectable } from '@angular/core';

import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class MyToolsService {
  constructor(private toastr: ToastrService) {}

  ShowSuccessNotification(Header, ContextMessage, DurationMS) {
    this.toastr.success(ContextMessage, Header, {
      timeOut: DurationMS,
      positionClass: 'toast-top-right'
    })
  }
  ShowErrorNotification(Header, ContextMessage, DurationMS) {
    this.toastr.error(ContextMessage, Header, {
      timeOut: DurationMS,
      positionClass: 'toast-top-right'
    })
  }

}
