using Microsoft.AspNetCore.Mvc;
using booksdto;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace srvbook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        DTOClass dtoClass = new DTOClass();
        static List<Person> listPerson = new List<Person>();
        //static List<Book> listBooks = new List<Book>();
        [HttpGet]
        [Route("/Books/CreatePersons/")]
        public void CreatePerson()
        {
            listPerson.Clear();
            Person person = new Person();
            person.firstName = "Имя1";
            person.lastName = "Фамилия1";
            person.middleName = "Отчетсво1";
            Book book = new Book();
            book.firstNameAuthor = "ИмяАвтора1";
            book.lastNameAuthor = "ФамилияАвтора1";
            book.nameBook = "Название книги1";
            person.books.Add(book);
            person.status = "schoolboy";
            listPerson.Add(person);

            person = new Person();
            person.firstName = "Имя2";
            person.lastName = "Фамилия2";
            person.middleName = "Отчетсво2";
            book = new Book();
            book.firstNameAuthor = "ИмяАвтора2";
            book.lastNameAuthor = "ФамилияАвтора2";
            book.nameBook = "Название книги2";
            person.books.Add(book);
            person.status = "student";
            listPerson.Add(person);

            person = new Person();
            person.firstName = "Имя3";
            person.lastName = "Фамилия3";
            person.middleName = "Отчетсво3";
            book = new Book();
            book.firstNameAuthor = "ИмяАвтора3";
            book.lastNameAuthor = "ФамилияАвтора3";
            book.nameBook = "Название книги3";
            person.books.Add(book);
            person.status = "employee";
            listPerson.Add(person);

            Console.WriteLine("CreatePerson");
        }
        [HttpGet]
        [Route("/Books/Test/")]
        public string Test()
        {
            string sql = "SELECT * FROM Books WHERE row_id=1";
            SqlCommand command = new SqlCommand(sql, ConnectBD());
            SqlDataReader reader = command.ExecuteReader();
            string testing = "";
            while (reader.Read())
            {
                testing += reader.GetString(0);
                testing += reader.GetString(1);
                testing += reader.GetString(2);
            }
            return testing;
        }
        SqlConnection ConnectBD()
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
        [HttpGet]
        [Route("/Books/RefreshPerson/")]
        public string RefreshPerson()
        {
            List<string> newComboList = dtoClass.RefreshPerson(listPerson);
            string result = JsonConvert.SerializeObject(newComboList);
            Console.WriteLine("RefreshPerson");
            return result;
        }
        [HttpGet]
        [Route("/Books/GetPerson/{index}")]
        public string GetPerson(int index)
        {
            string result = JsonConvert.SerializeObject(listPerson[index]);
            Console.WriteLine("GetPerson" + index);
            return result;
        }
        List<Book> GetListBooks()
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
        [HttpGet]
        [Route("/Books/GetBookLibrary/{index}")]
        public string GetBookLibrary(int index)
        {
            string result = JsonConvert.SerializeObject(GetListBooks()[index]);
            Console.WriteLine("GetBookLibrary");
            return result;
        }
        [HttpGet]
        [Route("/Books/GetBookPerson/{indexPerson},{indexBook}")]
        public string GetBookPerson(int indexPerson, int indexBook)
        {
            string result = JsonConvert.SerializeObject(listPerson[indexPerson].books[indexBook]);
            Console.WriteLine("GetBookPerson");
            return result;
        }
        [HttpGet]
        [Route("/Books/RefreshBooksLibrary/")]
        public string RefreshBooksLibrary()
        {
            List<string> newComboList = dtoClass.RefreshBooksLibrary(GetListBooks());
            string result = JsonConvert.SerializeObject(newComboList);
            Console.WriteLine("RefreshBooksLibrary");
            return result;
        }
        [HttpGet]
        [Route("/Books/RemoveBookLibrary/{firstNameAuthor},{lastNameAuthor},{nameBook}")]
        public void RemoveBookLibrary(string firstNameAuthor, string lastNameAuthor, string nameBook)
        {
            string sql = $"DELETE FROM Books WHERE firstNameAuthor='{firstNameAuthor}' AND lastNameAuthor='{lastNameAuthor}' AND nameBook='{nameBook}'";
            SqlCommand command = new SqlCommand(sql, ConnectBD());
            command.ExecuteReader();
            Console.WriteLine($"RemoveBookLibrary{firstNameAuthor} {lastNameAuthor} {nameBook}");
        }
        [HttpGet]
        [Route("/Books/RemoveBookPerson/{indexPerson},{indexBook}")]
        public void RemoveBookPerson(int indexPerson, int indexBook)
        {
            listPerson[indexPerson].books.RemoveAt(indexBook);
            Console.WriteLine("FinishRemoveBookPerson");
        }
        [HttpGet]
        [Route("/Books/AddBookPerson/{indexPerson},{indexBook}")]
        public void AddBookPerson(int indexPerson, int indexBook)
        {
            //listPerson[indexPerson].books.Add(listBooks[indexBook]);
            Console.WriteLine("AddBookPerson");
        }
        [HttpGet]
        [Route("/Books/AddBookLibrary/{book}")]
        public void AddBookLibrary(string book)
        {
            Book book2 = JsonConvert.DeserializeObject<Book>(book);
            string sql = $"INSERT INTO dbo.Books(firstNameAuthor, lastNameAuthor, nameBook) VALUES('{book2.firstNameAuthor}', '{book2.firstNameAuthor}', '{book2.nameBook}')";
            SqlCommand command = new SqlCommand(sql, ConnectBD());
            command.ExecuteReader();
            Console.WriteLine("AddBookLibrary");
        }
        [HttpGet]
        [Route("/Books/TransferAllLibrary/{indexPerson}")]
        public void TransferAllLibrary(int indexPerson)
        {
            //listBooks.AddRange(listPerson[indexPerson].books);
            //listPerson[indexPerson].books.Clear();
            Console.WriteLine("TransferAllLibrary");
        }
    }
}
