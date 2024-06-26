﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace WPFtves.AppData
{
    public class Configuration
    {
        private string filename = "./app.json";
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Base { get; set; }

        public Configuration(string filename = "./app.json")
        {
            this.filename = filename;
            try
            {
                FileStream file = File.OpenRead(filename);
                if (file != null)
                {
                    byte[] bytes = new byte[file.Length+1];
                    int bytesRead = file.Read(bytes, 0, bytes.Length);

                    file.Close();

                    if (bytesRead == 0 || bytesRead != bytes.Length)
                    {
                        Default();
                        Save();
                        return;
                    }

                    Configuration conf = JsonConvert.DeserializeObject(Encoding.Default.GetString(bytes)) as Configuration;
                    if (conf != null)
                    {
                        Host = conf.Host;
                        Port = conf.Port;
                        User = conf.User;
                        Pass = conf.Pass;
                        Base = conf.Base;
                    }
                    else
                    {
                        MessageBox.Show("Файл конфигурации имеет не корректный формат", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Default();
                        Save();
                    }
                } else
                {
                    Default();
                    Save();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                Default();
                Save();
            }
        }

        public void Default()
        {
            Host = "localhost";
            Port = "5432";
            User = "user01";
            Pass = "123QWE456";
            Base = "patients";
        }

        public void Save()
        {
            try
            {
                string jsonObject = JsonConvert.SerializeObject(this);
                byte[] bytes = Encoding.Default.GetBytes(jsonObject);

                FileStream file = File.OpenWrite(filename);
                file.Write(Encoding.Default.GetBytes(jsonObject), 0, bytes.Length);
                file.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string GetConnectionString()
        {
            return string.Format(
                "Server={0};Port={1};User Id={2};Password={3};Database={4}",
                Host, Port, User, Pass, Base);

        }

    }
}
