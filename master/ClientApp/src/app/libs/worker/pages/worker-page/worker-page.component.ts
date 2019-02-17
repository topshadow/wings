import { Component, ViewChild } from '@angular/core';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { environment } from 'src/environments/environment';
import { DxDataGridComponent } from 'devextreme-angular';
import { GameProcess } from '../../struct/GameProcess';
import { Router } from '@angular/router';
@Component({ selector: 'worker-page', templateUrl: './worker-page.component.html' })
export class WorkerPagecomponent {
  selectedRowKeys = [];
  @ViewChild('workerGrid') workerGrid: DxDataGridComponent;
  @ViewChild("gameProcessGrid") gameGrid: DxDataGridComponent;
  workerUrl = environment.ip + '/api/Cluster/Cluster';
  keys = []
  workerDataSource = AspNetData.createStore({
    key: "workerId",
    loadUrl: this.workerUrl,
    updateUrl: this.workerUrl,
    deleteUrl: this.workerUrl,
    insertUrl: this.workerUrl
  });
  gameProcessDataSource: any = [];
  constructor(private router:Router) { }
  selectWorkerChange() {
    if (this.workerGrid) {
      let data = this.workerGrid.instance.getSelectedRowsData()[0];
      let ip = (data.ip as string).startsWith("http://") ? data.ip : `http://` + data.ip;
      let url = ip + "/api/Worker/MH/Process/listGameProcess";
      this.gameProcessDataSource = AspNetData.createStore({
        key: "windowTitle",
        loadUrl: url,
        updateUrl: url,
        deleteUrl: url,
        insertUrl: url
      });

    } else {
      return [];
    }
    setTimeout(() => {
      this.gameGrid.instance.on("selectionChanged", () => { this.selectGameProcess(this.gameGrid.instance.getSelectedRowsData()[0]) });
    }, 1000 )

  }

  ngAfterViewInit(): void {
    //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    //Add 'implements AfterViewInit' to the class.
    this.workerGrid.instance.on('selectionChanged', () => { this.selectWorkerChange() });

  }
  popImageVisible = false;
  popImageUrl = "";
  popImage($event) {
    this.popImageVisible = true;
    this.popImageUrl = $event;
  }
  selectedGameProcess: GameProcess;
  selectGameProcess($event) {
    this.selectedGameProcess = $event;

  }
  goGameProcessDetailPage() {
    this.router.navigateByUrl(`/admin/worker/game-process-detail?pid=${this.selectedGameProcess.windowTitle}&&ip=${this.selectedGameProcess.ip}`)
  }
}  
