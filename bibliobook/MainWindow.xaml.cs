using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using booksdto;
using System.Net.Http;
using Newtonsoft.Json;

namespace bibliobook
{
    public partial class Bibliobook : Window
    {
        DTOClass dtoClass = new DTOClass();
        HttpClient client = new HttpClient();
        public Bibliobook()
        {
            InitializeComponent();
            dtoClass.CreatePerson();
        }
        private void ComboPerson_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> newList = RefreshPerson();
            if (newList.Count > 0)
            {
                ComboPerson.ItemsSource = newList;
                ComboPerson.SelectedIndex = 0;
                AddInfo();
            }
        }
        List<string> RefreshPerson()
        {
            var result = client.GetStringAsync("https://localhost:5001/Books/RefreshPerson/");
            Task.Delay(200).Wait();
            return JsonConvert.DeserializeObject<List<string>>(result.Result);
        }
        private void ListBooks_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshBooksLibrary();
        }
        void RefreshBooksLibrary()
        {
            var result = client.GetStringAsync("https://localhost:5001/Books/RefreshBooksLibrary/");
            Task.Delay(200).Wait();
            List<string> newList = JsonConvert.DeserializeObject<List<string>>(result.Result);
            ListBooks.ItemsSource = newList;
        }
        public void AddInfo()
        {
            if (ComboPerson.SelectedIndex >= 0)
            {
                Person person = GetPerson(ComboPerson.SelectedIndex);
                ListPersonBooks.ItemsSource = dtoClass.CreateStringListBook(person);
                if (person.status == "schoolboy")
                {
                    Schoolboy.IsChecked = true;
                    Student.IsChecked = false;
                    Employee.IsChecked = false;
                }
                else if (person.status == "student")
                {
                    Schoolboy.IsChecked = false;
                    Student.IsChecked = true;
                    Employee.IsChecked = false;
                }
                else if (person.status == "employee")
                {
                    Schoolboy.IsChecked = false;
                    Student.IsChecked = false;
                    Employee.IsChecked = true;
                }
                else
                {
                    Schoolboy.IsChecked = false;
                    Student.IsChecked = false;
                    Employee.IsChecked = false;
                }
            }
        }
        Person GetPerson(int index)
        {
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetPerson/{index}");
            Task.Delay(200).Wait();
            return JsonConvert.DeserializeObject<Person>(result.Result);
        }
        Book GetBookLibrary(int index)
        {
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetBookLibrary/{index}");
            Task.Delay(200).Wait();
            return JsonConvert.DeserializeObject<Book>(result.Result);
        }
        Book GetBookPerson(int indexPerson, int indexBook)
        {
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetBookPerson/{indexPerson},{indexBook}");
            Task.Delay(200).Wait();
            return JsonConvert.DeserializeObject<Book>(result.Result);
        }
        void RemoveBookLibrary(string firstNameAuthor, string lastNameAuthor, string nameBook)
        {
            client.GetStringAsync($"https://localhost:5001/Books/RemoveBookLibrary/{firstNameAuthor},{lastNameAuthor},{nameBook}");
            Task.Delay(200).Wait();
        }
        void RemoveBookPerson(int indexPerson, int indexBook)
        {
            client.GetStringAsync($"https://localhost:5001/Books/RemoveBookPerson/{indexPerson},{indexBook}");
            Task.Delay(200).Wait();
        }
        private void Button_Click_Pass_Book(object sender, RoutedEventArgs e)
        {
            if (ListBooks.SelectedIndex >= 0 && ComboPerson.SelectedIndex >= 0)
            {
                int indexPerson = ComboPerson.SelectedIndex;
                List<string> book = new List<string>(ListBooks.Items[ListBooks.SelectedIndex].ToString().Split(' '));
                string firstNameAuthor = book[0];
                string lastNameAuthor = book[1];
                string nameBook = book[2];
                Book book1 = new Book();
                book1.firstNameAuthor = firstNameAuthor;
                book1.lastNameAuthor = lastNameAuthor;
                book1.nameBook = nameBook;
                AddBookPerson(book1, indexPerson);
                AddInfo();
                RemoveBookLibrary(firstNameAuthor, lastNameAuthor, nameBook);
                RefreshBooksLibrary();
            }
        }
        private void Button_Click_Transfer_Library(object sender, RoutedEventArgs e)
        {
            if (ListPersonBooks.SelectedIndex >= 0 && ComboPerson.SelectedIndex >= 0)
            {
                int indexPerson = ComboPerson.SelectedIndex;
                int indexBookPerson = ListPersonBooks.SelectedIndex;
                Book book = GetBookPerson(indexPerson, indexBookPerson);
                AddBookLibrary(book);
                RemoveBookPerson(indexPerson, indexBookPerson);
                AddInfo();
                RefreshBooksLibrary();
            }
        }
        private void ComboPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddInfo();
        }
        void AddBookPerson(Book book, int indexPerson)
        {
            string result = JsonConvert.SerializeObject(book);
            client.GetStringAsync($"https://localhost:5001/Books/AddBookPerson/{result},{indexPerson}");
            Task.Delay(200).Wait();
        }
        void AddBookLibrary(Book book)
        {
            string result = JsonConvert.SerializeObject(book);
            client.GetStringAsync($"https://localhost:5001/Books/AddBookLibrary/{result}");
            Task.Delay(200).Wait();
        }
        void TransferAllLibrary(int indexPerson)
        {
            client.GetStringAsync($"https://localhost:5001/Books/TransferAllLibrary/{indexPerson}");
            Task.Delay(200).Wait();
        }
        private void Button_Click_Transfer_All_Library(object sender, RoutedEventArgs e)
        {
            if (ComboPerson.SelectedIndex >= 0)
            {
                int indexPerson = ComboPerson.SelectedIndex;
                TransferAllLibrary(indexPerson);
                AddInfo();
                RefreshBooksLibrary();
            }
        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Button_Click_Remove_Book_Library(object sender, RoutedEventArgs e)
        {
            if (ListBooks.SelectedIndex >= 0)
            {
                List<string> book = new List<string>(ListBooks.Items[ListBooks.SelectedIndex].ToString().Split(' '));
                string firstNameAuthor = book[0];
                string lastNameAuthor = book[1];
                string nameBook = book[2];
                RemoveBookLibrary(firstNameAuthor, lastNameAuthor, nameBook);
                RefreshBooksLibrary();
            }
        }
        private void Button_Click_Add_Book_Library(object sender, RoutedEventArgs e)
        {
            Book book = new Book();
            book.firstNameAuthor = FirstNameAuthor.Text;
            book.lastNameAuthor = LastNameAuthor.Text;
            string nameBook = NameBook.Text.Replace(' ', '_');
            book.nameBook = nameBook;
            AddBookLibrary(book);
            RefreshBooksLibrary();
        }
        private void Button_Click_Remove_AllBook_Library(object sender, RoutedEventArgs e)
        {
            client.GetStringAsync($"https://localhost:5001/Books/RemoveAllBookLibrary/");
            Task.Delay(200).Wait();
            RefreshBooksLibrary();
        }
    }
}