using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MultiStepForm.Models;

namespace MultiStepForm.Pages
{
    public partial class AddressPage : Page
    {
        public AddressPage()
        {
            InitializeComponent();

            CityBox.Text = FormData.Current.City;
            StreetBox.Text = FormData.Current.Street;
            HouseBox.Text = FormData.Current.HouseNumber;
            ApartmentBox.Text = FormData.Current.ApartmentNumber;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем введённое
            FormData.Current.City = CityBox.Text.Trim();
            FormData.Current.Street = StreetBox.Text.Trim();
            FormData.Current.HouseNumber = HouseBox.Text.Trim();
            FormData.Current.ApartmentNumber = ApartmentBox.Text.Trim();

            if (NavigationService?.CanGoBack == true)
                NavigationService.GoBack();
            else
                NavigationService?.Navigate(new ContactDataPage());
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Финальная валидация
            if (string.IsNullOrWhiteSpace(CityBox.Text))
            {
                MessageBox.Show("Введите город.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(StreetBox.Text))
            {
                MessageBox.Show("Введите улицу.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(HouseBox.Text))
            {
                MessageBox.Show("Введите номер дома.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Сохраняем данные
            FormData.Current.City = CityBox.Text.Trim();
            FormData.Current.Street = StreetBox.Text.Trim();
            FormData.Current.HouseNumber = HouseBox.Text.Trim();
            FormData.Current.ApartmentNumber = ApartmentBox.Text.Trim();

            // Формируем итоговое сообщение
            var sb = new StringBuilder();
            sb.AppendLine("Проверьте введённые данные:");
            sb.AppendLine();
            sb.AppendLine("— Личные данные —");
            sb.AppendLine($"Имя: {FormData.Current.FirstName}");
            sb.AppendLine($"Фамилия: {FormData.Current.LastName}");
            sb.AppendLine($"Дата рождения: {FormData.Current.BirthDate:dd.MM.yyyy}");
            sb.AppendLine();
            sb.AppendLine("— Контактные данные —");
            sb.AppendLine($"Email: {FormData.Current.Email}");
            sb.AppendLine($"Телефон: {FormData.Current.Phone}");
            sb.AppendLine();
            sb.AppendLine("— Адрес —");
            sb.AppendLine($"Город: {FormData.Current.City}");
            sb.AppendLine($"Улица: {FormData.Current.Street}");
            sb.AppendLine($"Дом: {FormData.Current.HouseNumber}");
            if (!string.IsNullOrWhiteSpace(FormData.Current.ApartmentNumber))
                sb.AppendLine($"Квартира: {FormData.Current.ApartmentNumber}");

            MessageBox.Show(sb.ToString(), "Данные формы",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}