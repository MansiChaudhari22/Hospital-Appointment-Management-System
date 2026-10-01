using CIE_2.Models;

namespace CIE_2.Extensions
{
    public static class DoctorExtensions
    {
        public static decimal CalculateTotalCharge(this Doctor doctor)
        {
            return doctor.ConsultationFee + 100;
        }
    }
}