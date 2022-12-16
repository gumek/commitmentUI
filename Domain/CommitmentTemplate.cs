namespace CommitmentUI.Domain
{
    public class CommitmentTemplate
    {
        public ECommitmentType CommitmentType { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public TimeSpan DefaultOffset { get; set; }
    }
}
