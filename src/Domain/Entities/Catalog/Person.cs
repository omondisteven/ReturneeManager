using System;
using ReturneeManager.Domain.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReturneeManager.Domain.Entities.Catalog
{
    public class Person : AuditableEntity<int>
    {
        public int IdTypeId { get; set; }
        public virtual IdType IdType { get; set; }
        public string IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Name { get; set; }
        public int GenderId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public virtual Gender Gender { get; set; }
        public string MobileNumber { get; set; }

        //Present Address
        public bool SameAddress { get; set; }
        public string HouseVillage { get; set; }
        public string StreetAddress { get; set; }
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public int UpazilaId { get; set; }
        public virtual Upazila Upazila { get; set; }
        public int WardId { get; set; }
        public virtual Ward Ward { get; set; }
        public string PostCode { get; set; }

        //Permanent Address
        public string HouseVillage2 { get; set; }
        public string StreetAddress2 { get; set; }
        public int DivisionId2 { get; set; }
        public int DistrictId2 { get; set; }
        public int UpazilaId2 { get; set; }
        public int WardId2 { get; set; }
        public string PostCode2 { get; set; }
        public int FromCountryId { get; set; }
        public virtual FromCountry FromCountry { get; set; }
        public string ReturnReason { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string ReturnDocument { get; set; }
        public string ImageDataURL { get; set; }
    }
}
