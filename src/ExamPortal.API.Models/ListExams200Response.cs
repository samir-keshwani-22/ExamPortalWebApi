/*
 * Exam Portal API
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ExamPortal.API.Converters;

namespace ExamPortal.API.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ListExams200Response : IEquatable<ListExams200Response>
    {
        /// <summary>
        /// Gets or Sets Paging
        /// </summary>
        [DataMember(Name="paging", EmitDefaultValue=false)]
        public Paging Paging { get; set; }

        /// <summary>
        /// Gets or Sets Exams
        /// </summary>
        [DataMember(Name="exams", EmitDefaultValue=false)]
        public List<Exam> Exams { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ListExams200Response {\n");
            sb.Append("  Paging: ").Append(Paging).Append("\n");
            sb.Append("  Exams: ").Append(Exams).Append("\n");
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
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ListExams200Response)obj);
        }

        /// <summary>
        /// Returns true if ListExams200Response instances are equal
        /// </summary>
        /// <param name="other">Instance of ListExams200Response to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ListExams200Response other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Paging == other.Paging ||
                    Paging != null &&
                    Paging.Equals(other.Paging)
                ) && 
                (
                    Exams == other.Exams ||
                    Exams != null &&
                    other.Exams != null &&
                    Exams.SequenceEqual(other.Exams)
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
                    if (Paging != null)
                    hashCode = hashCode * 59 + Paging.GetHashCode();
                    if (Exams != null)
                    hashCode = hashCode * 59 + Exams.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ListExams200Response left, ListExams200Response right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ListExams200Response left, ListExams200Response right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
