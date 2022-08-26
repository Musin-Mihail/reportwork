using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookdb;
using booksdto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;



namespace unittestBooks
{
    [TestClass]
    public class Test_bookdb
    {
        Random random = new Random();
        Bookdb bookdb = new Bookdb();
        [TestMethod]
        public void Test1_ConnectBD()
        {
            SqlConnection connection = bookdb.ConnectBD();
            string test = connection.State.ToString();
            Assert.AreEqual("Open", test);
        }
        [TestMethod]
        public void Test2_GetListBooks()
        {
            int number = random.Next(100);
            Book book = new Book();
            book.firstNameAuthor = "Имя" + number;
            book.lastNameAuthor = "Фамилия" + number;
            book.nameBook = "Название_книги" + number;
            string result = JsonConvert.SerializeObject(book);
            bookdb.AddBookLibrary(result);

            var list = bookdb.GetListBooks();
            Assert.IsTrue(list.Count > 0);
        }
        [TestMethod]
        public void Test3_RemoveBookLibrary()
        {
            int number = random.Next(100);
            Book book = new Book();
            string firstNameAuthor = "Имя" + number;
            book.firstNameAuthor = firstNameAuthor;
            string lastNameAuthor = "Фамилия" + number;
            book.lastNameAuthor = lastNameAuthor;
            string nameBook = "Название_книги" + number;
            book.nameBook = nameBook;
            string result = JsonConvert.SerializeObject(book);
            bookdb.AddBookLibrary(result);
            int count1 = bookdb.GetListBooks().Count;
            bookdb.RemoveBookLibrary(firstNameAuthor, lastNameAuthor, nameBook);
            int count2 = bookdb.GetListBooks().Count;
            Assert.IsTrue(count2 - count1 == -1);
        }
        [TestMethod]
        public void Test4_AddBookLibrary()
        {
            int count1 = bookdb.GetListBooks().Count;
            int number = random.Next(100);
            Book book = new Book();
            book.firstNameAuthor = "Имя" + number;
            book.lastNameAuthor = "Фамилия" + number;
            book.nameBook = "Название_книги" + number;
            string result = JsonConvert.SerializeObject(book);
            bookdb.AddBookLibrary(result);
            int count2 = bookdb.GetListBooks().Count;
            Assert.IsTrue(count2 - count1 == 1);
        }
        [TestMethod]
        public void Test5_RemoveAllBookLibrary()
        {
            int number = random.Next(100);
            Book book = new Book();
            book.firstNameAuthor = "Имя" + number;
            book.lastNameAuthor = "Фамилия" + number;
            book.nameBook = "Название_книги" + number;
            string result = JsonConvert.SerializeObject(book);
            bookdb.AddBookLibrary(result);
            bookdb.RemoveAllBookLibrary();
            var list = bookdb.GetListBooks();
            Assert.IsTrue(list.Count == 0);
        }
    }
}
