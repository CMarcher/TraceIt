using System;
using System.Collections.Generic;
using System.Text;

namespace TraceIt.Utilities
{
    public static class Constants
    {
        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.SharedCache;
    }
}
