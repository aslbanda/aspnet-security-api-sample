import { Component, OnInit } from '@angular/core';
// models
import { BreadcrumbItem } from '../../common/content-header/breadcrumbs/breadcrumbs.component';
import { ManagedDevice } from '../../../models/graph/managed-device.model';
import { Queries } from 'src/app/models/response';
import { DeviceComplianceResponse } from '../../../models/response/device-compliance-model';
// services
import { DevicesService } from 'src/app/services/devices.service';
import { LoaderService } from 'src/app/services/loader.service';
import { from } from 'rxjs';

@Component({
    selector: 'app-device-page',
    templateUrl: './devices.component.html',
    styleUrls: ['./devices.components.css']
})

export class DeviceComponent implements OnInit {

    public devices: ManagedDevice[];
    public isExpandedAll = false;
    public doughnutData: DeviceComplianceResponse;

    public title = 'Devices';
    public breadcrumbItems: BreadcrumbItem[] = [
        { Title: 'Dashboard', URL: '/dashboard' },
        { Title: 'Devices', URL: '/devices' }
    ];

    constructor(
        private deviceService: DevicesService,
        private loader: LoaderService
    ) {
        this.loader.Show('');
    }

    ngOnInit(): void{
        this.getUserDetails();
        this.getNonCompliantDevices();
    }

    expandAll(): void {
        this.isExpandedAll = !this.isExpandedAll;
    }

    getUserDetails(): void{
        this.loader.Show('');
        this.deviceService.getNCDeviceList().subscribe(response => {
            if(response) {
                this.devices = response;
            }

            this.loader.Hide();
        });
    }

    getNonCompliantDevices(): void {
        this.deviceService.getNCDetailsForChart().subscribe(response => {
            if(response){
                this.doughnutData = response;
            }
        });
    }
}