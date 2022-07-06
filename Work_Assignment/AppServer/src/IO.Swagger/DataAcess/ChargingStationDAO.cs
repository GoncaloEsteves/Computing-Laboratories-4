using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using NLog;
using IO.Swagger.Models;

namespace IO.Swagger.DataAcess
{
    public class ChargingStationDAO
    {
        public ChargingStationDAO(String con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public void Put(ChargingStation post)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;

            ChargingStationOperator chargingStationOperator = new ChargingStationOperator();
            Position position = new Position();

            try
            {
                // id, latitude, longitude, operatorName, website, status, priceByActivation, priceByMinute, priceByKwh, waitingToCharge
                
                myConn.Open();
                myTrans = myConn.BeginTransaction();

                string query =
                    "INSERT INTO ChargingStation (id, latitude, longitude, operatorName, website, status, priceByActivation, priceByMinute, priceByKwh, waitingToCharge) VALUES (@id, @latitude, @longitude, @operatorName, @website, @status, @priceByActivation, @priceByMinute, @priceByKwh, @waitingToCharge)";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("@id", post.Id);

                position = post.Location;
                myCommand.Parameters.AddWithValue("@latitude", position.Latitude);
                myCommand.Parameters.AddWithValue("@longitude", position.Longitude);

                chargingStationOperator=post.ChargingStationOperator;
                myCommand.Parameters.AddWithValue("@operatorName", chargingStationOperator.OperatorName);
                myCommand.Parameters.AddWithValue("@website", chargingStationOperator.WebsiteURL);

                myCommand.Parameters.AddWithValue("@status", post.Status);
                myCommand.Parameters.AddWithValue("@priceByActivation", post.PriceByActivation);
                myCommand.Parameters.AddWithValue("@priceByMinute", post.PriceByMinute);
                myCommand.Parameters.AddWithValue("@priceByKwh", post.PriceByKwh);
                myCommand.Parameters.AddWithValue("@waitingToCharge", post.WaitingToCharge);

                myCommand.ExecuteNonQuery();

                foreach (Connector t in post.ChargingSockets)
                {
                        
                        query = "INSERT INTO connector (id, status, powerOutput, amps, voltage, phase, type, rate, station_id) VALUES (@id, @status, @powerOutput, @amps, @voltage, @phase, @type, @rate, @station_id)";
                        myCommand = new MySqlCommand(query, myConn);
                        myCommand.Parameters.AddWithValue("@id", t.Id);
                        myCommand.Parameters.AddWithValue("@status", t.Status);
                        myCommand.Parameters.AddWithValue("@powerOutput", t.PowerKw);

                        myCommand.Parameters.AddWithValue("@amps", t.Amps);
                        myCommand.Parameters.AddWithValue("@voltage", t.Voltage);
                        myCommand.Parameters.AddWithValue("@phase", t.Phase);

                        myCommand.Parameters.AddWithValue("@type", t.ConnectorType);

                        myCommand.Parameters.AddWithValue("@rate", t.Rate);
                        myCommand.Parameters.AddWithValue("@station_id", post.Id);
                        myCommand.ExecuteNonQuery();
                        
                }

                myTrans.Commit();
                Console.WriteLine("Charging station added to database.");
                logger.Info("Put ChargingStation transaction postId:"+post.Id+ " added");
            }
            catch (Exception e)
            {
                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    
                    }
                    logger.Error(ex,"Put ChargingStation transaction postId:"+post.Id+ " not rolledback\n");
                }
                logger.Error(e,"Put ChargingStation transaction postId:"+post.Id+ " not added\n");
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
                    logger.Error(e,"Put ChargingStation connection postId:"+post.Id+ " not closed\n");
                }
            }
        }


