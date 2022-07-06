namespace PGB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ExternalChargingStationOCM
    {
        [JsonProperty("DataProvider")]
        public DataProvider DataProvider { get; set; }

        [JsonProperty("OperatorInfo")]
        public OperatorInfo OperatorInfo { get; set; }

        [JsonProperty("UsageType")]
        public UsageType UsageType { get; set; }

        [JsonProperty("StatusType")]
        public StatusType StatusType { get; set; }

        [JsonProperty("SubmissionStatus")]
        public SubmissionStatus SubmissionStatus { get; set; }

        [JsonProperty("UserComments")]
        public object UserComments { get; set; }

        [JsonProperty("PercentageSimilarity")]
        public object PercentageSimilarity { get; set; }

        [JsonProperty("MediaItems")]
        public object MediaItems { get; set; }

        [JsonProperty("IsRecentlyVerified")]
        public bool IsRecentlyVerified { get; set; }

        [JsonProperty("DateLastVerified")]
        public object DateLastVerified { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("UUID")]
        public string Uuid { get; set; }

        [JsonProperty("ParentChargePointID")]
        public object ParentChargePointId { get; set; }

        [JsonProperty("DataProviderID")]
        public long DataProviderId { get; set; }

        [JsonProperty("DataProvidersReference")]
        public object DataProvidersReference { get; set; }

        [JsonProperty("OperatorID")]
        public long OperatorId { get; set; }

        [JsonProperty("OperatorsReference")]
        public object OperatorsReference { get; set; }

        [JsonProperty("UsageTypeID")]
        public long UsageTypeId { get; set; }

        [JsonProperty("UsageCost")]
        public object UsageCost { get; set; }

        [JsonProperty("AddressInfo")]
        public AddressInfo AddressInfo { get; set; }

        [JsonProperty("Connections")]
        public IList<Connection> Connections { get; set; }

        [JsonProperty("NumberOfPoints")]
        public long NumberOfPoints { get; set; }

        [JsonProperty("GeneralComments")]
        public object GeneralComments { get; set; }

        [JsonProperty("DatePlanned")]
        public object DatePlanned { get; set; }

        [JsonProperty("DateLastConfirmed")]
        public object DateLastConfirmed { get; set; }

        [JsonProperty("StatusTypeID")]
        public long StatusTypeId { get; set; }

        [JsonProperty("DateLastStatusUpdate")]
        public DateTimeOffset DateLastStatusUpdate { get; set; }

        [JsonProperty("MetadataValues")]
        public object MetadataValues { get; set; }

        [JsonProperty("DataQualityLevel")]
        public long DataQualityLevel { get; set; }

        [JsonProperty("DateCreated")]
        public DateTimeOffset DateCreated { get; set; }

        [JsonProperty("SubmissionStatusTypeID")]
        public long SubmissionStatusTypeId { get; set; }
    }

    public partial class AddressInfo
    {
        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("AddressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("AddressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("Town")]
        public string Town { get; set; }

        [JsonProperty("StateOrProvince")]
        public string StateOrProvince { get; set; }

        [JsonProperty("Postcode")]
        public string Postcode { get; set; }

        [JsonProperty("CountryID")]
        public long CountryId { get; set; }

        [JsonProperty("Country")]
        public Country Country { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("ContactTelephone1")]
        public object ContactTelephone1 { get; set; }

        [JsonProperty("ContactTelephone2")]
        public object ContactTelephone2 { get; set; }

        [JsonProperty("ContactEmail")]
        public object ContactEmail { get; set; }

        [JsonProperty("AccessComments")]
        public object AccessComments { get; set; }

        [JsonProperty("RelatedURL")]
        public object RelatedUrl { get; set; }

        [JsonProperty("Distance")]
        public object Distance { get; set; }

        [JsonProperty("DistanceUnit")]
        public long DistanceUnit { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("ISOCode")]
        public string IsoCode { get; set; }

        [JsonProperty("ContinentCode")]
        public string ContinentCode { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class Connection
    {
        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("ConnectionTypeID")]
        public long ConnectionTypeId { get; set; }

        [JsonProperty("ConnectionType")]
        public ConnectionType ConnectionType { get; set; }

        [JsonProperty("Reference")]
        public object Reference { get; set; }

        [JsonProperty("StatusTypeID")]
        public long StatusTypeId { get; set; }

        [JsonProperty("StatusType")]
        public StatusType StatusType { get; set; }

        [JsonProperty("LevelID")]
        public long LevelId { get; set; }

        [JsonProperty("Level")]
        public Level Level { get; set; }

        [JsonProperty("Amps")]
        public long Amps { get; set; }

        [JsonProperty("Voltage")]
        public long Voltage { get; set; }

        [JsonProperty("PowerKW")]
        public double PowerKw { get; set; }

        [JsonProperty("CurrentTypeID")]
        public long CurrentTypeId { get; set; }

        [JsonProperty("CurrentType")]
        public CurrentType CurrentType { get; set; }

        [JsonProperty("Quantity")]
        public long Quantity { get; set; }

        [JsonProperty("Comments")]
        public object Comments { get; set; }
    }

    public partial class ConnectionType
    {
        [JsonProperty("FormalName")]
        public string FormalName { get; set; }

        [JsonProperty("IsDiscontinued")]
        public bool IsDiscontinued { get; set; }

        [JsonProperty("IsObsolete")]
        public bool IsObsolete { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class CurrentType
    {
        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class Level
    {
        [JsonProperty("Comments")]
        public string Comments { get; set; }

        [JsonProperty("IsFastChargeCapable")]
        public bool IsFastChargeCapable { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class StatusType
    {
        [JsonProperty("IsOperational")]
        public bool IsOperational { get; set; }

        [JsonProperty("IsUserSelectable")]
        public bool IsUserSelectable { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class DataProvider
    {
        [JsonProperty("WebsiteURL")]
        public Uri WebsiteUrl { get; set; }

        [JsonProperty("Comments")]
        public object Comments { get; set; }

        [JsonProperty("DataProviderStatusType")]
        public DataProviderStatusType DataProviderStatusType { get; set; }

        [JsonProperty("IsRestrictedEdit")]
        public bool IsRestrictedEdit { get; set; }

        [JsonProperty("IsOpenDataLicensed")]
        public bool IsOpenDataLicensed { get; set; }

        [JsonProperty("IsApprovedImport")]
        public bool IsApprovedImport { get; set; }

        [JsonProperty("License")]
        public string License { get; set; }

        [JsonProperty("DateLastImported")]
        public object DateLastImported { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class DataProviderStatusType
    {
        [JsonProperty("IsProviderEnabled")]
        public bool IsProviderEnabled { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class OperatorInfo
    {
        [JsonProperty("WebsiteURL")]
        public Uri WebsiteUrl { get; set; }

        [JsonProperty("Comments")]
        public object Comments { get; set; }

        [JsonProperty("PhonePrimaryContact")]
        public object PhonePrimaryContact { get; set; }

        [JsonProperty("PhoneSecondaryContact")]
        public object PhoneSecondaryContact { get; set; }

        [JsonProperty("IsPrivateIndividual")]
        public bool IsPrivateIndividual { get; set; }

        [JsonProperty("AddressInfo")]
        public object AddressInfo { get; set; }

        [JsonProperty("BookingURL")]
        public object BookingUrl { get; set; }

        [JsonProperty("ContactEmail")]
        public object ContactEmail { get; set; }

        [JsonProperty("FaultReportEmail")]
        public object FaultReportEmail { get; set; }

        [JsonProperty("IsRestrictedEdit")]
        public object IsRestrictedEdit { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class SubmissionStatus
    {
        [JsonProperty("IsLive")]
        public bool IsLive { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class UsageType
    {
        [JsonProperty("IsPayAtLocation")]
        public bool IsPayAtLocation { get; set; }

        [JsonProperty("IsMembershipRequired")]
        public bool IsMembershipRequired { get; set; }

        [JsonProperty("IsAccessKeyRequired")]
        public bool IsAccessKeyRequired { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public partial class ExternalChargingStationOCM
    {
        public static ExternalChargingStationOCM FromJson(string json) => JsonConvert.DeserializeObject<ExternalChargingStationOCM>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ExternalChargingStationOCM self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}