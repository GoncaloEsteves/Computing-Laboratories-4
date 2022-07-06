using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace PGB.Models
{
    public partial class TripPlan
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("metadata")]
        public IternioMetadata Metadata { get; set; }

        [JsonProperty("result")]
        public IternioResult Result { get; set; }
    }

    public partial class IternioResult
    {
        [JsonProperty("plan_uuid")]
        public Guid PlanUuid { get; set; }

        [JsonProperty("car_model")]
        public string CarModel { get; set; }

        [JsonProperty("routes")]
        public IternioRoute[] Routes { get; set; }

        [JsonProperty("path_indices")]
        public Dictionary<string, long> PathIndices { get; set; }

        [JsonProperty("plan_log_id")]
        public long PlanLogId { get; set; }
    }

    public partial class IternioRoute
    {
        [JsonProperty("steps")]
        public IternioStep[] Steps { get; set; }

        [JsonProperty("total_dist")]
        public long TotalDist { get; set; }

        [JsonProperty("total_charge_duration")]
        public long TotalChargeDuration { get; set; }

        [JsonProperty("total_drive_duration")]
        public long TotalDriveDuration { get; set; }

        [JsonProperty("average_consumption")]
        public double AverageConsumption { get; set; }
    }

    public partial class IternioStep
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("utc_offset")]
        public long UtcOffset { get; set; }

        [JsonProperty("wp_type")]
        public long WpType { get; set; }

        [JsonProperty("is_charger")]
        public bool IsCharger { get; set; }

        [JsonProperty("is_station")]
        public bool IsStation { get; set; }

        [JsonProperty("is_end_station")]
        public bool IsEndStation { get; set; }

        [JsonProperty("charger_type")]
        public IternioChargerType ChargerType { get; set; }

        [JsonProperty("is_waypoint")]
        public bool IsWaypoint { get; set; }

        [JsonProperty("is_new_waypoint")]
        public bool IsNewWaypoint { get; set; }

        [JsonProperty("waypoint_idx", NullValueHandling = NullValueHandling.Ignore)]
        public long? WaypointIdx { get; set; }

        [JsonProperty("is_amenity_charger")]
        public bool IsAmenityCharger { get; set; }

        [JsonProperty("is_destcharger")]
        public bool IsDestcharger { get; set; }

        [JsonProperty("arrival_perc")]
        public long ArrivalPerc { get; set; }

        [JsonProperty("departure_perc")]
        public long DeparturePerc { get; set; }

        [JsonProperty("departure_duration")]
        public long DepartureDuration { get; set; }

        [JsonProperty("departure_dist")]
        public long DepartureDist { get; set; }

        [JsonProperty("arrival_dist")]
        public long ArrivalDist { get; set; }

        [JsonProperty("arrival_duration")]
        public long ArrivalDuration { get; set; }

        [JsonProperty("max_speed")]
        public double MaxSpeed { get; set; }

        [JsonProperty("is_mod_speed")]
        public bool IsModSpeed { get; set; }

        [JsonProperty("is_valid_step")]
        public bool IsValidStep { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("drive_duration", NullValueHandling = NullValueHandling.Ignore)]
        public long? DriveDuration { get; set; }

        [JsonProperty("wait_duration", NullValueHandling = NullValueHandling.Ignore)]
        public long? WaitDuration { get; set; }

        [JsonProperty("drive_dist", NullValueHandling = NullValueHandling.Ignore)]
        public long? DriveDist { get; set; }

        [JsonProperty("charge_duration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ChargeDuration { get; set; }

        [JsonProperty("charge_energy", NullValueHandling = NullValueHandling.Ignore)]
        public double? ChargeEnergy { get; set; }

        [JsonProperty("charge_cost", NullValueHandling = NullValueHandling.Ignore)]
        public double? ChargeCost { get; set; }

        [JsonProperty("charge_cost_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string ChargeCostCurrency { get; set; }

        [JsonProperty("charger", NullValueHandling = NullValueHandling.Ignore)]
        public IternioCharger Charger { get; set; }
    }

    public partial class IternioCharger
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("url")]
        public object Url { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("outlets")]
        public IternioOutlet[] Outlets { get; set; }

        [JsonProperty("locationid")]
        public string Locationid { get; set; }
    }

    public partial class IternioOutlet
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stalls")]
        public long Stalls { get; set; }

        [JsonProperty("power")]
        public double Power { get; set; }

        [JsonProperty("status")]
        public object Status { get; set; }
    }

    public partial struct IternioChargerType
    {
        public long? Integer;
        public string String;

        public static implicit operator IternioChargerType(long Integer) => new IternioChargerType { Integer = Integer };
        public static implicit operator IternioChargerType(string String) => new IternioChargerType { String = String };
    }
}