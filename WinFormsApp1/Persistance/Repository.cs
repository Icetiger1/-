using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Infrastructure;
using WinFormsApp1.Model;

//репозиторий crud операций для работы с бд oracle
namespace WinFormsApp1.Persistance
{
    public class Repository<T> : IRepository<T>
        where T : Entities, new()
    {
        private QueryData queryData;
        private List<T> storage;

        public Repository()
        {
            queryData = new QueryData();
            storage = new List<T>();
        }

        public void Append(T t)
        {
            //сделать добавление строки в базу
            string sqlQuery = "";
            queryData.Execute(sqlQuery);

        }

        public T Get(int id)
        {
            //получение конкретной строки
            string sqlQuery = "";
            DataTable dataTable = new DataTable();
            queryData.Execute(sqlQuery, dataTable);

            return this.storage.Where(item => item.Id == id).FirstOrDefault();
        }

        public void Move()
        {
            //сделать перемещение каталога и подкаталогов в базе 
            string sqlQuery = "";
            queryData.Execute(sqlQuery);
        }

        public void Remove(int id)
        {
            //удаление каталоги и всех подкаталогов
            string sqlQuery = "";
            queryData.Execute(sqlQuery);
        }

        public void Rename(int id, string newName)
        {
            //переименование конкретного каталога в иерархии
            string sqlQuery = "";
            queryData.Execute(sqlQuery);
        }
    }
}
