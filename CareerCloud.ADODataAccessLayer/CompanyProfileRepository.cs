using System.Data.SqlClient;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
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
                cmd.CommandText = @"INSERT INTO Company_Profiles
						           ([Id]
                                    ,[Registration_Date]
                                    ,[Company_Website]
                                    ,[Contact_Phone]
                                    ,[Contact_Name]
                                    ,[Company_Logo])
									VALUES
						           (@Id, @Registration_Date, @Company_Website, @Contact_Phone, @Contact_Name,@Company_Logo)";
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
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
                                          ,[Registration_Date]
                                          ,[Company_Website]
                                          ,[Contact_Phone]
                                          ,[Contact_Name]
                                          ,[Company_Logo]
                                    FROM[JOB_PORTAL_DB].[dbo].[Company_Profiles]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyProfilePoco[] appPocos = new CompanyProfilePoco[1000];
                while (rdr.Read() && x < 1000)
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    if (!rdr.IsDBNull(0) && !rdr.IsDBNull(1) && !rdr.IsDBNull(2) && !rdr.IsDBNull(3) &&
                        !rdr.IsDBNull(4) && !rdr.IsDBNull(5))
                    {
                        poco.Id = rdr.GetGuid(0);
                        poco.RegistrationDate = rdr.GetDateTime(1);
                        poco.CompanyWebsite = rdr.GetString(2);
                        poco.ContactPhone = rdr.GetString(3);
                        poco.ContactName = rdr.GetString(4);
                        poco.CompanyLogo = (byte[])rdr[5];

                        appPocos[x] = poco;
                        x++;
                    }
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params CompanyProfilePoco[] items)
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
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                       SET [Registration_Date] = @Registration_Date
                          ,[Company_Website] = @Company_Website
                          ,[Contact_Phone] = @Contact_Phone
                          ,[Contact_Name] = @Contact_Name
                          ,[Company_Logo] = @Company_Logo
                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Remove(params CompanyProfilePoco[] items)
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
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles] WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        IList<CompanyProfilePoco> IDataRepository<CompanyProfilePoco>.GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        void IDataRepository<CompanyProfilePoco>.CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }


    }
}

