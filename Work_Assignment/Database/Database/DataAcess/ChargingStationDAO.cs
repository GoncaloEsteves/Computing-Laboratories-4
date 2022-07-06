using Data.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataAcess
{
    public class ChargingStation
    {
        public ChargingStation(String con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        public void Put(Classes.ChargingStation post)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "INSERT INTO Posto (id, longitude, latitude, currentStatus, priceByActivation, priceByMinute, priceByKwh,  waitingToCharge, chargingSockets) VALUES (@id, @long, @lat, @currStats, @priceByAct, @priceByMin, @priceByKwh,  @waitChrg, @chargScks)";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@id", post.Id);
                myCommand.Parameters.AddWithValue("@long", post.Location.Longitude);
                myCommand.Parameters.AddWithValue("@lat", post.Location.Latitude);
                myCommand.Parameters.AddWithValue("@currStats", post.CurrentStatus);
                myCommand.Parameters.AddWithValue("@priceByAct", post.PriceByActivation);
                myCommand.Parameters.AddWithValue("@priceByMin", post.PriceByMinute);
                myCommand.Parameters.AddWithValue("@priceByKwh", post.PriceByKwh);
                myCommand.Parameters.AddWithValue("@waitChrg", post.WaitingToCharge);
                
                myCommand.ExecuteNonQuery();

                foreach (EV_Connector t in post.ChargingSockets)
                {
                    query = "INSERT INTO EV_Connector (id, status, powerOutput, type) VALUES (@id, @status, @pow, @type)";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", t.Id);
                    myCommand.Parameters.AddWithValue("@status", t.Status);
                    myCommand.Parameters.AddWithValue("@pow", t.PowerOutput);
                    myCommand.Parameters.AddWithValue("@type", t.Type);
                    myCommand.ExecuteNonQuery();

                    
                    query = "INSERT INTO EV_ConnectorChargingPost (ChargingStation_id, EV_Connector_id) VALUES (@idp, @idt)";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@idp", post.Id);
                    myCommand.Parameters.AddWithValue("@idt", t.Id);
                    myCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    myConn.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public Classes.ChargingStation Get(int id)
        {
            Classes.ChargingStation post = new Classes.ChargingStation();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM Posto WHERE id = @id";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@id", id);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    post.Id = myReader.GetInt32("id");
                    post.Location.Longitude = myReader.GetDouble("longitude");
                    post.Location.Latitude = myReader.GetDouble("latitude");

                    post.CurrentStatus = myReader.GetString("currStats");
                    post.PriceByActivation= myReader.GetDouble("priceByActivation"); ;
                    post.PriceByMinute= myReader.GetDouble("priceByMinute"); ;
                    post.PriceByKwh= myReader.GetDouble("priceByKwh");

                    post.WaitingToCharge= myReader.GetInt32("waitingToCharge"); ;
                    post.PriceByActivation= myReader.GetDouble("longitude"); ;

                    
                    myReader.Close();

                    query = "SELECT EV_Connector_id FROM EV_ConnectorChargingPost WHERE ChargingStation_id = @id";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();

                    List<string> tomadas = new List<string>();

                    while (myReader.Read())
                    {
                        tomadas.Add(myReader.GetInt32("EV_Connector_id"));
                    }
                    myReader.Close();

                    foreach (string i in tomadas)
                    {
                        query = "SELECT * FROM EV_Connector WHERE id = @id";
                        myCommand = new MySqlCommand(query, myConn);
                        myCommand.Parameters.AddWithValue("@id", i);
                        myReader = myCommand.ExecuteReader();

                        if (myReader.Read())
                        {
                            EV_Connector t = new EV_Connector();
                            t.Id = myReader.GetInt32("idTomada");
                            t.Status = myReader.GetInt32("estadoTomada");
                            t.PowerOutput = myReader.GetInt32("potencia");
                            t.Type = myReader.GetString("tipo");
                            post.Tomadas.Add(t);
                        }
                        myReader.Close();
                    }

                }
                else
                    myReader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    myConn.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return post;
        }

        // id, longitude, latitude, currentStatus, priceByActivation, priceByMinute, priceByKwh,  waitingToCharge, chargingSockets) VALUES (@id, @long, @lat, @currStats, @priceByAct, @priceByMin, @priceByKwh,  @waitChrg, @chargScks)";
        public void Update(Classes.ChargingStation posto)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "UPDATE ChargingStation SET id = @id,longitude = @long, latitude = @lat, currentStatus = @currStats, priceByActivation = @priceByAct, priceByMinute = @priceByMin, priceByKwh = @priceByKwh,  waitingToCharge = @waitChrg, chargingSockets = @chargScks  WHERE id = @id";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@long", posto.Location.Longitude);
                myCommand.Parameters.AddWithValue("@lat", posto.Location.Latitude);

                myCommand.Parameters.AddWithValue("@currStats" = posto.CurrentStatus);

                myCommand.Parameters.AddWithValue("@priceByAct", posto.PriceByActivation); ;
                myCommand.Parameters.AddWithValue("@priceByMin", posto.PriceByMinute);
                myCommand.Parameters.AddWithValue("@priceByKwh", posto.PriceByKwh);
                myCommand.Parameters.AddWithValue("@waitChrg", posto.WaitingToCharge);

                myCommand.Parameters.AddWithValue("@chargScks", posto.ChargingSockets);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    myConn.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public ICollection<Classes.ChargingStation> GetAll()
        {
            ICollection<Classes.ChargingStation> postos = new List<Classes.ChargingStation>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM ChargingStation";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Classes.ChargingStation posto = new Classes.ChargingStation();
                    posto.Id = myReader.GetInt32("id");
                    posto.Position.Longitude = myReader.GetDouble("longitude");
                    posto.Position.Latitude = myReader.GetDouble("latitude");
                    
                    posto.CurrentStatus = myReader.GetInt32("currentStatus");


                    postos.Add(posto);
                }

                myReader.Close();

                foreach(Classes.ChargingStation posto in postos)
                {
                    query = "SELECT EV_Connector_id FROM EV_ConectorChargingPost WHERE ChargingPost_id = @id";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", posto.Id);
                    myReader = myCommand.ExecuteReader();
                    List<string> idstomada = new List<string>();

                    while (myReader.Read())
                    {
                        idstomada.Add(myReader.GetInt32("EV_Conector_id"));
                    }

                    myReader.Close();

                    foreach(string idtomada in idstomada)
                    {
                        query = "SELECT * FROM EV_Connector WHERE id = @id";
                        myCommand = new MySqlCommand(query, myConn);
                        myCommand.Parameters.AddWithValue("@id", idtomada);
                        myReader = myCommand.ExecuteReader();

                        EV_Connector t = null;

                        if (myReader.Read())
                        {
                            t = new EV_Connector();
                            t.Id= myReader.GetInt32("id");
                            t.Status = myReader.GetInt32("status");
                            t.PowerOutput = myReader.GetInt32("powerOutput");
                            t.Type = myReader.GetString("type");
                        }

                        myReader.Close();
                        /* desnecessari agora com a nova classe de EV_Connector/tomada
                        if(t != null)
                        {
                            query = "SELECT Utilizador_email FROM FilaEsperaTomadas WHERE EV_Connector_id = @id";
                            myCommand = new MySqlCommand(query, myConn);
                            myCommand.Parameters.AddWithValue("@id", t.IdTomada);
                            myReader = myCommand.ExecuteReader();

                            while (myReader.Read())
                            {
                                t.FilaEspera.Add(myReader.GetString("Utilizador_email"));
                            }

                            myReader.Close();

                            posto.Tomadas.Add(t);
                        }*/
                    }
                }

                myReader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    myConn.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return postos;
        }
    }
}
