using System;
using System.Data.SQLite;
using System.Collections.Generic;


namespace MvcSkoki{
    public class DatabasesLoader {

        public static void ClearAllTables(SQLiteConnection connection) {
            List<string> tableNames = new List<string>();
    
            // Pobierz listę wszystkich tabel
            using (SQLiteCommand command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table'", connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
            }

            // Wyczyść dane z każdej tabeli
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                foreach (string tableName in tableNames)
                {
                    command.CommandText = $"DELETE FROM {tableName}";
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("All tables have been cleared successfully.");
        }
        
        public static void LoadCsvDataToTable(SQLiteConnection connection, String tableName, String fileName) {
            List<List<string>> data = new List<List<string>>();
            List<string> columnNames = new List<string>();

                using (var reader = new StreamReader(fileName))
    {
        int lineCount = 0;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(';');

            if (lineCount == 0)
            {
                foreach (var value in values)
                {
                    columnNames.Add(value);
                }
            }
            else
            {
                data.Add(new List<string>(values));
            }

            lineCount++;
        }
    }

    using (SQLiteCommand command = new SQLiteCommand(connection))
    {
        foreach (List<string> row in data)
        {
            // Buduj zapytanie SQL sprawdzające istnienie rekordu
            string checkQuery = $"SELECT COUNT(*) FROM {tableName} WHERE ";
            for (int i = 0; i < columnNames.Count; i++)
            {
                string value = string.IsNullOrEmpty(row[i]) ? "IS NULL" : $"= \"{row[i]}\"";
                checkQuery += $"{columnNames[i]} {value} AND ";
            }
            checkQuery = checkQuery.TrimEnd(" AND ".ToCharArray());

            // Sprawdź, czy rekord już istnieje
            command.CommandText = checkQuery;
            long count = (long)command.ExecuteScalar();

            if (count == 0)
            {
                // Buduj zapytanie SQL do wstawienia rekordu
                string insertQuery = $"INSERT INTO {tableName} VALUES(";
                for (int i = 0; i < row.Count; i++)
                {
                    string value = string.IsNullOrEmpty(row[i]) ? "NULL" : $"\"{row[i]}\"";
                    insertQuery += $"{value}, ";
                }
                insertQuery = insertQuery.TrimEnd(',', ' ') + ")";
                Console.WriteLine(insertQuery);

                // Wykonaj zapytanie wstawiające rekord
                command.CommandText = insertQuery;
                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine($"Data inserted into table '{tableName}' successfully.");
        }
    }}
}
