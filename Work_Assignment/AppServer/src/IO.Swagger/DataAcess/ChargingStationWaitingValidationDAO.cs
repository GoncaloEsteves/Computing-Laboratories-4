using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using NLog;
using IO.Swagger.Models;

namespace IO.Swagger.DataAcess
{
    public class ChargingStationWaitingValidationDAO
    {
        public ChargingStationWaitingValidationDAO(String con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        private Logger logger = LogManager.GetCurrentClassLogger();

// string nameChargingStation, string Street, String City, String LocationType, String AccessType, String Restrictions, String AditionalInfo 

        public ChargingStationWaitingValidation Get(string nameChargingStation)
        {
            ChargingStationWaitingValidation post = new ChargingStationWaitingValidation();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM ChargingStationWaitingValidation WHERE nameChargingStation = @nameChargingStation";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@nameChargingStation", nameChargingStation);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    post.NameChargingStation = myReader.GetString("nameChargingStation");
                    post.Street = myReader.GetString("Street");
                    post.City = myReader.GetString("City");

                    post.LocationType = myReader.GetString("LocationType");
                    post.AccessType = myReader.GetString("AccessType");
                    post.Restritions = myReader.GetString("Restritions"); 
                    post.AditionalInfo= myReader.GetString("AditionalInfo");
                    myReader.Close();
                    
                    
                        myReader.Close();
                        logger.Info("Get ChargingStationWaitingValidation nameChargingStation:"+nameChargingStation+ " fetched\n");
                }
                else{
                    myReader.Close();
                    logger.Info("Get ChargingStationWaitingValidation nameChargingStation:"+nameChargingStation+ " searched not exist\n");
                    }
            }
            catch (Exception e)
            {
                logger.Error(e,"Get ChargingStationWaitingValidation nameChargingStation:"+nameChargingStation+ " not fetched\n");
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
                    logger.Error(e,"Get ChargingStationWaitingValidation connection nameChargingStation:"+nameChargingStation+ " not closed\n");
                    throw e;
                }
            }

            return post;
        }

// string nameChargingStation, string Street, String City, String LocationType, String AccessType, String Restrictions, String AditionalInfo 

        public void Put(ChargingStationWaitingValidation post)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;

            try
            {
                
                myConn.Open();
                myTrans = myConn.BeginTransaction();

                string query =
                    "INSERT INTO ChargingStationWaitingValidation (nameChargingStation, Street, City, LocationType, AccessType, Restritions, AditionalInfo) VALUES (@nameChargingStation,  @Street, @City, @LocationType, @AccessType, @Restritions, @AditionalInfo)";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("@nameChargingStation", post.NameChargingStation);
                myCommand.Parameters.AddWithValue("@Street", post.Street);
                myCommand.Parameters.AddWithValue("@City", post.City);
                myCommand.Parameters.AddWithValue("@LocationType", post.LocationType);
                myCommand.Parameters.AddWithValue("@AccessType", post.AccessType);
                myCommand.Parameters.AddWithValue("@Restritions", post.Restritions);
                myCommand.Parameters.AddWithValue("@AditionalInfo", post.AditionalInfo);

                myCommand.ExecuteNonQuery();

                myTrans.Commit();
                Console.WriteLine("ChargingWaitingValidation station added to database.");
                logger.Info("Put ChargingStationWaitingValidation transaction nameChargingStation:"+post.NameChargingStation+ " added");
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
                    logger.Error(ex,"Put ChargingStationWaitingValidation transaction nameChargingStation:"+post.NameChargingStation+ " not rolledback\n");
                }
                logger.Error(e,"Put ChargingStationWaitingValidation transaction nameChargingStation:"+post.NameChargingStation+ " not added\n");
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
                    logger.Error(e,"Put ChargingStationWaitingValidation connection nameChargingStation:"+post.NameChargingStation+ " not closed\n");
                }
            }
        }


        
