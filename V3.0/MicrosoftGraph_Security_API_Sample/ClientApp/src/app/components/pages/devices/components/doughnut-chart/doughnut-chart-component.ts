import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { DeviceComplianceResponse } from 'src/app/models/response/device-compliance-model';

@Component({
  selector: 'device-doughnut-diagram',
  templateUrl: './doughnut-chart-component.html',
  styleUrls: ['./doughnut-chart-component.css']
})
export class DevicesChartComponent implements OnChanges {

  @Input() public doughnutData: DeviceComplianceResponse;
  @Input() public isDashboard = false;
  @Input() public isDevicePage = false;

  public chart = null;

  ngOnChanges() {
    if (this.doughnutData) {
      this.chart = {
        type: 'doughnut',
        data: this.doughnutData && [this.doughnutData.nonCompliant, this.doughnutData.compliant],
        labels: ['Non-Compliant', 'Compliant'],
        legend: false,
        colors: [
          { backgroundColor: ['#BF0000', '#C4C4C4'] }
        ]
      };
    }
  }
}
