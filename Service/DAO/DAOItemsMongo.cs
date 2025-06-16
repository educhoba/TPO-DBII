using Service.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Service.DAO
{
    public class DAOItemsMongo
    {
        public DAOItemsMongo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
        }
        private static string connectionString = "";
        private static SqlTransaction transaction;

        public static DataTable GetDT(string query)
        {
            var table = new DataTable();
            using (var da = new SqlDataAdapter(query, connectionString))
            {
                da.Fill(table);
            }
            return table;
        }
        //public static void StartTransaction()
        //{
        //    var table = new DataTable();
        //    using (var da = new SqlDataAdapter(query, connectionString))
        //    {
        //        da.Fill(table);
        //    }
        //    return table;
        //}
        public static string GetCS()
        {
            return connectionString;
        }
        private static void CreateCommand(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private static string getTableName()
        {
            throw new NotImplementedException();
        }

        public List<ItemMongoDTO> ReadAll()
        {
            throw new NotImplementedException();
        }
        public List<ItemMongoDTO> ReadAll(string whereCondition = "")
        {

            throw new NotImplementedException();
        }

        public ItemMongoDTO Read(int id)
        {
            throw new NotImplementedException();
        }
        public int Insert(ItemMongoDTO dto)
        {
            throw new NotImplementedException();
        }
        public void Update(ItemMongoDTO dto)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}