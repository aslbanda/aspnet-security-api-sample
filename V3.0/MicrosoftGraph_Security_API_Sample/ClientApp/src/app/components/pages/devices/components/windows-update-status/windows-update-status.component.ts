import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { WindowsUpdateStatuses } from 'src/app/models/response/windows-update-status.model';

@Component({
  selector: 'windows-update-status-doughnut-diagram',
  templateUrl: './windows-update-status.component.html',
  styleUrls: ['./windows-update-status.component.css']
})
export class WindowsUpdateStatusChartComponent implements OnChanges {

  @Input() public doughnutData: WindowsUpdateStatuses;
  @Input() public isDashboard = false;
  @Input() public isDevicePage = false;

  public chart = null;

  ngOnChanges() {
    if (this.doughnutData) {
      this.chart = {
        type: 'doughnut',
        data: this.doughnutData && [this.doughnutData.failed, this.doughnutData.pendingInstallation, this.doughnutData.pendingReboot, this.doughnutData.upToDate],
        labels: ['Failed', 'Pending Installations', 'Pending Re-boot', 'Up to Date'],
        legend: false,
        colors: [
          { backgroundColor: ['#BF0000', '#A68614', '#0C19A6', '#14A63D'] }
        ]
      };
    }
  }
}
