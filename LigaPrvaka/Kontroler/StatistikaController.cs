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
    [RoutePrefix("api/Statistika")]
    public class StatistikaController : ApiController
    {
        string connectionString;
        SqlConnection db;

        public StatistikaController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }

        [Route("GET")]
        [HttpGet]
        public List<Statistika> Citanje()
        {
            SqlCommand command = new SqlCommand("getAllFromStatistika", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            List<Statistika> sList = new List<Statistika>();
            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Statistika stat = new Statistika();
                    stat.FKIgracID = Convert.ToInt32(reader[0]);
                    stat.FKUtakmicaID = Convert.ToInt32(reader[1]);
                    stat.Br_golova = Convert.ToInt32(reader[2]);

                    sList.Add(stat);
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sList;
        }


        [Route("GET/{idIgraca}/{idUtakmice}")]
        [HttpGet]
        public Statistika CitanjePojedinacno(int idIgraca,int idUtakmice)
        {
            SqlCommand command = new SqlCommand("getStatistikaById", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IgracId", SqlDbType.Int).Value = idIgraca;
            command.Parameters.Add("@UtakmicaId", SqlDbType.Int).Value = idUtakmice;

            Statistika stat = new Statistika();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    stat.FKIgracID = Convert.ToInt32(reader[0]);
                    stat.FKUtakmicaID = Convert.ToInt32(reader[1]);
                    stat.Br_golova = Convert.ToInt32(reader[2]);
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return stat;
        }

        [Route("POST")]
        [HttpPost]
        public void Unos(Statistika s)
        {

            SqlCommand command = new SqlCommand("insertIntoStatistika", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IgracId", SqlDbType.Int).Value = s.FKIgracID;
            command.Parameters.Add("@UtakmicaId", SqlDbType.Int).Value = s.FKUtakmicaID;
            command.Parameters.Add("@Golovi", SqlDbType.Int).Value = s.Br_golova;
            
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

        [Route("PUT/{idIgraca}/{idUtakmice}")]
        [HttpPut]
        public void Izmjena(Statistika s)
        {
            SqlCommand command = new SqlCommand("updateToStatistika", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IgracId", SqlDbType.Int).Value = s.FKIgracID;
            command.Parameters.Add("@UtakmicaId", SqlDbType.Int).Value = s.FKUtakmicaID;
            command.Parameters.Add("@Golovi", SqlDbType.Int).Value = s.Br_golova;


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

        [Route("DELETE/{idIgraca}/{idUtakmice}")]
        [HttpDelete]
        public void Brisanje(int idIgraca, int idUtakmice)
        {
            SqlCommand command = new SqlCommand("deleteFromStatistika", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IgracId", SqlDbType.Int).Value = idIgraca;
            command.Parameters.Add("@UtakmicaId", SqlDbType.Int).Value = idUtakmice;

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