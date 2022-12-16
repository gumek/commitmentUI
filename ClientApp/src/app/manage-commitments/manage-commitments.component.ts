import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Commitment, CommitmentTemplate } from '../interfaces';
import { SolidCommitment } from '../classes';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-manage-commitments',
  templateUrl: './manage-commitments.component.html',
  styleUrls: ['./manage-commitments.css']
})
export class ManageCommitmentsComponent {
  public templates: CommitmentTemplate[] = [];
  public selectedTemplate: CommitmentTemplate;

  private baseUrl: string;
  private http: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private _snackBar: MatSnackBar) {
    this.baseUrl = baseUrl;
    this.http = http;

    this.getTemplates();
  }

  getTemplates() {
    this.http.get<CommitmentTemplate[]>(this.baseUrl + 'templates').subscribe(result => {
      this.templates = result;
      this.selectedTemplate = this.templates[0];
    }, error => console.error(error));
  }

  createCommitment(template: CommitmentTemplate) {
    let commitment = new SolidCommitment();
    commitment.title = template.title;
    commitment.description = template.description;
    commitment.deadline = template.deadline;

    console.log(commitment);
    this.sendCommitment(commitment);
  }

  sendCommitment(commitment: Commitment) {
    this.http.post<Commitment>(this.baseUrl + 'commitments', commitment).subscribe(result => {
      console.log(result);
    }, error => console.error(error));

    this.openSnackBar('Commitment created!', 'Close');
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
}
