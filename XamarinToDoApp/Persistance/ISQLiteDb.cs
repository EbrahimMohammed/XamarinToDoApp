using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinToDoApp.Persistance
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
