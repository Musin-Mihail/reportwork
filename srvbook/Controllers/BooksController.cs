using Microsoft.AspNetCore.Mvc;
using booksdto;
using Newtonsoft.Json;

namespace srvbook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        DTOClass dtoClass = new DTOClass();
        static List<Person> listPerson = new List<Person>();
        static List<Book> listBooks = new List<Book>();
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
        [Route("/Books/CreateBooks/")]
        public void CreateBooks()
        {
            listBooks.Clear();
            listBooks = new List<Book>();
            for (int i = 1; i < 6; i++)
            {
                Book book = new Book();
                book.firstNameAuthor = i + "ИмяАвтора";
                book.lastNameAuthor = i + "ФамилияАвтора";
                book.nameBook = i + "Название книги";
                listBooks.Add(book);
            }
            Console.WriteLine("CreateBooks");
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
            Console.WriteLine("GetPerson"+ index);
            return result;
        }
        [HttpGet]
        [Route("/Books/GetBookLibrary/{index}")]
        public string GetBookLibrary(int index)
        {
            string result = JsonConvert.SerializeObject(listBooks[index]);
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
            List<string> newComboList = dtoClass.RefreshBooksLibrary(listBooks);
            string result = JsonConvert.SerializeObject(newComboList);
            Console.WriteLine("RefreshBooksLibrary");
            return result;
        }
        [HttpGet]
        [Route("/Books/RemoveBookLibrary/{index}")]
        public void RemoveBookLibrary(int index)
        {
            listBooks.RemoveAt(index);
            Console.WriteLine("RemoveBookLibrary");
        }
        [HttpGet]
        [Route("/Books/RemoveBookPerson/{indexPerson},{indexBook}")]
        public void RemoveBookPerson(int indexPerson, int indexBook)
        {
            Console.WriteLine("StartRemoveBookPerson");
            Console.WriteLine($"{indexPerson} {indexBook}" );
            Console.WriteLine($"{listPerson.Count} {listPerson[indexPerson].books.Count}");
            listPerson[indexPerson].books.RemoveAt(indexBook);
            Console.WriteLine("FinishRemoveBookPerson");
        }
        [HttpGet]
        [Route("/Books/AddBookPerson/{indexPerson},{indexBook}")]
        public void AddBookPerson(int indexPerson, int indexBook)
        {
            listPerson[indexPerson].books.Add(listBooks[ indexBook]);
            Console.WriteLine("AddBookPerson");
        }
        [HttpGet]
        [Route("/Books/AddBookLibrary/{book}")]
        public void AddBookLibrary(string book)
        {
            listBooks.Add(JsonConvert.DeserializeObject<Book>(book));
            Console.WriteLine("AddBookLibrary");
        }
        [HttpGet]
        [Route("/Books/TransferAllLibrary/{indexPerson}")]
        public void TransferAllLibrary(int indexPerson)
        {
            listBooks.AddRange(listPerson[indexPerson].books);
            listPerson[indexPerson].books.Clear();
            Console.WriteLine("TransferAllLibrary");
        }
    }
}
