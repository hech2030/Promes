import { Component, OnInit } from '@angular/core';
import { Magasin } from '../../../../Models/bo/Stock/Magasin/magasin';
import { Router, ActivatedRoute } from '@angular/router';
import { MagasinService } from '../../../../Shared/Stock/Magasin/magasin.service';
import { MyToolsService } from '../../../../Shared/Tools/my-tools.service';
import Swal from 'sweetalert2';

@Component({
  host: { 'class': 'content-wrapper' },
  templateUrl: './magasin-details.component.html',
  styleUrls: ['./magasin-details.component.css']
})
export class MagasinDetailsComponent implements OnInit {
  Isloading: boolean;
  magasin: Magasin = new Magasin();

  constructor(private router: Router, private magasinService: MagasinService,
    private route: ActivatedRoute, private tools: MyToolsService) { }

  ngOnInit() {
    this.Isloading = true;
    this.magasin.id = parseInt(this.route.snapshot.paramMap.get('id'));

    this.magasinService.GetMagasin(this.magasin)
      .subscribe((data: any) => {
        console.log(data);
        this.magasin = data.result[0];
        this.Isloading = false;
      });
  }
  Quit() {
    this.router.navigate(['/Magasin']);
  }
  Update() {
    this.magasinService.SaveMagasin(this.magasin).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.tools.ShowSuccessNotification("Magasin", "La modification a été effectuée avec succès", '10000');
          this.router.navigate(['/Magasin']);
        }
        else {
          Swal.fire('Oops...', res.errors[0].description, 'error');
        }
      },
      err => {
        if (err.status == 400) {
          this.tools.ShowErrorNotification("Magasin", err.error.message, '10000');
        }
        else {
          return console.log(err);
        }
      }
    );
  }
}
