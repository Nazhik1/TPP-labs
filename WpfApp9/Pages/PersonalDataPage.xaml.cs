using System.Windows;
using System.Windows.Controls;
using MultiStepForm.Models;

namespace MultiStepForm.Pages
{
    public partial class PersonalDataPage : Page
    {
        public PersonalDataPage()
        {
            InitializeComponent();

            // Восстанавливаем ранее введённые данные
            FirstNameBox.Text = FormData.Current.FirstName;
            LastNameBox.Text = FormData.Current.LastName;
            BirthDatePicker.SelectedDate = FormData.Current.BirthDate;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text))
            {
                MessageBox.Show("Введите имя.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(LastNameBox.Text))
            {
                MessageBox.Show("Введите фамилию.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (BirthDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату рождения.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Сохраняем данные
            FormData.Current.FirstName = FirstNameBox.Text.Trim();
            FormData.Current.LastName = LastNameBox.Text.Trim();
            FormData.Current.BirthDate = BirthDatePicker.SelectedDate;

            // Переход на следующую страницу
            NavigationService?.Navigate(new ContactDataPage());
        }
    }
}
