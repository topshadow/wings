import { Component } from '@angular/core';
import * as AspNet from "devextreme-aspnet-data-nojquery";
import { environment } from 'src/environments/environment';
@Component({ selector: "position-page", templateUrl:"./position-page.component.html" })
export class PositionPageComponent {
  positionUrl = environment.ip +"/api/Cluster/Basic/Position"
  positionDataSource = AspNet.createStore({
    loadUrl: this.positionUrl,
    insertUrl: this.positionUrl,
    updateUrl: this.positionUrl,
    deleteUrl: this.positionUrl
  });
}
