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
    [RoutePrefix("api/Timovi")]
    public class TimoviController : ApiController
    {
        string connectionString;
        SqlConnection db;

        public TimoviController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }

        [Route("GET")]
        [HttpGet]
        public List<Tim> Citanje()
        {
            SqlCommand command = new SqlCommand("getAllFromTimovi", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            List<Tim> tList = new List<Tim>();
            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tim tim = new Tim();
                    tim.PKTimID = Convert.ToInt32(reader[0]);
                    tim.Naziv = Convert.ToString(reader[1]);
                    tim.Broj_igraca = Convert.ToInt32(reader[2]);

                    tList.Add(tim);
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tList;
        }

        [Route("GET/{idTima}")]
        [HttpGet]
        public Tim CitanjePojedinacno(int idTima)
        {
            SqlCommand command = new SqlCommand("getTimById", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@TimId", SqlDbType.Int).Value = idTima;

            Tim tim = new Tim();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tim.PKTimID = Convert.ToInt32(reader[0]);
                    tim.Naziv = Convert.ToString(reader[1]);
                    tim.Broj_igraca = Convert.ToInt32(reader[2]);
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tim;
        }

        [Route("POST")]
        [HttpPost]
        public void Unos(Tim t)
        {

            SqlCommand command = new SqlCommand("insertIntoTimovi", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Naziv", SqlDbType.VarChar).Value = t.Naziv;
            command.Parameters.Add("@Broj", SqlDbType.Int).Value = t.Broj_igraca;

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
        public void Izmjena(Tim t)
        {
            SqlCommand command = new SqlCommand("updateToTimovi", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Naziv", SqlDbType.VarChar).Value = t.Naziv;
            command.Parameters.Add("@Broj", SqlDbType.Int).Value = t.Broj_igraca;
            command.Parameters.Add("@TimId", SqlDbType.Int).Value = t.PKTimID;
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


        [Route("DELETE/{idTima}")]
        [HttpDelete]
        public void Brisanje(int idTima)
        {
            SqlCommand command = new SqlCommand("deleteFromTimovi", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IdTima", SqlDbType.Int).Value = idTima;

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