// Define the namespace containing application models.
namespace CIE_2.Models
{
   // Define the Appointment class for storing appointment information.
   public class Appointment
   {
       // Store the selected doctor's ID.
       public int DoctorId { get; set; }


       // Store the patient's name.
       public string PatientName { get; set; } = string.Empty;


       // Store the patient's email address.
       public string Email { get; set; } = string.Empty;


       // Store the requested appointment date.
       public DateTime AppointmentDate { get; set; }
   }
}
