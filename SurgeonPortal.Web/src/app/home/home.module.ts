import { NgModule } from "@angular/core";
import { SharedModule } from "../shared/shared.module";
import { HomeComponent } from "./components/home/home.component";
import { HomeRoutingModule } from "./home-routing.module";

@NgModule({
    imports: [
        HomeRoutingModule,
        SharedModule
    ],
    declarations: [
        HomeComponent
    ]
})
export class HomeModule { }