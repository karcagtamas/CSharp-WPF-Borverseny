using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_11_27_Borverseny.Data
{
    public class Kategoria
    {
        public int? Id { get; set; }
        public string Megnevezes { get; set; }

        public Kategoria()
        {

        }
        public Kategoria(MySqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["Id"]);
            Megnevezes = reader["Megnevezes"].ToString();
        }
    }
}
