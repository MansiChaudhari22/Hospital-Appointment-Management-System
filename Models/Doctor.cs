// Import the System namespace for basic data types.
using System;


// Define the namespace for the Doctor model.
namespace CIE_2.Models
{
   // Define the Doctor class to store doctor information.
   public class Doctor
   {
       // Store the unique identification number of the doctor.
       public int DoctorId { get; set; }


       // Store the name of the doctor.
       public string DoctorName { get; set; } = string.Empty;


       // Store the doctor's medical specialization.
       public string Specialization { get; set; } = string.Empty;


       // Store the number of years of professional experience.
       public int Experience { get; set; }


       // Store the basic consultation fee of the doctor.
       public decimal ConsultationFee { get; set; }
   }
}


