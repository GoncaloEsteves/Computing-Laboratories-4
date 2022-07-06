/*
 * PlugGoBeyond
 *
 * Rest API of the mobile app PlugGoBeyond of the project course LI IV in the program Mestrado Integrado em Engenharia Informática
 *
 * OpenAPI spec version: 1.0.0
 * Contact: a34900/a82888/a85731/a86618/a89982@alunos.uminho.pt
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class User : IEquatable<User>
    { 
        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Username
        /// </summary>
        [DataMember(Name="username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets Email
        /// </summary>
        [DataMember(Name="email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets Nif
        /// </summary>
        [DataMember(Name="nif")]
        public string Nif { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        [DataMember(Name="password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets CreditCards
        /// </summary>
        [DataMember(Name="creditCards")]
        public List<CreditCard> CreditCards { get; set; }

        /// <summary>
        /// Gets or Sets FavoriteChargingStations
        /// </summary>
        [DataMember(Name="favoriteChargingStations")]
        public List<string> FavoriteChargingStations { get; set; }

        /// <summary>
        /// Gets or Sets Trips
        /// </summary>
        [DataMember(Name="trips")]
        public List<int?> Trips { get; set; }

        /// <summary>
        /// Gets or Sets VehiclesIds
        /// </summary>
        [DataMember(Name="vehiclesIds")]
        public List<string> VehiclesIds { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class User {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Username: ").Append(Username).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  Nif: ").Append(Nif).Append("\n");
            sb.Append("  Password: ").Append(Password).Append("\n");
            sb.Append("  CreditCards: ").Append(CreditCards).Append("\n");
            sb.Append("  FavoriteChargingStations: ").Append(FavoriteChargingStations).Append("\n");
            sb.Append("  Trips: ").Append(Trips).Append("\n");
            sb.Append("  VehiclesIds: ").Append(VehiclesIds).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((User)obj);
        }

        /// <summary>
        /// Returns true if User instances are equal
        /// </summary>
        /// <param name="other">Instance of User to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) && 
                (
                    Username == other.Username ||
                    Username != null &&
                    Username.Equals(other.Username)
                ) && 
                (
                    Email == other.Email ||
                    Email != null &&
                    Email.Equals(other.Email)
                ) && 
                (
                    Nif == other.Nif ||
                    Nif != null &&
                    Nif.Equals(other.Nif)
                ) && 
                (
                    Password == other.Password ||
                    Password != null &&
                    Password.Equals(other.Password)
                ) && 
                (
                    CreditCards == other.CreditCards ||
                    CreditCards != null &&
                    CreditCards.SequenceEqual(other.CreditCards)
                ) && 
                (
                    FavoriteChargingStations == other.FavoriteChargingStations ||
                    FavoriteChargingStations != null &&
                    FavoriteChargingStations.SequenceEqual(other.FavoriteChargingStations)
                ) && 
                (
                    Trips == other.Trips ||
                    Trips != null &&
                    Trips.SequenceEqual(other.Trips)
                ) && 
                (
                    VehiclesIds == other.VehiclesIds ||
                    VehiclesIds != null &&
                    VehiclesIds.SequenceEqual(other.VehiclesIds)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (Username != null)
                    hashCode = hashCode * 59 + Username.GetHashCode();
                    if (Email != null)
                    hashCode = hashCode * 59 + Email.GetHashCode();
                    if (Nif != null)
                    hashCode = hashCode * 59 + Nif.GetHashCode();
                    if (Password != null)
                    hashCode = hashCode * 59 + Password.GetHashCode();
                    if (CreditCards != null)
                    hashCode = hashCode * 59 + CreditCards.GetHashCode();
                    if (FavoriteChargingStations != null)
                    hashCode = hashCode * 59 + FavoriteChargingStations.GetHashCode();
                    if (Trips != null)
                    hashCode = hashCode * 59 + Trips.GetHashCode();
                    if (VehiclesIds != null)
                    hashCode = hashCode * 59 + VehiclesIds.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
