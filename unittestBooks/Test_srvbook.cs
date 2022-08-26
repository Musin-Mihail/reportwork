using booksdto;
using Newtonsoft.Json;
using bookdb;

namespace unittestBooks
{
    [TestClass]
    public class Test_srvbook
    {
        HttpClient client = new HttpClient();
        Bookdb bookdb = new Bookdb();
        Random random = new Random();
        [TestMethod]
        public void Test1_CreatePersons()
        {
            client.GetStringAsync("https://localhost:5001/Books/TestRemoveAllPerson/");
            Task.Delay(200).Wait();
            client.GetStringAsync("https://localhost:5001/Books/CreatePersons/");
            Task.Delay(200).Wait();
            var result = client.GetStringAsync("https://localhost:5001/Books/TestGetAllPerson/");
            Task.Delay(200).Wait();
            List<Person> list = JsonConvert.DeserializeObject<List<Person>>(result.Result);
            Assert.AreEqual(3, list.Count);
        }
        [TestMethod]
        public void Test2_RefreshPerson()
        {
            client.GetStringAsync("https://localhost:5001/Books/TestRemoveAllPerson/");
            Task.Delay(200).Wait();
            client.GetStringAsync("https://localhost:5001/Books/CreatePersons/");
            Task.Delay(200).Wait();
            var result = client.GetStringAsync("https://localhost:5001/Books/RefreshPerson/");
            Task.Delay(200).Wait();
            var list = JsonConvert.DeserializeObject<List<string>>(result.Result);
            Assert.AreEqual(3, list.Count);
        }
        [TestMethod]
        public void Test3_GetPerson()
        {
            client.GetStringAsync("https://localhost:5001/Books/TestRemoveAllPerson/");
            Task.Delay(200).Wait();
            client.GetStringAsync("https://localhost:5001/Books/CreatePersons/");
            Task.Delay(200).Wait();
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetPerson/0");
            Task.Delay(200).Wait();
            Person person = JsonConvert.DeserializeObject<Person>(result.Result);
            Assert.AreEqual("Имя1", person.firstName);
            Assert.AreEqual("Фамилия1", person.lastName);
            Assert.AreEqual("Отчетсво1", person.middleName);
            Assert.AreEqual(1, person.books.Count);
            Assert.AreEqual("schoolboy", person.status);
        }
        [TestMethod]
        public void Test4_GetBookLibrary()
        {
            bookdb.RemoveAllBookLibrary();

            int number = random.Next(100);
            Book book = new Book();
            string firstNameAuthor = "Имя" + number;
            book.firstNameAuthor = firstNameAuthor;
            string lastNameAuthor = "Фамилия" + number;
            book.lastNameAuthor = lastNameAuthor;
            string nameBook = "Название_книги" + number;
            book.nameBook = nameBook;
            string result1 = JsonConvert.SerializeObject(book);
            bookdb.AddBookLibrary(result1);

            var result = client.GetStringAsync($"https://localhost:5001/Books/GetBookLibrary/0");
            Task.Delay(200).Wait();
            book = JsonConvert.DeserializeObject<Book>(result.Result);
            Assert.IsTrue(book.firstNameAuthor == firstNameAuthor);
            Assert.IsTrue(book.lastNameAuthor == lastNameAuthor);
            Assert.IsTrue(book.nameBook == nameBook);
        }
        [TestMethod]
        public void Test5_GetBookPerson()
        {
            client.GetStringAsync("https://localhost:5001/Books/TestRemoveAllPerson/");
            Task.Delay(200).Wait();
            client.GetStringAsync("https://localhost:5001/Books/CreatePersons/");
            Task.Delay(200).Wait();
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetBookPerson/0,0");
            Task.Delay(200).Wait();
            Book book = JsonConvert.DeserializeObject<Book>(result.Result);
            Assert.AreEqual("ИмяАвтора1", book.firstNameAuthor);
            Assert.AreEqual("ФамилияАвтора1", book.lastNameAuthor);
            Assert.AreEqual("Название_книги1", book.nameBook);
        }
        [TestMethod]
        public void Test6_RefreshBooksLibrary()
        {
            bookdb.RemoveAllBookLibrary();

            int number = random.Next(100);
            Book book = new Book();
            string firstNameAuthor = "Имя" + number;
            book.firstNameAuthor = firstNameAuthor;
            string lastNameAuthor = "Фамилия" + number;
            book.lastNameAuthor = lastNameAuthor;
            string nameBook = "Название_книги" + number;
            book.nameBook = nameBook;
            string result1 = JsonConvert.SerializeObject(book);
            bookdb.AddBookLibrary(result1);

            var result = client.GetStringAsync("https://localhost:5001/Books/RefreshBooksLibrary/");
            Task.Delay(200).Wait();
            List<string> newList = JsonConvert.DeserializeObject<List<string>>(result.Result);
            Assert.AreEqual(1, newList.Count);
        }
        //[TestMethod]
        //public void Test7_RemoveBookLibrary()
        //{
        //    client.GetStringAsync($"https://localhost:5001/Books/RemoveBookLibrary/{firstNameAuthor},{lastNameAuthor},{nameBook}");
        //    Task.Delay(200).Wait();
        //}
        [TestMethod]
        public void Test8_RemoveBookPerson()
        {
            client.GetStringAsync("https://localhost:5001/Books/TestRemoveAllPerson/");
            Task.Delay(200).Wait();
            client.GetStringAsync("https://localhost:5001/Books/CreatePersons/");
            Task.Delay(200).Wait();
            client.GetStringAsync($"https://localhost:5001/Books/RemoveBookPerson/0,0");
            Task.Delay(200).Wait();
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetPerson/0");
            Task.Delay(200).Wait();
            Person person = JsonConvert.DeserializeObject<Person>(result.Result);
            Assert.AreEqual(0, person.books.Count);
        }
        [TestMethod]
        public void Test9_AddBookPerson()
        {
            client.GetStringAsync("https://localhost:5001/Books/TestRemoveAllPerson/");
            Task.Delay(200).Wait();
            client.GetStringAsync("https://localhost:5001/Books/CreatePersons/");
            Task.Delay(200).Wait();
            Book book = new Book();
            book.firstNameAuthor = "Имя9";
            book.lastNameAuthor = "Фамилия9";
            book.nameBook = "Название_книги9";
            string result = JsonConvert.SerializeObject(book);
            client.GetStringAsync($"https://localhost:5001/Books/AddBookPerson/{result},0");
            Task.Delay(200).Wait();
            var result2 = client.GetStringAsync($"https://localhost:5001/Books/GetPerson/0");
            Task.Delay(200).Wait();
            Person person = JsonConvert.DeserializeObject<Person>(result2.Result);
            Assert.AreEqual(2, person.books.Count);
            Assert.AreEqual("Имя9", person.books[1].firstNameAuthor);
            Assert.AreEqual("Фамилия9", person.books[1].lastNameAuthor);
            Assert.AreEqual("Название_книги9", person.books[1].nameBook);
        }
        //[TestMethod]
        //public void Test10_AddBookLibrary()
        //{
        //    string result = JsonConvert.SerializeObject(book);
        //    client.GetStringAsync($"https://localhost:5001/Books/AddBookLibrary/{result}");
        //    Task.Delay(200).Wait();
        //}
        [TestMethod]
        public void Test11_TransferAllLibrary()
        {
            client.GetStringAsync("https://localhost:5001/Books/TestRemoveAllPerson/");
            Task.Delay(200).Wait();
            client.GetStringAsync("https://localhost:5001/Books/CreatePersons/");
            Task.Delay(200).Wait();
            bookdb.RemoveAllBookLibrary();

            client.GetStringAsync($"https://localhost:5001/Books/TransferAllLibrary/0");
            Task.Delay(200).Wait();
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetPerson/0");
            Task.Delay(200).Wait();
            Person person = JsonConvert.DeserializeObject<Person>(result.Result);
            var list = bookdb.GetListBooks();
            Assert.IsTrue(list.Count == 1);
            Assert.IsTrue(person.books.Count == 0);
        }
        //[TestMethod]
        //public void Test12_RemoveAllBookLibrary()
        //{
        //    client.GetStringAsync($"https://localhost:5001/Books/RemoveAllBookLibrary/");
        //    Task.Delay(200).Wait();
        //    RefreshBooksLibrary();
        //}
    }
}