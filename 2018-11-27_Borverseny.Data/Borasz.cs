using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_11_27_Borverseny.Data
{
    public class Borasz
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        public Borasz()
        { }

        public Borasz(MySqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["Id"]);
            Nev = reader["Nev"].ToString();
            Email = reader["Email"].ToString();
            Telefon = reader["Telefon"].ToString();
        }
    }
}
