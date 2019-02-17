import { Component, ViewChild } from '@angular/core';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { environment } from 'src/environments/environment';
import { DxDataGridComponent } from 'devextreme-angular';
@Component({ selector: "user-page", templateUrl: './user-page.component.html' })
export class UserPageComponent {
  @ViewChild(DxDataGridComponent) dataGridComponent: DxDataGridComponent;
  userUrl = environment.ip + '/api/rbac/User'
  dataSource = AspNetData.createStore({
    key: "userId",
    loadUrl: this.userUrl,
    insertUrl: this.userUrl,
    updateUrl: this.userUrl,
    deleteUrl: this.userUrl,
    onBeforeSend: function (method, ajaxOptions) {
      ajaxOptions.xhrFields = { withCredentials: true };
    }
  });
  onEditorPreparing(e) {
    if (e.parentType == "dataRow" && e.dataField == "password")
      e.editorOptions.mode = 'password';
  }

  ngAfterViewInit(): void {
    //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    //Add 'implements AfterViewInit' to the class.
  }
} 