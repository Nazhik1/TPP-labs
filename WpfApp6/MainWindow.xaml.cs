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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

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
                case "C":
                    Number.Text = "";
                    AnimateClear();
                    break;
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
                            AnimateError();
                        }
                        else
                        {
                            Number.Text = Dres.ToString();
                            AnimateResult();
                        }
                    }
                    catch (Exception)
                    {
                        Number.Text = "Ошибка в примере";
                        AnimateError();
                    }
                    break;
            }
        }

        private void AnimateResult()
        {
            DoubleAnimation fade = new DoubleAnimation(1, 0.3, TimeSpan.FromSeconds(0.2));
            fade.AutoReverse = true;
            Number.BeginAnimation(TextBlock.OpacityProperty, fade);

            DoubleAnimation scaleX = new DoubleAnimation(1, 1.08, TimeSpan.FromSeconds(0.2));
            scaleX.AutoReverse = true;
            Number.BeginAnimation(ScaleTransform.ScaleXProperty, scaleX);

            DoubleAnimation scaleY = new DoubleAnimation(1, 1.08, TimeSpan.FromSeconds(0.2));
            scaleY.AutoReverse = true;
            Number.BeginAnimation(ScaleTransform.ScaleYProperty, scaleY);
        }

        private void AnimateError()
        {
            DoubleAnimation fade = new DoubleAnimation(1, 0.5, TimeSpan.FromSeconds(0.1));
            fade.AutoReverse = true;
            fade.RepeatBehavior = new RepeatBehavior(2);
            Number.BeginAnimation(TextBlock.OpacityProperty, fade);
        }

        private void AnimateClear()
        {
            DoubleAnimation fade = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.15));
            fade.AutoReverse = true;
            Number.BeginAnimation(TextBlock.OpacityProperty, fade);
        }
    }
}