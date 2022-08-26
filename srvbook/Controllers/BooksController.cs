using Microsoft.AspNetCore.Mvc;
using booksdto;
using Newtonsoft.Json;
using bookdb;

namespace srvbook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        DTOClass dtoClass = new DTOClass();
        Bookdb bookdb = new Bookdb();
        static List<Person> listPerson = new List<Person>();
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
            book.nameBook = "Название_книги1";
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
            book.nameBook = "Название_книги2";
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
            book.nameBook = "Название_книги3";
            person.books.Add(book);
            person.status = "employee";
            listPerson.Add(person);

            Console.WriteLine("CreatePerson");
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
        [HttpGet]
        [Route("/Books/GetBookLibrary/{index}")]
        public string GetBookLibrary(int index)
        {
            string result = JsonConvert.SerializeObject(bookdb.GetListBooks()[index]);
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
            List<string> newComboList = dtoClass.RefreshBooksLibrary(bookdb.GetListBooks());
            string result = JsonConvert.SerializeObject(newComboList);
            Console.WriteLine("RefreshBooksLibrary");
            return result;
        }
        [HttpGet]
        [Route("/Books/RemoveBookLibrary/{firstNameAuthor},{lastNameAuthor},{nameBook}")]
        public void RemoveBookLibrary(string firstNameAuthor, string lastNameAuthor, string nameBook)
        {
            bookdb.RemoveBookLibrary(firstNameAuthor, lastNameAuthor, nameBook);
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
        [Route("/Books/AddBookPerson/{book},{indexPerson}")]
        public void AddBookPerson(string book, int indexPerson)
        {
            Book book1 = JsonConvert.DeserializeObject<Book>(book);
            listPerson[indexPerson].books.Add(book1);
            Console.WriteLine("AddBookPerson");
        }
        [HttpGet]
        [Route("/Books/AddBookLibrary/{book}")]
        public void AddBookLibrary(string book)
        {
            bookdb.AddBookLibrary(book);
            Console.WriteLine("AddBookLibrary");
        }
        [HttpGet]
        [Route("/Books/TransferAllLibrary/{indexPerson}")]
        public void TransferAllLibrary(int indexPerson)
        {
            foreach (Book book in listPerson[indexPerson].books)
            {
                string result = JsonConvert.SerializeObject(book);
                AddBookLibrary(result);
            }
            listPerson[indexPerson].books.Clear();
            Console.WriteLine("TransferAllLibrary");
        }
        [HttpGet]
        [Route("/Books/RemoveAllBookLibrary/")]
        public void RemoveAllBookLibrary()
        {
            bookdb.RemoveAllBookLibrary();
            Console.WriteLine("RemoveAllBookLibrary");
        }
    }
}
