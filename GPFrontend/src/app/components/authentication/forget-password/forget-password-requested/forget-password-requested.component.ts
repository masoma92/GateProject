import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-forget-password-requested',
  templateUrl: './forget-password-requested.component.html',
  styleUrls: ['./forget-password-requested.component.scss']
})
export class ForgetPasswordRequestedComponent implements OnInit {

  email: string;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.email = params['email'];
    });
  }

}
