using AutoMapper;
using CommitmentUI.Domain;
using CommitmentUI.Mapper;
using CommitmentUI.Presentation;
using System.Globalization;

namespace CommitmentWebApi
{
    public class Program
    {
        private static IMapper? _mapper;
        private const string SampleUserName = "Michal";
        private const string SecondaryUserName = "Bob";

        private static readonly string[] Users = new string[] { SampleUserName, SecondaryUserName };

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            _mapper = ConfigureMapper();
            var commitmentFactory = new CommitmentFactory();

            var allCommitments = CreateSampleCommitments(commitmentFactory).ToList();
            ConfigureGetTemplates(app, commitmentFactory);
            ConfigureGetCommitments(app, allCommitments);
            ConfigurePutCommitments(app, allCommitments);
            ConfigurePostCommitments(app, commitmentFactory, allCommitments);

            app.Run();
        }

        private static void ConfigurePostCommitments(WebApplication app, CommitmentFactory commitmentFactory, List<Commitment> allCommitments)
        {
            app.MapPost("/commitments/", (CommitmentDto inputCommitment) =>
            {
                foreach (var user in Users)
                {
                    var newCommitment = new Commitment()
                    {
                        Template = commitmentFactory.GetTemplates().First(t => t.Title == inputCommitment.Title),
                        Deadline = DateTime.ParseExact(inputCommitment.Deadline, "yyyy-MM-dd HH:ss", CultureInfo.InvariantCulture),
                        UserName = user
                    };

                    newCommitment.Status = newCommitment.ResolveStatus();
                    allCommitments.Add(newCommitment);
                }

                return Results.Ok();
            });
        }

        private static void ConfigurePutCommitments(WebApplication app, List<Commitment> allCommitments)
        {
            app.MapPut("/commitments/{id}", (Guid id, CommitmentDto inputCommitment) =>
            {
                var commitment = allCommitments.FirstOrDefault(c => c.Id == id);
                if (commitment == null)
                {
                    return Results.NotFound();
                }

                commitment.Answer = inputCommitment.Answer;
                commitment.Status = commitment.ResolveStatus();

                return Results.NoContent();
            });
        }

        private static void ConfigureGetCommitments(WebApplication app, List<Commitment> allCommitments)
        {
            app.MapGet("/commitments", (string? userName) =>
            {
                var filteredCommitments = allCommitments;

                if (!string.IsNullOrWhiteSpace(userName))
                {
                    filteredCommitments = allCommitments.Where(c => c.UserName == userName).ToList();
                }

                return _mapper.Map<Commitment[], CommitmentDto[]>(filteredCommitments.ToArray());
            });
        }

        private static void ConfigureGetTemplates(WebApplication app, CommitmentFactory commitmentFactory)
        {
            app.MapGet("/templates", () =>
            {
                var templates = commitmentFactory.GetTemplates();
                return _mapper.Map<CommitmentTemplate[], CommitmentTemplateDto[]>(templates.ToArray());
            }).WithOpenApi();
        }

        private static Commitment[] CreateSampleCommitments(CommitmentFactory commitmentFactory)
        {
            var commitments = new List<Commitment>();

            foreach (var user in Users)
            {
                var userCommitments = new List<Commitment>
                {
                    commitmentFactory.CreateCommitment(ECommitmentType.BankAccountNumber),
                    commitmentFactory.CreateCommitment(ECommitmentType.LaptopSerialNumber),
                    commitmentFactory.CreateCommitment(ECommitmentType.Education)
                };

                foreach (var commitment in userCommitments)
                {
                    commitment.UserName = user;
                }

                commitments.AddRange(userCommitments);
            }

            return commitments.ToArray();
        }

        private static IMapper ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CommitmentProfile>();
                cfg.AddProfile<CommitmentTemplateProfile>();
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}