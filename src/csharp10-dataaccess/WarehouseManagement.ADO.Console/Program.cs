using Microsoft.Data.SqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
        var connectionString = "Server=localhost;Database=WarehouseManagementDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;MultipleActiveResultSets=true;User ID=sa;Password=qaz?123";

        using SqlConnection connection = new SqlConnection(connectionString);

        using SqlCommand command = new(@"
            INSERT INTO [Customers]
            (Id, Name, Address, PostalCode, Country, PhoneNumber)
            VALUES(NEWID(), @Name, @Address, @PostalCode, @Country, @PhoneNumber)
        ", connection);

        var nameParameter =
            new SqlParameter("Name", System.Data.SqlDbType.NVarChar);
                nameParameter.Value = "Thamara Hoffmann";
                command.Parameters.Add(nameParameter);

        var addressParameter =
            new SqlParameter("Address", System.Data.SqlDbType.NVarChar);
        addressParameter.Value = "Happy St.";
        command.Parameters.Add(addressParameter);

        var postalCodeParameter =
            new SqlParameter("PostalCode", System.Data.SqlDbType.NVarChar);
        postalCodeParameter.Value = "FD 123 45";
        command.Parameters.Add(postalCodeParameter);

        var countryParameter =
            new SqlParameter("Country", System.Data.SqlDbType.NVarChar);
        countryParameter.Value = "Ireland";
        command.Parameters.Add(countryParameter);

        var phoneNumberParameter =
            new SqlParameter("PhoneNumber", System.Data.SqlDbType.NVarChar);
        phoneNumberParameter.Value = "89 11 11 11";
        command.Parameters.Add(phoneNumberParameter);


        connection.Open();
        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine($"--------------------------------------------");
        Console.WriteLine($"Rows affected: {rowsAffected}");
        Console.WriteLine($"--------------------------------------------");
        Console.ReadLine();


        using SqlCommand commandSelect = new(
            "SELECT * FROM [Customers]", connection);

        using var reader = commandSelect.ExecuteReader();


        if (reader.HasRows == false) return;

        Console.WriteLine($"--------------------------------------------");
        Console.WriteLine($"List of Customers: {rowsAffected}");
        Console.WriteLine($"--------------------------------------------");
        while (reader.Read())
        {
            Console.WriteLine($"    Id: {reader["Id"]}");
            Console.WriteLine($"    Name: {reader["Name"]}");
            Console.WriteLine($"--------------------------------------------");
        }

        Console.ReadLine();
    }
}