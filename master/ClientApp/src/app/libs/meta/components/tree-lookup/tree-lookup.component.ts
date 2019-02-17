import { Component, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { DxiColumnComponent } from 'devextreme-angular/ui/nested/column-dxi';
import { DxDataGridComponent, DxTreeListComponent } from 'devextreme-angular';
import DevExpress from 'devextreme/bundles/dx.all';
import { ModeEnum } from '../../enums/Mode.enum';

@Component({ selector: 'tree-lookup', templateUrl: './tree-lookup.component.html' })
export class TreeLookupComponent {
    popupVisible: boolean = false;
    selectedRowKeys = [];
    ModeEnum=ModeEnum;
    @Input() mode: ModeEnum = ModeEnum.Query;
    @Input() value: any = {};
    @Output() valueChange = new EventEmitter();
    @Input() isMultity = false;
    @Input() dataSource = [];
    @Input() title = "添加";
    @Input() columns: DevExpress.ui.dxTreeListColumn[] = []
    @ViewChild(DxTreeListComponent) treeListComponent: DxTreeListComponent;
    ngOnInit(): void {
        if (!this.title)
            switch (this.mode) {
                case ModeEnum.Query:
                    this.title = "查看";
                    break;
                case ModeEnum.Edit:
                    this.title = "编辑";
                    break;
                case ModeEnum.Create:
                    this.title = "创建";
                    break;
            }
    }
    ngAfterViewInit(): void {
        //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
        //Add 'implements AfterViewInit' to the class.
        this.treeListComponent.columns = this.columns;

    }
    @Input()selection:'none'|'single'|'multiple'='none';

    /**
     * 提交所有已经选择的数据
     */
    submit() {
       let rows= this.treeListComponent.instance.getSelectedRowsData();
        this.valueChange.emit(rows);
        this.popupVisible=false;
    }

} 