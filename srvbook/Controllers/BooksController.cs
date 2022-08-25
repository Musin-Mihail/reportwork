using Microsoft.AspNetCore.Mvc;
using booksdto;
using Newtonsoft.Json;

namespace srvbook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        [Route("/Books/CreatePersons/")]
        public string CreatePerson()
        {
            List<Person> listPerson = new List<Person>();
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
            string result = JsonConvert.SerializeObject(listPerson);
            return result;
        }
        [HttpGet]
        [Route("/Books/CreateBooks/")]
        public string CreateBooks()
        {
            List<Book> listBooks = new List<Book>();
            for (int i = 1; i < 6; i++)
            {
                Book book = new Book();
                book.firstNameAuthor = i + "ИмяАвтора";
                book.lastNameAuthor = i + "ФамилияАвтора";
                book.nameBook = i + "Название книги";
                listBooks.Add(book);
            }
            string result = JsonConvert.SerializeObject(listBooks);
            return result;
        }
    }
}
