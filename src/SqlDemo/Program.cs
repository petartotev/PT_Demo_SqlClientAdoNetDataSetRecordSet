namespace SqlDemo;

public class Program
{
    public static void Main()
    {
        // Before you try the demo, please create database TestDb with the following SQL query:
        // CREATE DATABASE TestDb;
        // GO

        var sqlManager = new SqlManager();

        sqlManager.CreatePersonsTableIfNotExists();

        sqlManager.PopulatePersonsTable();

        sqlManager.CreateDataSetFromPersons();
    }
}