using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFtves.AppData;

namespace WPFtves.Objects
{
    public class Measurement : INotifyPropertyChanged
    {

        public Measurement()
        {
        }

        public Measurement(int id, double height, double weight, int idPatient)
        {
            Id = id;
            Height = height;
            Weight = weight;
            IdPatient = idPatient;
        }

        public int Id { get; set; }

        private double? height;
        
        public double? Height 
        {
            get => height;
            set
            {
                if (value != height)
                {
                    height = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Imt");
                }
            }
        }

        private double? weight;

        public double? Weight
        {
            get => weight;
            set
            {
                if (value != weight)
                {
                    weight = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Imt");
                }
            }
        }

        public int IdPatient { get; set; }

        public double? Imt 
        {
            get
            {
                if (Height == null || Weight == null)
                {
                    return null;
                }

                if (Weight == 0 || Height == 0)
                {
                    return null; ;
                }

                double? a = Weight / ((Height / 100) * (Height / 100));

                if (a <= 5 || a >= 80)
                {
                    return null;
                }

                return Math.Round((double)a, 1);
                
            }
        }




        public bool Create()
        {
            var command = Database.GetCommand("INSERT INTO measurements (height, weight, id_patient) VALUES (@height, @weight, @idPatient)");

            command.Parameters.AddWithValue("@height", NpgsqlDbType.Double, Height);
            command.Parameters.AddWithValue("@weight", NpgsqlDbType.Double, Weight);
            //command.Parameters.AddWithValue("@imt", NpgsqlDbType.Double, Imt);
            command.Parameters.AddWithValue("@idPatient", NpgsqlDbType.Integer, IdPatient);
            var result = command.ExecuteNonQuery();
            return result == 1;
        }

        public static ObservableCollection<Measurement> GetList(int idPatient)
        {
            ObservableCollection<Measurement> list = new ObservableCollection<Measurement>();
            try
            {
                var command = Database.GetCommand(@$"
                SELECT id, height, weight, id_patient
                FROM measurements
                WHERE id_patient=@idPatient 
                ORDER BY height, weight");

                command.Parameters.AddWithValue("@idPatient", NpgsqlDbType.Integer, idPatient);

                var result = command.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        list.Add(new Measurement(
                            result.GetInt32(0),
                            result.GetDouble(1),
                            result.GetDouble(2),
                            result.GetInt32(3)));                    
                    }
                }
                result.Close();
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить журнал измерений! \n\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return list;
        }

        public bool Delete()
        {
            var command = Database.GetCommand($"DELETE FROM measurements WHERE id = {Id}");
            var result = command.ExecuteNonQuery();
            return result == 1;

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
