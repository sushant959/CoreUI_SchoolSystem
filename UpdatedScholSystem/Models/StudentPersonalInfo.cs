using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class StudentPersonalInfo
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth_BS { get; set; }
        public string DateOfBirth_AD { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string FatherName { get; set; }
        public string FatherNumber { get; set; }
        public string MotherName { get; set; }
        public string MotherNumber { get; set; }
        public string Status { get; set; }
        public string StudentType { get; set; }
        public int CompanyId { get; set; }
        public List<StudentPastEducation> PastEducation { get; set; }
        public StudentCurrentEducation CurrentEducation { get; set; }
        public Address CurrentAddress { get; set; }
        public Address PermanentAddress { get; set; }
        public EmergencyContact EmergencyDetails { get; set; }
        public StudentScholarship Scholarship { get; set; }
        
    }

    public class StudentPastEducation
    {
        public int PastEducationId { get; set; }
        public string Degree { get; set; }
        public string School { get; set; }
        public int TotalMarks { get; set; }
        public string Division { get; set; }
        public string StudentId { get; set; }
        public int CompanyId { get; set; }
    }

    public class StudentCurrentEducation
    {
        public int CurrentEducationId { get; set; }
        public string Batch { get; set; }
        public string Faculty { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string StudentId { get; set; }
        public int CompanyId { get; set; }
    }

    public class Address
    {
        public int AddressId { get; set; }
        public string Qno { get; set; }
        public string Street { get; set; }
        public string Municipality { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string StudentId { get; set; }
        public int CompanyId { get; set; }
    }
    public class EmergencyContact
    {
        public int EmergencyContactId { get; set; }
        public string ParentName { get; set; }
        public string ContactNumber { get; set; }
        public string Location { get; set; }
        public string StudentId { get; set; }
        public int CompanyId { get; set; }
    }

    public class StudentScholarship
    {
        public int StudentScholarshipId { get; set; }
        public string ScholarshipName { get; set; }
        public string Description { get; set; }
        public string DateOfAdmission { get; set; }
        public string StudentId { get; set; }
        public int CompanyId { get; set; }
    }

}