// string nameChargingStation, string Street, String City, String LocationType, String AccessType, String Restrictions, String AditionalInfo 

        public void Update(ChargingStationWaitingValidation posto)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;

            try
            {
                myConn.Open(); 
                myTrans = myConn.BeginTransaction();
                // string nameChargingStation, string Street, String City, String LocationType, String AccessType, String Restrictions, String AditionalInfo 

                string query = "UPDATE ChargingStationWaitingValidation SET nameChargingStation = @nameChargingStation, Street  = @Street, City = @City, LocationType = @LocationType,  AccessType = @AccessType, Restritions = @Restritions, AditionalInfo = @AditionalInfo WHERE nameChargingStation = @nameChargingStation";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                
                myCommand.Parameters.AddWithValue("@nameChargingStation", posto.NameChargingStation);
                myCommand.Parameters.AddWithValue("@Street", posto.Street);
                myCommand.Parameters.AddWithValue("@City",posto.City);
                myCommand.Parameters.AddWithValue("@LocationType",posto.LocationType);
                myCommand.Parameters.AddWithValue("@AccessType", posto.AccessType);
                myCommand.Parameters.AddWithValue("@Restritions", posto.Restritions);
                myCommand.Parameters.AddWithValue("@AditionalInfo", posto.AditionalInfo);

                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("ChargingStationWaitingValidation updated in database.");
                logger.Info("Update ChargingStationWaitingValidation Transaction nameChargingStation:"+posto.NameChargingStation+ " updated\n");
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
                    logger.Error(ex,"Update ChargingStationWaitingValidation Transaction nameChargingStation:"+posto.NameChargingStation+ " not rolledback\n");
                }
                logger.Error(e,"Update ChargingStationWaitingValidation Transaction, namechargingStation:"+posto.NameChargingStation+ " not fetched\n");
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
                    logger.Error(e,"Update ChargingStationWaitingValidation Connection, nameChargingStation:"+posto.NameChargingStation+ " not closed\n");
                    throw e;
                }
            }
        }

 // string nameChargingStation, string Street, String City, String LocationType, String AccessType, String Restrictions, String AditionalInfo 

        public ICollection<ChargingStationWaitingValidation> GetAll()
        {
            ICollection<ChargingStationWaitingValidation> postos = new List<ChargingStationWaitingValidation>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM ChargingStationWaitingValidation";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    ChargingStationWaitingValidation posto = new ChargingStationWaitingValidation();
                    posto.NameChargingStation = myReader.GetString("nameChargingStation");
                    posto.Street = myReader.GetString("Street");
                    posto.City = myReader.GetString("City");

                    posto.LocationType = myReader.GetString("LocationType");
                    posto.AccessType= myReader.GetString("AccessType");
                    posto.Restritions = myReader.GetString("Restritions");
                    posto.AditionalInfo= myReader.GetString("AditionalInfo");


                    postos.Add(posto);
                }

                myReader.Close();
                logger.Info("GetAll ChargingStationWaitingValidation fetched\n");
            }
            catch (Exception e)
            {
                logger.Error(e,"GetAll ChargingStationWaitingValidation not fetcheded\n");
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
                    logger.Error(e,"GetAll ChargingStationWaitingValidation Connection not closed\n");
                    throw e;
                }
            }

            return postos;
        }

// string nameChargingStation, string Street, String City, String LocationType, String AccessType, String Restrictions, String AditionalInfo 

        public void Delete(string nameChargingStation)
        {

            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "DELETE FROM ChargingStationWaitingValidation Where nameChargingStation = @nameChargingStation";
                MySqlCommand myCommand = new MySqlCommand(query, myConn,myTrans);
                myCommand.Parameters.AddWithValue("@nameChargingStation", nameChargingStation);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("ChargingStationWaitingValidation deleted from database.");
                logger.Info("Delete ChargingStationWaitingValidation Connection, nameChargingStation:"+nameChargingStation+ " deleted\n");
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
                    logger.Error(ex,"Delete ChargingStationWaitingValidation Transaction, nameChargingStation:"+nameChargingStation+ " not rolledback\n");
                }
                logger.Error(e,"Delete ChargingStationWaitingValidation Transaction, nameChargingStation:"+nameChargingStation+ " not deleted\n");
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
                    logger.Error(e,"Delete ChargingStationWaitingValidation Connection, nameChargingStation:"+nameChargingStation+ " not closed\n");
                    throw e;
                }
            }
        }
    }
}
