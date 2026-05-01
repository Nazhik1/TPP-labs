using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string currentFilePath;
        private string filesFolder;
        public MainWindow()
        {
            InitializeComponent();
            string baseDirectory = @"C:\Users\Dmitriy\AppData\Local\lab3";

            filesFolder = System.IO.Path.Combine(baseDirectory, "Data");
            LoadFile();
        }

        public void Del(object sender, RoutedEventArgs e)
        {
            Zam.Items.Remove(Zam.SelectedItem);
        }
        public void LoadFile()
        {
            Zam.Items.Clear();
            string[] txtFiles = Directory.GetFiles(filesFolder, "*.txt");
 

            foreach (string file in txtFiles)
            {
                Zam.Items.Add(System.IO.Path.GetFileName(file));
            }
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            string fileName = "отчёт.txt";
            string fullPath = Path.Combine(filesFolder, fileName);

            File.WriteAllText(fullPath, "");
        }
    }
}
