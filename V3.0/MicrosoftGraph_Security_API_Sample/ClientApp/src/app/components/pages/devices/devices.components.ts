import { Component, OnInit } from '@angular/core';
// models
import { BreadcrumbItem } from '../../common/content-header/breadcrumbs/breadcrumbs.component';
import { ManagedDevice } from '../../../models/graph/managed-device.model';
import { Queries } from 'src/app/models/response';
import { DeviceComplianceResponse } from '../../../models/response/device-compliance-model';
import { SoftwareInventoryResponse } from '../../../models/response/software-inventory-model';
import { DeviceScore } from '../../../models/response/device-score.model';
import { WindowsUpdateStatuses } from '../../../models/response/windows-update-status.model';
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
    public softwares: SoftwareInventoryResponse[];
    public deviceScore: DeviceScore;
    public windowsUpdateStatus: WindowsUpdateStatuses;
    public isModalShow = false;

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
        this.getSoftwareInventory();
        this.getDeviceScore();
        this.getWindowsUpdateStatuses();
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

    getSoftwareInventory(): void {
        this.deviceService.getSoftwareInventory().subscribe(response => {
            if(response){
                this.softwares = response;
            }
        });
    }

    getDeviceScore(): void {
        this.deviceService.getDeviceScore().subscribe(response => {
            this.deviceScore = response;
        });
    }

    getWindowsUpdateStatuses(): void {
        this.deviceService.getWindowsUpdateStatus().subscribe(response => {
            this.windowsUpdateStatus = response;
        })
    }

    showModal(): void {
        this.isModalShow = true;
    }

    closeModal(): void {
        this.isModalShow = false;
    }
}