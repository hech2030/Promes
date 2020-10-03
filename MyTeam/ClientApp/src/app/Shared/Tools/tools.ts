import * as $ from 'jquery';
import * as toastr from 'toastr';
export class tools {
  showSuccessNotification(headerMessage, Context, Duration) {
    $.toast(
      {
        heading: headerMessage,
        position: "top-right",
        loader: false,
        icon: "success",
        hideAfter: Duration,
        stack: 6
      }
    )
    return true;
  }
  showErrorNotification(headerMessage, Context, Duration) {
    $.toast(
      {
        heading: headerMessage,
        position: "top-right",
        loader: false,
        icon: "error",
        hideAfter: Duration,
        stack: 6
      }
    )
    return true;
  }
}
