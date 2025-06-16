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
    public class DAOBase<T> where T : DTOBase, new()
    {
        public DAOBase()
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
            return typeof(T).Name.Replace("DTO", "");
        }

        public List<T> ReadAll()
        {
            return ReadAll("");
        }
        public List<T> ReadAll(string whereCondition = "")
        {
            var retList = new List<T>();
            Type dtoType = typeof(T);
            PropertyInfo[] dtoProps = dtoType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string queryString = $"SELECT * FROM {getTableName()}";
            if (whereCondition != "")
                queryString += $" WHERE {whereCondition}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object dto = Activator.CreateInstance(dtoType);

                        foreach (PropertyInfo P in dtoProps)
                        {
                            if (P.CanWrite)
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    if (P.Name.ToUpper() == reader.GetName(i).ToUpper())
                                    {
                                        P.SetValue(dto, reader[i]);
                                    }
                                }
                            }
                        }
                        retList.Add((T)dto);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            return retList;
        }

        public List<T> ReadAll(SqlTransaction transaction, string whereCondition = "")
        {
            var retList = new List<T>();
            Type dtoType = typeof(T);
            PropertyInfo[] dtoProps = dtoType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string queryString = $"SELECT * FROM {getTableName()}";
            if (whereCondition != "")
                queryString += $" WHERE {whereCondition}";

            SqlCommand command = new SqlCommand(queryString, transaction.Connection);
            command.Transaction = transaction;
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    object dto = Activator.CreateInstance(dtoType);

                    foreach (PropertyInfo P in dtoProps)
                    {
                        if (P.CanWrite)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (P.Name.ToUpper() == reader.GetName(i).ToUpper())
                                {
                                    P.SetValue(dto, reader[i]);
                                }
                            }
                        }
                    }
                    retList.Add((T)dto);
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            return retList;
        }

        public T Read(int id)
        {
            return ReadAll($"{nameof(DTOBase.id)} = {id}").FirstOrDefault();
        }
        public int Insert(T dto)
        {
            var type = dto.GetType();
            var dtoProps = type.GetProperties();

            using (SqlConnection connection = transaction != null ? transaction.Connection : new SqlConnection(connectionString))
            {
                string cols = string.Empty;
                string vals = string.Empty;
                bool first = true;
                foreach (PropertyInfo P in dtoProps)
                {
                    if (CheckProp(P))
                    {
                        if (!first)
                        {
                            cols += ",";
                            vals += ",";
                        }
                        else
                            first = false;
                        cols += P.Name;
                        vals += "@" + P.Name;
                    }
                }

                string query = $"INSERT INTO dbo.{getTableName()} ({cols}) OUTPUT INSERTED.{nameof(DTOBase.id)} VALUES ({vals}) ";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }
                    else
                        connection.Open();

                    foreach (PropertyInfo P in dtoProps)
                    {
                        if (CheckProp(P))
                        {
                            command.Parameters.AddWithValue("@" + P.Name, P.GetValue(dto));
                        }
                    }
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }

        }
        public int Insert(T dto, SqlTransaction transaction)
        {
            var type = dto.GetType();
            var dtoProps = type.GetProperties();


            string cols = string.Empty;
            string vals = string.Empty;
            bool first = true;
            foreach (PropertyInfo P in dtoProps)
            {
                if (CheckProp(P))
                {
                    if (!first)
                    {
                        cols += ",";
                        vals += ",";
                    }
                    else
                        first = false;
                    cols += P.Name;
                    vals += "@" + P.Name;
                }
            }

            string query = $"INSERT INTO dbo.{getTableName()} ({cols}) OUTPUT INSERTED.{nameof(DTOBase.id)} VALUES ({vals}) ";


            using (SqlCommand command = new SqlCommand(query, transaction.Connection))
            {
                command.Transaction = transaction;

                foreach (PropertyInfo P in dtoProps)
                {
                    if (CheckProp(P))
                    {
                        var check = P.GetValue(dto) ?? DBNull.Value;
                        command.Parameters.AddWithValue("@" + P.Name, P.GetValue(dto) ?? DBNull.Value);
                    }
                }
                return Convert.ToInt32(command.ExecuteScalar());
            }

        }
        public void Update(T dto)
        {
            var type = dto.GetType();
            var dtoProps = type.GetProperties();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string vals = string.Empty;
                bool first = true;
                int id = -1;
                foreach (PropertyInfo P in dtoProps)
                {
                    if (CheckProp(P))
                    {
                        if (!first)
                        {
                            vals += ",";
                        }
                        else
                            first = false;
                        vals += P.Name;
                        vals += "=@" + P.Name;
                    }
                    if (P.Name == nameof(DTOBase.id))
                        id = (int)P.GetValue(dto);
                }

                string query = $"UPDATE dbo.{getTableName()} SET {vals} WHERE {nameof(DTOBase.id)} = {id}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    foreach (PropertyInfo P in dtoProps)
                    {
                        if (CheckProp(P))
                        {
                            command.Parameters.AddWithValue("@" + P.Name, P.GetValue(dto));
                        }
                    }
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }
        }

        public void Update(T dto, SqlTransaction transaction)
        {
            var type = dto.GetType();
            var dtoProps = type.GetProperties();

            string vals = string.Empty;
            bool first = true;
            int id = -1;
            foreach (PropertyInfo P in dtoProps)
            {
                if (CheckProp(P))
                {
                    if (!first)
                    {
                        vals += ",";
                    }
                    else
                        first = false;
                    vals += P.Name;
                    vals += "=@" + P.Name;
                }
                if (P.Name == nameof(DTOBase.id))
                    id = (int)P.GetValue(dto);
            }

            string query = $"UPDATE dbo.{getTableName()} SET {vals} WHERE {nameof(DTOBase.id)} = {id}";
            using (SqlCommand command = new SqlCommand(query, transaction.Connection))
            {
                command.Transaction = transaction;

                foreach (PropertyInfo P in dtoProps)
                {
                    if (CheckProp(P))
                    {
                        command.Parameters.AddWithValue("@" + P.Name, P.GetValue(dto));
                    }
                }
                int result = command.ExecuteNonQuery();

                if (result < 0)
                    Console.WriteLine("Error inserting data into Database!");
            }
        }
        public void Delete(int id)
        {
            var retList = new List<T>();
            Type dtoType = typeof(T);
            PropertyInfo[] dtoProps = dtoType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string queryString = $"DELETE FROM {getTableName()} WHERE {$"{nameof(DTOBase.id)} = {id}"}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error deleting data from Database!");
                }
            }
        }
        public void Delete(int id, SqlTransaction transaction)
        {
            var retList = new List<T>();
            Type dtoType = typeof(T);
            PropertyInfo[] dtoProps = dtoType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string queryString = $"DELETE FROM {getTableName()} WHERE {$"{nameof(DTOBase.id)} = {id}"}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, transaction.Connection))
                {
                    command.Transaction = transaction;

                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error deleting data from Database!");
                }
            }
        }
        private bool CheckProp(PropertyInfo prop)
        {
            return prop.Name != nameof(DTOBase.id) && prop.CanWrite;
        }
        public List<T> ReadQuery(string query = "")
        {
            var retList = new List<T>();
            Type dtoType = typeof(T);
            PropertyInfo[] dtoProps = dtoType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string queryString = query;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object dto = Activator.CreateInstance(dtoType);

                        foreach (PropertyInfo P in dtoProps)
                        {
                            if (P.CanWrite)
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    if (P.Name.ToUpper() == reader.GetName(i).ToUpper())
                                    {
                                        P.SetValue(dto, reader[i]);
                                    }
                                }
                            }
                        }
                        retList.Add((T)dto);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            return retList;
        }

    }

}