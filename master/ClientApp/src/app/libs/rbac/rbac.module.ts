import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { OrgPageComponent } from './pages/org-page/org-page.component';
import { DxDataGridModule, DxDateBoxModule } from 'devextreme-angular';
import { SharedModule } from 'src/app/shared/shared.module';
import { MenuPageComponent } from './pages/menu-page/menu-page.component';
import { RolePageComponent } from './pages/role-page/role-page.component';
import { MetaModule } from '../meta/meta.module';
import { UserPageComponent } from './pages/user-page/user-page.component';

@NgModule({
  imports: [
    SharedModule,
    MetaModule,
    RouterModule.forChild([
      { path: "org", component: OrgPageComponent },
      { path: "menu", component: MenuPageComponent },
      { path: "role", component: RolePageComponent },
      { path: 'user', component: UserPageComponent }
    ])],
  declarations: [OrgPageComponent, MenuPageComponent, RolePageComponent, UserPageComponent],
  exports: [MetaModule]
})
export class RbacModule {

}
