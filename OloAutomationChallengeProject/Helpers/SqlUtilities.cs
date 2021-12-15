using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace OloAutomationChallengeProject.Helpers
{
    public class SqlUtilities
    {
        //Factory factory = new Factory();
        /// <summary>
        /// This method returns the value of a given column. The column must be named in the query provided. The expectation is the query should only return 1 row.
        /// </summary>
        /// <param name="connection">Connection string to the database</param>
        /// <param name="command">User defined query to execute</param>
        /// <param name="columnName">Column name from which the data should be returned</param>
        /// <returns>Value from provided database column</returns>
        public static object ExecuteSqlAndReturnSingleColumnValue(string connection, SqlCommand command, string columnName)
        {
            Factory factory = new Factory();
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var conn = factory.CreateNewSqlConnection(connection);
            object result = null;

            try
            {
                if (conn != null)
                {
                    conn.Open();
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                    SqlDataReader sdr = command.ExecuteReader();

                    if (sdr.Read())
                    {
                        int colIndex = sdr.GetOrdinal(columnName);
                        result = sdr.GetSqlValue(colIndex);
                    }
                    else
                    {
                        Console.WriteLine("No rows found from query");
                    }
                }
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
                throw new Exception("Error executing executeSqlAndReturnSingleColumnValue: " + e);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// This method will execute the provided query using the provided connection string
        /// </summary>
        /// <param name="connection">Connection string to the database</param>
        /// <param name="command">User defined query to execute</param>
        public static void ExecuteSql(string connection, SqlCommand command)
        {
            Factory factory = new Factory();
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var conn = factory.CreateNewSqlConnection(connection);

            try
            {
                if (conn != null)
                {
                    conn.Open();
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
                throw new Exception("Error executing query: " + e);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
