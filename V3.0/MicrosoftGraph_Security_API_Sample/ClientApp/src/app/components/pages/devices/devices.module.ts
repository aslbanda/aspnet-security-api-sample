import { NgModule } from '@angular/core';
import { ChartsModule } from 'ng2-charts';

// ngx-perfect-scrollbar
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
};
// common reusable components
import { CommonComponentsModule } from '../../common/common-components.module';

//components
import {DeviceComponent} from './devices.components';
import {DevicesListComponent} from './components/devices-expandable-list/devices-expandable-list.component';
import {DevicesChartComponent} from './components/doughnut-chart/doughnut-chart-component';
import {SoftwareInventoryComponent} from './components/software-inventory/software-inventory.component';
import {WindowsUpdateStatusChartComponent} from './components/windows-update-status/windows-update-status.component';
import { from } from 'rxjs';

const components = [
    DevicesListComponent,
    DeviceComponent,
    DevicesChartComponent,
    SoftwareInventoryComponent,
    WindowsUpdateStatusChartComponent
];

@NgModule({
    imports: [
        CommonComponentsModule,
        PerfectScrollbarModule,
        ChartsModule
    ],
    providers: [
        {
            provide: PERFECT_SCROLLBAR_CONFIG,
            useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
        }
    ],
    declarations: components,
    exports: components
})

export class DeviceModule { }
