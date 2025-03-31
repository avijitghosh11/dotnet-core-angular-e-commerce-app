import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { environment } from '../../../environments/environment'

@Component({
  selector: 'app-test-errors',
  imports: [
    MatButton
  ],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.scss'
})
export class TestErrorsComponent {
  baseUrl = environment.apiUrl + 'buggy/';
  private http = inject(HttpClient);
  errorType: string = '';
  validationErrors?: string[];

  get404Error() {
    this.errorType = 'notfound';
    this.getError();
  }

  get400Error() {
    this.errorType = 'badrequest';
    this.getError();
  }

  get401Error() {
    this.errorType = 'unauthorized';
    this.getError();
  }

  get500Error() {
    this.errorType = 'internalerror';
    this.getError();
  }

  get400ValidationError() {
    this.errorType = 'validationerror';
    this.getError();
  }

  private getError() {
    if (this.errorType == 'validationerror') {
      this.http.post(this.baseUrl + this.errorType, {}).subscribe({
        next: response => console.log(response),
        error: error => this.validationErrors = error
      });
    }
    else {
      this.http.get(this.baseUrl + this.errorType).subscribe({
        next: response => console.log(response),
        error: error => console.log(error)
      });
    }
  }
}
