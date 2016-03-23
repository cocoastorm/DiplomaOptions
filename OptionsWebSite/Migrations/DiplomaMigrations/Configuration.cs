namespace OptionsWebSite.Migrations.DiplomaMigrations
{
    using DiplomaDataModel.Models;
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.Models.DiplomaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\DiplomaMigrations";
        }

        protected override void Seed(DiplomaDataModel.Models.DiplomaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.YearTerms.AddOrUpdate(
                p => p.YearTermId,
                getYearTerms().ToArray());

            context.Options.AddOrUpdate(
                p => p.OptionId,
                getOptions().ToArray());

            context.Choices.AddOrUpdate(
                p => p.ChoiceId,
                getChoices().ToArray());

            context.SaveChanges();
        }

        public static List<YearTerm> getYearTerms()
        {
            var yearterms = new List<YearTerm>
            {
                new YearTerm { YearTermId = 1, Year = 2015,  Term = 20, IsDefault = false },
                new YearTerm { YearTermId = 2, Year = 2015,  Term = 30, IsDefault = false },
                new YearTerm { YearTermId = 3, Year = 2015,  Term = 10, IsDefault = false },
                new YearTerm { YearTermId = 4, Year = 2016,  Term = 10, IsDefault = true }
            };

            return yearterms;
        }

        public static List<Option> getOptions()
        {
            var options = new List<Option>
            {
                new Option {OptionId = 1, Title = "Data Communications", IsActive = true },
                new Option {OptionId = 2, Title = "Client Server", IsActive = true },
                new Option {OptionId = 3, Title = "Digital Processing", IsActive = true },
                new Option {OptionId = 4, Title = "Information Systems", IsActive = true },
                new Option {OptionId = 5, Title = "Database", IsActive = false },
                new Option {OptionId = 6, Title = "Web and Mobile", IsActive = true },
                new Option {OptionId = 7, Title = "Tech Pro", IsActive = false }
            };

            return options;
        }

        public static List<Choice> getChoices()
        {
            var choices = new List<Choice>
            {
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00333333",
                    StudentFirstName = "Sarah",
                    StudentLastName = "Coldwell",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00444444",
                    StudentFirstName = "Bob",
                    StudentLastName = "Smith",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00555555",
                    StudentFirstName = "Jason",
                    StudentLastName = "Jolo",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 3,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 1,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00666666",
                    StudentFirstName = "Chantelle",
                    StudentLastName = "Blanc",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00777777",
                    StudentFirstName = "Joseph",
                    StudentLastName = "Band",
                    FirstChoiceOptionId = 3,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 1,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00888888",
                    StudentFirstName = "Bonnie",
                    StudentLastName = "Tran",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00999999",
                    StudentFirstName = "Jessica",
                    StudentLastName = "Jones",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 4,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00211111",
                    StudentFirstName = "Bones",
                    StudentLastName = "Doc",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00232222",
                    StudentFirstName = "Lightning",
                    StudentLastName = "Farron",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 2,
                    StudentId = "A00233333",
                    StudentFirstName = "Benjamin",
                    StudentLastName = "Cook",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 3,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 1,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00234333",
                    StudentFirstName = "Sarah",
                    StudentLastName = "Dinh",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00244444",
                    StudentFirstName = "Bob",
                    StudentLastName = "Bobbie",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00255555",
                    StudentFirstName = "Jason",
                    StudentLastName = "Derulo",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 3,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 1,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00666666",
                    StudentFirstName = "Chantal",
                    StudentLastName = "Dinh",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00777777",
                    StudentFirstName = "Joseph",
                    StudentLastName = "Dinh",
                    FirstChoiceOptionId = 3,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 1,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00888888",
                    StudentFirstName = "Bonnie",
                    StudentLastName = "Tran",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00999999",
                    StudentFirstName = "Jessica",
                    StudentLastName = "Holo",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 4,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00111211",
                    StudentFirstName = "Bones",
                    StudentLastName = "Doc",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00224222",
                    StudentFirstName = "Hope",
                    StudentLastName = "Esthieum",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice {
                    YearTermId = 4,
                    StudentId = "A00334343",
                    StudentFirstName = "Benjamin",
                    StudentLastName = "Bot",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 3,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 1,
                    SelectionDate = DateTime.Now
                },
            };

            return choices;
        }
    }
}