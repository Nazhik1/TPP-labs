using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void got(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == "0" || tb.Text == "Введите пример:" || tb.Text == "Ошибка: деление на 0" || tb.Text == "Ошибка в примере")
            {
                tb.Text = "";
            }
        }

        public void ClickButton(object sender, RoutedEventArgs e)
        {
            Number.Focus();
            Button btn = sender as Button;
            string btn_content = btn.Content.ToString();
            switch (btn_content)
            {
                case "1": Number.Text += btn_content; break;
                case "2": Number.Text += btn_content; break;
                case "3": Number.Text += btn_content; break;
                case "4": Number.Text += btn_content; break;
                case "5": Number.Text += btn_content; break;
                case "6": Number.Text += btn_content; break;
                case "7": Number.Text += btn_content; break;
                case "9": Number.Text += btn_content; break;
                case "8": Number.Text += btn_content; break;
                case "0": Number.Text += btn_content; break;
                case "C": Number.Text = ""; break;
                case "+": Number.Text += btn_content; break;
                case "-": Number.Text += btn_content; break;
                case "*": Number.Text += btn_content; break;
                case "/": Number.Text += btn_content; break;
                case "=":
                    try
                    {
                        var result = new DataTable().Compute(Number.Text, null);
                        double Dres = Convert.ToDouble(result);
                        if (double.IsInfinity(Dres))
                        {
                            Number.Text = "Ошибка: деление на 0";
                        }
                        else { Number.Text = Dres.ToString(); }
                    }
                    catch (Exception) {
                        Number.Text = "Ошибка в примере";
                    }
                        break;

            }
        }
    }
    
}
