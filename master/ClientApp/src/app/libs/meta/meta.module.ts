import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { TreeLookupComponent } from './components/tree-lookup/tree-lookup.component';


const COMPONENTS=[TreeLookupComponent]

@NgModule({
    imports:[SharedModule],
    exports:[SharedModule,COMPONENTS],
    declarations:[COMPONENTS]
})
export class MetaModule{

}