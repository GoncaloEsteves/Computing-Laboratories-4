using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PGB.Settings
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault("Username", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Username", value);
            }
        }
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }
        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static string Model
        {
            get
            {
                return AppSettings.GetValueOrDefault("Model", "Electric Vehicle");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Model", value);
            }
        }
        public static string TypeCode
        {
            get
            {
                return AppSettings.GetValueOrDefault("TypeCode", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("TypeCode", value);
            }
        }

        public static string FavRegistrationNumber
        {
            get
            {
                return AppSettings.GetValueOrDefault("FavRegistrationNumber", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("FavRegistrationNumber", value);
            }
        }
    }
}

