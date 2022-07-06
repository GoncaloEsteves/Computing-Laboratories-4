using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using NLog;
using IO.Swagger.Models;

namespace IO.Swagger.DataAcess
{
    public class VehicleDAO
    {
        public VehicleDAO(string con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public void Put(String username, Vehicle viat)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            try
            {// string registrationNumber, string typecode, string name, double lastComsumptionReport, string username
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "INSERT INTO Vehicle (registrationNumber, typecode, name, lastConsumptionReport, username) VALUES (@registrationNumber, @typecode, @name, @lastConsumptionReport, @username)";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("@registrationNumber", viat.RegistrationNumber);
                myCommand.Parameters.AddWithValue("@typecode", viat.TypeCode);
                myCommand.Parameters.AddWithValue("@name", viat.Name);
                myCommand.Parameters.AddWithValue("@lastConsumptionReport",viat.LastConsumptionReport);
                myCommand.Parameters.AddWithValue("@username",username);
            
                
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                logger.Info("Put vehicle user "+username+ ", vehicle: "+viat.RegistrationNumber+" added\n");
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
                    logger.Error(ex," Put vehicle Transaction user "+username+ ", vehicle: "+viat.RegistrationNumber+" not rolledback\n");
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
                    logger.Error(e," Put vehicle connection user "+username+ ", vehicle: "+viat.RegistrationNumber+" not closed\n");
                    throw e;
                }
            }
        }

        public Vehicle Get(string registrationNumber)
        {
            Vehicle via = new Vehicle();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM Vehicle WHERE registrationNumber = @registrationNumber";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@registrationNumber", registrationNumber);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    // string registrationNumber, string brand, string model, string version, int year, double batteryCapacity, double lastComsumptionReport

                    via.RegistrationNumber = myReader.GetString("registrationNumber");
                    via.TypeCode = myReader.GetString("typeCode");
                    via.Name = myReader.GetString("name");
                    via.LastConsumptionReport = myReader.GetDouble("lastConsumptionReport");
                    via.Username = myReader.GetString("username");
                    

                    myReader.Close();

                    logger.Info(" Get vehicle  vehicle: "+registrationNumber+" fetched\n");
                }

                myReader.Close();
            }
            catch (Exception e)
            {
                logger.Error(e," Get vehicle vehicle: "+registrationNumber+" not fetched\n");
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
                    logger.Error(e," Get vehicle connection vehicle: "+registrationNumber+" not closed\n");
                    throw e;
                }
            }
            return via;
        }


        public ICollection<Vehicle> GetAll()
        {
            ICollection<Vehicle> allVehicles = new List<Vehicle>();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM Vehicle";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Vehicle newV = new Vehicle()
                    {
                        RegistrationNumber = myReader.GetString("registrationNumber"),
                        TypeCode = myReader.GetString("typeCode"),
                        Name = myReader.GetString("name"),
                        LastConsumptionReport = myReader.GetDouble("lastConsumptionReport"),
                    };
                    allVehicles.Add(newV);
                }
                logger.Info(" GetAll vehiclefetched\n");
                myReader.Close();
            }
            catch (Exception e)
            {
                logger.Error(e," GetAll vehicle not fetched\n");
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
                    logger.Error(e," GetAll vehicle connection not closed\n");
                    throw e;
                }
            }

            return allVehicles;
        }


        public ICollection<Vehicle> GetByList(List<string> idsString)
        {
            
            ICollection<Vehicle> vehicles = new List<Vehicle>();
            Vehicle ve = new Vehicle();

            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();

                foreach(string rn in idsString){                                                 
                    
                    ve = this.Get(rn);
                    vehicles.Add(ve);
                }
                
                logger.Info("GetByIds Vehicle fetched\n");
            }
            catch (Exception e)
            {
                logger.Error(e,"GetByIds Vehicle not fetcheded\n");
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
                    logger.Error(e,"GetByIds Vehicle Connection not closed\n");
                    throw e;
                }
            }

            return vehicles;
        }















        public void Update(Vehicle via)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;

            try
            {// string registrationNumber, string brand, string model, string version, int year, double batteryCapacity, double lastComsumptionReport

                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "UPDATE Vehicle SET registrationNumber = @registrationNumber, typeCode = @typeCode, name = @name, lastConsumptionReport = @lastConsumptionReport  WHERE registrationNumber = @registrationNumber";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("registrationNumber", via.RegistrationNumber);
                myCommand.Parameters.AddWithValue("@typeCode", via.TypeCode);
                myCommand.Parameters.AddWithValue("@name", via.Name);
                myCommand.Parameters.AddWithValue("@lastConsumptionReport", via.LastConsumptionReport);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                logger.Info("Update vehicle  vehicle: "+via.RegistrationNumber+" updated\n");
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
                    logger.Error(ex,"Update vehicle Transaction vehicle: "+via.RegistrationNumber+" rolledback\n");
                }
                
                logger.Error(e,"Update vehicle Transaction vehicle: "+via.RegistrationNumber+" rolledback\n");               
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
                    logger.Error(e,"Update vehicle connection vehicle: "+via.RegistrationNumber+" not closed\n");
                    throw e;
                }
            }
        }


        public void UpdateConsumption(string consumption, string registrationNumber)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;

            try
            {// string registrationNumber, string brand, string model, string version, int year, double batteryCapacity, double lastComsumptionReport

                myConn.Open();
                myTrans = myConn.BeginTransaction();
                
                string query = "UPDATE Vehicle SET lastConsumptionReport = @lastConsumptionReport  WHERE registrationNumber = @registrationNumber";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("registrationNumber", registrationNumber);
                myCommand.Parameters.AddWithValue("@lastConsumptionReport", consumption);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                logger.Info("UpdateConsumption vehicle  vehicle: "+registrationNumber+" updated\n");
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
                    logger.Error(ex,"UpdateConsumption vehicle Transaction vehicle: "+registrationNumber+" rolledback\n");
                }
                
                logger.Error(e,"UpdateConsumption vehicle Transaction vehicle: "+registrationNumber+" rolledback\n");               
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
                    logger.Error(e,"UpdateConsumption vehicle connection vehicle: "+registrationNumber+" not closed\n");
                    throw e;
                }
            }
        }


        public void Delete(string username, string registrationNumber){
            
            MySqlConnection myConn = new MySqlConnection(MyConnection);
            MySqlTransaction myTrans = null;
            
            try{
                myConn.Open();
                myTrans = myConn.BeginTransaction();
                    
                string query = "DELETE FROM Vehicle Where registrationNumber = @registrationNumber";
                MySqlCommand myCommand = new MySqlCommand(query, myConn, myTrans);
                myCommand.Parameters.AddWithValue("@registrationNumber", registrationNumber);
                //Deve faltar apagar tb da lista de veiculos deste utilizador este veiculo
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
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
                    logger.Error(ex,"Delete vehicle Transaction username: "+username+ ", vehicle: "+registrationNumber+" not rolledback\n");
                }
                logger.Error(e,"Delete vehicle Transaction username: "+username+ ", vehicle: "+registrationNumber+" rolledback\n");
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
                    logger.Error(e,"Delete vehicle connection username: "+username+ ", vehicle: "+registrationNumber+" not closed\n");
                    throw e;
                }
            }
        }
    }
}
