import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Agence } from '../../../../Models/bo/Agence/agence.model';
import { AgenceService } from '../../../../Shared/Agence/agence.service';
import { MyToolsService } from '../../../../Shared/Tools/my-tools.service';
import Swal from 'sweetalert2';

@Component({
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'],
  host: { 'class': 'content-wrapper' },
})
export class AgenceDetailsComponent implements OnInit {

  Isloading: boolean;
  agence: Agence = new Agence();

  constructor(private router: Router, private agenceService: AgenceService,
    private route: ActivatedRoute, private tools: MyToolsService) { }

  ngOnInit() {
    this.Isloading = true;
    this.agence.id = parseInt(this.route.snapshot.paramMap.get('id'));

    this.agenceService.GetAgence(this.agence)
      .subscribe((data: any) => {
        console.log(data);
        this.agence = data.result[0];
        this.Isloading = false;
      });
  }
  Quit() {
    this.router.navigate(['/Agence']);
  }
  Update() {
    this.agenceService.SaveAgence(this.agence).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.tools.ShowSuccessNotification("Agence", "La modification a été effectuée avec succès", '10000');
          this.router.navigate(['/Agence']);
        }
        else {
          Swal.fire('Oops...', res.errors[0].description, 'error');
        }
      },
      err => {
        if (err.status == 400) {
          this.tools.ShowErrorNotification("Agence", err.error.message, '10000');
        }
        else {
          return console.log(err);
        }
      }
    );
  }
}
