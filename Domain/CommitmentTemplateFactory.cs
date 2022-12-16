namespace CommitmentUI.Domain
{
    public class CommitmentTemplateFactory
    {
        public IEnumerable<CommitmentTemplate> CreateCommitmentTemplates()
        {
            return new List<CommitmentTemplate>()
            {
                CreateBankAccountCommitmentTemplate(),
                CreateLaptopSerialNumberCommitmentTemplate(),
                CreateEducationCommitmentTemplate(),
                CreateTshirtSizeCommitmentTemplate(),
                CreateBirthdayCommitmentTemplate()
            };
        }

        private CommitmentTemplate CreateBankAccountCommitmentTemplate()
        {
            return new CommitmentTemplate()
            {
                CommitmentType = ECommitmentType.BankAccountNumber,
                Title = "Provide Bank Account Number",
                Description = "Provide Bank Account Number so that company can pay your salary. Make sure there are no white spaces in your answer.",
                DefaultOffset = TimeSpan.FromDays(-20)
            };
        }

        private CommitmentTemplate CreateLaptopSerialNumberCommitmentTemplate()
        {
            return new CommitmentTemplate()
            {
                CommitmentType = ECommitmentType.LaptopSerialNumber,
                Title = "Provide Laptop Serial Number",
                Description = "Provide Laptop Serial Number so that company can keep proper inventory of its assets.",
                DefaultOffset = TimeSpan.FromHours(-8)
            };
        }

        private CommitmentTemplate CreateEducationCommitmentTemplate()
        {
            return new CommitmentTemplate()
            {
                CommitmentType = ECommitmentType.Education,
                Title = "Provide your education history",
                Description = "Provide your education history so that company can keep proper track of its employes.",
                DefaultOffset = TimeSpan.FromHours(24)
            };
        }

        private CommitmentTemplate CreateTshirtSizeCommitmentTemplate()
        {
            return new CommitmentTemplate()
            {
                CommitmentType = ECommitmentType.TshirtSize,
                Title = "Provide your T-shirt size",
                Description = "Provide your T-shirt size so that we can send you awsome tshirts.",
                DefaultOffset = TimeSpan.FromDays(10)
            };
        }

        private CommitmentTemplate CreateBirthdayCommitmentTemplate()
        {
            return new CommitmentTemplate()
            {
                CommitmentType = ECommitmentType.Birthday,
                Title = "Provide your birth date",
                Description = "We need your birth date so that we can send you birthday present!",
                DefaultOffset = TimeSpan.FromDays(-10)
            };
        }
    }
}
