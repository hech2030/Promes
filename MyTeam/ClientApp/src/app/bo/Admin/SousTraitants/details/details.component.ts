import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { SousTraitant } from '../../../../Models/bo/SousTraitant/soustraitant.model';
import { SousTraitantService } from '../../../../Shared/SousTraitant/soustraitant.service';
import { MyToolsService } from '../../../../Shared/Tools/my-tools.service';
import Swal from 'sweetalert2';

@Component({
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'],
  host: { 'class': 'content-wrapper' },
})
export class SousTraitantDetailsComponent implements OnInit {

  Isloading: boolean;
  soustraitant: SousTraitant = new SousTraitant();

  constructor(private router: Router, private sousTraitantService: SousTraitantService,
    private route: ActivatedRoute, private tools: MyToolsService) { }

  ngOnInit() {
    this.Isloading = true;
    this.soustraitant.id = parseInt(this.route.snapshot.paramMap.get('id'));

    this.sousTraitantService.GetSousTraitant(this.soustraitant)
      .subscribe((data: any) => {
        console.log(data);
        this.soustraitant = data.result[0];
        this.Isloading = false;
      });
  }
  Quit() {
    this.router.navigate(['/SousTraitant']);
  }
  Update() {
    this.sousTraitantService.SaveSousTraitant(this.soustraitant).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.tools.ShowSuccessNotification("SousTraitant", "La modification a été effectuée avec succès", '10000');
          this.router.navigate(['/SousTraitant']);
        }
        else {
          Swal.fire('Oops...', res.errors[0].description, 'error');
        }
      },
      err => {
        if (err.status == 400) {
          this.tools.ShowErrorNotification("SousTraitant", err.error.message, '10000');
        }
        else {
          return console.log(err);
        }
      }
    );
  }
}
