import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Chart } from 'chart.js';
import {MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS} from '@angular/material-moment-adapter';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import {MatDatepicker} from '@angular/material/datepicker';
// Depending on whether rollup is used, moment needs to be imported differently.
// Since Moment.js doesn't have a default export, we normally need to import using the `* as`
// syntax. However, rollup creates a synthetic default module and we thus need to import it using
// the `default as` syntax.
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import {default as _rollupMoment, Moment} from 'moment';
import { DashboardService } from 'src/app/services/dashboard/dashboard.service';
import { EntityResult } from 'src/app/services/common/entity.service';
import { ChartResponse, CreateAccountChart } from 'src/app/services/dashboard/dashboard';
import { MatSnackBar } from '@angular/material';

const moment = _rollupMoment || _moment;

const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

// See the Moment.js docs for the meaning of these formats:
// https://momentjs.com/docs/#/displaying/format/
export const MY_FORMATS = {
  parse: {
    dateInput: 'MM/YYYY',
  },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'dashboard-admin',
  templateUrl: './dashboard-admin.component.html',
  styleUrls: ['./dashboard-admin.component.scss'],
  providers: [
    // `MomentDateAdapter` can be automatically provided by importing `MomentDateModule` in your
    // application's root module. We provide it at the component level here, due to limitations of
    // our example generation script.
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },

    {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS},
  ],
})
export class DashboardAdminComponent implements OnInit {

  constructor(private dashboardService: DashboardService,
    private snackBar: MatSnackBar) { }
  
  //sum
  sumAccountsResult = new EntityResult<number>();
  sumGatesResult = new EntityResult<number>();
  sumUsersResult = new EntityResult<number>();
  sumErrorsResult = new EntityResult<number>();
  
  // chart
  public chart;
  currentDate = new Date();
  isDatePickerHidden = false;
  fromDate = new FormControl(moment("2020-01-29 00:00"));
  toDate = new FormControl(moment("2020-12-29 00:00"));

  chartResult = new EntityResult<ChartResponse>();
  chartFinished = false;

  xValues = [];
  yValues = [];

  ngOnInit() {

    this.dashboardService.getAdminSums("sumAccounts", this.sumAccountsResult);
    this.dashboardService.getAdminSums("sumGates", this.sumGatesResult);
    this.dashboardService.getAdminSums("sumUsers", this.sumUsersResult);
    this.dashboardService.getAdminSums("sumErrors", this.sumErrorsResult);
    
    window.onresize = () => this.isDatePickerHidden = window.innerWidth <= 1112;
    this.go();
  }

  chosenFromYearHandler(normalizedYear: Moment) {
    const ctrlValue = this.fromDate.value;
    ctrlValue.year(normalizedYear.year());
    this.fromDate.setValue(ctrlValue);
  }

  chosenFromMonthHandler(normalizedMonth: Moment, datepicker: MatDatepicker<Moment>) {
    const ctrlValue = this.fromDate.value;
    ctrlValue.month(normalizedMonth.month());
    this.fromDate.setValue(ctrlValue);
    datepicker.close();
  }

  chosenToYearHandler(normalizedYear: Moment) {
    const ctrlValue = this.toDate.value;
    ctrlValue.year(normalizedYear.year());
    this.toDate.setValue(ctrlValue);
  }

  chosenToMonthHandler(normalizedMonth: Moment, datepicker: MatDatepicker<Moment>) {
    const ctrlValue = this.toDate.value;
    ctrlValue.month(normalizedMonth.month());
    this.toDate.setValue(ctrlValue);
    datepicker.close();
  }

  go() {
    if (this.fromDate.value > this.toDate.value) {
      this.snackBar.open("Wrong period!", "Close", { duration: 2000, panelClass: 'toast.success' });
      return;
    }

    this.chartFinished = false;
    this.xValues = [];
    this.yValues = [];
    this.chartResult.onFinished = () => {
      Object.keys(this.chartResult.value.chartData).forEach(key => {
        this.xValues.push(monthNames[+key.split('.')[0]-1]);
      });
      console.log(this.xValues);
      Object.values(this.chartResult.value.chartData).forEach(value => {
        this.yValues.push(+value);
      });
      console.log(this.yValues);
      this.initChart();
      this.chartFinished = true;
    }
    this.dashboardService.createAccountChart(new CreateAccountChart(this.fromDate.value.toDate(), this.toDate.value.toDate()), this.chartResult);
  }

  initChart() {
    this.chart = new Chart('canvas', {
      type: 'line',

      data: {
        labels: this.xValues,
        datasets: [{
            borderColor: "#6bd098",
            backgroundColor: "#6bd098",
            pointRadius: 0,
            pointHoverRadius: 0,
            borderWidth: 3,
            data: this.yValues}
        ]
      },
      options: {
        legend: {
          display: false
        },

        tooltips: {
          enabled: false
        },

        scales: {
          yAxes: [{

            ticks: {
              fontColor: "#9f9f9f",
              beginAtZero: false,
              maxTicksLimit: 5,
              //padding: 20
            },
            gridLines: {
              drawBorder: false,
              zeroLineColor: "#ccc",
              color: 'rgba(255,255,255,0.05)'
            }

          }],

          xAxes: [{
            barPercentage: 1.6,
            gridLines: {
              drawBorder: false,
              color: 'rgba(255,255,255,0.1)',
              zeroLineColor: "transparent",
              display: false,
            },
            ticks: {
              padding: 20,
              fontColor: "#9f9f9f"
            }
          }]
        },
      }
    });
  }
}
