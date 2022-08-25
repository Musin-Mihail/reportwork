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

namespace bibliobook
{
    public partial class Bibliobook : Window
    {
        DTOClass dtoClass = new DTOClass();
        HttpClient client = new HttpClient();
        public List<Person> listPerson = new List<Person>();
        public List<Book> listBooks = new List<Book>();
        public Bibliobook()
        {
            InitializeComponent();
            listPerson = dtoClass.CreatePerson();
            listBooks = dtoClass.CreateBooks();
        }
        private void ComboPerson_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> newComboList = dtoClass.RefreshPerson(listPerson);
            if (newComboList.Count > 0)
            {
                ComboPerson.ItemsSource = newComboList;
                ComboPerson.SelectedIndex = 0;
                AddInfo(listPerson[ComboPerson.SelectedIndex]);
            }
        }
        private void ListBooks_Loaded(object sender, RoutedEventArgs e)
        {
            ListBooks.ItemsSource = dtoClass.RefreshBooksLibrary(listBooks);
        }
        public void AddInfo(Person person)
        {
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
        private void Button_Click_Pass_Book(object sender, RoutedEventArgs e)
        {
            if (ListBooks.SelectedIndex >= 0 && ComboPerson.SelectedIndex >= 0)
            {
                int indexPerson = ComboPerson.SelectedIndex;
                int indexBookLibrary = ListBooks.SelectedIndex;
                Person person = listPerson[indexPerson];
                Book book = listBooks[indexBookLibrary];
                person.books.Add(book);
                AddInfo(person);
                listBooks.RemoveAt(indexBookLibrary);
                ListBooks.ItemsSource = dtoClass.RefreshBooksLibrary(listBooks);
            }
        }
        private void Button_Click_Transfer_Library(object sender, RoutedEventArgs e)
        {
            if (ListPersonBooks.SelectedIndex >= 0 && ComboPerson.SelectedIndex >= 0)
            {
                int indexPerson = ComboPerson.SelectedIndex;
                int indexBookPerson = ListPersonBooks.SelectedIndex;
                Person person = listPerson[indexPerson];
                Book book = person.books[indexBookPerson];
                listBooks.Add(book);
                person.books.RemoveAt(indexBookPerson);
                AddInfo(person);
                ListBooks.ItemsSource = dtoClass.RefreshBooksLibrary(listBooks);
            }
        }
        private void ComboPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddInfo(listPerson[ComboPerson.SelectedIndex]);
        }
        private void Button_Click_Transfer_All_Library(object sender, RoutedEventArgs e)
        {
            if (ComboPerson.SelectedIndex >= 0)
            {
                int indexPerson = ComboPerson.SelectedIndex;
                Person person = listPerson[indexPerson];
                listBooks.AddRange(person.books);
                person.books.Clear();
                AddInfo(person);
                ListBooks.ItemsSource = dtoClass.RefreshBooksLibrary(listBooks);
            }
        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}