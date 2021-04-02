using System.Collections.Generic;
using System.Data;

namespace EasyADO
{
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
}
