using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using EasyModbus;

namespace ControlService
{
    public partial class Service1 : ServiceBase
    {
        ModbusClient modbusClient;
        Timer timer = new Timer();

        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        string ip = "ip"; // PLC IP adresi
        int port = 1; // Modbus TCP/IP bağlantı noktası
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteFile("Servis çalışmaya başladı." + DateTime.Now);
            modbusClient = new ModbusClient(ip, port); // ModbusClient oluştur
            modbusClient.Connect(); // Modbus sunucusuna bağlanma

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteFile("Servis durdu." + DateTime.Now);
            if (modbusClient != null && modbusClient.Connected)
            {
                modbusClient.Disconnect(); // Modbus sunucusundan bağlantıyı kesme
            }
        }

        public void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            GetFeaturesFromDatabase();
            //WriteFile("Servis çalışmaya devam ediyor." + DateTime.Now);
            SendValueToPLC();
        }

        public void GetFeaturesFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Requests WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", 1);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        StringBuilder featureBuilder = new StringBuilder();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            featureBuilder.Append(reader.GetName(i) + ": " + reader.GetValue(i).ToString() + ", ");
                        }
                        string features = featureBuilder.ToString().TrimEnd(' ', ',');
                        WriteFile("Features:" + features);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                WriteFile("Hata: GetFeaturesFromDatabase metodu çalışırken bir hata oluştu: " + ex.ToString());
            }
        }

        public void WriteFile(string message)
        {
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "/Logs";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string textPath = AppDomain.CurrentDomain.BaseDirectory + "/Logs/service.txt";
                if (!File.Exists(textPath))
                {
                    using (StreamWriter sw = File.CreateText(textPath))
                    {
                        sw.WriteLine(message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(textPath))
                    {
                        sw.WriteLine(message);
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("ControlService", "Hata: WriteFile metodu çalışırken bir hata oluştu: " + ex.Message, EventLogEntryType.Error);
            }
        }

		public bool GetValueFromDatabase()
		{
			bool value = false; // Varsayılan değer false

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string query = "SELECT value FROM Requests WHERE Id = @Id";
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", 1);
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					value = Convert.ToBoolean(reader["Value"]); // Veritabanından bool olarak al
				}
				reader.Close();
			}
			return value;
		}

		public void SendValueToPLC()
		{
			try
			{
				bool value = GetValueFromDatabase(); // Veritabanından değeri al
													 // Değeri y0 adresine yazma
				modbusClient.WriteSingleCoil(1280, value);

				WriteFile("Değer başarıyla PLC'ye gönderildi: " + value);
			}
			catch (Exception ex)
			{
				WriteFile("Hata: SendValueToPLC metodu çalışırken bir hata oluştu: " + ex.Message);
			}
		}
	}
}
