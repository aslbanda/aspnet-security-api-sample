import { Injectable } from '@angular/core';
import { Observable, from } from 'rxjs';

//models
import { ManagedDevice } from '../models/graph/managed-device.model';
import { DeviceComplianceResponse } from '../models/response/device-compliance-model';
import { SoftwareInventoryResponse } from '../models/response/software-inventory-model';
import { DeviceScore } from "../models/response/device-score.model";
import { WindowsUpdateStatuses } from '../models/response/windows-update-status.model';

//services
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root'})

export class DevicesService {
    constructor(
        private http: HttpService
    ){}

    getNCDeviceList(): Observable<ManagedDevice[]> {
        return this.http.get<ManagedDevice[]>('devices/GetNCDeviceList');
    }

    getNCDetailsForChart(): Observable<DeviceComplianceResponse> {
        return this.http.get<DeviceComplianceResponse>('devices/GetDeviceComplianceDetails');
    }

    getSoftwareInventory(): Observable<SoftwareInventoryResponse[]> {
        return this.http.get<SoftwareInventoryResponse[]>('security/GetSoftwareInventoryAsync');
    }

    getDeviceScore(): Observable<DeviceScore> {
        return this.http.get<DeviceScore>('security/GetDeviceScoreAsync');
    }

    getWindowsUpdateStatus(): Observable<WindowsUpdateStatuses> {
        return this.http.get<WindowsUpdateStatuses>('security/GetWindowsUpdateStatus');
    }
}