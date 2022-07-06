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
    public partial class Vehicle : IEquatable<Vehicle>
    { 
        /// <summary>
        /// Gets or Sets RegistrationNumber
        /// </summary>
        [DataMember(Name="registrationNumber")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or Sets TypeCode
        /// </summary>
        [DataMember(Name="typeCode")]
        public string TypeCode { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets LastConsumptionReport
        /// </summary>
        [DataMember(Name="lastConsumptionReport")]
        public double? LastConsumptionReport { get; set; }

        /// <summary>
        /// Gets or Sets Username
        /// </summary>
        [DataMember(Name="username")]
        public string Username { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Vehicle {\n");
            sb.Append("  RegistrationNumber: ").Append(RegistrationNumber).Append("\n");
            sb.Append("  TypeCode: ").Append(TypeCode).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  LastConsumptionReport: ").Append(LastConsumptionReport).Append("\n");
            sb.Append("  Username: ").Append(Username).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Vehicle)obj);
        }

        /// <summary>
        /// Returns true if Vehicle instances are equal
        /// </summary>
        /// <param name="other">Instance of Vehicle to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Vehicle other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    RegistrationNumber == other.RegistrationNumber ||
                    RegistrationNumber != null &&
                    RegistrationNumber.Equals(other.RegistrationNumber)
                ) && 
                (
                    TypeCode == other.TypeCode ||
                    TypeCode != null &&
                    TypeCode.Equals(other.TypeCode)
                ) && 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) && 
                (
                    LastConsumptionReport == other.LastConsumptionReport ||
                    LastConsumptionReport != null &&
                    LastConsumptionReport.Equals(other.LastConsumptionReport)
                ) && 
                (
                    Username == other.Username ||
                    Username != null &&
                    Username.Equals(other.Username)
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
                    if (RegistrationNumber != null)
                    hashCode = hashCode * 59 + RegistrationNumber.GetHashCode();
                    if (TypeCode != null)
                    hashCode = hashCode * 59 + TypeCode.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (LastConsumptionReport != null)
                    hashCode = hashCode * 59 + LastConsumptionReport.GetHashCode();
                    if (Username != null)
                    hashCode = hashCode * 59 + Username.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Vehicle left, Vehicle right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vehicle left, Vehicle right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}