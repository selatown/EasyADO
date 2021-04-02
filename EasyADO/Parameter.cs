using System.Data;
namespace EasyADO
{
    public class Parameter
    {
        public string ParameterName { get; set; }
        public SqlDbType ParameterType { get; set; }
        public dynamic ParameterValue { get; set; }
    }
}
