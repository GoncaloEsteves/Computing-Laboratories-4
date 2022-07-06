using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using IO.Swagger.Models;
using NLog;

namespace IO.Swagger.DataAcess
{
    public class TripDAO
    {
        public TripDAO(string con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public void Put(String username, Trip newtrip)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            Position positionOrigin= new Position();
            Position positionDestiny = new Position();
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "INSERT INTO Trip (name, vehicleRegistrationNumber, localStartLatitude, localStartLongitude, localEndLatitude, localEndLongitude, date, duration, cost, username) VALUES (@name, @vehicleReg, @startlat, @startlong, @endlat, @endlong, @date,@duration, @cost, @username)";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);

                //myCommand.Parameters.AddWithValue("@tripId", newtrip.TripId);
                myCommand.Parameters.AddWithValue("@name", newtrip.TripName);
                myCommand.Parameters.AddWithValue("@vehicleReg", newtrip.VehicleRegistrationNumber);
                positionOrigin=newtrip.Origin;
                myCommand.Parameters.AddWithValue("@startlat", positionOrigin.Latitude);
                myCommand.Parameters.AddWithValue("@startlong", positionOrigin.Longitude);
                positionDestiny=newtrip.Destination;
                myCommand.Parameters.AddWithValue("@endlat", positionDestiny.Latitude);
                myCommand.Parameters.AddWithValue("@endlong", positionDestiny.Longitude);
                myCommand.Parameters.AddWithValue("@date", newtrip.Date);
                myCommand.Parameters.AddWithValue("@duration", newtrip.Duration);
                myCommand.Parameters.AddWithValue("@cost", newtrip.Cost);
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.ExecuteNonQuery();

                query = "SELECT LAST_INSERT_ID()";
                myCommand = new MySqlCommand(query, myConn);

                MySqlDataReader myReader = myCommand.ExecuteReader();
                int? increment_id = 0;
                if (myReader.Read())
                {
                    increment_id = myReader.GetInt32(0);
                    Console.WriteLine(increment_id + "");

                };
                    myReader.Close();

                foreach (string idStation in newtrip.UsedChargingStations)
                {
                    query = "INSERT INTO UsedStations (id_station, trip_id) VALUES (@idStation, @idTrip)";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@idTrip", increment_id);
                    myCommand.Parameters.AddWithValue("@idStation", idStation);
                    myCommand.ExecuteNonQuery();

                }

