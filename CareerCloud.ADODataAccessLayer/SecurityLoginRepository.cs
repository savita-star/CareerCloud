using System.Data.SqlClient;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
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
                cmd.CommandText = @"INSERT INTO Security_Logins
						                  ([Id]
                                          ,[Login]
                                          ,[Password]
                                          ,[Created_Date]
                                          ,[Password_Update_Date]
                                          ,[Agreement_Accepted_Date]
                                          ,[Is_Locked]
                                          ,[Is_Inactive]
                                          ,[Email_Address]
                                          ,[Phone_Number]
                                          ,[Full_Name]
                                          ,[Force_Change_Password]
                                          ,[Prefferred_Language])
                                    VALUES
						           (@Id, @Login, @Password, @Created_Date, 
                                    @Password_Update_Date, @Agreement_Accepted_Date, 
                                    @Is_Locked, @Is_Inactive, @Email_Address, @Phone_Number, @Full_Name,
                                    @Force_Change_Password, @Prefferred_Language)";
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
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
                                              ,[Login]
                                              ,[Password]
                                              ,[Created_Date]
                                              ,[Password_Update_Date]
                                              ,[Agreement_Accepted_Date]
                                              ,[Is_Locked]
                                              ,[Is_Inactive]
                                              ,[Email_Address]
                                              ,[Phone_Number]
                                              ,[Full_Name]
                                              ,[Force_Change_Password]
                                              ,[Prefferred_Language]
                                    FROM[JOB_PORTAL_DB].[dbo].[Security_Logins]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityLoginPoco[] appPocos = new SecurityLoginPoco[1000];
                while (rdr.Read() && x < 1000)
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    if (!rdr.IsDBNull(0) && !rdr.IsDBNull(1) && !rdr.IsDBNull(2) && !rdr.IsDBNull(3) &&
                        !rdr.IsDBNull(4) && !rdr.IsDBNull(5) && !rdr.IsDBNull(6) && !rdr.IsDBNull(7) &&
                        !rdr.IsDBNull(8) && !rdr.IsDBNull(9) && !rdr.IsDBNull(10) && !rdr.IsDBNull(11)
                        && !rdr.IsDBNull(12))
                    {
                        poco.Id = rdr.GetGuid(0);
                        poco.Login = rdr.GetString(1);
                        poco.Password = rdr.GetString(2);
                        poco.Created = rdr.GetDateTime(3);
                        poco.PasswordUpdate = rdr.GetDateTime(4);
                        poco.AgreementAccepted = rdr.GetDateTime(5);
                        poco.IsLocked = rdr.GetBoolean(6);
                        poco.IsInactive = rdr.GetBoolean(7);
                        poco.EmailAddress = rdr.GetString(8);
                        poco.PhoneNumber = rdr.GetString(9);
                        poco.FullName = rdr.GetString(10);
                        poco.ForceChangePassword = rdr.GetBoolean(11);
                        poco.PrefferredLanguage = rdr.GetString(12);
                        appPocos[x] = poco;
                        x++;
                    }
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params SecurityLoginPoco[] items)
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
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins]
                       SET [Login] = @Login
                          ,[Password] = @Password
                          ,[Created_Date] = @Created_Date
                          ,[Password_Update_Date] = @Password_Update_Date
                          ,[Agreement_Accepted_Date] = @Agreement_Accepted_Date
                          ,[Is_Locked] = @Is_Locked
                          ,[Is_Inactive] = @Is_Inactive
                          ,[Email_Address] = @Email_Address
                          ,[Phone_Number] = @Phone_Number
                          ,[Full_Name] = @Full_Name
                          ,[Force_Change_Password] = @Force_Change_Password
                          ,[Prefferred_Language] = @Prefferred_Language
                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Remove(params SecurityLoginPoco[] items)
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
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins] WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        IList<SecurityLoginPoco> IDataRepository<SecurityLoginPoco>.GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        void IDataRepository<SecurityLoginPoco>.CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}
