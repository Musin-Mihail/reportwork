using Newtonsoft.Json;
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
    public class DTOClass
    {
        HttpClient client = new HttpClient();
        public List<Person> CreatePerson()
        {
            var result = client.GetStringAsync("https://localhost:5001/Books/CreatePersons/");
            Task.Delay(1000).Wait();
            return JsonConvert.DeserializeObject<List<Person>>(result.Result);
        }
        public List<Book> CreateBooks()
        {
            var result = client.GetStringAsync("https://localhost:5001/Books/CreateBooks/");
            Task.Delay(1000).Wait();
            return JsonConvert.DeserializeObject<List<Book>>(result.Result);
        }
        public List<string> RefreshBooksLibrary(List<Book> listBooks)
        {
            List<string> newBookList = new List<string>();
            foreach (Book book in listBooks)
            {
                newBookList.Add($"{book.firstNameAuthor} {book.lastNameAuthor} {book.nameBook}");
            }
            return newBookList;
        }
        public List<string> CreateStringListBook(Person person)
        {
            List<string> infoListBoks = new List<string>();
            foreach (Book book in person.books)
            {
                infoListBoks.Add($"{book.lastNameAuthor} {book.firstNameAuthor} {book.nameBook}");
            }
            return infoListBoks;
        }
        public List<string> RefreshPerson(List<Person> listPerson)
        {
            List<string> newComboList = new List<string>();
            foreach (Person person in listPerson)
            {
                newComboList.Add($"{person.firstName} {person.lastName} {person.middleName}");
            }
            return newComboList;
        }
    }
}