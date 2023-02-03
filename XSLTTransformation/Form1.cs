using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XSLTTransformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*В файле xslt не успел разобраться с корректным отображением формата. 
             * Сейчас он выводится в виде строки*/
            textBox1.Clear();
            TransformXmlByXslt();
            AddItemsCount();
            DisplayFiles();
        }
        private void TransformXmlByXslt()
        {
            XPathDocument myXPathDoc = new XPathDocument("List.xml");
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load("XSLTransform.xslt", new XsltSettings(true, true), new XmlUrlResolver());
            using (XmlTextWriter myWriter = new XmlTextWriter("Groups.xml", null))
            {
                myXslTrans.Transform(myXPathDoc, null, myWriter);
            }
        }
        private void AddItemsCount()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Groups.xml");
            XmlNode groups = doc.DocumentElement;
            foreach (XmlElement group in groups.ChildNodes)
                group.SetAttribute("count", group.ChildNodes.Count.ToString());

            doc.Save("Groups.xml");

            XmlDocument List = new XmlDocument();
            List.Load("List.xml");
            XmlElement items = List.DocumentElement;
            items.SetAttribute("count", items.ChildNodes.Count.ToString());

            List.Save("List.xml");
        }
        private void DisplayFiles()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Groups.xml");
            textBox1.AppendText("Groups.xml" + Environment.NewLine);

            XmlNode groups = doc.DocumentElement;
            foreach (XmlElement group in groups.ChildNodes)
            {
                textBox1.AppendText("Название: " + group.GetAttribute("name") + "; Количество элементов: " + group.GetAttribute("count") + Environment.NewLine);
            }

            doc.Save("Groups.xml");

            XmlDocument List = new XmlDocument();
            List.Load("List.xml");

            textBox1.AppendText(Environment.NewLine + "List.xml" + Environment.NewLine);
            XmlElement items = List.DocumentElement;
            textBox1.AppendText("Название: " + items.Name + "; Количество элементов: " + items.GetAttribute("count") + Environment.NewLine);
            List.Save("List.xml");
        }
    }
}
