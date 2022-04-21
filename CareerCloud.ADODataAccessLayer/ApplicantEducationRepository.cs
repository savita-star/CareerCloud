using System.Data.SqlClient;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            var _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"INSERT INTO Applicant_Educations
						           (Id
						           ,Applicant
						           ,Major
						           ,Certificate_Diploma
						           ,Start_Date
						           ,Completion_Date
						           ,Completion_Percent)
									VALUES
						           (@Id, @Applicant, @Major, @Certificate_Diploma, @Start_Date,@Completion_Date, @Completion_Percent)";
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            var _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Id]
                                  ,[Applicant]
                                  ,[Major]
                                  ,[Certificate_Diploma]
                                  ,[Start_Date]
                                  ,[Completion_Date]
                                  ,[Completion_Percent]
                                    FROM[JOB_PORTAL_DB].[dbo].[Applicant_Educations]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantEducationPoco[] appPocos = new ApplicantEducationPoco[1000];
                while (rdr.Read() && x < 1000)
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    if (!rdr.IsDBNull(0) && !rdr.IsDBNull(1) && !rdr.IsDBNull(2) && !rdr.IsDBNull(3)
                        && !rdr.IsDBNull(4) && !rdr.IsDBNull(5) && !rdr.IsDBNull(6))
                    {
                        poco.Id = rdr.GetGuid(0);
                        poco.Applicant = rdr.GetGuid(1);
                        poco.Major = rdr.GetString(2);
                        poco.CertificateDiploma = rdr.GetString(3);
                        poco.StartDate = rdr.GetDateTime(4);
                        poco.CompletionDate = rdr.GetDateTime(5);
                        poco.CompletionPercent = (byte?)rdr[6];

                        appPocos[x] = poco;
                        x++;
                    }
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            var _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantEducationPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations]
                       SET [Applicant] = @Applicant
                          ,[Major] = @Major
                          ,[Certificate_Diploma] = @Certificate_Diploma
                          ,[Start_Date] = @Start_Date
                          ,[Completion_Date] = @Completion_Date
                          ,[Completion_Percent] = @Completion_Percent
                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            var _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantEducationPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Educations] WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        IList<ApplicantEducationPoco> IDataRepository<ApplicantEducationPoco>.GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        void IDataRepository<ApplicantEducationPoco>.CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}
