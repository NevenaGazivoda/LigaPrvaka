using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LigaPrvaka.Models;

namespace LigaPrvaka.Kontroler
{
    [RoutePrefix("api/Igraci")]
    public class IgraciController : ApiController
    {
        string connectionString;
        SqlConnection db;

        public IgraciController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }

        [Route("GET")]
        [HttpGet]
        public List<Igrac> Citanje()
        {
            SqlCommand command = new SqlCommand("getAllFromIgraci", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            List<Igrac> iList = new List<Igrac>();
            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Igrac igrac = new Igrac();
                    igrac.PKIgracID = Convert.ToInt32(reader[0]);
                    igrac.Ime = Convert.ToString(reader[1]);
                    igrac.FKTimID = Convert.ToInt32(reader[2]);

                    iList.Add(igrac);
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return iList;
        }

        [Route("GET/{idIgraca}")]
        [HttpGet]
        public Igrac CitanjePojedinacno(int idIgraca)
        {
            SqlCommand command = new SqlCommand("getIgracById", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IgracId", SqlDbType.Int).Value = idIgraca;

            Igrac igrac = new Igrac();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    igrac.PKIgracID = Convert.ToInt32(reader[0]);
                    igrac.Ime = Convert.ToString(reader[1]);
                    igrac.FKTimID = Convert.ToInt32(reader[2]);
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return igrac;
        }

        [Route("POST")]
        [HttpPost]
        public void Unos(Igrac i)
        {

            SqlCommand command = new SqlCommand("insertIntoIgraci", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Ime", SqlDbType.VarChar).Value = i.Ime;
            command.Parameters.Add("@IdTima", SqlDbType.Int).Value = i.FKTimID;

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
        public void Izmjena(Igrac i)
        {
            SqlCommand command = new SqlCommand("updateToIgraci", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Ime", SqlDbType.VarChar).Value = i.Ime;
            command.Parameters.Add("@TimId", SqlDbType.Int).Value = i.FKTimID;
            command.Parameters.Add("@IgracId", SqlDbType.Int).Value = i.PKIgracID;
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


        [Route("DELETE/{idIgraca}")]
        [HttpDelete]
        public void Brisanje(int idIgraca)
        {
            SqlCommand command = new SqlCommand("deleteFromIgraci", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IdIgraca", SqlDbType.Int).Value = idIgraca;

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
