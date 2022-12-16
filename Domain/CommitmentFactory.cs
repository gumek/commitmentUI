using CommitmentUI.Domain;

namespace CommitmentUI.Domain
{
    public class CommitmentFactory
    {
        private IEnumerable<CommitmentTemplate> _templates;

        public CommitmentFactory()
        {
            var templateFactory = new CommitmentTemplateFactory();
            _templates = templateFactory.CreateCommitmentTemplates();
        }

        public Commitment CreateCommitment(ECommitmentType commitmentType)
        {
            var template = _templates.First(t => t.CommitmentType == commitmentType);
            var commitment = new Commitment()
            {
                Template = template,
                Answer = string.Empty,
                Deadline = DateTime.Now.Add(template.DefaultOffset)
            };

            commitment.Status = commitment.ResolveStatus();

            return commitment;
        }

        public IEnumerable<CommitmentTemplate> GetTemplates()
        {
            return _templates;
        }
    }
}
