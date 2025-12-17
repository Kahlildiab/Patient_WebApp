using Patient_WebApp.Models;
using System;
using System.ComponentModel.DataAnnotations;
using WebApp_Patient.Models;

public class Patient
{
    [Key]
    public int PatientId { get; set; }

    [Required(ErrorMessage = "File No is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "File No cannot be empty")]
    public string FileNo { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    [StringLength(50)]
    public string FirstName { get; set; }

   [Required(ErrorMessage = "Second Name is required")]
    [StringLength(50)]
    public string SecondName { get; set; }

   [Required(ErrorMessage = "Third Name is required")]
    [StringLength(50)]
    public string ThirdName { get; set; }

[Required(ErrorMessage = "Family Name is required")]
    [StringLength(50)]
    public string FamilyName { get; set; }

    [StringLength(20)]
    [RegularExpression(@"^\d+$", ErrorMessage = "National Number must contain digits only")]
    public string NationalNo { get; set; }

    [StringLength(150)]
   [Required(ErrorMessage = "Mother Name is required")]
    public string MotherName { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DOB { get; set; }

    public int? Age { get; set; }

    [StringLength(150, ErrorMessage = "Place of Birth must not exceed 150 characters")]
    [RegularExpression(@"^[a-zA-Z\u0600-\u06FF\s\-]+$", ErrorMessage = "Place of Birth can contain letters only")]
    public string PlaceOfBirth { get; set; }

    [RegularExpression(@"^9627[789]\d{7}$",
        ErrorMessage = "Example: 962799999999")]
    [StringLength(15)]
    public string TelephoneNo { get; set; }

    [StringLength(150, ErrorMessage = "Email must not exceed 150 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

   [Required(ErrorMessage = "Gender is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Select Gender")]
    public string Gender { get; set; }

   [Required(ErrorMessage = "Nationality is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select Nationality")]
    public string Nationality { get; set; }

    [StringLength(500)]
    public string Comments { get; set; }
    public virtual ICollection<PatientReference> PatientReferences { get; set; }
}