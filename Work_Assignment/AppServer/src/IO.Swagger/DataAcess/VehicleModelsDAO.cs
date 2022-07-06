using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using IO.Swagger.Models;
using NLog;

namespace IO.Swagger.DataAcess
{
    public class VehicleModelsDAO
    {
        public VehicleModelsDAO(string con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        private Logger logger = LogManager.GetCurrentClassLogger();

//       string typeCode, string name, string levelTwoVCharges, string fastCharges

    public VehicleModels Get(string typeCode)
        {
            VehicleModels vm = null;
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM VehicleModels WHERE typeCode = @typeCode";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@typeCode", typeCode);


                myCommand.ExecuteNonQuery();
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    vm = new VehicleModels() {
                        TypeCode = myReader.GetString("typeCode"),
                        Name = myReader.GetString("name"),
                        LevelTwoCharges = myReader.GetString("levelTwoCharges"),
                        FastCharges = myReader.GetString("fastCharges"),
                    };
                    myReader.Close();
                }

                logger.Info("vehicleMode "+ typeCode +" fetched from database\n");
                myReader.Close();
            }
            catch (Exception e)
            {
                
                logger.Error(e,"VehicleModel "+ typeCode+"couldn't be fetched\n");
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
                    logger.Error(e,"Get VehicleModel Database connection couldn't be closed");
                    throw e;
                }
            }

            return vm;
        } 
  public ICollection<VehicleModels> GetAll()
        {
            ICollection<VehicleModels> newVehicleModels = new List<VehicleModels>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM VehicleModels";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);

                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    VehicleModels newM = new VehicleModels() {
                        TypeCode = myReader.GetString("typeCode"),
                        Name = myReader.GetString("name"),
                        LevelTwoCharges = myReader.GetString("levelTwoCharges"),
                        FastCharges = myReader.GetString("fastCharges"),
                        
                    };
                    newVehicleModels.Add(newM);
                }
                
                myReader.Close();

                logger.Info("Get VehicleModels fetched"); 
            }
            catch (Exception e)
            {
                logger.Error(e,"Get VehicleModels not fetched"); 
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
                    logger.Error(e,"Get VehicleModels Connection not closed"); 
                    throw e;
                }
            }

            return newVehicleModels;
        }


        public void Put(VehicleModels model)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "INSERT INTO vehiclemodels (typeCode, name, fastCharges, levelTwoCharges) VALUES (@typeCode, @name, @fastCharges, @levelTwoCharges)";

                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);

                myCommand.Parameters.AddWithValue("@typeCode", model.TypeCode);
                myCommand.Parameters.AddWithValue("@name", model.Name);
                myCommand.Parameters.AddWithValue("@levelTwoCharges", model.LevelTwoCharges);
                myCommand.Parameters.AddWithValue("@fastCharges", model.FastCharges);
    
                myCommand.ExecuteNonQuery();

            
                myTrans.Commit();
                Console.WriteLine("ModelVehicle added to database.");
                logger.Info("Put ModelVehicle Transaction : "+model.TypeCode+" done");    

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
                        Console.WriteLine("An exception of type " + ex.GetType() + " was encountered while attempting to roll back the transaction.");
                    }
                    logger.Error(ex,"Put ModelVehicle Transaction: "+model.TypeCode+" not rolledback"); 
                }
                logger.Error(e,"Put ModelVehicle transaction typeCode:"+model.TypeCode+ " not added\n");
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
                    logger.Error(e,"Put ModelVehicle connection database user: "+model.TypeCode+" notclosed"); 
                    throw e;
                }
            }
        }


        public void Update(VehicleModels model)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;

            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();

                // tripId, name, vehicleRegistrationNumber, localStartLatitude, localStartLongitude, localEndLatitude, localEndLongitude, date, duration, cost, usedChargingStations
                string query = "UPDATE VehicleModels SET typeCode = @typeCode, name = @name, fastCharges = @fastCharges, levelTwoCharges = @levelTwoCharges  WHERE typeCode = @typeCode";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);

                myCommand.Parameters.AddWithValue("@typeCode", model.TypeCode);
                myCommand.Parameters.AddWithValue("@name", model.Name);
                
                myCommand.Parameters.AddWithValue("@levelTwoCharges", model.LevelTwoCharges);
                myCommand.Parameters.AddWithValue("@fastCharges", model.FastCharges);
                
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("VehicleModel updated in database.");
                logger.Info("Update VehicleModel Transaction database  typeCode: "+model.TypeCode+" updated"); 
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
                    logger.Error(e,"Update VehicleModel Transaction  typecode: "+model.TypeCode+" not rolledback"); 
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
                    logger.Error(e,"Update VehicleModel connection database typeCode: "+model.TypeCode+" not closed"); 
                    throw e;
                }
            }
        }

      

        public void Delete(string typeCode) {

            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "DELETE FROM vehiclemodels Where typeCode = @typeCode";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("@typeCode", typeCode);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("Vehiclemodel deleted from database.");
                logger.Info("Delete VehicleModel transaction typeCode:"+typeCode+ " deleted"); 

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
                    logger.Error(ex,"Delete VehicleModel Connection typeCode:"+typeCode+ " not rolledback"); 
                }
                logger.Error(e,"Delete Trip Connection typeCode:"+typeCode+ " not deleted"); 
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
                    logger.Error(e,"Delete VehicleModel Connection typeCode:"+typeCode+ " not closed"); 
                    throw e;
                }
            }
        }
    }
}
