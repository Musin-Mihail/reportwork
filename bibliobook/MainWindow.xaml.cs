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

namespace bibliobook
{
    public partial class MainWindow : Window
    {
        List<Person> listPerson = new List<Person>();
        List<string> listBooks = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            CreatePerson();
            CreateBooks();
        }
        void CreatePerson()
        {
            Person person = new Person();
            person.name = "Имя1";
            person.books.Add("КнигаНаРуках1");
            person.status = "schoolboy";
            listPerson.Add(person);

            person = new Person();
            person.name = "Имя2";
            person.books.Add("КнигаНаРуках2");
            person.status = "student";
            listPerson.Add(person);

            person = new Person();
            person.name = "Имя3";
            person.books.Add("КнигаНаРуках3");
            person.status = "employee";
            listPerson.Add(person);
        }
        void CreateBooks()
        {
            for (int i = 1; i < 6; i++)
            {
                listBooks.Add("Книга" + i);
            }
        }
        private void ComboPerson_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> newComboList = new List<string>();
            foreach (Person person in listPerson)
            {
                newComboList.Add(person.name);
            }
            ComboPerson.ItemsSource = newComboList;
            ComboPerson.SelectedIndex = 0;
            AddInfo(listPerson[ComboPerson.SelectedIndex]);
        }
        void AddInfo(Person person)
        {
            List<string> infoListBoks = new List<string>();
            foreach (string book in person.books)
            {
                infoListBoks.Add(book);
            }
            ListPersonBooks.ItemsSource = infoListBoks;
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
        }
        private void ListBooks_Loaded(object sender, RoutedEventArgs e)
        {
            ListBooks.ItemsSource = listBooks;
        }
        private void Button_Click_Pass_Book(object sender, RoutedEventArgs e)
        {
            if (ListBooks.SelectedIndex >= 0 && ComboPerson.SelectedIndex >= 0)
            {
                listPerson[ComboPerson.SelectedIndex].books.Add(listBooks[ListBooks.SelectedIndex]);
                AddInfo(listPerson[ComboPerson.SelectedIndex]);
                listBooks.RemoveAt(ListBooks.SelectedIndex);
                ListBooks.ItemsSource = null;
                ListBooks.ItemsSource = listBooks;
            }
        }
        private void Button_Click_Transfer_Library(object sender, RoutedEventArgs e)
        {
            if (ListPersonBooks.SelectedIndex >= 0 && ComboPerson.SelectedIndex >= 0)
            {
                listBooks.Add(listPerson[ComboPerson.SelectedIndex].books[ListPersonBooks.SelectedIndex]);
                listPerson[ComboPerson.SelectedIndex].books.RemoveAt(ListPersonBooks.SelectedIndex);
                AddInfo(listPerson[ComboPerson.SelectedIndex]);
                ListBooks.ItemsSource = null;
                ListBooks.ItemsSource = listBooks;
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
                listBooks.AddRange(listPerson[ComboPerson.SelectedIndex].books);
                listPerson[ComboPerson.SelectedIndex].books.Clear();
                AddInfo(listPerson[ComboPerson.SelectedIndex]);
                ListBooks.ItemsSource = null;
                ListBooks.ItemsSource = listBooks;
            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}