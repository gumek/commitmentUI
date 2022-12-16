import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Commitment } from '../interfaces';

@Component({
  selector: 'app-answers-list',
  templateUrl: './answers-list.component.html'
})
export class AnswersListComponent {
  public commitments: Commitment[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Commitment[]>(baseUrl + 'commitments').subscribe(result => {
      this.commitments = result;
    }, error => console.error(error));
  }
}
