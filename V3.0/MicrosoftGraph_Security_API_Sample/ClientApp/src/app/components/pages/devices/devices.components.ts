import { Component, OnInit } from '@angular/core';
// models
import { BreadcrumbItem } from '../../common/content-header/breadcrumbs/breadcrumbs.component';
import { Device } from '../../../models/graph/device.model';
import { Queries } from 'src/app/models/response';
// services
import { DevicesService } from 'src/app/services/devices.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
    selector: 'app-device-page',
    templateUrl: './devices.component.html',
    styleUrls: ['./devices.components.css']
})

export class DeviceComponent implements OnInit {

    public devices: Device[];
    public isExpandedAll = false;

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
    }

    expandAll(): void {
        this.isExpandedAll = !this.isExpandedAll;
    }

    getUserDetails(): void{
        this.loader.Show('');
        this.deviceService.getNCDeviceList().subscribe(response => {
            if(response) {
                this.devices = response;
                console.log(response);
            }

            this.loader.Hide();
        })
    }
}