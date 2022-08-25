//using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bookdb
{
    internal class bookdb
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Запущен");
        }


        //Переименование столбца
        //String sql = "EXEC sp_rename 'dbo.Books.Name', 'firstNameAuthor','column' ";

        // Добавить колонку
        //String sql = "ALTER TABLE dbo.Books ADD lastNameAuthor nchar(50) NULL";
        //NOT NULL

        //Создать таблицу
        //String sql = "CREATE TABLE Persons(firstName nchar(50) NULL, lastName CHAR(50) NULL, middleName CHAR(50) NULL, books CHAR(500) NULL, status CHAR(50) NULL)";

        //Изменить тип колонки
        //string sql = "ALTER TABLE dbo.Persons ALTER COLUMN firstName CHAR(50) NULL";

        //Добавить строчку
        //string sql = "INSERT INTO dbo.Books(firstNameAuthor, lastNameAuthor, nameBook) VALUES('Имя', 'Фамилия', 'Книга')";

        //Очистить таблицу
        //string sql = "DELETE FROM Books";

        //Прочитать все строки
        //string sql = "SELECT * FROM Books"; 

        //void UpdateRow(MySqlConnection conn)
        //{
        //    string sql = "UPDATE status SET date = 777 WHERE id = '1'";
        //}
        //void ReadingRow(MySqlConnection conn)
        //{
        //    string sql = "SELECT * FROM status WHERE id = '1'";
        //}
        //void GetTypeColums(MySqlConnection conn)
        //{
        //    string sql = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'status' AND COLUMN_NAME = 'date';";
        //}
    }
}