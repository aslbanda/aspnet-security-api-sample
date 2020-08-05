import { Component, Input, AfterContentInit } from '@angular/core';
// models
import { SoftwareInventoryResponse } from 'src/app/models/response/software-inventory-model';

@Component({
    selector: 'software-inventory',
    templateUrl: './software-inventory.component.html',
    styleUrls: ['./software-inventory.component.css']
})
export class SoftwareInventoryComponent implements AfterContentInit {
    @Input() public software: SoftwareInventoryComponent;

    ngAfterContentInit() {
    }
}
