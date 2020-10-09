import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InfoData } from '../data/info.data';

@Component({
  selector: 'app-info-template',
  templateUrl: './info-template.component.html',
  styleUrls: ['./info-template.component.scss']
})
export class InfoTemplateComponent implements OnInit {

  // databindings

  email: string = "";

  path: string = "";

  data: InfoData;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['email']){
        this.email = params['email'];
      }
      this.route.data.subscribe(x => {
        this.path = x.path;
        this.data = InfoData.getData(this.path);
        for (let i = 0; i < this.data.text.length; i++) {
          this.data.text[i] = this.data.text[i].replace('[email]', this.email);
        }
      });
    });
  }

}
