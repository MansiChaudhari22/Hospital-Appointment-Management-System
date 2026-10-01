// Import the ASP.NET Core MVC namespace for Controller and action methods.
using Microsoft.AspNetCore.Mvc;


// Import the memory caching namespace for IMemoryCache.
using Microsoft.Extensions.Caching.Memory;


// Import the application models.
using CIE_2.Models;


// Import the extension methods used by the application.
using CIE_2.Extensions;


// Define the namespace for application controllers.
namespace CIE_2.Controllers
{
   // Define the DoctorController class.
   public class DoctorController : Controller
   {
       // Declare the memory cache object used to cache doctors.
       private readonly IMemoryCache _memoryCache;


       // Declare the list of sample doctors.
       private readonly List<Doctor> _doctors;


       // Create the DoctorController constructor.
       public DoctorController(IMemoryCache memoryCache)
       {
           // Store the injected memory cache service.
           _memoryCache = memoryCache;


           // Create sample doctor records.
           _doctors = new List<Doctor>
           {
               // Create the first doctor.
               new Doctor
               {
                   // Set the doctor ID.
                   DoctorId = 1,


                   // Set the doctor name.
                   DoctorName = "Dr. Rajesh Patel",


                   // Set the specialization.
                   Specialization = "Cardiology",


                   // Set the experience.
                   Experience = 15,


                   // Set the consultation fee.
                   ConsultationFee = 1000
               },


               // Create the second doctor.
               new Doctor
               {
                   // Set the doctor ID.
                   DoctorId = 2,


                   // Set the doctor name.
                   DoctorName = "Dr. Priya Shah",


                   // Set the specialization.
                   Specialization = "Neurology",


                   // Set the experience.
                   Experience = 12,


                   // Set the consultation fee.
                   ConsultationFee = 1200
               },


               // Create the third doctor.
               new Doctor
               {
                   // Set the doctor ID.
                   DoctorId = 3,


                   // Set the doctor name.
                   DoctorName = "Dr. Amit Mehta",


                   // Set the specialization.
                   Specialization = "Orthopedics",


                   // Set the experience.
                   Experience = 10,


                   // Set the consultation fee.
                   ConsultationFee = 900
               },


               // Create the fourth doctor.
               new Doctor
               {
                   // Set the doctor ID.
                   DoctorId = 4,


                   // Set the doctor name.
                   DoctorName = "Dr. Neha Joshi",


                   // Set the specialization.
                   Specialization = "Dermatology",


                   // Set the experience.
                   Experience = 8,


                   // Set the consultation fee.
                   ConsultationFee = 700
               }
           };
       }


       // Display the list of doctors.
       public IActionResult Index()
       {
           // Define the cache key for the doctor list.
           const string cacheKey = "DoctorList";


           // Try to retrieve the doctor list from memory cache.
           if (!_memoryCache.TryGetValue(cacheKey, out List<Doctor>? doctors))
           {
               // Use the original doctor list when cache is empty.
               doctors = _doctors;


               // Create cache configuration.
               var cacheOptions = new MemoryCacheEntryOptions
               {
                   // Expire the cached doctor list after three minutes.
                   AbsoluteExpirationRelativeToNow =
                       TimeSpan.FromMinutes(3)
               };


               // Store the doctor list in memory cache.
               _memoryCache.Set(cacheKey, doctors, cacheOptions);
           }


           // Send the doctor list to the Index view.
           return View(doctors);
       }


       // Enable response caching for the doctor details page.
       [ResponseCache(
           Duration = 60,
           Location = ResponseCacheLocation.Any)]


       // Display details of a selected doctor.
       public IActionResult Details(int id)
       {
           // Find the doctor using the ID from the query string.
           var doctor = _doctors.FirstOrDefault(
               d => d.DoctorId == id);


           // Check whether the doctor exists.
           if (doctor == null)
           {
               // Return a 404 response when the doctor is not found.
               return NotFound();
           }


           // Calculate the total consultation charge.
           ViewBag.TotalCharge =
               doctor.CalculateTotalCharge();


           // Send the doctor information to the Details view.
           return View(doctor);
       }


       // Display the appointment form.
       [HttpGet]
       public IActionResult Appointment(int id)
       {
           // Find the selected doctor using the query string ID.
           var doctor = _doctors.FirstOrDefault(
               d => d.DoctorId == id);


           // Check whether the doctor exists.
           if (doctor == null)
           {
               // Return 404 if the doctor cannot be found.
               return NotFound();
           }


           // Store the doctor's specialization in session.
           HttpContext.Session.SetString(
               "SelectedSpecialization",
               doctor.Specialization);


           // Create an appointment model.
           var appointment = new Appointment
           {
               // Store the DoctorId in the appointment model.
               // This will later be submitted using a hidden field.
               DoctorId = doctor.DoctorId
           };


           // Pass the doctor information to the appointment view.
           ViewBag.Doctor = doctor;


           // Display the appointment form.
           return View(appointment);
       }


       // Process the submitted appointment form.
       [HttpPost]


       // Protect the form against Cross-Site Request Forgery attacks.
       [ValidateAntiForgeryToken]
       public IActionResult Appointment(Appointment appointment)
       {
           // Check whether the submitted form is valid.
           if (!ModelState.IsValid)
           {
               // Find the selected doctor again.
               var invalidDoctor = _doctors.FirstOrDefault(
                   d => d.DoctorId == appointment.DoctorId);


               // Pass the doctor back to the form.
               ViewBag.Doctor = invalidDoctor;


               // Return the form with validation errors.
            
               return View("Appointment", appointment);
           }


           // Store the patient's name in a browser cookie.
           Response.Cookies.Append(
               "PatientName",
               appointment.PatientName,
               new CookieOptions
               {
                   // Prevent JavaScript from accessing the cookie.
                   HttpOnly = true,


                   // Mark the cookie as essential.
                   IsEssential = true,


                   // Keep the cookie for one day.
                   Expires = DateTimeOffset.UtcNow.AddDays(1)
               });


           // Retrieve the selected specialization from session.
           var specialization =
               HttpContext.Session.GetString(
                   "SelectedSpecialization");


           // Find the selected doctor.
           var doctor = _doctors.FirstOrDefault(
               d => d.DoctorId == appointment.DoctorId);


           // Pass the doctor to the success page.
           ViewBag.Doctor = doctor;


           // Pass the specialization to the success page.
           ViewBag.Specialization = specialization;


           // Display a success message.
           ViewBag.Message =
               $"Appointment requested successfully for " +
               $"{appointment.PatientName}.";


           // Display the appointment success page.
           return View("AppointmentSuccess", appointment);
       }
   }
}
