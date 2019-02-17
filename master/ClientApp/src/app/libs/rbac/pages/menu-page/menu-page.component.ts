import { Component } from '@angular/core';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
@Component({ selector: "menu-page", templateUrl:"./menu-page.component.html" })
export class MenuPageComponent {
  dataSource: any;
  priority: any[];
  constructor() {
    var url = "http://localhost:5000/api/rbac/Menu";
    this.dataSource = AspNetData.createStore({
      key: "menuId",
      loadUrl: url,
      insertUrl: url,
      updateUrl: url,
      deleteUrl: url,
      onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
      }
    });
  }
}
