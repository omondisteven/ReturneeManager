using System;
namespace ReturneeManager.Application.Features.Persons.Queries.GetAllPaged
{
    public class GetAllPagedPersonsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }       

        //IdType
        public string IdType { get; set; }
        public int IdTypeId { get; set; }

        //Gender
        public string Gender { get; set; }
        public int GenderId { get; set; }

        //District
        public string District { get; set; }
        public int DistrictId { get; set; }

        //Division
        public string Division { get; set; }
        public int DivisionId { get; set; }

        //Upazila
        public string Upazila { get; set; }
        public int UpazilaId { get; set; }

        //Ward
        public string Ward { get; set; }
        public int WardId { get; set; }

        //FromCountry
        public string FromCountry { get; set; }
        public int FromCountryId { get; set; }

        //Other non-lookup fields
        public string IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string MobileNumber { get; set; }
        public string HouseVillage { get; set; }
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string ReturnReason { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string ReturnDocument { get; set; }

        //Permanent Address
        public bool SameAddress { get; set; }
        public string HouseVillage2 { get; set; }
        public string StreetAddress2 { get; set; }

        //District
        public int DistrictId2 { get; set; }

        //Division
        public int DivisionId2 { get; set; }

        //Upazila
        public int UpazilaId2 { get; set; }

        //Ward
        public int WardId2 { get; set; }

        public string PostCode2 { get; set; }
    }


    
}
