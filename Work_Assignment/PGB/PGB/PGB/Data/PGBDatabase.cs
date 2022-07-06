using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGB.Data.Models;
using PGB.Models;
using SQLite;
using Xamarin.Forms.Internals;

namespace PGB.Data
{
    public class PGBDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => 
            new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags));

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;
        
        public PGBDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        static async Task InitializeAsync()
        {
            // Responsible for creating the tables if they dont exist
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ChargingStationDB).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ChargingStationDB)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ConnectorDB).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ConnectorDB)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<int> SaveChargingStationAsync(ExternalChargingStationOCM station)
        {
            Log.Warning(Constants.Tag, "InsertChargingStation");

            ChargingStationDB chargingStationDb = new ChargingStationDB();

            if (station.AddressInfo != null && station.OperatorInfo != null && station.AddressInfo.Title != null && station.OperatorInfo.Title != null)
            {
                chargingStationDb.Id = station.Id;
                chargingStationDb.Latitude = station.AddressInfo.Latitude;
                chargingStationDb.Longitude = station.AddressInfo.Longitude;
                chargingStationDb.StationName = station.AddressInfo.Title;
                chargingStationDb.ChargingStationOperator = station.OperatorInfo.Title;

                for (int i = 0; i < station.Connections.Count; i++)
                {
                    ConnectorDB connectorDB = new ConnectorDB
                    {
                        Id = station.Connections[i].Id,
                        IsFastChargeCapable = station.Connections[i].Level.IsFastChargeCapable,
                        IsOperational = station.Connections[i].StatusType.IsOperational,
                        StationId = station.Id
                    };

                    SaveConnectorAsync(connectorDB);
                }
            }

            return Database.InsertAsync(chargingStationDb);
        }
        
        public Task<int> SaveChargingStationsAsync(IList<ExternalChargingStationOCM> stations)
        {
            List<ChargingStationDB> chargingStations = new List<ChargingStationDB>();
            List<ConnectorDB> connectorDbs = new List<ConnectorDB>();
            
            foreach (var stationOcm in stations){
                var chargingStationDb = new ChargingStationDB();
                if (stationOcm.AddressInfo != null && stationOcm.OperatorInfo != null && stationOcm.AddressInfo.Title != null
                    && stationOcm.OperatorInfo.Title != null)
                {
                    chargingStationDb.Id = stationOcm.Id;
                    chargingStationDb.Latitude = stationOcm.AddressInfo.Latitude;
                    chargingStationDb.Longitude = stationOcm.AddressInfo.Longitude;
                    chargingStationDb.StationName = stationOcm.AddressInfo.Title;
                    chargingStationDb.ChargingStationOperator = stationOcm.OperatorInfo.Title;

                    for (int i = 0; i < stationOcm.Connections.Count; i++)
                    {
                        var connectorDb = new ConnectorDB
                        {
                            Id = stationOcm.Connections[i].Id,
                            IsFastChargeCapable = stationOcm.Connections[i].Level.IsFastChargeCapable,
                            IsOperational = stationOcm.Connections[i].StatusType.IsOperational,
                            StationId = stationOcm.Id
                        };
                        connectorDbs.Add(connectorDb);
                    }
                }

                chargingStations.Add(chargingStationDb);
            }


            Database.InsertAllAsync(connectorDbs);
            return Database.InsertAllAsync(chargingStations);
        }

        public Task<int> SaveConnectorsAsync(IEnumerable<ConnectorDB> connectorsDBs)
        {
            foreach (ConnectorDB connector in connectorsDBs)
            {
                SaveConnectorAsync(connector);
            }

            Log.Warning(Constants.Tag, "InsertConnectors");
            return null;
        }

        private static Task<int> SaveConnectorAsync(ConnectorDB connectorDB)
        {
            Log.Warning(Constants.Tag, "InsertConnector");
            return Database.InsertAsync(connectorDB);
        }

        /// <summary>
        /// Get a charging station by a given id
        /// </summary>
        /// <returns></returns>
        public async Task<ExternalChargingStationOCM> GetChargingStationAsync(long Id)
        {
            ChargingStationDB chargingStationDB = await Database.Table<ChargingStationDB>().Where(i => i.Id == Id).FirstOrDefaultAsync();
            
            return await CreateExternalChargingStationAsync(chargingStationDB);
        }

        public async Task<List<ExternalChargingStationOCM>> GetChargingStationsAsync()
        {
            List<ChargingStationDB> chargingStationDBs = await Database.Table<ChargingStationDB>().ToListAsync();
            List<ExternalChargingStationOCM> output = new List<ExternalChargingStationOCM>();

            foreach (ChargingStationDB stationDB in chargingStationDBs)
            {
                output.Add(await CreateExternalChargingStationAsync(stationDB));
            }
            return output;
        }

        private static async Task<List<Connection>> GetConnectionsAsync(long chargingStationId)
        {
            List<Connection> output = new List<Connection>();
            List<ConnectorDB> connectors = await Database.QueryAsync<ConnectorDB>
                ("SELECT * FROM [ConnectorDB] WHERE [StationId] = ?", chargingStationId);

            foreach (ConnectorDB connectorDB in connectors)
            {
                Connection connection = new Connection
                {
                    Id = connectorDB.Id,
                    Level = new Level()
                };
                connection.Level.IsFastChargeCapable = connectorDB.IsFastChargeCapable;
                connection.StatusType = new StatusType
                {
                    IsOperational = connectorDB.IsOperational
                };
                output.Add(connection);
            }
            return output;
        }

        public int DatabaseSize()
        {
            return Database.Table<ChargingStationDB>().CountAsync().Result;
        }

        private static async Task<ExternalChargingStationOCM> CreateExternalChargingStationAsync(ChargingStationDB stationDB)
        {
            ExternalChargingStationOCM stationOCM = new ExternalChargingStationOCM
            {
                Id = stationDB.Id,
                AddressInfo = new AddressInfo
                {
                    Latitude = stationDB.Latitude,
                    Longitude = stationDB.Longitude,
                    Title = stationDB.StationName
                },
                OperatorInfo = new OperatorInfo
                {
                    Title = stationDB.ChargingStationOperator
                },
                Connections = await GetConnectionsAsync(stationDB.Id)
            };
            return stationOCM;
        }
    }
}
