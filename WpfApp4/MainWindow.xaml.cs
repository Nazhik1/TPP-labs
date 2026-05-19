using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }


        static Random rnd = new Random();
        static int number = rnd.Next(1,101);


        private string _x="?";
        public string X
        {
            get { return _x; }
            set
            { 
                _x = value;
                OnPropertyChanged();
            }
        }

        private int _try = 0;

        public int _Try
        {
            get { return _try; }
            set { _try = value; OnPropertyChanged(); }
        }

        private string res = "...";

        public string _Result
        {
            get { return res; }
            set { res = value; OnPropertyChanged(); }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int y;

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Number.Text !=null)
                {
                    _Try += 1;
                    y = int.Parse(Number.Text);
                    switch (y)
                    {
                        case int _y when _y == number:
                            _Result = "Вы угадали";
                            X=number.ToString();
                            Sav.IsEnabled = false;
                            break;
                        case int _y when _y < number:
                            _Result = "Слишком мало";
                            break;
                        case int _y when _y > number:
                            _Result = "Слишком много";
                            break;

                    }
                }
            }
            catch (Exception)
            {
                Number.Text = "Ошибка";
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            X = "?";
            _Try = 0;
            _Result = "...";
            Sav.IsEnabled = true;
            number = rnd.Next(1,101);
            Number.Text = "";
        }
    }
}