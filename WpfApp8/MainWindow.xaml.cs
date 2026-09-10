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
using System.IO;
using Microsoft.Win32;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        private string currentFilePath = null;
        private bool isModified = false;

        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
                (s, e) => NewFile_Click(s, e)));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open,
                (s, e) => OpenFile_Click(s, e)));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save,
                (s, e) => SaveFile_Click(s, e)));

            UpdateTitle();
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfirmSave()) return;

            MainTextBox.Clear();
            currentFilePath = null;
            isModified = false;
            UpdateTitle();
        }
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfirmSave()) return;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Открыть файл"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    MainTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                    currentFilePath = openFileDialog.FileName;
                    isModified = false;
                    UpdateTitle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (currentFilePath == null)
            {
                SaveAsFile_Click(sender, e);
            }
            else
            {
                SaveToFile(currentFilePath);
            }
        }
        private void SaveAsFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Сохранить как",
                FileName = currentFilePath != null ?
                    Path.GetFileName(currentFilePath) : "Без имени.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveToFile(saveFileDialog.FileName);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmSave())
            {
                Application.Current.Shutdown();
            }
        }

        private void MainTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!isModified)
            {
                isModified = true;
                UpdateTitle();
            }
        }

        private void SaveToFile(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, MainTextBox.Text);
                currentFilePath = filePath;
                isModified = false;
                UpdateTitle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool ConfirmSave()
        {
            if (!isModified) return true;

            MessageBoxResult result = MessageBox.Show(
                "Сохранить изменения в текущем документе?",
                "Блокнот",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    SaveFile_Click(null, null);
                    return !isModified;
                case MessageBoxResult.No:
                    return true;
                case MessageBoxResult.Cancel:
                    return false;
                default:
                    return false;
            }
        }

        private void UpdateTitle()
        {
            string fileName = currentFilePath != null ?
                Path.GetFileName(currentFilePath) : "Без имени";
            string modified = isModified ? "*" : "";
            Title = $"{modified}{fileName} - Блокнот";
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!ConfirmSave())
            {
                e.Cancel = true;
            }
        }
    }
}