// id, latitude, longitude, operatorName, website, status, priceByActivation, priceByMinute, priceByKwh, waitingToCharge
        public ChargingStation Get(string id)
        {
            ChargingStation post = new ChargingStation();
            Position position = new Position();
            ChargingStationOperator chargingStationOperator = new ChargingStationOperator();
            List<Connector> chargingSockets= new List<Connector>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);
             

            try
            {
                myConn.Open();
                string query = "SELECT * FROM ChargingStation WHERE id = @id";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@id", id);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    post.Id = myReader.GetString("id");
                    position.Longitude = myReader.GetDouble("longitude");
                    position.Latitude = myReader.GetDouble("latitude");
                    post.Location = position;
                    chargingStationOperator.OperatorName = myReader.GetString("operatorName");
                    chargingStationOperator.WebsiteURL = myReader.GetString("website");
                    post.ChargingStationOperator =chargingStationOperator;
                    post.Status = (ChargingStation.StatusEnum) myReader.GetInt32("status"); 
                    post.PriceByActivation= myReader.GetDouble("priceByActivation"); ;
                    post.PriceByMinute= myReader.GetDouble("priceByMinute"); ;
                    post.PriceByKwh= myReader.GetDouble("priceByKwh");
                    post.WaitingToCharge= myReader.GetInt32("waitingToCharge");
                    myReader.Close();
                    
                    query = "SELECT id FROM Connector WHERE station_id = @id";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();

                    List<string> tomadas = new List<string>();

                    while (myReader.Read())
                    {
                        tomadas.Add(myReader.GetString("id"));
                    }
                    myReader.Close();

                    foreach (string i in tomadas)
                    {
                        query = "SELECT * FROM Connector WHERE id = @id";
                        myCommand = new MySqlCommand(query, myConn);
                        myCommand.Parameters.AddWithValue("@id", i);
                        myReader = myCommand.ExecuteReader();

                        if (myReader.Read())
                        {
                            Connector t = new Connector();
                            t.Id = myReader.GetString("id");
                            t.Status =(Connector.StatusEnum) myReader.GetInt32("status");
                            t.PowerKw = myReader.GetDouble("powerOutput");
                            t.Amps = myReader.GetInt32("amps");
                            t.Voltage=myReader.GetInt32("voltage");
                            t.Phase = (Connector.PhaseEnum) myReader.GetInt32("phase");
                            t.ConnectorType = (Connector.ConnectorTypeEnum) myReader.GetInt32("type");
                            t.Rate = (Connector.RateEnum) myReader.GetInt32("rate"); 
                            chargingSockets.Add(t);
                        }
                        myReader.Close();
                        
                    }
                    post.ChargingSockets = chargingSockets;
                    logger.Info("Get ChargingStation postId:"+id+ " fetched\n");
                }
                else{
                    myReader.Close();
                    logger.Info("Get ChargingStation postId:"+id+ " searched not exist\n");
                    }
            }
            catch (Exception e)
            {
                logger.Error(e,"Get ChargingStation postId:"+post.Id+ " not fetched\n");
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
                    logger.Error(e,"Get ChargingStation connection postId:"+id+ " not closed\n");
                    throw e;
                }
            }

            return post;
        }

