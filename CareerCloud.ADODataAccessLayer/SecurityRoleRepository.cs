using System.Data.SqlClient;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
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
                cmd.CommandText = @"INSERT INTO Security_Roles
						           ([Id]
                                    ,[Role]
                                    ,[Is_Inactive])
									VALUES
						           (@Id, @Role, @Is_Inactive)";
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
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
                                          ,[Role]
                                          ,[Is_Inactive]
                                    FROM[JOB_PORTAL_DB].[dbo].[Security_Roles]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityRolePoco[] appPocos = new SecurityRolePoco[1000];
                while (rdr.Read() && x < 1000)
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Role = rdr.GetString(1);
                    poco.IsInactive = rdr.GetBoolean(2);

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params SecurityRolePoco[] items)
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
                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Roles]
                       SET [Role] = @Role
                          ,[Is_Inactive] = @Is_Inactive
                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Remove(params SecurityRolePoco[] items)
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
                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Roles] WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        IList<SecurityRolePoco> IDataRepository<SecurityRolePoco>.GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        void IDataRepository<SecurityRolePoco>.CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}
