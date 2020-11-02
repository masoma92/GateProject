import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { EntityResult } from 'src/app/services/common/entity.service';
import { ChartResponse, CreateChart } from 'src/app/services/dashboard/dashboard';
import { DashboardService } from 'src/app/services/dashboard/dashboard.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'dashboard-user',
  templateUrl: './dashboard-user.component.html',
  styleUrls: ['./dashboard-user.component.scss']
})
export class DashboardUserComponent implements OnInit {

  currentDate = new Date();

  // sum
  sumGateAdminAccesses = new EntityResult<number>();
  sumGateAccesses = new EntityResult<number>();
  lastGateAccessDate = new EntityResult<Date>();
  registrationDate = new EntityResult<Date>();

  // chart
  public chart;
  isDatePickerHidden = false;
  fromDate = new FormControl(new Date(new Date().setHours(0,0,0,1)));
  toDate = new FormControl(new Date(new Date().setDate(new Date().getDate() + 7)));

  chartResult = new EntityResult<ChartResponse>();
  chartFinished = false;

  xValues = [];
  yValues = [];

  constructor(private dashboardService: DashboardService,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.dashboardService.getSums("sumGateAdminAccesses", this.sumGateAdminAccesses);
    this.dashboardService.getSums("sumGateAccesses", this.sumGateAccesses);
    this.dashboardService.getRegDate(this.registrationDate);
    this.dashboardService.getLastGateAccessDate(this.lastGateAccessDate);

    this.go();
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
        this.xValues.push(key);
      });
      Object.values(this.chartResult.value.chartData).forEach(value => {
        this.yValues.push(+value);
      });
      this.initChart();
      this.chartFinished = true;
    }

    console.log(new Date(this.fromDate.value.setHours(0,0,0,1)));
    console.log(new Date(this.toDate.value.setHours(23,59,59,59)));

    this.dashboardService.createChart("createGateUsageChart", new CreateChart(new Date(this.fromDate.value.setHours(0,0,0,1)), new Date(this.toDate.value.setHours(23,59,59,59))), this.chartResult);
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
