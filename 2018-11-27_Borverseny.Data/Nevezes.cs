using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_11_27_Borverseny.Data
{
    public class Nevezes
    {
        public int Id { get; set; }
        public int BoraszId { get; set; }
        public string FantaziaNev { get; set; }
        public int Evjarat { get; set; }
        public string Borvidek { get; set; }
        public int KategoriaId { get; set; }
        public int? Helyezes { get; set; }
        public string KategoriaNev { get; set; }

        public Nevezes()
        { }

        public Nevezes(MySqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["Id"]);
            BoraszId = Convert.ToInt32(reader["BoraszId"]);
            FantaziaNev = reader["FantaziaNev"].ToString();
            Evjarat = Convert.ToInt32(reader["Evjarat"]);
            Borvidek = reader["Borvidek"].ToString();
            KategoriaId = Convert.ToInt32(reader["KategoriaId"]);
            Helyezes = reader["Helyezes"] == DBNull.Value? null : (int?)Convert.ToInt32(reader["Helyezes"]);
            KategoriaNev = reader["KategoriaNev"].ToString();
        }
    }
}
