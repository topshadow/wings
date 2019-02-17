import { Component } from '@angular/core';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
@Component({ selector: "org-page", templateUrl: "./org-page.component.html" })
export class OrgPageComponent {
  dataSource: any;
  priority: any[];
  constructor() {
  
    var url = "http://localhost:5000/api/rbac/Org";

    this.dataSource = AspNetData.createStore({
      key: "orgId",
      loadUrl: url ,
      insertUrl: url ,
      updateUrl: url ,
      deleteUrl: url ,
      onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
      }
    });
  }
}
