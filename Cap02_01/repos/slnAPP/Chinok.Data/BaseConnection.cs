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
            //string cadenaConexion = "Server= localhost; Database= Chinook; Integrated Security=True;";
            //string cadenaConexion = "Server=LENOVO-PC,Authentication=Windows Authentication, Database=Chinook";
            //string cadenaConexion = "Server=LENOVO-PC;DataBase=Chinook;User=;Password=";
            string cadenaConexion = "Server=S000-00;DataBase=Chinook;User=sa;Password=sql;";
            return cadenaConexion;
        }
    }
}
