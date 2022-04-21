using System.Data.SqlClient;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
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
                cmd.CommandText = @"INSERT INTO Applicant_Work_History
						           ([Id]
                                          ,[Applicant]
                                          ,[Company_Name]
                                          ,[Country_Code]
                                          ,[Location]
                                          ,[Job_Title]
                                          ,[Job_Description]
                                          ,[Start_Month]
                                          ,[Start_Year]
                                          ,[End_Month]
                                          ,[End_Year])
                                    VALUES
						           (@Id, @Applicant, @Company_Name, @Country_Code, @Location,@Job_Title, @Job_Description
                                   , @Start_Month, @Start_Year, @End_Month, @End_Year)";
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", item.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
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
                                      ,[Company_Name]
                                      ,[Country_Code]
                                      ,[Location]
                                      ,[Job_Title]
                                      ,[Job_Description]
                                      ,[Start_Month]
                                      ,[Start_Year]
                                      ,[End_Month]
                                      ,[End_Year]
                                    FROM[JOB_PORTAL_DB].[dbo].[Applicant_Work_History]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantWorkHistoryPoco[] appPocos = new ApplicantWorkHistoryPoco[1000];
                while (rdr.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.CompanyName = rdr.GetString(2);
                    poco.CountryCode = rdr.GetString(3);
                    poco.Location = rdr.GetString(4);
                    poco.JobTitle = rdr.GetString(5);
                    poco.JobDescription = rdr.GetString(6);
                    poco.StartMonth = rdr.GetInt16(7);
                    poco.StartYear = rdr.GetInt32(8);
                    poco.EndMonth = rdr.GetInt16(9);
                    poco.EndYear = rdr.GetInt32(10);
                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
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
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                       SET [Applicant] = @Applicant
                          ,[Company_Name] = @Company_Name
                          ,[Country_Code] = @Country_Code
                          ,[Location] = @Location
                          ,[Job_Title] = @Job_Title
                          ,[Job_Description] = @Job_Description
                          ,[Start_Month] = @Start_Month
                             ,[Start_Year] = @Start_Year
                             ,[End_Month] = @End_Month
                             ,[End_Year] = @End_Year
                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", item.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                    cmd.Parameters.AddWithValue("@Id", item.Id);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
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
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Work_History] WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        IList<ApplicantWorkHistoryPoco> IDataRepository<ApplicantWorkHistoryPoco>.GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        void IDataRepository<ApplicantWorkHistoryPoco>.CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}

