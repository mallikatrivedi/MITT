using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
namespace MITT
{
    public class Class1
    {
        public static void WriteLog(String ex)
        {

            string LogPath = AppDomain.CurrentDomain.BaseDirectory;
            string filename = "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            string filepath = LogPath + filename;
            if (File.Exists(filepath))
            {
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("-------------------START-------------" + DateTime.Now);
                    writer.WriteLine("Source :" +"1");
                    writer.WriteLine(ex);
                    writer.WriteLine("-------------------END-------------" + DateTime.Now);
                }
            }
            else
            {
                StreamWriter writer = File.CreateText(filepath);
                writer.WriteLine("-------------------START-------------" + DateTime.Now);
                writer.WriteLine("Source :" + "2");
                writer.WriteLine(ex);
                writer.WriteLine("-------------------END-------------" + DateTime.Now);
                writer.Close();
            }
        }
    }
}