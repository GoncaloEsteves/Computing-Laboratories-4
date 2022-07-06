using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data.DataAcess;
using Data.Classes;
using System.Text;
using System.IO;

namespace Data
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            DataAcess.ChargingStation postos = new DataAcess.ChargingStation("server=plugandgobeyonddb.mysql.database.azure.com;user=PlugAndGoBeyond@plugandgobeyonddb;database=plugandgobeyonddb;port=3306;password=basededadosLI4");

            List<ChargingStation> postosaux = postos.GetAll();

            foreach (ChargingStation aux in postosaux)
            {
                MessageBox.Show(aux.Id.ToString());
                MessageBox.Show(aux.Position.Longitude.ToString());
                MessageBox.Show(aux.Position.Latitude.ToString());
                foreach (EV_Connector t in aux.ChargingSockets)
                {
                    MessageBox.Show(t.Id.ToString());
                    MessageBox.Show(t.Status.ToString());
                    MessageBox.Show(t.PowerOutput.ToString());
                    MessageBox.Show(t.Type);
                }
            }
        }
    }
}
