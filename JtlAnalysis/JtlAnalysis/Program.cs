using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace JtlAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            int minute_time = 30;
            int seconds_time = 30 * 60;
            int interval_time = 20;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("First");
            doc.AppendChild(root);
            XmlDocument xml = new XmlDocument();
            xml.Load("E://工作内容//25_JmeterFileProcess//result.xml");
            XmlNodeList nodeList = xml.SelectNodes("testResults/httpSample");
            int lastnodeNum = seconds_time / interval_time;
            int nodeListCount = nodeList.Count;
            int aGroupNodeNum = nodeListCount / lastnodeNum;
            int i = 0;
            double sum = 0;
            int j = 0;
            foreach (XmlNode node in nodeList)
            {
                i++;
                XmlElement element = (XmlElement)node;
                string t = element.GetAttribute("t");
                sum += Convert.ToInt32(t);
                if (i % aGroupNodeNum == 0 || i == nodeListCount)
                {
                    double tps = Math.Round((i / sum) * 1000, 3);
                    j++;
                    //WriteResultXML(tps, j * interval_time);
                    XmlElement element1 = doc.CreateElement("httpSample");
                    element1.SetAttribute("tps", tps.ToString());
                    element1.SetAttribute("time", (j * interval_time).ToString());
                    root.AppendChild(element1);
                }
            }
            doc.Save(@"d:\result.xml");
            sw.Stop();
            long costTime = sw.ElapsedMilliseconds;
        }
        
        //public static void WriteResultXML(int tps, int time)
        //{
        //    if (!File.Exists("E://工作内容//25_JmeterFileProcess//resutl.xml"))
        //    { 
                
        //    }
        //}
    }
}
