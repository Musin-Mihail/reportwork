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
                book.firstNameAuthor = i + "���������";
                book.lastNameAuthor = i + "�������������";
                book.nameBook = i + "�������� �����";
                listBooks.Add(book);
            }
            return listBooks;
        }
        public List<Person> CreatePerson()
        {
            List<Person> listPerson = new List<Person>();
            Person person = new Person();
            person.firstName = "���1";
            person.lastName = "�������1";
            person.middleName = "��������1";
            Book book = new Book();
            book.firstNameAuthor = "���������1";
            book.lastNameAuthor = "�������������1";
            book.nameBook = "�������� �����1";
            person.books.Add(book);
            person.status = "schoolboy";
            listPerson.Add(person);

            person = new Person();
            person.firstName = "���2";
            person.lastName = "�������2";
            person.middleName = "��������2";
            book = new Book();
            book.firstNameAuthor = "���������2";
            book.lastNameAuthor = "�������������2";
            book.nameBook = "�������� �����2";
            person.books.Add(book);
            person.status = "student";
            listPerson.Add(person);

            person = new Person();
            person.firstName = "���3";
            person.lastName = "�������3";
            person.middleName = "��������3";
            book = new Book();
            book.firstNameAuthor = "���������3";
            book.lastNameAuthor = "�������������3";
            book.nameBook = "�������� �����3";
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