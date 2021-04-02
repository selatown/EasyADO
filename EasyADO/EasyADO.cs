using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EasyADO
{
    public class DBInstance
    {
        private readonly string ConnectionString;

        public DBInstance(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public async Task<DataTable> ExecuteQueryForResultsAsync(string query)
        {
            using SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    await Connection.OpenAsync();
                }
                SqlCommand Command = new SqlCommand(query, Connection);
                SqlDataReader reader = await Command.ExecuteReaderAsync();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                await reader.DisposeAsync();
                await Connection.CloseAsync();
                return dataTable;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> ExecuteQueryAsync(string query)
        {
            using SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    await Connection.OpenAsync();
                }
                SqlCommand Command = new SqlCommand(query, Connection);
                int AffectedRows = await Command.ExecuteNonQueryAsync();
                await Connection.CloseAsync();
                return AffectedRows > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<DataTable> ExecuteSafeQueryForResultsAsync(string query, List<Parameter> parameters)
        {
            using SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    await Connection.OpenAsync();
                }
                SqlCommand Command = new SqlCommand(null, Connection);
                Command.CommandText = query;
                for (int i = 0; i < parameters.Count; i++)
                {
                    string ParamName = parameters[i].ParameterName;
                    SqlDbType ParamType = parameters[i].ParameterType;
                    dynamic ParamValue = parameters[i].ParameterValue;
                    Command.Parameters.Add(ParamName, ParamType).Value = ParamValue;
                }
                SqlDataReader reader = await Command.ExecuteReaderAsync();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                await reader.DisposeAsync();
                await Connection.CloseAsync();
                return dataTable;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> ExecuteSafeQueryAsync(string query, List<Parameter> parameters)
        {
            using SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    await Connection.OpenAsync();
                }
                SqlCommand Command = new SqlCommand(null, Connection);
                Command.CommandText = query;
                for (int i = 0; i < parameters.Count; i++)
                {
                    string ParamName = parameters[i].ParameterName;
                    SqlDbType ParamType = parameters[i].ParameterType;
                    dynamic ParamValue = parameters[i].ParameterValue;
                    Command.Parameters.Add(ParamName, ParamType).Value = ParamValue;
                }
                int AffectedRows = await Command.ExecuteNonQueryAsync();
                await Connection.CloseAsync();
                return AffectedRows > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> ExecuteStoredProcedureAsync(string StoredProcedureName, List<Parameter> parameters)
        {
            using SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    await Connection.OpenAsync();
                }
                SqlCommand Command = new SqlCommand(StoredProcedureName, Connection);
                Command.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parameters.Count; i++)
                {
                    string ParamName = parameters[i].ParameterName;
                    SqlDbType ParamType = parameters[i].ParameterType;
                    dynamic ParamValue = parameters[i].ParameterValue;
                    Command.Parameters.Add(ParamName, ParamType).Value = ParamValue;
                }
                int AffectedRows = await Command.ExecuteNonQueryAsync();
                await Connection.CloseAsync();
                return AffectedRows > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<DataTable> ExecuteStoredProcedureForResultsAsync(string StoredProcedureName, List<Parameter> parameters)
        {
            using SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    await Connection.OpenAsync();
                }
                SqlCommand Command = new SqlCommand(StoredProcedureName, Connection);
                Command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        string ParamName = parameters[i].ParameterName;
                        SqlDbType ParamType = parameters[i].ParameterType;
                        dynamic ParamValue = parameters[i].ParameterValue;
                        Command.Parameters.Add(ParamName, ParamType).Value = ParamValue;
                    }
                }
                SqlDataReader reader = await Command.ExecuteReaderAsync();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                await reader.DisposeAsync();
                await Connection.CloseAsync();
                return dataTable;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }

    public static class DataTableConverter
    {
        public static List<Dictionary<string, dynamic>> ToDictionary(DataTable dt)
        {
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dictionary<string, dynamic> rowData = new Dictionary<string, dynamic>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string columnName = dt.Columns[j].ColumnName.Substring(0, 1).ToLower() + dt.Columns[j].ColumnName[1..];
                    var columnValue = dt.Rows[i][columnName].ToString().Equals(string.Empty) ? "" : dt.Rows[i][columnName];
                    rowData.Add(columnName, columnValue);
                }
                result.Add(rowData);
            }
            return result;
        }
    }

    public static class MonthConverter
    {
        public static string ToText(int monthIndex)
        {
            return monthIndex switch
            {
                1 => "January",
                2 => "Feburary",
                3 => "March",
                4 => "April",
                5 => "May",
                6 => "June",
                7 => "July",
                8 => "August",
                9 => "September",
                10 => "October",
                11 => "November",
                12 => "December",
                _ => "Invalid Month",
            };
        }
        public static string ToKhmerText(int monthIndex)
        {
            return monthIndex switch
            {
                1 => "មករា",
                2 => "កម្ភៈ",
                3 => "មីនា",
                4 => "មេសា",
                5 => "ឧសភា",
                6 => "មិថុនា",
                7 => "កក្កដា",
                8 => "សីហា",
                9 => "កញ្ញា",
                10 => "តុលា",
                11 => "វិច្ឆិកា",
                12 => "ធ្នូ",
                _ => "ខែមិនត្រឹមត្រូវ",
            };
        }
    }

    public static class SQlServerConnection
    {
        public static string GetConnectionString(string ServerName, string DatabaseName, string UserName, string Password)
        {
            return $"Server={ServerName};Database={DatabaseName};User Id={UserName};Password={Password}";
        }
    }
}
