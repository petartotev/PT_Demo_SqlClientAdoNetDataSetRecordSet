using System.Data;
using System.Data.SqlClient;

namespace SqlDemo
{
    public class SqlManager
    {
        private readonly string _connectionString = "Server=localhost;Database=TestDb;Integrated Security=True;";

        public void CreatePersonsTableIfNotExists()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                @"CREATE TABLE Persons(
                    PersonID int NOT NULL,
                    FirstName varchar(255),
                    LastName varchar(255),
                    Gender varchar(1),
                    Address varchar(255),
                    City varchar(255),
                    PRIMARY KEY (PersonID)
                );", connection);

                try
                {
                    var result = command.ExecuteNonQuery();

                    Console.WriteLine(result > 0 ? "Table Persons created" : "Table Persons already exists.");
                    Console.WriteLine($"Numbers affected: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                connection.Close();
            };
        }

        public void PopulatePersonsTable()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM Persons", connection);

                try
                {
                    var result = command.ExecuteNonQuery();

                    Console.WriteLine(result > 0 ? "Table Persons already populated" : "About to populate Table Persons...");

                    var command2 = new SqlCommand(
                    @"INSERT INTO dbo.Persons(PersonID, FirstName, LastName, Gender, Address, City)
                    VALUES(1, 'Pavel, 'Botev', 'M', 'Maria Luisa str.', 'Burgas');
                    INSERT INTO dbo.Persons(PersonID, FirstName, LastName, Gender, Address, City)
                    VALUES(2, 'Tanya', 'Konstantinova', 'F', 'Tsar Simeon str.', 'Stara Zagora');
                    INSERT INTO dbo.Persons(PersonID, FirstName, LastName, Gender, Address, City)
                    VALUES(3, 'Ivaylo', 'Ivanov', 'M', 'Mladost 4', 'Sofia');
                    INSERT INTO dbo.Persons(PersonID, FirstName, LastName, Gender, Address, City)
                    VALUES(4, 'Jordanka', 'Tsenkova', 'F', 'Mladost 4', 'Sofia');
                    INSERT INTO dbo.Persons(PersonID, FirstName, LastName, Gender, Address, City)
                    VALUES(5, 'Stanimira', 'Brozig', 'F', 'Evlogi Georgiev str.', 'Sofia');
                    INSERT INTO dbo.Persons(PersonID, FirstName, LastName, Gender, Address, City)
                    VALUES(6, 'Hristina', 'Hranov', 'F', 'Cypress hill', 'Bosilegrad');", connection);

                    var result2 = command2.ExecuteNonQuery();

                    Console.WriteLine(result2 > 0 ? "Database TestDb populated!" : "Database TestDb already populated.");
                    Console.WriteLine($"Numbers affected: {result2}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                connection.Close();
            };
        }

        public void CreateDataSetFromPersons()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                DataSet myDataSet = new();

                SqlDataAdapter adapter = new()
                {
                    SelectCommand = new SqlCommand("SELECT * FROM Persons", connection)
                };

                adapter.Fill(myDataSet);

                PrintDataSet(myDataSet);
            };
        }

        private static void PrintDataSet(DataSet dataSet)
        {
            foreach (DataTable dataTable in dataSet.Tables)
            {
                Console.WriteLine($"{dataTable.TableName} Table:");

                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    Console.Write(dataTable.Columns[col].ColumnName.Trim() + new string(' ', 4));
                }

                Console.WriteLine();

                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    DataRow rowCurrent = dataTable.Rows[row];

                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        object colCurrent = rowCurrent[col];

                        Console.Write(colCurrent.ToString().Trim() + new string(' ', 4));
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
