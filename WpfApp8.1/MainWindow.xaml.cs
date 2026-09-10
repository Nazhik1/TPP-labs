using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using WpfApp8._1.Localization;

namespace WpfApp8._1
{
    public partial class MainWindow : Window
    {
        private string currentFilePath = null;
        private bool isModified = false;
        private bool suppressTextChanged = false;

        private LocalizationManager Loc => LocalizationManager.Instance;

        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
                (s, e) => NewFile_Click(s, e)));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open,
                (s, e) => OpenFile_Click(s, e)));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save,
                (s, e) => SaveFile_Click(s, e)));

            Loc.PropertyChanged += (s, e) => UpdateTitle();
            UpdateTitle();
        }

        private void SetRussian_Click(object sender, RoutedEventArgs e)
        {
            Loc.Culture = new CultureInfo("ru");
        }

        private void SetEnglish_Click(object sender, RoutedEventArgs e)
        {
            Loc.Culture = new CultureInfo("en");
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfirmSave()) return;

            suppressTextChanged = true;
            MainTextBox.Clear();
            suppressTextChanged = false;

            currentFilePath = null;
            isModified = false;
            UpdateTitle();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfirmSave()) return;

            var dlg = new OpenFileDialog
            {
                Filter = Loc["TextFilesFilter"],
                Title = Loc["OpenFileDialogTitle"]
            };

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    suppressTextChanged = true;
                    MainTextBox.Text = File.ReadAllText(dlg.FileName);
                    suppressTextChanged = false;

                    currentFilePath = dlg.FileName;
                    isModified = false;
                    UpdateTitle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        string.Format(Loc["OpenErrorMessage"], ex.Message),
                        Loc["ErrorTitle"],
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e) => SaveDocument();
        private void SaveAsFile_Click(object sender, RoutedEventArgs e) => SaveAsDocument();
        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private bool SaveDocument()
        {
            if (currentFilePath == null) return SaveAsDocument();
            return SaveToFile(currentFilePath);
        }

        private bool SaveAsDocument()
        {
            var dlg = new SaveFileDialog
            {
                Filter = Loc["TextFilesFilter"],
                Title = Loc["SaveFileDialogTitle"],
                FileName = currentFilePath != null
                    ? Path.GetFileName(currentFilePath)
                    : Loc["DefaultFileName"]
            };

            if (dlg.ShowDialog() != true) return false;
            return SaveToFile(dlg.FileName);
        }

        private bool SaveToFile(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, MainTextBox.Text);
                currentFilePath = filePath;
                isModified = false;
                UpdateTitle();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(Loc["SaveErrorMessage"], ex.Message),
                    Loc["ErrorTitle"],
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (suppressTextChanged || isModified) return;
            isModified = true;
            UpdateTitle();
        }

        private bool ConfirmSave()
        {
            if (!isModified) return true;

            var result = MessageBox.Show(
                Loc["ConfirmSaveMessage"],
                Loc["ConfirmSaveTitle"],
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    return SaveDocument();
                case MessageBoxResult.No:
                    isModified = false;
                    return true;
                case MessageBoxResult.Cancel:
                default:
                    return false;
            }
        }

        private void UpdateTitle()
        {
            string fileName = currentFilePath != null
                ? Path.GetFileName(currentFilePath)
                : Loc["Untitled"];
            string modified = isModified ? "*" : "";
            Title = $"{modified}{fileName} - {Loc["AppTitle"]}";
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!ConfirmSave()) e.Cancel = true;
        }
    }
}
