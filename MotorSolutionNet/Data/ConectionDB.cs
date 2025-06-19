using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MotorSolutionNet.Services;
using MotorSolutionNet.Utilities;
using MySql.Data.MySqlClient;

namespace MotorSolutionNet.Data
{
    public class ConectionDB
    {

        private readonly string connectionString = ConfigManager.GetConfigValue("DATABASE_URL");

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public bool ExecuteSentence(string sql, bool returnData)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (returnData)
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                return reader.HasRows;
                            }
                        }
                        else
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ExecuteSentence: " + ex.Message);
                return false;
            }
        }

        public bool ExecuteProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ExecuteProcedure: " + ex.Message);
                return false;
            }

        }

        public DataTable ExecuteQuery(string sql)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ExecuteQuery: " + ex.Message);
                return null;
            }
        }

        public DataTable ExecuteProcedureQuery(string procedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ExecuteProcedureQuery: " + ex.Message);
                return null;
            }
        }

    }
}