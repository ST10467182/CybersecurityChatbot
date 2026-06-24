// =============================================================
// TaskManager.cs
// Cybersecurity Awareness Chatbot - Task Manager with SQLite
// =============================================================
// Handles all database operations for the Task Assistant.
// Uses SQLite via Microsoft.Data.Sqlite — no server or password
// required. The database file is created automatically in the
// application folder on first run.
//
// SQLite was chosen as a lightweight, serverless alternative
// that satisfies the database integration requirement without
// requiring MySQL server configuration.
//
// References:
// [15] Microsoft, 2024. Microsoft.Data.Sqlite Overview [Online].
//      Available at: https://learn.microsoft.com/en-us/dotnet/
//      standard/data/sqlite [Accessed 20 May 2026].
//
// [16] Microsoft, 2024. Using Statement (C#) [Online].
//      Available at: https://learn.microsoft.com/en-us/dotnet/
//      csharp/language-reference/statements/using
//      [Accessed 20 May 2026].
// =============================================================

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

namespace CybersecurityChatbotGUI
{
    public class TaskManager
    {
        // SQLite database file stored in the application folder
        // No server, no password — just a local file [15]
        private readonly string _dbPath;
        private readonly string _connectionString;


        public TaskManager()
        {
            // Store the database file next to the executable
            _dbPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "cybersecurity_tasks.db"
            );
            _connectionString = $"Data Source={_dbPath}";

            // Create the database file and table on first run
            InitialiseDatabase();
        }

        // Creates the tasks table if it does not already exist [15]
        private void InitialiseDatabase()
        {
            using SqliteConnection conn = new SqliteConnection(_connectionString);
            conn.Open();

            string createTable = @"
                CREATE TABLE IF NOT EXISTS tasks (
                    id          INTEGER PRIMARY KEY AUTOINCREMENT,
                    title       TEXT NOT NULL,
                    description TEXT,
                    reminder    TEXT,
                    is_completed INTEGER DEFAULT 0,
                    created_at  TEXT DEFAULT (datetime('now','localtime'))
                );";

            using SqliteCommand cmd = new SqliteCommand(createTable, conn);
            cmd.ExecuteNonQuery();
        }

        // Adds a new task to the SQLite database [15]
        // Returns the auto-generated ID of the new task
        public int AddTask(CyberTask task)
        {
            using SqliteConnection conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = @"
                INSERT INTO tasks (title, description, reminder, is_completed, created_at)
                VALUES (@title, @desc, @reminder, 0, @created);
                SELECT last_insert_rowid();";

            using SqliteCommand cmd = new SqliteCommand(sql, conn);
            // Parameterised queries prevent SQL injection [15]
            cmd.Parameters.AddWithValue("@title", task.Title);
            cmd.Parameters.AddWithValue("@desc", task.Description);
            cmd.Parameters.AddWithValue("@reminder", task.Reminder);
            cmd.Parameters.AddWithValue("@created", task.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));

            object? result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }

        // Retrieves all tasks from the database as a List<CyberTask> [16]
        public List<CyberTask> GetAllTasks()
        {
            List<CyberTask> tasks = new List<CyberTask>();

            using SqliteConnection conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT id, title, description, reminder, is_completed, created_at FROM tasks ORDER BY created_at DESC;";
            using SqliteCommand cmd = new SqliteCommand(sql, conn);
            using SqliteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(new CyberTask
                {
                    Id          = reader.GetInt32(0),
                    Title       = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Reminder    = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    IsCompleted = reader.GetInt32(4) == 1,
                    CreatedAt   = DateTime.TryParse(reader.GetString(5), out DateTime dt)
                                  ? dt : DateTime.Now
                });
            }

            return tasks;
        }

        // Marks a task as completed in the database [15]
        public void MarkComplete(int taskId)
        {
            using SqliteConnection conn = new SqliteConnection(_connectionString);
            conn.Open();
            using SqliteCommand cmd = new SqliteCommand(
                "UPDATE tasks SET is_completed = 1 WHERE id = @id;", conn);
            cmd.Parameters.AddWithValue("@id", taskId);
            cmd.ExecuteNonQuery();
        }

        // Deletes a task from the database by ID [15]
        public void DeleteTask(int taskId)
        {
            using SqliteConnection conn = new SqliteConnection(_connectionString);
            conn.Open();
            using SqliteCommand cmd = new SqliteCommand(
                "DELETE FROM tasks WHERE id = @id;", conn);
            cmd.Parameters.AddWithValue("@id", taskId);
            cmd.ExecuteNonQuery();
        }

        // Tests that the database file is accessible
        public bool TestConnection()
        {
            try
            {
                using SqliteConnection conn = new SqliteConnection(_connectionString);
                conn.Open();
                return true;
            }
            catch { return false; }
        }

        // Returns the path to the SQLite database file
        public string GetDatabasePath() => _dbPath;
    }
}
