import { Injectable } from '@angular/core';
import { Observable, from } from 'rxjs';

//models
import { Device } from '../models/graph/device.model';

//services
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root'})

export class DevicesService {
    constructor(
        private http: HttpService
    ){}

    getNCDeviceList(): Observable<Device[]> {
        return this.http.get<Device[]>('devices/GetNCDeviceList');
    }
}