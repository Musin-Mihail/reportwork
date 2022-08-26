using System.Data.SqlClient;
using booksdto;
using Newtonsoft.Json;

namespace bookdb
{
    public class Bookdb
    {
        public SqlConnection ConnectBD()
        {
            SqlConnection connection;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "OSNOVA\\SQLEXPRESS";
            builder.UserID = "sa";
            builder.Password = "123";
            builder.InitialCatalog = "reportwork";
            builder.MultipleActiveResultSets = true;
            connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            return connection;
        }
        public List<Book> GetListBooks()
        {
            List<Book> listBooks = new List<Book>();
            string sql = "SELECT * FROM Books";
            SqlCommand command = new SqlCommand(sql, ConnectBD());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Book book = new Book();
                book.firstNameAuthor = reader.GetString(0);
                book.lastNameAuthor = reader.GetString(1);
                book.nameBook = reader.GetString(2);
                listBooks.Add(book);
            }
            return listBooks;
        }
        public void RemoveBookLibrary(string firstNameAuthor, string lastNameAuthor, string nameBook)
        {
            string sql = $"DELETE FROM Books WHERE firstNameAuthor='{firstNameAuthor}' AND lastNameAuthor='{lastNameAuthor}' AND nameBook='{nameBook}'";
            SqlCommand command = new SqlCommand(sql, ConnectBD());
            command.ExecuteReader();
        }
        public void AddBookLibrary(string book)
        {
            Book book2 = JsonConvert.DeserializeObject<Book>(book);
            string sql = $"INSERT INTO dbo.Books(firstNameAuthor, lastNameAuthor, nameBook) VALUES('{book2.firstNameAuthor}', '{book2.lastNameAuthor}', '{book2.nameBook}')";
            SqlCommand command = new SqlCommand(sql, ConnectBD());
            command.ExecuteReader();
        }
        public void RemoveAllBookLibrary()
        {
            string sql = "DELETE FROM Books";
            SqlCommand command = new SqlCommand(sql, ConnectBD());
            command.ExecuteReader();
        }
    }
}