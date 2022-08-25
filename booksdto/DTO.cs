using System.Collections.Generic;

namespace booksdto
{
    public class Person
    {
        public string firstName = "";
        public string lastName = "";
        public string middleName = "";
        public List<Book> books = new List<Book>();
        public string status = "";
    }
    public class Book
    {
        public string firstNameAuthor = "";
        public string lastNameAuthor = "";
        public string nameBook = "";
    }
}