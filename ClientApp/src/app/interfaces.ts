export interface Commitment {
  id: string;
  title: string;
  description: string;
  answer: string;
  deadline: string;
  status: string;
  userName: string;
}

export interface CommitmentTemplate {
  title: string;
  description: string;
  deadline: string;
}
