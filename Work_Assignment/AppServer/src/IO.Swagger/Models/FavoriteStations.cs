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
    public partial class FavoriteStations : IEquatable<FavoriteStations>
    { 
        /// <summary>
        /// Gets or Sets ChargingStationId
        /// </summary>
        [DataMember(Name="chargingStationId")]
        public string ChargingStationId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FavoriteStations {\n");
            sb.Append("  ChargingStationId: ").Append(ChargingStationId).Append("\n");
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
            return obj.GetType() == GetType() && Equals((FavoriteStations)obj);
        }

        /// <summary>
        /// Returns true if FavoriteStations instances are equal
        /// </summary>
        /// <param name="other">Instance of FavoriteStations to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FavoriteStations other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    ChargingStationId == other.ChargingStationId ||
                    ChargingStationId != null &&
                    ChargingStationId.Equals(other.ChargingStationId)
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
                    if (ChargingStationId != null)
                    hashCode = hashCode * 59 + ChargingStationId.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(FavoriteStations left, FavoriteStations right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FavoriteStations left, FavoriteStations right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
