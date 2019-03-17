import { NgModule } from "@angular/core";
import { SharedModule } from "../../shared/shared.module";
import { MetaModule } from '../meta/meta.module';
import { XSSTestPageComponent } from './pages/xss-test-page/xss-test-page.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [XSSTestPageComponent],
  imports: [SharedModule, MetaModule,
    RouterModule.forChild([{ path: "xss-test", component: XSSTestPageComponent }])
  ]
})
export class XSSModule {

}
