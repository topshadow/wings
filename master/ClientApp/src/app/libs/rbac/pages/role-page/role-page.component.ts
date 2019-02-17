import { Component } from '@angular/core';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import DevExpress from 'devextreme/bundles/dx.all';
import { environment } from 'src/environments/environment';
import { ModeEnum } from 'src/app/libs/meta/enums/Mode.enum';
@Component({ selector: "role-page", templateUrl: './role-page.component.html' })
export class RolePageComponent {
  Mode = ModeEnum
  dataSource: any;
  popupVisible = false;
  menuColumns: DevExpress.ui.dxTreeListColumn[] = [{ caption: "菜单", dataField: "text" }, { caption: "链接", dataField: 'link' }]
  constructor() {
    var url = environment.ip + "/api/rbac/Role";

    this.dataSource = AspNetData.createStore({
      key: "roleId",
      loadUrl: url,
      insertUrl: url,
      updateUrl: url,
      deleteUrl: url,
      onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
      }
    });
  }
  menuUrl = environment.ip;
  editMenuDataSource = AspNetData
    .createStore({
      key: "menuId",
      loadUrl: environment.ip + "/api/rbac/Menu"
    })
}
