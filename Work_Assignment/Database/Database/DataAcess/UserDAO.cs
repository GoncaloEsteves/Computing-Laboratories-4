using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Classes;

namespace Data.DataAcess
{
    public class UserDAO
    {
        public UserDAO(string con)
        {
            MyConnection = con;
        }

        private string MyConnection;

        public void Put(Utilizador util)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            { //User(string name, string username, string nif, string password, List<CreditCard> creditCards, List<string> vehiclesIds)
                myConn.Open();
                string query = "INSERT INTO Utilizador (name, username, nif, password, creditCard) VALUES (@nome, @nif, @username, @pass, @creditCard)";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                
                myCommand.Parameters.AddWithValue("@nome", util.Name);
                myCommand.Parameters.AddWithValue("@nif", util.Username);
                myCommand.Parameters.AddWithValue("@nif", util.Nif);
                myCommand.Parameters.AddWithValue("@pass", util.Password);

               

                foreach (string vi in util.VehiclesIds)
                {
                    query = "INSERT INTO UserVehicles (User_username, Vehicle_vehiclesIds) VALUES (@username,@vehiclesIds)";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@username", util.Username);
                    myCommand.Parameters.AddWithValue("@vehiclesIds", vi);
                    myCommand.ExecuteNonQuery();
                }

                myCommand.ExecuteNonQuery();

                foreach (string cc in util.CreditCards)
                {
                    query = "INSERT INTO UserCreditCard (User_username, User_creditCard) VALUES (@username, @creditCard)";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@username", util.Username);
                    myCommand.Parameters.AddWithValue("@creditCard", cc);
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

        public User Get(string email)
        {
            User util = new User();
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {
                myConn.Open();
                string query = "SELECT * FROM User WHERE username = @name";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@username", util.Username);
                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    util.Nif = myReader.GetString("nif");
                    util.Name = myReader.GetString("name");
                    util.Username = myReader.GetString("username");
                    util.Email = myReader.GetString("email");
                    util.Password = myReader.GetString("password");
                   

                    myReader.Close();

                    query = "SELECT vehicle_matricula FROM UserVehicles WHERE User_username = @username";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@username", util.Username);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        util.VehicleIds.Add(myReader.GetString("vehicle_matricula"));
                    }


                    query = "SELECT user_creditCard FROM UserCreditCard WHERE User_username = @username";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@username", util.Username);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        util.CreditCards.Add(myReader.GetString("user_creditCard"));
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

            return util;
        } 

        public void Update(User util)
        {
            MySqlConnection myConn = new MySqlConnection(MyConnection);

            try
            {//User(string name, string username, string nif, string password, List<CreditCard> creditCards, List<string> vehiclesIds)
                myConn.Open();
                string query = "UPDATE User SET nome = @nome, email = @email, password = @pass, creditCards = @creditCard, vehiclesIds = @vehiclesIds WHERE username = @username";
                MySqlCommand myCommand = new MySqlCommand(query, myConn);
                myCommand.Parameters.AddWithValue("@name", util.Name);
                myCommand.Parameters.AddWithValue("@email", util.Email);
                myCommand.Parameters.AddWithValue("@pass", util.Password);
                myCommand.Parameters.AddWithValue("@creditCards", util.CreditCards);
                myCommand.Parameters.AddWithValue("@vehicleIds", util.VehicleIds);
                myCommand.Parameters.AddWithValue("@nif", util.NIF);
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
                    util.Nif = myReader.GetString("nif");
                    util.Email = myReader.GetString("email");
                    util.Password = myReader.GetString("password");
                    util.CreditCards = myReader.GetString("creditCards");
                    util.VehiclesId = myReader.GetString("vehiclesId");
                    users.Add(util);
                }

                myReader.Close();

                foreach(User util in users)
                {//SELECT vehicle_matricula FROM UserVehicles
                    query = "SELECT vehicle_matricula FROM UserVehicles WHERE User_username = @username";
                    myCommand = new MySqlCommand(query, myConn);
                    myCommand.Parameters.AddWithValue("@username", util.username);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        util.VehiclesId.Add(myReader.GetString("vehicle_matricula"));
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

            return users;
        }
    }

   //TODO: FazerDelete
}
