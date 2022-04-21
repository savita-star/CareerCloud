using System.Data.SqlClient;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
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
                cmd.CommandText = @"INSERT INTO Company_Locations
						           ([Id]
                                    ,[Company]
                                    ,[Country_Code]
                                    ,[State_Province_Code]
                                    ,[Street_Address]
                                    ,[City_Town]
                                    ,[Zip_Postal_Code])
									VALUES
						           (@Id, @Company, @Country_Code, @State_Province_Code, @Street_Address,@City_Town, @Zip_Postal_Code)";
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
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
                                          ,[Company]
                                          ,[Country_Code]
                                          ,[State_Province_Code]
                                          ,[Street_Address]
                                          ,[City_Town]
                                          ,[Zip_Postal_Code]
                                    FROM[JOB_PORTAL_DB].[dbo].[Company_Locations]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyLocationPoco[] appPocos = new CompanyLocationPoco[1000];
                while (rdr.Read() && x < 1000)
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    if (!rdr.IsDBNull(0) && !rdr.IsDBNull(1) && !rdr.IsDBNull(2) && !rdr.IsDBNull(3)
                        && !rdr.IsDBNull(4) && !rdr.IsDBNull(5) && !rdr.IsDBNull(6))
                    {
                        poco.Id = rdr.GetGuid(0);
                        poco.Company = rdr.GetGuid(1);
                        poco.CountryCode = rdr.GetString(2);
                        poco.Province = rdr.GetString(3);
                        poco.Street = rdr.GetString(4);
                        poco.City = rdr.GetString(5);
                        poco.PostalCode = rdr.GetString(6);

                        appPocos[x] = poco;
                        x++;
                    }
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params CompanyLocationPoco[] items)
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
                foreach (CompanyLocationPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Locations]
                       SET [Company] = @Company
                          ,[Country_Code] = @Country_Code
                          ,[State_Province_Code] = @State_Province_Code
                          ,[Street_Address] = @Street_Address
                          ,[City_Town] = @City_Town
                          ,[Zip_Postal_Code] = @Zip_Postal_Code
                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Remove(params CompanyLocationPoco[] items)
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
                foreach (CompanyLocationPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Locations] WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        IList<CompanyLocationPoco> IDataRepository<CompanyLocationPoco>.GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        void IDataRepository<CompanyLocationPoco>.CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}