// id, latitude, longitude, operatorName, website, status, priceByActivation, priceByMinute, priceByKwh, waitingToCharge
        public void Update(ChargingStation posto)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;

            Position position = posto.Location;
            ChargingStationOperator chargingStationOperator = posto.ChargingStationOperator;
            

            try
            {
                myConn.Open(); // id, latitude, longitude, operatorName, website, status, priceByActivation, priceByMinute, priceByKwh, waitingToCharge
                myTrans = myConn.BeginTransaction();
                
                string query = "UPDATE ChargingStation SET id = @id,latitude  = @latitude, longitude = @longitude, operatorName = @operatorName,  website = @website, status = @status, priceByActivation = @priceByActivation, priceByMinute = @priceByMinute, priceByKwh = @priceByKwh,  waitingToCharge = @waitingToCharge WHERE id = @id";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("@id", posto.Id);
                myCommand.Parameters.AddWithValue("@latitude", position.Latitude);
                myCommand.Parameters.AddWithValue("@longitude", position.Longitude);

                myCommand.Parameters.AddWithValue("@operatorName",chargingStationOperator.OperatorName);
                myCommand.Parameters.AddWithValue("@website",chargingStationOperator.WebsiteURL);
                myCommand.Parameters.AddWithValue("@status", posto.Status);
                

                myCommand.Parameters.AddWithValue("@priceByActivation", posto.PriceByActivation);
                myCommand.Parameters.AddWithValue("@priceByMinute", posto.PriceByMinute);
                myCommand.Parameters.AddWithValue("@priceByKwh", posto.PriceByKwh);
                myCommand.Parameters.AddWithValue("@waitingToCharge", posto.WaitingToCharge);

                
                myCommand.ExecuteNonQuery();
                
                

                // eliminar tomadas antigas
                
                query = "DELETE FROM Connector Where station_id = @station_id";
                myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("@station_id", posto.Id);
                myCommand.ExecuteNonQuery();

                myCommand = new MySqlCommand(query, myConn);
                // colocar as novas
                foreach (Connector t in posto.ChargingSockets)
                {
                    query = "INSERT INTO Connector (id, status, powerOutput, amps, voltage, phase, type, rate, station_id) VALUES (@id, @status, @powerOutput, @amps, @voltage, @phase, @type, @rate, @station_id)";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", t.Id);
                    myCommand.Parameters.AddWithValue("@status", t.Status);
                    myCommand.Parameters.AddWithValue("@powerOutput", t.PowerKw);

                    myCommand.Parameters.AddWithValue("@amps", t.Amps);
                    myCommand.Parameters.AddWithValue("@voltage", t.Voltage);
                    myCommand.Parameters.AddWithValue("@phase", t.Phase);

                    myCommand.Parameters.AddWithValue("@type", t.ConnectorType);

                    myCommand.Parameters.AddWithValue("@rate", t.Rate);
                    myCommand.Parameters.AddWithValue("@station_id", posto.Id);
                    myCommand.ExecuteNonQuery();
                }
                myTrans.Commit();
                Console.WriteLine("Charging station updated in database.");
                logger.Info("Update ChargingStation Transaction postId:"+posto.Id+ " updated\n");
            }
            catch (Exception e)
            {
                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                    logger.Error(ex,"Update ChargingStation Transaction postId:"+posto.Id+ " not rolledback\n");
                }
                logger.Error(e,"Update ChargingStation Transaction, postId:"+posto.Id+ " not fetched\n");
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
                    logger.Error(e,"Update ChargingStation Connection, postId:"+posto.Id+ " not closed\n");
                    throw e;
                }
            }
        }

        public ICollection<ChargingStation> GetAll()
        {
            
            Position position = new Position();
            ChargingStationOperator chargingStationOperator = new ChargingStationOperator();
            List<Connector> chargingSockets= new List<Connector>();
            ICollection<ChargingStation> postos = new List<ChargingStation>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM ChargingStation";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    ChargingStation posto = new ChargingStation();
                    posto.Id = myReader.GetString("id");
                    position.Longitude = myReader.GetDouble("longitude");
                    position.Latitude = myReader.GetDouble("latitude");
                    posto.Location=position;

                    chargingStationOperator.OperatorName = myReader.GetString("operatorName");
                    chargingStationOperator.WebsiteURL = myReader.GetString("website");
                    posto.ChargingStationOperator = chargingStationOperator;
                    posto.Status = (ChargingStation.StatusEnum) myReader.GetInt32("status");
                    posto.PriceByActivation= myReader.GetDouble("priceByActivation"); ;
                    posto.PriceByMinute= myReader.GetDouble("priceByMinute"); ;
                    posto.PriceByKwh= myReader.GetDouble("priceByKwh");
                    posto.WaitingToCharge= myReader.GetInt32("waitingToCharge");

                    postos.Add(posto);
                }

                myReader.Close();

                foreach(ChargingStation posto in postos)
                {
                    query = "SELECT id FROM Connector WHERE station_id = @id";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", posto.Id);
                    myReader = myCommand.ExecuteReader();
                    
                    //create a list of connectors id
                    List<string> idstomada = new List<string>();
                    
                    while (myReader.Read())
                    {
                        idstomada.Add(myReader.GetString("id"));
                    }

                    myReader.Close();

                    // create a list of Connectors
                    foreach(string idtomada in idstomada)
                    {
                        query = "SELECT * FROM Connector WHERE id = @id";
                        myCommand = new MySqlCommand(query, myConn);
                        myCommand.Parameters.AddWithValue("@id", idtomada);
                        myReader = myCommand.ExecuteReader();

                        Connector t = null;

                        if (myReader.Read())
                        {
                            t = new Connector();
                            t.Id = myReader.GetString("id");
                            t.Status = (Connector.StatusEnum) myReader.GetInt32("status");
                            t.PowerKw = myReader.GetDouble("powerOutput");
                            t.Amps = myReader.GetInt32("amps");
                            t.Voltage=myReader.GetInt32("voltage");
                            t.Phase=(Connector.PhaseEnum) myReader.GetInt32("phase");
                            t.ConnectorType = (Connector.ConnectorTypeEnum) myReader.GetInt32("type");
                            t.Rate=(Connector.RateEnum) myReader.GetInt32("rate"); 
                            chargingSockets.Add(t);
                        }

                        myReader.Close();
                    }
                    //add list of charging sockets to posto/station
                    posto.ChargingSockets = chargingSockets;
                }

                myReader.Close();
                logger.Info("GetAll ChargingStation fetched\n");
            }
            catch (Exception e)
            {
                logger.Error(e,"GetAll ChargingStation not fetcheded\n");
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
                    logger.Error(e,"GetAll ChargingStation Connection not closed\n");
                    throw e;
                }
            }

            return postos;
        }

        public ICollection<ChargingStation> GetFast()
        {
            ICollection<ChargingStation> postos = new List<ChargingStation>();
            ICollection<string> fastStationIds = new List<string>();
            string fastId;
            ChargingStation cs = new ChargingStation();

            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT station_id FROM connector WHERE rate=1;";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    fastId = myReader.GetString("station_id");
                    if(!fastStationIds.Contains(fastId))
                        fastStationIds.Add(fastId);
                }
               
                myReader.Close();
                 
                foreach(string id in fastStationIds)
                {
                    cs = this.Get(id);
                    postos.Add(cs);
                
                }

                logger.Info("GetFast ChargingStation fetched\n");
            }
            catch (Exception e)
            {
                logger.Error(e,"GetFast ChargingStation not fetcheded\n");
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
                    logger.Error(e,"GetFast ChargingStation Connection not closed\n");
                    throw e;
                }
            }

            return postos;
        }

        public ICollection<ChargingStation> GetByStatus(List<string> statusString)
        {
            
            ICollection<ChargingStation> postos = new List<ChargingStation>();
            ICollection<string> statusStationIds = new List<string>();
            string statusId;
            int numeroStatus;
            ChargingStation cs = new ChargingStation();

            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();

                foreach(string sts in statusString){
                    numeroStatus = Int32.Parse(sts);
                    string query = "SELECT id FROM chargingstation WHERE status=@status;";
                    MySqlCommand myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@status", numeroStatus);
                    MySqlDataReader myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        statusId = myReader.GetString("id");
                        if(!statusStationIds.Contains(statusId))
                            statusStationIds.Add(statusId);
                    }
               
                    myReader.Close();
                 
                    foreach(string id in statusStationIds)
                    {
                       cs = this.Get(id);
                        postos.Add(cs);
                    }
                }
                logger.Info("GetStatus ChargingStation fetched\n");
            }
            catch (Exception e)
            {
                logger.Error(e,"GetStatus ChargingStation not fetcheded\n");
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
                    logger.Error(e,"GetStatus ChargingStation Connection not closed\n");
                    throw e;
                }
            }

            return postos;
        }



        public void Delete(string idSt)
        {

            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "DELETE FROM ChargingStation Where id = @idStation";
                MySqlCommand myCommand = new MySqlCommand(query, myConn,myTrans);
                myCommand.Parameters.AddWithValue("@idStation", idSt);

                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("Charging station deleted from database.");
                logger.Info("Delete ChargingStation Connection, postId:"+idSt+ " deleted\n");
            }
            catch (Exception e)
            {
                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                    logger.Error(ex,"Delete ChargingStation Transaction, postId:"+idSt+ " not rolledback\n");
                }
                logger.Error(e,"Delete ChargingStation Transaction, postId:"+idSt+ " not deleted\n");
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
                    logger.Error(e,"Delete ChargingStation Connection, postId:"+idSt+ " not closed\n");
                    throw e;
                }
            }
        }
    }
}
