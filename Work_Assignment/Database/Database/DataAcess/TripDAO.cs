using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Classes;

namespace Data.DataAcess
{
    public class TripDAO
    {
        public TripDAO(string con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        public void Put(Trip via)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "INSERT INTO Viagem (tripId, vehicleRegistrationNumber, localStartLongitude, localStartLatitude, localEndLongitude, localEndLatitude, duration, cost, usedChargingStations) VALUES (@id, @partlong, @partlat, @chegalong, @chegalat, @duration, @cost, @usedChargingStations)";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@id", via.TripId);
                myCommand.Parameters.AddWithValue("@partlong", via.Origin.Longitude);
                myCommand.Parameters.AddWithValue("@partlat", via.Origin.Latitude);
                myCommand.Parameters.AddWithValue("@chegalong", via.Destination.Longitude);
                myCommand.Parameters.AddWithValue("@chegalat", via.Destination.Latitude);
                myCommand.Parameters.AddWithValue("@duracao", via.Duration);
                myCommand.Parameters.AddWithValue("@custo", via.Cost);
                myCommand.Parameters.AddWithValue("@data", via.Data);
                myCommand.ExecuteNonQuery();

                foreach (string i in via.UsedChargingStations)
                {
                    query = "INSERT INTO UsedChargingStations (Trip_idViagem, ChargingPost_id) VALUES (@id, @post)";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", via.TripId);
                    myCommand.Parameters.AddWithValue("@post", i);
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

        public Trip Get(long id)
        {
            Trip via = new Trip();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM trip WHERE tirpId = @id";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@id", id);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    via.TripId = myReader.GetInt32("tripId");
                    via.VehicleRegistrationNumber = myReader.GetString("vehicleRegistrationNumber");
                    via.Origin = new Position(myReader.GetDouble("localStartLongitude"), myReader.GetDouble("localStartLatitude"));
                    via.Destination = new Position(myReader.GetDouble("localEndLongitude"), myReader.GetDouble("localEndLatitude"));
                    via.Duration = myReader.GetInt32("duration");
                    via.Cost = myReader.GetDouble("cost");
                    

                    myReader.Close();

                    query = "SELECT ChargingPost_id FROM UsedChargingStations WHERE trip_tripId = @id";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        via.UsedChargingStations.Add(myReader.GetInt32("ChargingPost_id"));
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

            return via;
        }

        public void Update(Trip via)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();

                // tripId, vehicleRegistrationNumber, localStartLongitude, localStartLatitude, localEndLongitude, localEndLatitude, duration, cost, usedChargingStations
                string query = "UPDATE User SET tripId = @id, vehicleRegistrationNumber = @vehicleRN, localStartLongitude = @partlong, localStartLatitude = @partlat, localEndLongitude = @chegalong, localEndLatitude = @chegalat, duration = @duration, cost = @cost, usedChargingStations = @usedChStation WHERE tripId = @id";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@vehicleRN", via.VehicleRegistrationNumber);
                myCommand.Parameters.AddWithValue("@partlong", via.Origin.Longitude);
                myCommand.Parameters.AddWithValue("@partlat", via.Origin.Latitude);
                myCommand.Parameters.AddWithValue("@chegalong", via.Destination.Longitude);
                myCommand.Parameters.AddWithValue("@chegalat", via.Destination.Latitude);
                myCommand.Parameters.AddWithValue("@duracao", via.Duration);
                myCommand.Parameters.AddWithValue("@custo", via.Cost);
                myCommand.Parameters.AddWithValue("@id", via.TripId);
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

        public ICollection<Trip> GetAll()
        {
            ICollection<Trip> viagens = new List<Trip>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM Trip";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Trip via = new Trip();
                    via.TripId = myReader.GetInt32("idViagem");
                    via.VehicleRegistrationNumber = myReader.GetInt32("vehicleRegistrationNumber");
                    via.Origin = new Position(myReader.GetDouble("localStartLongitude"), myReader.GetDouble("localStartLatitude"));
                    via.Destination = new Position(myReader.GetDouble("localEndLongitude"), myReader.GetDouble("localEndLatitude"));
                    via.Duration = myReader.GetInt32("duration");
                    via.Cost = myReader.GetDouble("cost");
               
                    viagens.Add(via);
                }

                myReader.Close();

                foreach (Trip via in viagens)
                {
                    query = "SELECT ChargingPost_id FROM UsedChargingStations WHERE trip_tripId = @id";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@id", via.TripId);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        via.UsedChargingStations.Add(myReader.GetInt32("ChargingPost_id"));
                    }

                    myReader.Close();
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

            return viagens;
        }
    }
}
