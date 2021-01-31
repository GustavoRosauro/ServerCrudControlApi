using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Commom
{
    public class GenericQuerys
    {
        public static string GerarInsertQuery(object o, string tabela)
        {
            StringBuilder sb = new StringBuilder();
            var props = o.GetType().GetProperties();
            string colunas = string.Join(",", props.Where(x => x.Name != "Id").Select(x => $"{x.Name}"));
            string parametros = string.Join(",", props.Where(x => x.Name != "Id").Select(x => $"@{x.Name}"));
            sb.AppendLine($"INSERT INTO {tabela} ({colunas}) values ({parametros})");
            return sb.ToString();
        }
        public static string GerarDeleteQuery(Guid id, string tabela)
        {
            string sqlDelete = $"DELETE FROM {tabela} WHERE ID = '{id}'";
            return sqlDelete;
        }
        public static string GerarSelectQuery(object o, string tabela, string orderby)
        {
            StringBuilder sb = new StringBuilder();
            var props = o.GetType().GetProperties();
            string colunas = string.Join(",", props.Select(x => $"{x.Name}"));
            string filtros = string.Join(" ", props.Where(x => x.GetValue(o) != null && x.GetValue(o).ToString() != "00000000-0000-0000-0000-000000000000" && !string.IsNullOrEmpty(x.GetValue(o).ToString())
                && x.GetValue(o).ToString() != "0").Select(x => $" AND {x.Name} = @{x.Name}"));
            sb.AppendLine($"SELECT {colunas} FROM {tabela} WHERE 1 = 1 {filtros} {orderby}");
            return sb.ToString();
        }
    }
}
