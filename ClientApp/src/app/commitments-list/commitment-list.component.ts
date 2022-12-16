import { Component, Inject, ViewChild } from '@angular/core';
import { MatAccordion } from '@angular/material/expansion';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Commitment } from '../interfaces';
import { GlobalVariable } from '../globals';


@Component({
  selector: 'app-commitment-list',
  templateUrl: './commitment-list.html',
  styleUrls: ['./commitment-list.css']
})
export class CommitmentListComponent {
  @ViewChild(MatAccordion) accordion: MatAccordion;
  public commitments: Commitment[] = [];

  private baseUrl: string;
  private http: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;

    this.getCommitments();
  }

  getCommitments() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("userName", GlobalVariable.USER_NAME);

    this.http.get<Commitment[]>(this.baseUrl + 'commitments', { params: queryParams }).subscribe(result => {
      this.commitments = result;
    }, error => console.error(error));
  }

  async sendAnswer(commitment: Commitment) {
    await this.sendCommitment(commitment);
    this.getCommitments();
  }

  sendCommitment(commitment: Commitment) {
    return this.http.put<Commitment>(this.baseUrl + 'commitments/' + commitment.id, commitment).toPromise();
  }

  // needed so that ngFor does not destroy expandable panel on update and as result collapse all opened panels
  identify(index: number, commitment: any) {
    return commitment.id;
  };
}
