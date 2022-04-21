using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

            optionsBuilder.UseSqlServer(_connStr);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void
            OnModelCreating(ModelBuilder modelBuilder)
        {
            // ApplicantEducation
            modelBuilder.Entity<ApplicantEducationPoco>
               (entity =>
               {
                   entity.HasOne(e => e.ApplicantProfile).
                     WithMany(p => p.ApplicantEducations).
                     HasForeignKey(e => e.Applicant);
               });
            // ApplicantProfile

            modelBuilder.Entity<ApplicantProfilePoco>
             (entity =>
             {
                 entity.HasOne(p => p.SecurityLogin).
                   WithMany(s => s.ApplicantProfiles).
                   HasForeignKey(p => p.Login);
             });

            modelBuilder.Entity<ApplicantProfilePoco>
             (entity =>
             {
                 entity.HasOne(p => p.SystemCountryCode).
                 WithMany(s => s.ApplicantProfile).
                 HasForeignKey(p => p.Country);
             });

            // ApplicantSkill
            modelBuilder.Entity<ApplicantSkillPoco>
               (entity =>
               {
                   entity.HasOne(s => s.ApplicantProfile).
                     WithMany(p => p.ApplicantSkills).
                     HasForeignKey(s => s.Applicant);
               });

            // ApplicantJobAppliction

            modelBuilder.Entity<ApplicantJobApplicationPoco>
               (entity =>
               {
                   entity.HasOne(a => a.ApplicantProfile).
                     WithMany(p => p.ApplicantJobApplications).
                     HasForeignKey(a => a.Applicant);
               });

            modelBuilder.Entity<ApplicantJobApplicationPoco>
               (entity =>
               {
                   entity.HasOne(a => a.CompanyJob).
                   WithMany(c => c.ApplicantJobApplications).
                   HasForeignKey(a => a.Job);
               });

            //ApplicantResume

            modelBuilder.Entity<ApplicantResumePoco>
             (entity =>
             {
                 entity.HasOne(r => r.ApplicantProfile).
                   WithMany(p => p.ApplicantResumes).
                   HasForeignKey(r => r.Applicant);
             });
            //ApplicantWorkHistory

            modelBuilder.Entity<ApplicantWorkHistoryPoco>
               (entity =>
               {
                   entity.HasOne(w => w.ApplicantProfile).
                     WithMany(p => p.ApplicantWorkHistorys).
                     HasForeignKey(w => w.Applicant);
               });

            modelBuilder.Entity<ApplicantWorkHistoryPoco>
               (entity =>
               {
                   entity.HasOne(w => w.SystemCountryCode).
                    WithMany(p => p.ApplicantWorkHistory).
                    HasForeignKey(w => w.CountryCode);
               });

            // SecurityLoginsLog

            modelBuilder.Entity<SecurityLoginsLogPoco>
              (entity =>
              {
                  entity.HasOne(p => p.SecurityLogin).
                    WithMany(s => s.SecurityLoginsLogs).
                    HasForeignKey(p => p.Login);

              });

            //CompanyJobSkill

            modelBuilder.Entity<CompanyJobSkillPoco>
               (entity =>
               {
                   entity.HasOne(c => c.CompanyJob).
                     WithMany(s => s.CompanyJobSkills).
                     HasForeignKey(j => j.Job);
               });

            //CompanyJobEducation

            modelBuilder.Entity<CompanyJobEducationPoco>
               (entity =>
               {
                   entity.HasOne(c => c.CompanyJob).
                   WithMany(j => j.CompanyJobEducations).
                   HasForeignKey(a => a.Job);
               });

            //CompanyJobDescription

            modelBuilder.Entity<CompanyJobDescriptionPoco>
               (entity =>
               {
                   entity.HasOne(c => c.CompanyJob).
                   WithMany(c => c.CompanyJobDescriptions).
                   HasForeignKey(c => c.Job);
               });

            //CompanyJob

            modelBuilder.Entity<CompanyJobPoco>
               (entity =>
               {
                   entity.HasOne(c => c.CompanyProfile).
                   WithMany(c => c.CompanyJobs).
                   HasForeignKey(c => c.Company);
               });


            //SecurityLoginsRole

            modelBuilder.Entity<SecurityLoginsRolePoco>
               (entity =>
               {
                   entity.HasOne(p => p.SecurityLogin).
                   WithMany(s => s.SecurityLoginsRoles).
                   HasForeignKey(p => p.Login);


               });

            modelBuilder.Entity<SecurityLoginsRolePoco>
               (entity =>
               {

                   entity.HasOne(s => s.SecurityRole).
                   WithMany(r => r.SecurityLoginsRoles).
                   HasForeignKey(r => r.Role);

               });

            //CompanyDescription

            modelBuilder.Entity<CompanyDescriptionPoco>
              (entity =>
              {
                  entity.HasOne(e => e.CompanyProfile)
                  .WithMany(p => p.CompanyDescriptions)
                  .HasForeignKey(e => e.Company);

                  entity.HasOne(l => l.SystemLanguageCode)
                  .WithMany(c => c.CompanyDescription)
                  .HasForeignKey(l => l.LanguageId);
              });

            //CompanyLocation

            modelBuilder.Entity<CompanyLocationPoco>
               (entity =>
               {
                   entity.HasOne(e => e.CompanyProfile)
                  .WithMany(p => p.CompanyLocations)
                  .HasForeignKey(e => e.Company);

               });


            base.OnModelCreating(modelBuilder);
        }
    }
}