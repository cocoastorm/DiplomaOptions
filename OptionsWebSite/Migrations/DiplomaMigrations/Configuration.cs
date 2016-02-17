namespace OptionsWebSite.Migrations.DiplomaMigrations
{
    using DiplomaDataModel.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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

            context.SaveChanges();

            context.Options.AddOrUpdate(
                p => p.OptionId,
                getOptions().ToArray());

            context.SaveChanges();
        }

        public static List<YearTerm> getYearTerms()
        {
            var yearterms = new List<YearTerm>
            {
                new YearTerm { Year = 2015,  Term = 20, IsDefault = false },
                new YearTerm { Year = 2015,  Term = 30, IsDefault = false },
                new YearTerm { Year = 2016,  Term = 10, IsDefault = false },
                new YearTerm { Year = 2016,  Term = 30, IsDefault = false }
            };

            return yearterms;
        }

        public static List<Option> getOptions()
        {
            var options = new List<Option>
            {
                new Option {Title = "Data Communications", IsActive = true },
                new Option {Title = "Client Server", IsActive = true },
                new Option {Title = "Digital Processing", IsActive = true },
                new Option {Title = "Information Systems", IsActive = true },
                new Option {Title = "Database", IsActive = false },
                new Option {Title = "Web and Mobile", IsActive = true },
                new Option {Title = "Tech Pro", IsActive = false }
            };

            return options;
        }

        public static List<ApplicationUser> getUsers()
        {
            var passwordHash = new PasswordHasher();
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "A00111111", Email = "a@a.a", PasswordHash = passwordHash.HashPassword("P@$$w0rd")},
                new ApplicationUser { UserName = "A00222222", Email = "s@s.s", PasswordHash = passwordHash.HashPassword("P@$$w0rd")}
            };

            return users;
        }
    }
}
