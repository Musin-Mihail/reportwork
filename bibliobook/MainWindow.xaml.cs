using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using booksdto;
using System.Net.Http;
using Newtonsoft.Json;
using System.Reflection;

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
            dtoClass.CreateBooks();
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
            Task.Delay(500).Wait();
            return JsonConvert.DeserializeObject<List<string>>(result.Result);
        }
        private void ListBooks_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshBooksLibrary();
        }
        void RefreshBooksLibrary()
        {
            var result = client.GetStringAsync("https://localhost:5001/Books/RefreshBooksLibrary/");
            Task.Delay(500).Wait();
            List<string> newList = JsonConvert.DeserializeObject<List<string>>(result.Result);
            ListBooks.ItemsSource = newList;
        }
        public void AddInfo()
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
        Person GetPerson(int index)
        {
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetPerson/{index}");
            Task.Delay(500).Wait();
            return JsonConvert.DeserializeObject<Person>(result.Result);
        }
        Book GetBookLibrary(int index)
        {
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetBookLibrary/{index}");
            Task.Delay(500).Wait();
            return JsonConvert.DeserializeObject<Book>(result.Result);
        }
        Book GetBookPerson(int indexPerson, int indexBook)
        {
            var result = client.GetStringAsync($"https://localhost:5001/Books/GetBookPerson/{indexPerson},{indexBook}");
            Task.Delay(500).Wait();
            return JsonConvert.DeserializeObject<Book>(result.Result);
        }
        void RemoveBookLibrary(int index)
        {
            client.GetStringAsync($"https://localhost:5001/Books/RemoveBookLibrary/{index}");
            Task.Delay(500).Wait();
        }
        void RemoveBookPerson(int indexPerson, int indexBook)
        {
            client.GetStringAsync($"https://localhost:5001/Books/RemoveBookPerson/{indexPerson},{indexBook}");
            Task.Delay(500).Wait();
        }
        private void Button_Click_Pass_Book(object sender, RoutedEventArgs e)
        {
            if (ListBooks.SelectedIndex >= 0 && ComboPerson.SelectedIndex >= 0)
            {
                int indexPerson = ComboPerson.SelectedIndex;
                int indexBookLibrary = ListBooks.SelectedIndex;
                AddBookPerson(indexPerson, indexBookLibrary);
                AddInfo();
                RemoveBookLibrary(indexBookLibrary);
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
        void AddBookPerson(int indexPerson, int indexBook)
        {
            client.GetStringAsync($"https://localhost:5001/Books/AddBookPerson/{indexPerson},{indexBook}");
            Task.Delay(500).Wait();
        }
        void AddBookLibrary(Book book)
        {
            string result = JsonConvert.SerializeObject(book);
            client.GetStringAsync($"https://localhost:5001/Books/AddBookLibrary/{result}");
            Task.Delay(500).Wait();
        }
        void TransferAllLibrary(int indexPerson)
        {
            client.GetStringAsync($"https://localhost:5001/Books/TransferAllLibrary/{indexPerson}");
            Task.Delay(500).Wait();
        }
        private void Button_Click_Transfer_All_Library(object sender, RoutedEventArgs e)
        {
            if (ComboPerson.SelectedIndex >= 0)
            {
                int indexPerson = ComboPerson.SelectedIndex;
                //Person person = listPerson[indexPerson];
                TransferAllLibrary(indexPerson);
                //listBooks.AddRange(person.books);
                //person.books.Clear();
                AddInfo();
                RefreshBooksLibrary();
            }
        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}