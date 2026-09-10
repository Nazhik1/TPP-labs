using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace WpfApp8._1.Localization
{
    public class LocalizationManager : INotifyPropertyChanged
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("WpfApp8._1.Localization.Strings",
                typeof(LocalizationManager).Assembly);

        private static LocalizationManager _instance;
        public static LocalizationManager Instance =>
            _instance ?? (_instance = new LocalizationManager());

        private CultureInfo _culture = new CultureInfo("ru");

        private LocalizationManager() { }

        public CultureInfo Culture
        {
            get => _culture;
            set
            {
                if (value == null) return;
                if (_culture != null && _culture.Name == value.Name) return;

                _culture = value;

                Thread.CurrentThread.CurrentCulture = value;
                Thread.CurrentThread.CurrentUICulture = value;
                CultureInfo.DefaultThreadCurrentCulture = value;
                CultureInfo.DefaultThreadCurrentUICulture = value;

                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(null));
            }
        }

        public string this[string key]
        {
            get
            {
                try
                {
                    return _resourceManager.GetString(key, _culture) ?? key;
                }
                catch
                {
                    return key;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}