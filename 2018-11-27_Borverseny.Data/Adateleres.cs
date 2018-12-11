using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _2018_11_27_Borverseny.Data
{
    public class Adateleres
    {
        private string conStr = "server=localhost;database=13a_borverseny;uid=root;pwd=;";
        public List<Kategoria> ListKategoriak()
        {
            List<Kategoria> lista = new List<Kategoria>();
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "SELECT Id, Megnevezes FROM Kategoria";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) lista.Add(new Kategoria(reader));
                }
            }
            return lista;
        }

        public List<Borasz> ListBoraszok(string nev)
        {
            List<Borasz> lista = new List<Borasz>();
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "SELECT Id, Nev, Telefon, Email FROM Borasz ";
                if (!string.IsNullOrEmpty(nev))
                {
                    sql += "WHERE nev LIKE @nev";
                }
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    if (!string.IsNullOrEmpty(nev))
                    {
                        cmd.Parameters.AddWithValue("@nev", "%" + nev + "%");
                    }
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) lista.Add(new Borasz(reader));
                    }
                }
            }
            return lista;
        }

        public Borasz GetBorasz(int id)
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "SELECT Id, Nev, Email, Telefon FROM Borasz WHERE Id=@Id";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return new Borasz(reader);
                        else return null;
                    }
                }
            }
        }

        public Borasz InsertBorasz(Borasz model)
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "INSERT INTO Borasz(Nev, Email, Telefon) VALUES(@Nev, @Email, @Telefon); ";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Nev", model.Nev);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Telefon", model.Telefon);
                    cmd.ExecuteNonQuery();
                    int id = (int)cmd.LastInsertedId;
                    return GetBorasz(id);
                }
            }
            
        }

        public Borasz UpdateBorasz(Borasz model)
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "UPDATE Borasz SET Nev = @Nev, Email = @Email, Telefon = @Telefon WHERE Id = @Id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Nev", model.Nev);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Telefon", model.Telefon);
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.ExecuteNonQuery();
                    return GetBorasz(model.Id);
                }
            }

        }

        public void DeleteBorasz(Borasz model)
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "DELETE FROM Borasz WHERE Id = @Id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        private string SqlCommandText(string fajlNev)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("_2018_11_27_Borverseny.Data.sql." + fajlNev))
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
        public List<Nevezes> ListNevezes(int? kategoriaid, int? evjarat)
        {
            List<Nevezes> lista = new List<Nevezes>();
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = SqlCommandText("Nevezes_Select.sql");

                if (kategoriaid != null) sql += " AND KategoriaId = @kategoriaid";
                else if (evjarat != null) sql += " AND Evjarat = @evjarat";

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@kategoriaid", kategoriaid);
                    cmd.Parameters.AddWithValue("@evjarat", evjarat);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) lista.Add(new Nevezes(reader));
                    }
                }
            }
            return lista;
        }

        public Nevezes GetNevezes(int id)
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = SqlCommandText("Nevezes_Select.sql");
                sql += " AND nevezes.id = @id";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return new Nevezes(reader);
                        else return null;
                    }
                }
            }
        }

        public Nevezes InsertNevezes(Nevezes model)
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "INSERT INTO nevezes(BoraszId, FantaziaNev, Borvidek, Evjarat, KategoriaId, Helyezes) VALUES(@BoraszId, @FantaziaNev, @Borvidek, @Evjarat, @KategoriaId, @Helyezes);";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@BoraszId", model.BoraszId);
                    cmd.Parameters.AddWithValue("@Borvidek", model.Borvidek);
                    cmd.Parameters.AddWithValue("@Evjarat", model.Evjarat);
                    cmd.Parameters.AddWithValue("@KategoriaId", model.KategoriaId);
                    cmd.Parameters.AddWithValue("@Helyezes", model.Helyezes);
                    cmd.Parameters.AddWithValue("@FantaziaNev", model.FantaziaNev);
                    cmd.ExecuteNonQuery();
                    int id = (int)cmd.LastInsertedId;
                    return GetNevezes(id);
                }
            }
        }

        public Nevezes UpdateNevezes(Nevezes model)
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "UPDATE Borasz SET BoraszId = @BoraszId, Borvidek = @Borvidek, Evjarat = @Evjarat, KategoriaId = @KategoriaId, Helyezes = @Helyezes, FantaziaNev = @FantaziaNev WHERE Id = @Id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@BoraszId", model.BoraszId);
                    cmd.Parameters.AddWithValue("@Borvidek", model.Borvidek);
                    cmd.Parameters.AddWithValue("@Evjarat", model.Evjarat);
                    cmd.Parameters.AddWithValue("@KategoriaId", model.KategoriaId);
                    cmd.Parameters.AddWithValue("@Helyezes", model.Helyezes);
                    cmd.Parameters.AddWithValue("@FantaziaNev", model.FantaziaNev);
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.ExecuteNonQuery();
                    return GetNevezes(model.Id);
                }
            }
        }

        public void DeleteNevezes(Nevezes model)
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "DELETE FROM nevezes WHERE Id = @Id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
