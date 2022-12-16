namespace CommitmentUI.Domain
{
    public class Commitment
    {
        public Guid Id { get; } = Guid.NewGuid();

        public CommitmentTemplate? Template { get; set; }

        public DateTime Deadline { get; set; }

        public string? Answer { get; set; }

        public ECommitmentStatus Status { get; set; }

        public string? UserName { get; set; }

        public ECommitmentStatus ResolveStatus()
        {
            if (!string.IsNullOrWhiteSpace(Answer))
            {
                return ECommitmentStatus.Completed;
            }

            if (DateTime.Now > Deadline) 
            {
                return ECommitmentStatus.Overdue;
            }

            return ECommitmentStatus.Due;
        }
    }
}
