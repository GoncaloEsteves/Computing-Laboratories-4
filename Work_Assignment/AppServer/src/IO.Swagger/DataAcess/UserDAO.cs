using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using IO.Swagger.Models;
//using Microsoft.Extensions.Logging.Console.Internal;
using NLog;
using Microsoft.Extensions.Logging;

namespace IO.Swagger.DataAcess
{
    public class UserDAO
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public UserDAO(string con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        public void Put(User util)
        
        {
                        
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try 
            {
                myConn.Open();
                
                myTrans= myConn.BeginTransaction();
                
                string query = "INSERT INTO User (name, username, nif, password, email) VALUES (@name, @username, @nif, @pass, @email)";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);

                myCommand.Parameters.AddWithValue("@name", util.Name);
                myCommand.Parameters.AddWithValue("@username", util.Username);
                myCommand.Parameters.AddWithValue("@email", util.Email);
                myCommand.Parameters.AddWithValue("@nif", util.Nif);
                myCommand.Parameters.AddWithValue("@pass", util.Password);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("User written in database.");
                logger.Info("Put User user: "+ util.Name+" done");
            }
            catch (Exception e)
            {
                try
                {
                    if (myTrans != null){
                        myTrans.Rollback();
                        logger.Info("Transaction Put User " +util.Name+ ", rolledback\n");
                    }
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                    logger.Error(ex,"Transaction Put user "+util.Name+ ", couldn't be rolledback\n");
                }
                 logger.Error(e,"Transaction Put user "+util.Name+ ", failed on database\n");
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
                    logger.Error(e,"Put User Database connection couldn't be closed");
                    throw e;
                }
            }
        }

        public User Get(string username)
        {
            User util = null;
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM User WHERE username = @name";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@name", username);


                myCommand.ExecuteNonQuery();
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    util = new User() {
                        Name = myReader.GetString("name"),
                        Username = myReader.GetString("username"),
                        Email = myReader.GetString("email"),
                        Nif = myReader.GetString("nif"),
                        Password = myReader.GetString("password"),
                        CreditCards = new List<CreditCard>(),
                        FavoriteChargingStations = new List<string>(),
                        Trips = new List<int?>(),
                        VehiclesIds = new List<string>()
                    };
                    myReader.Close();
                }

                logger.Info("User "+username +" fetched from database\n");
                myReader.Close();
            }
            catch (Exception e)
            {
                
                logger.Error(e,"User "+ username+"couldn't be fetched\n");
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
                    logger.Error(e,"Get User Database connection couldn't be closed");
                    throw e;
                }
            }

            return util;
        } 

        public void Update(User util)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();

                myTrans = myConn.BeginTransaction();
                string query = "UPDATE User SET name = @name, email = @email, nif = @nif, password = @pass WHERE username = @username";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                

                myCommand.Parameters.AddWithValue("@name", util.Name);
                myCommand.Parameters.AddWithValue("@email", util.Email);
                myCommand.Parameters.AddWithValue("@nif", util.Nif);
                myCommand.Parameters.AddWithValue("@pass", util.Password);
                myCommand.Parameters.AddWithValue("@username", util.Username);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("User updated in database.");
                logger.Info("Update User "+util.Name+" done");
            }
            catch (Exception e)
            {
                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                        logger.Info("Transaction Update User "+ util.Name+" rolledback\n");
                    }
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                    logger.Error(ex,"Update User "+ util.Name+" couldn't be executed\n");
                }
                logger.Error(e,"Update User couldn't be executed");
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
                    logger.Error(e,"Update User Database connection couldn't be closed");
                    throw e;
                }
            }
        }

        public void UpdatePassword(string password, string username)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "UPDATE User SET password = @pass WHERE username = @username";
                MySqlCommand myCommand = new MySqlCommand(query, myConn,myTrans);

                myCommand.Parameters.AddWithValue("@pass", password);
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("User password updated in database.");
                
                logger.Info("UpdatePassword Transaction User "+username+" done");
              
            }
            catch (Exception e)
            {
                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                        logger.Info("UpdatePassword Transaction User "+username+" rolledback");
                    }
            }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                    logger.Error(ex,"UpdatePassword Transaction User "+username+" couldn't be rolledback");
                }
                logger.Error(e,"UpdatePassword Transaction User "+username+" couldn't be executed");
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
                    logger.Error(e,"UpdatePassword User Database connection couldn't be closed");
                    throw e;
                }
            }
        }

        public ICollection<User> GetAll()
        {
            ICollection<User> users = new List<User>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            
            try
            {
                myConn.Open();
                string query = "SELECT * FROM User";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    User util = new User();

                    util.Name = myReader.GetString("name");
                    util.Username = myReader.GetString("username");
                    util.Email = myReader.GetString("email");
                    util.Nif = myReader.GetString("nif");
                    util.Password = myReader.GetString("password");
                    util.CreditCards = new List<CreditCard>();
                    util.FavoriteChargingStations = new List<string>();
                    util.Trips = new List<int?>();
                    util.VehiclesIds = new List<string>();

                    users.Add(util);
                }

                myReader.Close();

                foreach (User util in users)
                {
                    query = "SELECT * FROM CreditCard WHERE username = @username";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@username", util.Username);
                    myCommand.ExecuteNonQuery();
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        CreditCard cc = new CreditCard();
                        cc.CardType = myReader.GetString("type");
                        cc.CardNumber = myReader.GetString("number");
                        cc.ExpireDate = myReader.GetDateTime("expireDate");
                        cc.Cvv = myReader.GetInt32("cvv");

                        util.CreditCards.Add(cc);
                    }

                    myReader.Close();
                }

                foreach (User util in users)
                {
                    query = "SELECT station_id FROM FavoriteStations WHERE username = @username";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@username", util.Username);
                    myCommand.ExecuteNonQuery();
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        util.FavoriteChargingStations.Add(myReader.GetString("station_id"));
                    }

                    myReader.Close();
                }

                foreach (User util in users)
                {
                    query = "SELECT tripId FROM Trip WHERE username = @username";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@username", util.Username);
                    myCommand.ExecuteNonQuery();
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        util.Trips.Add(myReader.GetInt32("tripId"));
                    }

                    myReader.Close();
                }

                foreach (User util in users)
                {
                    query = "SELECT registrationNumber FROM Vehicle WHERE username = @username";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@username", util.Username);
                    myCommand.ExecuteNonQuery();
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        util.VehiclesIds.Add(myReader.GetString("registrationNumber"));
                    }

                    myReader.Close();
                }
                logger.Info("GetAll Users done");
            }
            catch (Exception e)
            {
                logger.Error(e,"GetAll User couldn't be fetched");
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
                    logger.Error(e,"GetAll Users connection couldn't be closed");
                    throw e;
                }
            }

            return users;
        }

        public void Delete(string username) {

            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "DELETE FROM User WHERE username = @username";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("User deleted from database.");
            }
            catch (Exception e)
            {
                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                        logger.Info("Delete user Transaction "+username+" rolledback");
                    }
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                    logger.Error(ex,"Delete User "+username+" couldn't be done");
                }
                
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

        public void DeleteCreditCard(string username, string creditCardN)
        {

            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "DELETE FROM creditcard WHERE username = @username AND number = @number";
                MySqlCommand myCommand = new MySqlCommand(query, myConn,myTrans);
             
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@number", creditCardN);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("User credit card deleted from database.");
                logger.Info("DeleteCreditCard credit card: "+ creditCardN+" form User: "+username+" deleted\n");
            }
            catch (Exception e)
            {
                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                        logger.Info("DeleteCreditCard User Transaction credit card: "+ creditCardN+" form User: "+username+" rolledbacb");
                    }
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                    logger.Error(ex,"DeleteCreditCard User Transaction, credit card: "+ creditCardN+" form User: "+username+" couldn't be rolledback");
                }
                logger.Error(e,"DeleteCreditCard User Transaction, credit card: "+ creditCardN+" form User: "+username+" couldn't be rolledback");
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
                    logger.Error(e,"DeleteCreditCard User Connection, credit card: "+ creditCardN+" form User: "+username+" couldn't be closed");
                    throw e;
                }
            }
        }

        public void DeleteFavoriteStation(string username, string id) {

            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "DELETE FROM favoritestations WHERE username = @username AND station_id = @id";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                

                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@id", id);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("User favourite station deleted from database.");
                logger.Info("DeleteFavoriteStation User user"+username+ " stid "+id+" done\n");
            }
            catch (Exception e)
            {
                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                        logger.Info("DeleteFavoriteStation User Transaction user"+username+ " stid "+id+" rolledback\n");
                    }
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                    logger.Error(ex,"DeleteFavoriteStation User Transaction user"+username+ " stid "+id+" couldn't be rolledback\n");
                }
                logger.Error(e,"DeleteFavoriteStation User Transaction user"+username+ " stid "+id+" couldn't be done\n");
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
                    logger.Error(e,"DeleteFavoriteStation User connectio database user"+username+ " stid "+id+" couldn't be closed\n");
                    throw e;
                }
            }
        }

        public void AddCreditCard(CreditCard cc, string username)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "INSERT INTO CreditCard (username, type, number, expireDate, cvv) VALUES (@username, @type, @number, @expiredate, @cvv)";
       
                MySqlCommand myCommand = new MySqlCommand(query, myConn,myTrans);

                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@type", cc.CardType);
                myCommand.Parameters.AddWithValue("@number", cc.CardNumber);
                myCommand.Parameters.AddWithValue("@expiredate", cc.ExpireDate);
                myCommand.Parameters.AddWithValue("@cvv", cc.Cvv);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("User credit card added to database.");

                logger.Info("AddCreditCard User user: "+username+" cc: "+cc.CardNumber+" added\n");
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
                    logger.Error(ex,"AddCreditCard User Transaction user: "+username+" cc: "+cc.CardNumber+" couldn't be rolledback\n");
                }
                logger.Error(e,"AddCreditCard User user: "+username+" cc: "+cc.CardNumber+"  couldn't be added\n");
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
                    logger.Error(e,"AddCreditCard User  connection user: "+username+" cc: "+cc.CardNumber+" couldn't be closed\n");
                    throw e;
                }
            }
        }

        public void AddFavChargingStation(string station, string username)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                string query = "INSERT INTO FavoriteStations (username, station_id) VALUES (@username, @station)";
          
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);

                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@station", station);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("User favourite station added to database.");
                logger.Info("AddFavChargingStation User user "+username+" station "+station+" added\n");
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
                    logger.Error(ex,"AddFavChargingStation User Transactions user "+username+" station "+station+" not rolledback\n");
                }
                logger.Error(e,"AddFavChargingStation User Transaction user "+username+" station "+station+" not added\n");
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
                    logger.Error(e,"AddFavChargingStation User connection database user "+username+" station "+station+" not closed\n");
                    throw e;
                }
            }
        }

        public ICollection<CreditCard> GetCreditCards(string username)
        {
            ICollection<CreditCard> ccs = new List<CreditCard>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();

                string query = "SELECT * FROM CreditCard WHERE username = @username";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
             
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.ExecuteNonQuery();
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    CreditCard cc = new CreditCard();
                    cc.CardType = myReader.GetString("type");
                    cc.CardNumber = myReader.GetString("number");
                    cc.ExpireDate = myReader.GetDateTime("expireDate");
                    cc.Cvv = myReader.GetInt32("cvv");

                    ccs.Add(cc);
                }
                logger.Info("GetCreditCards User from user"+username+" fetched");
                myReader.Close();
            }
            
            catch (Exception e)
            {
                logger.Error(e,"GetCreditCards User from user"+username+" not fetched");
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
                    logger.Error("GetCreditCards User connections database from user"+username+" not closed");
                    throw e;
                }
            }

            return ccs;
        }

        public CreditCard GetCreditCard(string username, string creditCardN)
        {
            CreditCard cc = new CreditCard();
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            

            try
            {
                myConn.Open();

                string query = "SELECT * FROM CreditCard WHERE username = @username AND number = @number";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@number", creditCardN);
                myCommand.ExecuteNonQuery();
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    cc.CardType = myReader.GetString("type");
                    cc.CardNumber = myReader.GetString("number");
                    cc.ExpireDate = myReader.GetDateTime("expireDate");
                    cc.Cvv = myReader.GetInt32("cvv");
                }

                logger.Info("GetCreditCard User from user"+username+" cc: "+creditCardN+" fetched");
                myReader.Close();
            }
            
            catch (Exception e)
            {
                logger.Error(e,"GetCreditCard User from user"+username+" cc: "+creditCardN+" not fetched");
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
                    logger.Error(e,"GetCreditCard User connection database from user"+username+" cc: "+creditCardN+" not closed");
                    throw e;
                }
            }

            return cc;
        }

        public ICollection<string> GetFavChargingStations(string username)
        {
            ICollection<string> fcs = new List<string>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();

                string query = "SELECT station_id FROM FavoriteStations WHERE username = @username";
          
                MySqlCommand myCommand = new MySqlCommand(query, myConn);

                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.ExecuteNonQuery();
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    fcs.Add(myReader.GetString("station_id"));
                }
                logger.Info("GetFavChargingStations User from user"+username+" fetched");
                myReader.Close();
            }
            
            catch (Exception e)
            {
                logger.Error(e,"GetFavChargingStations User from user"+username+" not fetched");
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
                    logger.Error(e,"GetFavChargingStations User connection from user"+username+" not closed");
                    throw e;
                }
            }

            return fcs;
        }

        public string GetFavChargingStation(string username, string id)
        {
            string fcs = null;
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();

                string query = "SELECT station_id FROM FavoriteStations WHERE username = @username AND station_id = @id";
          
                MySqlCommand myCommand = new MySqlCommand(query, myConn);

                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@id", id);
                myCommand.ExecuteNonQuery();
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    fcs = myReader.GetString("station_id");
                }
                logger.Info("GetFavChargingStations User user: "+username+" station: "+id+" fetched");
                myReader.Close();
            }
            
            catch (Exception e)
            {
                logger.Error(e,"GetFavChargingStations User user: "+username+" station: "+id+" fetched");
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
                    logger.Error(e,"GetFavChargingStation User connection from user"+username+" not closed");
                    throw e;
                }
            }

            return fcs;
        }

        public ICollection<int> GetTrips(string username)
        {
            ICollection<int> trips = new List<int>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();

                string query = "SELECT tripId FROM Trip WHERE username = @username";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);

                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.ExecuteNonQuery();
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    trips.Add(myReader.GetInt32("tripId"));
                }

                logger.Info("GetTrips User user"+username+" fetched");
                myReader.Close();
            }
            
            catch (Exception e)
            {
                logger.Error(e,"GetTrips User  user"+username+" not fetched");
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
                    logger.Error(e,"GetTrips User connection from user"+username+" not closed");
                    throw e;
                }
            }

            return trips;
        }

        public ICollection<Vehicle> GetVehicles(string username)
        {
            ICollection<Vehicle> vehicles = new List<Vehicle>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();

                string query = "SELECT * FROM Vehicle WHERE username = @username";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);

                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.ExecuteNonQuery();
                MySqlDataReader myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        Vehicle v = new Vehicle();
                        v.RegistrationNumber = myReader.GetString("registrationNumber");
                        v.TypeCode = myReader.GetString("typeCode");
                        v.Name = myReader.GetString("name");
                        v.LastConsumptionReport = myReader.GetDouble("lastConsumptionReport");

                        vehicles.Add(v);
                    }
                logger.Info("GetVehicles User  from user"+username+" fetched");
                myReader.Close();
            }
            
            catch (Exception e)
            {
                logger.Error(e,"GetVehicles User  user"+username+" not fetched");
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
                    logger.Error(e,"GetVehicles User connection from user"+username+" not closed");
                    throw e;
                }
            }

            return vehicles;
        }

        public string GetPassword(string username)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            string password = null;
            try
            {
                myConn.Open();

                string query = "SELECT password FROM User WHERE username = @username";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.ExecuteNonQuery();
                MySqlDataReader myReader = myCommand.ExecuteReader();
                myReader.Read();
                password = myReader.GetString("password");
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

            return password;
        }

    }
}
