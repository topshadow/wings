import { Component, HostBinding } from '@angular/core';
import { AuthService, ScreenService, AppInfoService } from './shared/services';
import { locale, loadMessages, formatMessage } from 'devextreme/localization';
import 'devextreme-intl';


//import deMessages from 'npm:devextreme/localization/messages/de.json!json';
import * as deMessages from 'devextreme/localization/messages/de.json';
import * as zhMessages from 'devextreme/localization/messages/zh.json';
//import ruMessages from 'npm:devextreme/localization/messages/ru.json!json';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  @HostBinding('class') get getClass() {
    return Object.keys(this.screen.sizes).filter(cl => this.screen.sizes[cl]).join(' ');
  }
  initMessages() {
    loadMessages(deMessages);
    //loadMessages(ruMessages);
    locale("zh");
  } 

  constructor(private authService: AuthService, private screen: ScreenService, public appInfo: AppInfoService) {
    this.initMessages();
  }

  isAutorized() {
    return this.authService.isLoggedIn;
  }
}
