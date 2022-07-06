using System;
using System.Collections.Generic;
using System.Text;
using Data.Classes;

namespace Data.DataAcess
{
    public class DAOController
    {
        public DAOController()
        {
            Users = new UserDAO(MyConnection);
            Vehicles = new VehicleDAO(MyConnection);
            Trips = new TripDAO(MyConnection);
            ChargingStations = new ChargingStation(MyConnection);
        }

        //Coneccao a base de dados
        public static string MyConnection = "server=li4pgbdb.mysql.database.azure.com;user=li4pdbadmin@li4pgbdb;database=plugandgobeyonddb;port=3306;password=li42019-2020";



        //Todos os utilizadores
        private UserDAO Users;
        //Todas as viaturas
        private VehicleDAO Vehicles;
        //Todas as viagens
        private TripDAO Trips;
        //Todos os postos
        private ChargingStation ChargingStations;
    }
}
