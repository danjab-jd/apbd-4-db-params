using System.Data.SqlClient;
using DbAPI.Models;

namespace DbAPI.Services;

public class BookDbService : IBookDbService
{
    private const string _connString = "Data Source=db-mssql;Initial Catalog=jd;Integrated Security=True";
    private const string _localConnString = "Data Source=localhost;Initial Catalog=jd;User ID={USERNAME};Password={PASSWORD}";

    public async Task<IList<Book>> GetBooksListAsync(string title)
    {
        List<Book> books = new();

        await using SqlConnection sqlConnection = new(_connString);
        await using SqlCommand sqlCommand = new();

        string sql;
        if (string.IsNullOrWhiteSpace(title))
        {
            sql = "SELECT * FROM Book";
        }
        else
        {
            sql = $"SELECT * FROM Book WHERE Title LIKE @title";

            sqlCommand.Parameters.AddWithValue("title", title);
        }

        sqlCommand.CommandText = sql;
        sqlCommand.Connection = sqlConnection;

        await sqlConnection.OpenAsync();

        await using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

        while (await sqlDataReader.ReadAsync())
        {
            Book book = new()
            {
                IdBook = int.Parse(sqlDataReader["IdBook"].ToString()),
                Title = sqlDataReader["Title"].ToString()
            };
            books.Add(book);
        }

        await sqlConnection.CloseAsync();

        return books;
    }
}