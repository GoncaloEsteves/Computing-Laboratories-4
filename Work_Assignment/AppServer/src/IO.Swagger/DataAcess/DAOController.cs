using System;
using System.Collections.Generic;
using System.Text;
using IO.Swagger.Models;

namespace IO.Swagger.DataAcess
{
    public class DAOController
    {
        public DAOController()
        {
            Users = new UserDAO(MyConnection);
            Vehicles = new VehicleDAO(MyConnection);
            Trips = new TripDAO(MyConnection);
            ChargingStations = new ChargingStationDAO(MyConnection);
            VehicleModels = new VehicleModelsDAO(MyConnection);
            ChargingStationWaitingValidation = new ChargingStationWaitingValidationDAO(MyConnection);
        }

        //Coneccao a base de dados
        public static string MyConnection = "server=localhost;user=PlugAndGoBeyond;database=plugandgobeyonddb;port=3306;password=basededadosLI4";
        //"server=li4pgbdb.mysql.database.azure.com;user=li4pdbadmin@li4pgbdb;database=plugandgobeyonddb;port=3306;password=li42019-2020"



        //Todos os utilizadores
        private UserDAO Users;
        //Todas as viaturas
        private VehicleDAO Vehicles;
        //All the Vehicle Models
        private VehicleModelsDAO VehicleModels;
        //Todas as viagens
        private TripDAO Trips;
        //Todos os postos
        private ChargingStationDAO ChargingStations;
        // Todas Charging Station Waiting for Validation 
        private ChargingStationWaitingValidationDAO ChargingStationWaitingValidation;
    }
}
