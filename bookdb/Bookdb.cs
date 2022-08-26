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
//Переименование столбца
//String sql = "EXEC sp_rename 'dbo.Books.Name', 'firstNameAuthor','column' ";

// Добавить колонку
//String sql = "ALTER TABLE dbo.Books ADD lastNameAuthor nchar(50) NULL";
//NOT NULL

//Создать таблицу
//String sql = "CREATE TABLE Persons(firstName nchar(50) NULL, lastName CHAR(50) NULL, middleName CHAR(50) NULL, books CHAR(500) NULL, status CHAR(50) NULL)";

//Изменить тип колонки
//string sql = "ALTER TABLE dbo.Persons ALTER COLUMN firstName CHAR(50) NULL";

//Добавить строчку
//string sql = "INSERT INTO dbo.Books(firstNameAuthor, lastNameAuthor, nameBook) VALUES('Имя', 'Фамилия', 'Книга')";

//Очистить таблицу
//string sql = "DELETE FROM Books";

//Прочитать все строки
//string sql = "SELECT * FROM Books"; 

//void UpdateRow(MySqlConnection conn)
//{
//    string sql = "UPDATE status SET date = 777 WHERE id = '1'";
//}
//void ReadingRow(MySqlConnection conn)
//{
//    string sql = "SELECT * FROM status WHERE id = '1'";
//}
//void GetTypeColums(MySqlConnection conn)
//{
//    string sql = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'status' AND COLUMN_NAME = 'date';";
//}