import { NgModule } from '@angular/core';
import { DxDataGridModule, DxTreeListModule, DxPopupModule, DxSwitchModule, DxButtonModule } from 'devextreme-angular';
import { CommonModule } from '@angular/common';

@NgModule({

  imports: [CommonModule,
    DxDataGridModule, DxTreeListModule,
    DxPopupModule, DxSwitchModule, DxButtonModule
  ],
  exports: [DxDataGridModule, DxTreeListModule,
    DxPopupModule, DxSwitchModule, DxButtonModule,CommonModule]
})
export class SharedModule {

}
