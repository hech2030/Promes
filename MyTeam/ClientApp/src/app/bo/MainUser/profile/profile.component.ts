import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { User } from '../../../Models/bo/user.model';
import { UserService } from '../../../Shared/user.service';
import { ActivatedRoute } from '@angular/router';
import { MyToolsService } from '../../../Shared/Tools/my-tools.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  host: { 'class': 'content-wrapper' }
})
export class ProfileComponent implements OnInit {

  PasswordOption: string;
  FileName: string = 'télécharger une image';
  profile: User = new User();
  constructor(private userService: UserService, private route: ActivatedRoute, private tools: MyToolsService) {}

  UpdateProfile() {
    if (this.PasswordOption != 'true') {
      this.profile.currentPassword = null;
      this.profile.newPassword = null;
    }
    if (this.profile.phoneNumber != null) {
      this.profile.phoneNumber = this.profile.phoneNumber.toString();
    }
    this.userService.register(this.profile).subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.userService.updateAppImage(this.profile.image);
          this.userService.updateUserName(this.profile.userName);
          this.tools.ShowSuccessNotification("User", "Utilisateur mis à jour", '10000');
        }
        else {
          this.tools.ShowErrorNotification("User", res.errors[0].description, '10000');
        }
      },
      err => {
        if (err.status == 400) {
          this.tools.ShowErrorNotification("User", err.error.message, '10000');
        }
        else {
          return console.log(err);
        }
      }
    );
  }


  ngOnInit() {
    this.profile.userName = localStorage.getItem('UserName');
    this.userService.GetProfileUser(this.profile.userName)
      .subscribe((data: any) => {
        this.profile = data.result[0];
      });

  }

  UploadImage() {
    $("#FileUploader").trigger('click');
  }
  changeListener($event): void {
    this.readThis($event.target);
  }

  readThis(inputValue: any): void {
    const reader = new FileReader();
    if (inputValue.files && inputValue.files.length) {
      var image: File = inputValue.files[0];
      if (image.size < 1000000) {
        this.FileName = image.name;

        reader.readAsDataURL(image);

        reader.onload = () => {
          this.profile.image = reader.result;
        };
      }
      else {
        Swal.fire('Oops...', "La taille maximale d'image est 1MB", 'error');
      }
    }
  }
}
