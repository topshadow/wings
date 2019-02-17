import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { MetaModule } from '../meta/meta.module';
import { WorkerPagecomponent } from './pages/worker-page/worker-page.component';
import { RouterModule } from '@angular/router';
import { GameProcessDetailPageComponent } from './pages/game-process-detail-page/game-process-detail-page.component';
import { PositionPageComponent } from './pages/basic/position-page/position-page.component';

@NgModule({
  declarations: [WorkerPagecomponent, GameProcessDetailPageComponent, PositionPageComponent],
    imports: [
      RouterModule.forChild([{ path: 'worker', component: WorkerPagecomponent },
        { path: "game-process-detail", component: GameProcessDetailPageComponent },
        { path: "basic/position", component: PositionPageComponent }
      ]),
        SharedModule,
        MetaModule]
})
export class WorkerModule {

}
