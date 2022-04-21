using System.Data.SqlClient;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
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
                cmd.CommandText = @"INSERT INTO System_Language_Codes
						           ([LanguageID]
                                      ,[Name]
                                      ,[Native_Name])
									VALUES
						           (@LanguageID, @Name, @Native_Name)";
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);

                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
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
                cmd.CommandText = @"Select[LanguageID]
                                              ,[Name]
                                              ,[Native_Name]
                                    FROM[JOB_PORTAL_DB].[dbo].[System_Language_Codes]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SystemLanguageCodePoco[] appPocos = new SystemLanguageCodePoco[1000];
                while (rdr.Read() && x < 1000)
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = rdr.GetString(0);
                    poco.Name = rdr.GetString(1);
                    poco.NativeName = rdr.GetString(2);

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
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
                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                       SET [LanguageID] = @LanguageID
                          ,[Name] = @Name
                           ,[Native_Name] = @Native_Name                 
                       WHERE [LanguageID] = @LanguageID";

                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Remove(params SystemLanguageCodePoco[] items)
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
                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[System_Language_Codes] WHERE [LanguageID] = @Code";
                    cmd.Parameters.AddWithValue("@Code", item.LanguageID);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        IList<SystemLanguageCodePoco> IDataRepository<SystemLanguageCodePoco>.GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        void IDataRepository<SystemLanguageCodePoco>.CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}


