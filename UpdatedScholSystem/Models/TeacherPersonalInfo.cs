using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class TeacherPersonalInfo
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Faculty { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string Batch { get; set; }
        public string Citizenship { get; set; }
        public string JoiningDate { get; set; }
        public string Status { get; set; }
        public int CompanyId { get; set; }
        public List<Education> Educations { get; set; }
        public List<Experience> Experiences { get; set; }
        public TeacherAddress CurrentAddress { get; set; }
        public TeacherAddress PermanentAddress { get; set; }
    }

    public class Education
    {
        public int EducationId { get; set; }
        public string Degree { get; set; }
        public string Institution { get; set; }
        public int TotalMarks { get; set; }
        public string Division { get; set; }
        public int TeacherId { get; set; }
        public int CompanyId { get; set; }
    }

    public class Experience
    {
        public int ExperienceId { get; set; }
        public string Organisation { get; set; }
        public string Post { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int TeacherId { get; set; }
        public int CompanyId { get; set; }
    }

    public class TeacherAddress
    {
        public int AddressId { get; set; }
        public string HouseNo { get; set; }
        public string Street { get; set; }
        public string Municipality { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public int TeacherId { get; set; }
        public int CompanyId { get; set; }
    }

}