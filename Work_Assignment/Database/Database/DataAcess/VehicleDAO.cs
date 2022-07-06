using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Classes;

namespace Data.DataAcess
{
    public class VehicleDAO
    {
        public VehicleDAO(string con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        public void Put(Vehicle viat)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {// string registrationNumber, string brand, string model, string version, int year, double batteryCapacity, double lastComsumptionReport
                myConn.Open();
                string query = "INSERT INTO Vehicle (registrationNumber, brand, model, version, year, batteryCapacity, lastComsumptionReport) VALUES (@registrationNumber, @brand, @model, @version, @year, @batteryCapacity, @lastComsumptionReport)";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@registrationNumber", viat.RegistrationNumber);
                myCommand.Parameters.AddWithValue("@brand", viat.Brand);
                myCommand.Parameters.AddWithValue("@model", viat.Model);
                myCommand.Parameters.AddWithValue("@version", viat.Version);
                myCommand.Parameters.AddWithValue("@year", viat.Year);
                myCommand.Parameters.AddWithValue("@batteryCapacity",viat.BatteryCapacity);
                myCommand.Parameters.AddWithValue("@astComsumptionReport",viat.LastComsumptionReport);
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

        public Vehicle Get(string registrationNumber)
        {
            Vehicle via = new Vehicle();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM Vehicle WHERE registrationNumber = @mregistrationNumber";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@registrationNumber", registrationNumber);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    // string registrationNumber, string brand, string model, string version, int year, double batteryCapacity, double lastComsumptionReport

                    via.RegistrationNumber = myReader.GetString("registrationNumber");
                    via.Brand = myReader.GetString("brand");
                    via.Model = myReader.GetString("modelo");
                    via.Version = myReader.GetString("version");
                    via.Year = myReader.GetInt32("year");
                    via.BatteryCapacity = myReader.GetDouble("batteryCapacity");
                    via.LastComsumptionReport = myReader.GetDouble("lastComsumptionReport");

                    myReader.Close();
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

        public void Update(Vehicle via)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {// string registrationNumber, string brand, string model, string version, int year, double batteryCapacity, double lastComsumptionReport

                myConn.Open();
                string query = "UPDATE Vehicle SET registrationNumber = @registrationNumber, brand = @brand, model = @model, version = @version, year = @year, batteryCapacity = @batteryCapacity , lastComsumptionReport = @lastComsumptionReport  WHERE registrationNumber = @registrationNumber";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("registrationNumber", via.RegistrationNumber);
                myCommand.Parameters.AddWithValue("@brand", via.Brand);
                myCommand.Parameters.AddWithValue("@model", via.Model);
                myCommand.Parameters.AddWithValue("@version", via.Version);
                myCommand.Parameters.AddWithValue("@year", via.Year);
                myCommand.Parameters.AddWithValue("@batteryCapacity", via.BatteryCapacity);
                myCommand.Parameters.AddWithValue("@lastComsumptionReport", via.LastComsumptionReport);

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
    }
}
