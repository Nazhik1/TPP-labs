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
        private int n = 1;
        public MainWindow()
        {
            InitializeComponent();
            string baseDirectory = @"C:\Users\Dmitriy\AppData\Local\lab3";

            filesFolder = System.IO.Path.Combine(baseDirectory, "Data");
            LoadFile();
        }

        public void Del(object sender, RoutedEventArgs e)
        {
                MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить этот элемент?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Del1();
                }
        }

        public void Del1()
        {
            if (Zam.SelectedItem != null)
            {
                string fileName = Zam.SelectedItem.ToString();
                string fullPath = Path.Combine(filesFolder, fileName) + ".txt";
                File.Delete(fullPath);
                LoadFile();
            }       
            
        }
        public void LoadFile()
        {
            Zam.Items.Clear();
            string[] txtFiles = Directory.GetFiles(filesFolder, "*.txt");
 

            foreach (string file in txtFiles)
            {
                string files = file.Remove(file.Length - 4);
                Zam.Items.Add(System.IO.Path.GetFileName(files));
            }
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            while (n > 0)
            {
                string fileName = "Заметка" + n.ToString() + ".txt";
                string fullPath = Path.Combine(filesFolder, fileName);
                if (File.Exists(fullPath))
                {
                    n += 1;
                }
                else
                {
                    File.WriteAllText(fullPath, "");
                    break;
                }
            }
            LoadFile();
            n += 1;
        }
    }
}
