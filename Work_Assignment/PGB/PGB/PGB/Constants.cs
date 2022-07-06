using System;
using System.IO;

namespace PGB
{
    public static class Constants
    {
        public const string GoogleMapsApiKey = "AIzaSyAKebUmEyg9SZThB5W9Pse-wPvhD4_ZvfI";
        public const string IternioPlanningApiKey = "6b0d4907-a5e1-4365-ba99-74e5444ba367";
        public const string DatabaseFilename = "PGB_database.db3";
        public const string Tag = "PGB"; // For logs
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}