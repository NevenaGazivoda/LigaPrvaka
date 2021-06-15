using LigaPrvaka.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LigaPrvaka.Kontroler
{
    [RoutePrefix("api/Utakmice")]
    public class UtakmiceController : ApiController
    {
        string connectionString;
        SqlConnection db;

        public UtakmiceController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }

        [Route("GET")]
        [HttpGet]
        public List<Utakmica> Citanje()
        {
            SqlCommand command = new SqlCommand("getAllFromUtakmice", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            List<Utakmica> uList = new List<Utakmica>();
            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Utakmica utakmica = new Utakmica();
                    utakmica.PKUtakmicaID = Convert.ToInt32(reader[0]);
                    utakmica.Domaci = Convert.ToInt32(reader[1]);
                    utakmica.Gosti = Convert.ToInt32(reader[2]);
                    utakmica.Br_golova_d = Convert.ToInt32(reader[3]);
                    utakmica.Br_golova_g = Convert.ToInt32(reader[4]);

                    uList.Add(utakmica);
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return uList;
        }

        [Route("GET/{idUtakmice}")]
        [HttpGet]
        public Utakmica CitanjePojedinacno(int idUtakmice)
        {
            SqlCommand command = new SqlCommand("getUtakmicaById", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@UtakmicaId", SqlDbType.Int).Value = idUtakmice;

            Utakmica utakmica = new Utakmica();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    utakmica.PKUtakmicaID = Convert.ToInt32(reader[0]);
                    utakmica.Domaci = Convert.ToInt32(reader[1]);
                    utakmica.Gosti = Convert.ToInt32(reader[2]);
                    utakmica.Br_golova_d = Convert.ToInt32(reader[3]);
                    utakmica.Br_golova_g = Convert.ToInt32(reader[4]);
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return utakmica;
        }

        [Route("POST")]
        [HttpPost]
        public void Unos(Utakmica u)
        {

            SqlCommand command = new SqlCommand("insertIntoUtakmice", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IdDomaci", SqlDbType.Int).Value = u.Domaci;
            command.Parameters.Add("@IdGosti", SqlDbType.Int).Value = u.Gosti;
            command.Parameters.Add("@Golovi_domaci", SqlDbType.Int).Value = u.Br_golova_d;
            command.Parameters.Add("@Golovi_gosti", SqlDbType.Int).Value = u.Br_golova_g;


            try
            {
                db.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            db.Close();
        }

        [Route("PUT")]
        [HttpPut]
        public void Izmjena(Utakmica u)
        {
            SqlCommand command = new SqlCommand("updateToUtakmice", db)
            {
                CommandType = CommandType.StoredProcedure
            };


            command.Parameters.Add("@IdDomaci", SqlDbType.Int).Value = u.Domaci;
            command.Parameters.Add("@IdGosti", SqlDbType.Int).Value = u.Gosti;
            command.Parameters.Add("@Golovi_domaci", SqlDbType.Int).Value = u.Br_golova_d;
            command.Parameters.Add("@Golovi_gosti", SqlDbType.Int).Value = u.Br_golova_g;
            command.Parameters.Add("@UtakmicaId", SqlDbType.Int).Value = u.PKUtakmicaID;
            try
            {
                db.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            db.Close();
        }


        [Route("DELETE/{idUtakmice}")]
        [HttpDelete]
        public void Brisanje(int idUtakmice)
        {
            SqlCommand command = new SqlCommand("deleteFromUtakmice", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IdUtakmice", SqlDbType.Int).Value = idUtakmice;

            try
            {
                db.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            db.Close();
        }
    }
}