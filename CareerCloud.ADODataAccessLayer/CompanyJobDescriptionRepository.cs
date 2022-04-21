using System.Data.SqlClient;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{

    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
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
                cmd.CommandText = @"INSERT INTO Company_Jobs_Descriptions
						                ([Id]
                                          ,[Job]
                                          ,[Job_Name]
                                          ,[Job_Descriptions])
									VALUES
						           (@Id, @Job, @Job_Name, @Job_Descriptions)";
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);

                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
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
                                          ,[Job]
                                          ,[Job_Name]
                                          ,[Job_Descriptions]
                                    FROM[JOB_PORTAL_DB].[dbo].[Company_Jobs_Descriptions]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobDescriptionPoco[] appPocos = new CompanyJobDescriptionPoco[1000];
                while (rdr.Read() && x < 1000)
                {

                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    if (!rdr.IsDBNull(0) && !rdr.IsDBNull(1) && !rdr.IsDBNull(2) && !rdr.IsDBNull(3))
                    {
                        poco.Id = rdr.GetGuid(0);
                        poco.Job = rdr.GetGuid(1);
                        poco.JobName = rdr.GetString(2);
                        poco.JobDescriptions = rdr.GetString(3);

                        appPocos[x] = poco;
                        x++;
                    }
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
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
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                       SET [Job] = @Job
                          ,[Job_Name] = @JOb_Name
                          ,[Job_Descriptions] = @Job_Descriptions
                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
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
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions] WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        IList<CompanyJobDescriptionPoco> IDataRepository<CompanyJobDescriptionPoco>.GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        void IDataRepository<CompanyJobDescriptionPoco>.CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}