                myTrans.Commit();
                Console.WriteLine("Trip added to database.");
                logger.Info("Put Trip Transaction user: "+username+" triP: "+newtrip.TripName+" done");    

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
                    logger.Error(ex,"Put Trip Transaction user: "+username+" triP: "+newtrip.TripName+" not rolledback"); 
                }
                logger.Error(e,"Put Trip Transaction user: "+username+" triP: "+newtrip.TripName+" not added"); 
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
                    logger.Error(e,"Put Trip connection database user: "+username+" triP: "+newtrip.TripName+" notclosed"); 
                    throw e;
                }
            }
        }

        public Trip Get(int id)
        {
            Trip newTrip = null;
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM Trip WHERE tripId = @id";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);

                myCommand.Parameters.AddWithValue("@id", id);
                myCommand.ExecuteNonQuery();

                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    newTrip = new Trip
                    {
                        TripId = myReader.GetInt32("tripId"),
                        TripName = myReader.GetString("name"),
                        VehicleRegistrationNumber = myReader.GetString("vehicleRegistrationNumber"),
                        Origin = new Position {
                            Latitude = myReader.GetDouble("localStartLatitude"),
                            Longitude = myReader.GetDouble("localStartLongitude")
                        },
                        Destination = new Position {
                                Latitude = myReader.GetDouble("localEndLatitude"),
                                Longitude = myReader.GetDouble("localEndLongitude")
                        },
                        Date = myReader.GetDateTime("date"),
                        Duration = myReader.GetString("duration"),
                        Cost = myReader.GetDouble("cost"),
                        UsedChargingStations = new List<string>()
                    };
                    myReader.Close();
                

                    query = "SELECT id_station FROM UsedStations WHERE trip_id = @id";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        newTrip.UsedChargingStations.Add(myReader.GetString("id_station"));
                    }
                    myReader.Close();
                }
                logger.Info("Put Trip Trasaction triPid: "+id+" done"); 
                myReader.Close();
            }
            catch (Exception e)
            {
                logger.Error(e,"Put Trip Transaction  triPid: "+id+" not done"); 
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
                    logger.Error(e,"Put Trip connection database  triPid: "+id+" not closed"); 
                    throw e;
                }
            }

            return newTrip;
        }

        public void Update(Trip newTrip)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            Position positionOrigin= new Position();
            Position positionDestiny = new Position(); 

            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();

                // tripId, name, vehicleRegistrationNumber, localStartLatitude, localStartLongitude, localEndLatitude, localEndLongitude, date, duration, cost, usedChargingStations
                string query = "UPDATE Trip SET tripId = @tripId, name = @name, vehicleRegistrationNumber = @vehicleRN, localStartLatitude = @startlat, localStartLongitude = @startlong, localEndLatitude = @endlat, localEndLongitude = @endlong, date = @dat, duration = @duration, cost = @cost WHERE tripId = @tripId";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);

                myCommand.Parameters.AddWithValue("@tripId", newTrip.TripId);
                myCommand.Parameters.AddWithValue("@name", newTrip.TripName);
                myCommand.Parameters.AddWithValue("@vehicleRN", newTrip.VehicleRegistrationNumber);
                positionOrigin=newTrip.Origin;
                myCommand.Parameters.AddWithValue("@startlat", positionOrigin.Latitude);
                myCommand.Parameters.AddWithValue("@startlong", positionOrigin.Longitude);
                
                positionDestiny=newTrip.Destination;
                myCommand.Parameters.AddWithValue("@endlat", positionDestiny.Latitude);
                myCommand.Parameters.AddWithValue("@endlong", positionDestiny.Longitude);
                
                myCommand.Parameters.AddWithValue("@dat", newTrip.Date);
                myCommand.Parameters.AddWithValue("@duration", newTrip.Duration);
                myCommand.Parameters.AddWithValue("@cost", newTrip.Cost);

                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("Trip updated in database.");
                logger.Info("Update Trip Transaction database  triPid: "+newTrip.TripId+" updated"); 
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
                    logger.Error(ex,"Update Trip Transaction  triPid: "+newTrip.TripId+" not rolledback"); 
                }
                logger.Error(e,"Update Trip Transaction database  triPid: "+newTrip.TripId+" not updated");
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
                    logger.Error(e,"Update Trip connection database  triPid: "+newTrip.TripId+" not closed"); 
                    throw e;
                }
            }
        }

        public ICollection<Trip> GetAll()
        {
            ICollection<Trip> newTrips = new List<Trip>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM Trip";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);

                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Trip newTrip = new Trip() {
                        TripId = myReader.GetInt32("tripId"),
                        TripName = myReader.GetString("name"),
                        VehicleRegistrationNumber = myReader.GetString("vehicleRegistrationNumber"),
                        Origin = new Position {
                            Latitude = myReader.GetDouble("localStartLatitude"),
                            Longitude = myReader.GetDouble("localStartLongitude")
                        },
                        Destination = new Position {
                                Latitude = myReader.GetDouble("localEndLatitude"),
                                Longitude = myReader.GetDouble("localEndLongitude")
                        },
                        Date = myReader.GetDateTime("date"),
                        Duration = myReader.GetString("duration"),
                        Cost = myReader.GetDouble("cost"),
                        UsedChargingStations = new List<string>()
                    };
                    newTrips.Add(newTrip);
                }
                
                myReader.Close();

                foreach (Trip newtrip in newTrips)
                {
                    query = "SELECT id_station FROM UsedStations WHERE trip_id = @id";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", newtrip.TripId);
                    myCommand.ExecuteNonQuery();
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        newtrip.UsedChargingStations.Add(myReader.GetString("id_station"));
                    }

                    myReader.Close();
                }
                logger.Info("GetAll Trip fetched"); 
            }
            catch (Exception e)
            {
                logger.Error(e,"GetAll Trip not fetched"); 
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
                    logger.Error(e,"GetAll Trip Connection not closed"); 
                    throw e;
                }
            }

            return newTrips;
        }

        public ICollection<Trip> GetTripList(List<int> ids)
        {
            ICollection<Trip> trips = new List<Trip>();
            Trip tl = new Trip();

            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                foreach(int tripId in ids){
                                      
                    tl = this.Get(tripId);
                    trips.Add(tl);
                    logger.Info("GetTripList Trip "+tripId+" fetched\n");
                }

                logger.Info("GetTripList Trip completa was fetched\n");
            }
            catch (Exception e)
            {
                logger.Error(e,"GetTripList Trip completa was not fetcheded\n");
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
                    logger.Error(e,"GetTripList Trip completa Connection not closed\n");
                    throw e;
                }
            }

            return trips;
        }

        public void Delete(int tripId) {

            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "DELETE FROM Trip Where tripId = @id";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("@id", tripId);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();

                query = "DELETE FROM UsedStations Where trip_Id = @id";
                myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@id", tripId);
                myCommand.ExecuteNonQuery();

                Console.WriteLine("Trip deleted from database.");
                logger.Info("Delete Trip transaction tripId:"+tripId+ " deleted"); 

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
                    logger.Error(ex,"Delete Trip Connection tripId:"+tripId+ " not rolledback"); 
                }
                logger.Error(e,"Delete Trip Connection tripId:"+tripId+ " not deleted"); 
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
                    logger.Error(e,"Delete Trip Connection tripId:"+tripId+ " not closed"); 
                    throw e;
                }
            }
        }
    }
}
