using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinok.Data
{
    public class BaseConnection
    {
        public string GetConnection()
        {
            string cadenaConexion = "Server=S000-00;DataBase=Chinook;User=sa;Password=sql;";
            return cadenaConexion;
        }
    }
}
