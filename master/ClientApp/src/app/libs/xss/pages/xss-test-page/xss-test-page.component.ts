import { Component } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({ selector: "xss-test-page", templateUrl:"./xss-test-page.component.html" })
export class XSSTestPageComponent {
  method = "GET";
  items: string[] = ["GET", "FORM_POST", "POST"];
  url: string = ""
  formData = "";

  async submit() {
    console.log("center");
    let data = await this.httpClient.post(environment.ip + "/api/xss/xssTest", { url: this.url,method:this.method,formData:this.formData }).toPromise();
    console.log(data);
  }
  constructor(private httpClient: HttpClient) {
  }

}
