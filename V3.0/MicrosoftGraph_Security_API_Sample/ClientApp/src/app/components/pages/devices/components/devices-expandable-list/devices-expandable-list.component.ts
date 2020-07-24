import { Component, Input, AfterContentInit } from '@angular/core';
// models
import { ManagedDevice } from 'src/app/models/graph/managed-device.model';

@Component({
    selector: 'devices-expandable-list',
    templateUrl: './devices-expandable-list.component.html',
    styleUrls: ['./devices-expandable-list.component.css']
})
export class DevicesListComponent implements AfterContentInit {
    @Input() public device: ManagedDevice;
    @Input() public isExpanded = false;

    ngAfterContentInit() {
    }

    public toggle(): void {
        this.isExpanded = !this.isExpanded;
    }
}
