using bibliobook;
using booksdto;
using Newtonsoft.Json;

namespace unittestBooks
{
    [TestClass]
    public class TestBibliobookClass
    {
        public List<Book> CreateBooks()
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
            return listBooks;
        }
        public List<Person> CreatePerson()
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
            return listPerson;
        }
        List<Book> listBooks;
        List<Person> listPerson;

        [TestInitialize]
        public void Test0()
        {
            listBooks = CreateBooks();
            listPerson = CreatePerson();
        }

        [TestMethod]
        public void Test1CreatePerson()
        {
            DTOClass bibliobook = new DTOClass();
            List<Person> listPerson = bibliobook.CreatePerson();
            Assert.AreEqual(3, listPerson.Count);
        }
        [TestMethod]
        public void Test2CreateBooks()
        {
            DTOClass bibliobook = new DTOClass();
            List<Book> listBooks = bibliobook.CreateBooks();
            Assert.AreEqual(5, listBooks.Count);
        }
        [TestMethod]
        public void Test3RefreshBooksLibrary()
        {
            DTOClass bibliobook = new DTOClass();
            List<string> newBookList = bibliobook.RefreshBooksLibrary(listBooks);
            Assert.AreEqual(5, newBookList.Count);
        }
        [TestMethod]
        public void Test4CreateStringListBook()
        {
            DTOClass bibliobook = new DTOClass();
            List<string> infoListBoks = bibliobook.CreateStringListBook(listPerson[0]);
            Assert.AreEqual(1, infoListBoks.Count);
        }
        [TestMethod]
        public void Test5RefreshPerson()
        {
            DTOClass bibliobook = new DTOClass();
            List<string> infoListBoks = bibliobook.RefreshPerson(listPerson);
            Assert.AreEqual(3, infoListBoks.Count);
        }
    }
}