using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using WPFtves.AppData;

namespace WPFtves.Objects
{
    public class Patient
    {

        public Patient() 
        { 
        }
        public Patient(int id, string lastname, string namepatient, string fathername, string birthday, string sex, string insurance, int height, int weight)
        {
            Id = id;
            LastName = lastname;
            NamePatient = namepatient;
            FatherName = fathername;
            Birthday = birthday;
            Sex = sex;
            Insurance = insurance;
            Height = height;
            Weight = weight;
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string NamePatient { get; set; }
        public string FatherName { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public string Insurance { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public bool Create()
        {
            var command = Database.GetCommand("INSERT INTO Patient (lastname, namepatient, fathername, birthday, sex, insurance, height, weight) VALUES (@a, @b, @c, @d, @e, @f, @g, @h)");

            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, LastName);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, NamePatient);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, FatherName);
            command.Parameters.AddWithValue("@d", NpgsqlDbType.Varchar, Birthday);
            command.Parameters.AddWithValue("@e", NpgsqlDbType.Varchar, Sex);
            command.Parameters.AddWithValue("@f", NpgsqlDbType.Varchar, Insurance);
            command.Parameters.AddWithValue("@g", NpgsqlDbType.Integer, Height);
            command.Parameters.AddWithValue("@h", NpgsqlDbType.Integer, Weight);
            var result = command.ExecuteNonQuery();
            return result == 1;
        }

        public static ObservableCollection<Patient> GetList()
        {
            ObservableCollection<Patient> list = new ObservableCollection<Patient>();
            try
            {
                var command = Database.GetCommand("SELECT id, lastname, namepatient, fathername, birthday, sex, insurance, height, weight FROM patient ORDER BY lastname, namepatient, fathername, birthday, sex, insurance, height, weight");
            
                var result = command.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        list.Add(new Patient(
                            result.GetInt32(0),
                            result.GetString(1),
                            result.GetString(2),
                            result.GetString(3),
                            result.GetString(4),
                            result.GetString(5),
                            result.GetString(6),
                            result.GetInt32(7),
                            result.GetInt32(8)));
                    }
                }
                result.Close();
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить список пациентов! \n\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return list;
        }

        public bool Delete(int Id)
        {
            var command = Database.GetCommand($"DELETE FROM Patient WHERE id = {Id}");

                                    
            var result = command.ExecuteNonQuery();
            return result == 1;

        }

    }
}
