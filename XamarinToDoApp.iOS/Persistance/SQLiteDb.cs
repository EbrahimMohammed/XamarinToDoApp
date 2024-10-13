using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using XamarinToDoApp.iOS;
using XamarinToDoApp.Persistance;

[assembly: Dependency(typeof(SQLiteDb))]
namespace XamarinToDoApp.iOS
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}