using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Model;


namespace WinFormsApp1.Infrastructure
{
    public class ApplicationDbContext
    {
        private string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                        @"AttachDbFilename=D:\Project\С#\Мое\Тестовое задание\WinFormsApp1\WinFormsApp1\Database1.mdf;" +
                        @"Integrated Security=True;User=DESKTOP-8PH2IA3\admin;Password=123";
        private SqlConnection connection = new();

        private void ConnOpen()
        {
            connection.ConnectionString = connString;
            connection.Open();
        }

        public List<Entities> GetTree()
        {
            List<Entities> data = new();

            using (connection)
            {
                ConnOpen();

                string sql = @"SELECT * FROM [Catalog] Order By [Path];";

                try
                {
                    SqlDataAdapter da = new();
                    da.SelectCommand = new(sql, connection);

                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    SqlDataReader reader = da.SelectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Entities root = new(
                                Convert.ToInt32(reader[0]),
                                reader[1] != DBNull.Value ? (string)reader[1] : string.Empty,
                                reader[2] != DBNull.Value ? (string)reader[2] : string.Empty,
                                reader[3] != DBNull.Value ? (string)reader[3] : string.Empty,
                                reader[4] != DBNull.Value ? Convert.ToInt32(reader[4]) : null
                            );

                        if (root.ParentId != null)
                        {
                            foreach (Entities entities in data)
                            {
                                if (entities.Id == root.ParentId)
                                {
                                    entities.Childrens.Add(root);
                                }
                                else
                                {
                                    root.RecursFillData(data);
                                }
                            }
                        }
                        else
                        {
                            data.Add(root);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return data;
            }
        }

        public void Move(Entities entities)
        {
            string sql = $"UPDATE [Catalog] SET [Path] = '{entities.Path}', Parent_id = {entities.ParentId} Where Id = {entities.Id};";

            using (connection)
            {
                ConnOpen();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            if (entities.Childrens.Count != 0)
            {
                foreach (Entities child in entities.Childrens)
                {
                    Move(child);
                }
            }
        }

        public void Delete(Entities entities)
        {
            string sql = $"DELETE [Catalog] Where Id = {entities.Id};";

            try
            {
                using (connection)
                {
                    ConnOpen();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    if (entities.Childrens.Count != 0)
                    {
                        foreach (Entities child in entities)
                        {
                            Delete(child);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        public void Create(Entities entities)
        {
            string sql = $"INSERT INTO [Catalog] VALUES('{entities.Name}', '{entities.Path}', '{entities.Type}', {entities.ParentId});";
            try
            {
                using (connection)
                {

                    ConnOpen();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        public void Rename(Entities entities)
        {
            string sql = $"UPDATE [Catalog] SET [Name] = '{entities.Name}', [PAth] = '{entities.Path}' Where Id = {entities.Id};";
            try
            {
                using (connection)
                {

                    ConnOpen();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }

            if (entities.Childrens.Count != 0)
            {
                foreach (Entities child in entities)
                {
                    Rename(child);
                }
            }
        }
    }
}
