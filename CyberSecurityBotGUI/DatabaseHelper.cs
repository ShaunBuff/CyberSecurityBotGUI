using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CyberSecurityBotGUI
{
    public class DatabaseHelper
    {
        private string connectionString =
            "server=localhost;database=CyberSecurityBotDB;uid=root;password=Password1;";

        // ADD TASK
        public void AddTask(string title, string description, DateTime? reminderDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO Tasks (Title, Description, ReminderDate) " +
                               "VALUES (@title, @description, @reminderDate)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);

                if (reminderDate.HasValue)
                    cmd.Parameters.AddWithValue("@reminderDate", reminderDate.Value);
                else
                    cmd.Parameters.AddWithValue("@reminderDate", DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        // GET ALL TASKS
        public List<string> GetTasks()
        {
            List<string> tasks = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT TaskID, Title, Description, ReminderDate, IsCompleted FROM Tasks";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string task =
                        $"ID: {reader["TaskID"]} | " +
                        $"Title: {reader["Title"]} | " +
                        $"Desc: {reader["Description"]} | " +
                        $"Reminder: {reader["ReminderDate"]} | " +
                        $"Completed: {reader["IsCompleted"]}";

                    tasks.Add(task);
                }
            }

            return tasks;
        }

        // DELETE TASK
        public void DeleteTask(int taskId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM Tasks WHERE TaskID = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", taskId);

                cmd.ExecuteNonQuery();
            }
        }

        // MARK AS COMPLETED
        public void MarkCompleted(int taskId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Tasks SET IsCompleted = TRUE WHERE TaskID = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", taskId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}