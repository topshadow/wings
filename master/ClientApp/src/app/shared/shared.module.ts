import { NgModule } from '@angular/core';
import { DxDataGridModule, DxTreeListModule, DxPopupModule, DxSwitchModule, DxButtonModule, DxTextAreaModule, DxTextBoxModule, DxSelectBoxModule } from 'devextreme-angular';
import { CommonModule } from '@angular/common';

@NgModule({

  imports: [CommonModule,
    DxTextAreaModule,
    DxDataGridModule, DxTreeListModule,
    DxPopupModule, DxSwitchModule, DxButtonModule, DxTextBoxModule, DxSelectBoxModule
  ],
  exports: [DxDataGridModule, DxTreeListModule,
    DxPopupModule, DxSwitchModule, DxButtonModule, CommonModule, DxTextAreaModule, DxTextBoxModule, DxSelectBoxModule]
})
export class SharedModule {

}
