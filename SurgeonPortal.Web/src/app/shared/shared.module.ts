import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { AppFooterComponent } from "./footer/footer.component";
import { AppHeaderComponent } from "./header/header.component";
import { MaterialModule } from "ytg-angular-material";
import { FlexLayoutModule } from "@angular/flex-layout";
import { ODNTrackerApiModule } from "../api/api.module";
import { AgGridModule } from '@ag-grid-community/angular';
import {
    ModuleRegistry,
    ClientSideRowModelModule,
    ClipboardModule,
    ColumnsToolPanelModule,
    ExcelExportModule,
    FiltersToolPanelModule,
    MasterDetailModule,
    MenuModule,
    RangeSelectionModule,
    RichSelectModule,
    RowGroupingModule,
    SetFilterModule,
    SideBarModule,
    StatusBarModule
} from '@ag-grid-enterprise/all-modules';

@NgModule({
    imports: [
        AgGridModule.withComponents(),
        ODNTrackerApiModule,
        CommonModule,
        FlexLayoutModule,
        MaterialModule,
    ],
    declarations: [
        AppFooterComponent,
        AppHeaderComponent
    ],
    exports: [
        AppFooterComponent,
        AppHeaderComponent
    ]
})
export class SharedModule {
    constructor() {
        ModuleRegistry.registerModules([
            ClientSideRowModelModule,
            ClipboardModule,
            ColumnsToolPanelModule,
            ExcelExportModule,
            FiltersToolPanelModule,
            MasterDetailModule,
            MenuModule,
            RangeSelectionModule,
            RichSelectModule,
            RowGroupingModule,
            SetFilterModule,
            SideBarModule,
            StatusBarModule
        ]);
    }
}