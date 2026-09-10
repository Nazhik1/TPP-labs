using System;

namespace MultiStepForm.Models
{
    public class FormData
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }

        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string ApartmentNumber { get; set; } = string.Empty;

        public static FormData Current { get; } = new FormData();

        public static void Reset()
        {
            Current.FirstName = string.Empty;
            Current.LastName = string.Empty;
            Current.BirthDate = null;
            Current.Email = string.Empty;
            Current.Phone = string.Empty;
            Current.City = string.Empty;
            Current.Street = string.Empty;
            Current.HouseNumber = string.Empty;
            Current.ApartmentNumber = string.Empty;
        }
    }
}