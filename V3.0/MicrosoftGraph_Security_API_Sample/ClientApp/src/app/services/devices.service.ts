import { Injectable } from '@angular/core';
import { Observable, from } from 'rxjs';

//models
import { ManagedDevice } from '../models/graph/managed-device.model';
import {DeviceComplianceResponse} from '../models/response/device-compliance-model';

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
}