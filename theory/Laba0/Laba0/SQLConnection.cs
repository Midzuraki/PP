using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace Laba0
{
    public class SQLConnection
    {
        string ConKey;
        System.Data.SqlClient.SqlConnection connection;
        public string DestinationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Catalog";
        public SQLConnection(string ConnectionKey)
        {
            this.ConKey = ConnectionKey;
        }
        public bool CheckForInit()
        {
            if (this.connection == null)
            {
                throw new Exception("Connection not initialized!");
            }
            else
            {
                return true;
            }
        }
        public void Init()
        {
            this.connection = new System.Data.SqlClient.SqlConnection(this.ConKey);
        }
        public async Task InitAsync()
        {
            await Task.Run( () => Init());
        }
        public IEnumerable<string> GetString(string sql)
        {
            using (var conn = this.connection)
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                while (conn.State == ConnectionState.Open)
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    yield return read.GetString(0);
                }
            }
        }
        public IEnumerable<CatalogCard> GetProduct(string sql)
        {
            using (var conn = this.connection)
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                while (conn.State == ConnectionState.Open)
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    using (read)
                    {
                        while (true)
                        {
                            if (read.Read() == false)
                            {
                                break;
                            }
                            CatalogCard card = new CatalogCard()
                            {
                                id = read.GetInt32(0),
                                name = read.GetString(1),
                                description = read.GetString(2),
                                cal = read.GetDouble(3),
                                img = read.GetString(4),
                                price = read.GetDouble(5),
                                stock = read.GetInt32(6),
                            };
                            yield return card;
                        }
                    }
                }
            }
        }
        public async Task<IEnumerable<CatalogCard>> GetProductAsync(string sql)
        {
            return await Task.Run( () => GetProductAsync(sql));
        }
        //public async Task<IEnumerable<CatalogCard>> GetProductAsync(string sql)
        //{
        //    using (var conn = this.connection)
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        while (conn.State == ConnectionState.Open)
        //        {
        //            SqlDataReader read = cmd.ExecuteReader();
        //            using (read)
        //            {
        //                while (true)
        //                {
        //                    if (read.Read() == false)
        //                    {
        //                        break;
        //                    }
        //                    CatalogCard card = new CatalogCard()
        //                    {
        //                        id = read.GetInt32(0),
        //                        name = read.GetString(1),
        //                        description = read.GetString(2),
        //                        cal = read.GetDouble(3),
        //                        img = read.GetString(4),
        //                        price = read.GetDouble(5),
        //                        stock = read.GetInt32(6),
        //                    };
        //                    yield return card;
        //                }
        //            }
        //        }
        //    }
        //}

    }
}
