import { Device } from '../graph/device.model';

export class UserResultResponse {
    givenName: string;
    email: string;
    jobTitle: string;
    devices: Device[];
}