using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MultiStepForm.Models;

namespace MultiStepForm.Pages
{
    public partial class ContactDataPage : Page
    {
        public ContactDataPage()
        {
            InitializeComponent();

            EmailBox.Text = FormData.Current.Email;
            PhoneBox.Text = FormData.Current.Phone;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем введённое, чтобы не потерять при возврате
            FormData.Current.Email = EmailBox.Text.Trim();
            FormData.Current.Phone = PhoneBox.Text.Trim();

            if (NavigationService?.CanGoBack == true)
                NavigationService.GoBack();
            else
                NavigationService?.Navigate(new PersonalDataPage());
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text.Trim();
            string phone = PhoneBox.Text.Trim();

            // Проверка email
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Введите корректный email (пример: user@example.com).",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка телефона (только цифры, +, -, пробелы, скобки; 10–15 цифр)
            if (!IsValidPhone(phone))
            {
                MessageBox.Show("Введите корректный номер телефона (10–15 цифр).",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            FormData.Current.Email = email;
            FormData.Current.Phone = phone;

            NavigationService?.Navigate(new AddressPage());
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            // Простой, но рабочий шаблон
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }

        private static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;

            // Допускаем цифры, +, -, пробелы, скобки
            if (!Regex.IsMatch(phone, @"^[0-9\+\-\s\(\)]+$"))
                return false;

            // Количество цифр должно быть от 10 до 15
            string digits = Regex.Replace(phone, @"\D", "");
            return digits.Length >= 10 && digits.Length <= 15;
        }
    }
}