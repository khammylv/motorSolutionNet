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

        private readonly string connectionString = ConfigurationVar.ConectionDB;

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public bool ExecuteSentence(string sql, bool returnData)
        {
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        if (returnData)
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
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
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
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
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
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
                using (MySqlConnection connection = GetConnection())
                {
                    using (MySqlCommand command = new MySqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
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