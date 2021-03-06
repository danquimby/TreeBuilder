﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Web.Script.Serialization;

namespace TreeBuilder
{
    public partial class TreeBuilder : Form
    {
        List<Color> ColorIndex = new List<Color>();
        private List<Button> ListItems = new List<Button>();
        List<string> HistoryNode = new List<string>();
        private readonly List<string> ListOfAllCategory = new List<string>();
        private readonly List<string> ListOfAllParentCategory = new List<string>(); 
        public TreeBuilder()
        {
            InitializeComponent();
            btnBack.Enabled = HistoryNode.Count > 0;
            ColorIndex.Add(Color.Brown);
            ColorIndex.Add(Color.BlueViolet);
            ColorIndex.Add(Color.CadetBlue);
            ColorIndex.Add(Color.Chartreuse);
            ColorIndex.Add(Color.Coral);
            ColorIndex.Add(Color.Cyan);

        }

        private dynamic Deserialise(string _json)
        {
            try
            {
                JavaScriptSerializer var = new JavaScriptSerializer();
                var.MaxJsonLength = _json.Length + 50;
                return var.DeserializeObject(_json);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return _json;
            }
        }
        private string Serialise(dynamic _value)
        {
            return new JavaScriptSerializer().Serialize(_value);
        }

        private HttpWebRequest CreateHttp(string createOption)
        {
            return WebRequest.Create(createOption) as HttpWebRequest;
        }
        private dynamic SendRequset(string options)
        {
            dynamic ev = new EventData();
            string result = "";
            HttpWebRequest httpWeb = CreateHttp(options);
            if (httpWeb == null)
            {
                return new Exception("error");
            }
            httpWeb.Timeout = 40000;
            httpWeb.ReadWriteTimeout = 40000;
            httpWeb.Method = "GET";
            httpWeb.ContentType = "application/x-www-form-urlencoded; boundary=----WebKitFormBoundarySkAQdHysJKel8YBM";
            httpWeb.UseDefaultCredentials = true;
            httpWeb.ServicePoint.Expect100Continue = false;

            httpWeb.Headers.Add(HttpRequestHeader.Authorization,
                    "Basic YWthZG9tQGdtYWlsLmNvbTpZVGcxWmpBMU1tUTFNelZoTW1JeFpESTVabUZqTmpZNVpUazBPVFUyTWprNk1tUQ");

            try
            {
                HttpWebResponse httpResponse = httpWeb.GetResponse() as HttpWebResponse;
                if (httpResponse == null)
                {
                    return new Exception("error");
                }
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
                Dictionary<string, string> headers = new Dictionary<string, string>();
                for (int i = 0; i < httpResponse.Headers.Count; ++i)
                    headers.Add(httpResponse.Headers.Keys[i], httpResponse.Headers[i]);
                ev.headers = headers;
                ev.successful = true;
                ev.items = Deserialise(result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return ev;
        }

        private void btnSendRequest_Click_1(object sender, EventArgs e)
        {
            HistoryNode.Add(tbValue.Text);
            btnBack.Enabled = HistoryNode.Count > 1;
            MainPanel.Controls.Clear();
            ListItems.Clear();
            int StepW = 1;
            int StepH = 1;
            ListOfAllCategory.Clear();
            //string reqest = GenerateRequest(tbValue.Text);
            CreateTreeNodesEx(tbValue.Text, "", new Point(0, 0));
        }

        private string GenerateRequest(string id)
        {
            string reqest = tbAsk.Text;
            reqest += "?expand=categoriesDescription&advancedFilter=[{";
            reqest += tbParent.Text + ": ";
            reqest += id;
            reqest += "},{\"categories_status\": 1}]";
            return reqest;
        }

        private void AddCategory(string categoryNumebr, string categoryDescription, int x)
        {
            string category = "";
            category += '\r';
            category += '\n';
            for (int i = 0; i < x * 2; i++)
                category += ' ';
            category += categoryNumebr;
            category += " - ";
            category += categoryDescription;

            ListOfAllCategory.Add(category);
            if (categoryDescription != String.Empty)
                ListOfAllParentCategory.Add(categoryDescription);
        }
        private Point CreateTreeNodesEx(string rootNumber, string rootDescription, Point point)
        {
            dynamic data = SendRequset(GenerateRequest(rootNumber));

            if (rootDescription == String.Empty)
                rootDescription = ParseFromDynamicData(data.items, 0, "categoriesDescription", "categories_name").ToString();
            if (rootDescription == String.Empty)
                rootDescription = ParseFromDynamicData(data.items, 0, "categoriesDescription", "categories_description").ToString();
            if (rootDescription == String.Empty)
                rootDescription = ParseFromDynamicData(data.items, 0, "categoriesDescription", "categories_heading_title").ToString();
            if (rootDescription == String.Empty)
                rootDescription = "нет описание";
            AddCategory(rootNumber, rootDescription, point.X);
            CreateAndPushNode(rootNumber, rootDescription, point);
            Point tmpPoint = new Point(point.X + 1, point.Y);
            foreach (var item in data.items)
            {

                string description = ParseFromDynamicData(item, "categoriesDescription", "categories_name").ToString();
                if (description == String.Empty)
                    description = ParseFromDynamicData(item, "categoriesDescription", "categories_description").ToString();
                if (description == String.Empty)
                    description = ParseFromDynamicData(item, "categoriesDescription", "categories_heading_title").ToString();
                if (description == String.Empty)
                    description = "нет описание";

                Console.WriteLine("parse dd " + description);
                string number = item[tbKey.Text].ToString();
                tmpPoint = new Point(tmpPoint.X, tmpPoint.Y + 1);
                tmpPoint = CreateTreeNodesEx(number, description, tmpPoint);

                Control[] ww = MainPanel.Controls.Find(number, true);
                if (ww.Count() == 0)
                {
                    AddCategory(rootNumber, rootDescription, point.X);
                    CreateAndPushNode(number, description, tmpPoint);
                }
            }
            point = new Point(point.X, tmpPoint.Y);
            return point;
        }

        private void CreateAndPushNode(string id, string text, Point point)
        {
            string complexName = id + "\n" + text;
            NodeItem node = new NodeItem(id, complexName, new Point(10 + point.X * 200, 70 * point.Y));
            node.MouseUp += ClickToNode;
            node.BackColor = ColorIndex[point.X];
            MainPanel.Controls.Add(node);
        }
        private void ClickToNode(object o, MouseEventArgs e)
        {
            /*
                        if (e.Button == MouseButtons.Right)
                        {
            //                Clipboard.SetText(NameButton);
                        }
                        else
                        {
                            NodeItem item = o as NodeItem;
                            if (item == null) return;
                            tbValue.Text = item.Text;
                            btnSendRequest_Click_1(null, null);
                        }
            */
        }
        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            OnPaint(e);

            /*
                        using (Graphics g = e.Graphics)
                        {
                            for (int i = 0; i < 300; i += 8)
                            {
                                var p = new Pen(Color.Red, 2);
                                var point1 = new Point(234 + i, 118);
                                var point2 = new Point(293 - i, 228);
                                g.DrawLine(p, point1, point2);
                            }
                        }
            */

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (HistoryNode.Count > 1)
            {
                tbValue.Text = HistoryNode[HistoryNode.Count - 2];
                HistoryNode.RemoveAt(HistoryNode.Count - 2);
                HistoryNode.RemoveAt(HistoryNode.Count - 1);
                btnSendRequest_Click_1(null, null);
            }
            btnBack.Enabled = HistoryNode.Count > 1;
        }

        private void tbValue_KeyUp(object sender, KeyEventArgs e)
        {
            button2_Click(null, null);
        }
        public dynamic ParseFromDynamicData(dynamic data, params object[] keys)
        {

            if (keys.Length == 0) return String.Empty;
            foreach (object key in keys)
            {
                try
                {
                    if ((key is string) && !data.ContainsKey(key.ToString()))
                    {
                        //MessageBox.Show("Не корректные данные от сервера для ключа " + KeyHistory + "/" + key);
                        return String.Empty;
                    }
                }
                catch
                {
                    return String.Empty;
                }

                try
                {
                    if ((key is int) && Convert.ToInt32(key.ToString()) >= data.Length)
                    {
                        return String.Empty;
                    }
                }
                catch
                {
                    return "";
                }
                if (key is string)
                    data = data[key.ToString()];
                else
                    data = data[Convert.ToInt32(key)];
            }

            return data ?? String.Empty;
        }

        private void btnToText_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "category.txt";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Console.WriteLine(saveFileDialog1.FileName);


                using (StreamWriter outfile = new StreamWriter(saveFileDialog1.FileName, true))
                {
                    for (int i = 0; i < ListOfAllCategory.Count; i++)
                    {
                        outfile.WriteLine(ListOfAllCategory[i]);
                        Console.WriteLine(ListOfAllCategory[i] + (i >= ListOfAllParentCategory.Count ? " " : ListOfAllParentCategory[i]));
                    }
                }
            }
        }

    }
    public class EventData : DynamicObject, ICloneable
    {
        public bool successful { set; get; }
        public int sessionId { set; get; }
        public object sender { set; get; }
        public string image { set; get; }
        // путь к Api
        public string PathApi { set; get; }
        // метод "GET" "PUT" и тд
        public string Method { set; get; }
        // другое поля не обязательное
        //public dynamic Data { set; get; }

        public Dictionary<string, object> BodyParam { set; get; }
        public Dictionary<string, object> UrlParam { set; get; }

        public Dictionary<string, string> DictionaryData;

        public EventData()
        {
            image = "";
            sender = new object();
            sessionId = -1;
        }
        // The inner dictionary.
        public Dictionary<string, object> dictionary
            = new Dictionary<string, object>();


        // This property returns the number of elements
        // in the inner dictionary.
        public int Count
        {
            get { return dictionary.Count; }
        }

        // If you try to get a value of a property 
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name.ToLower();

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            return dictionary.TryGetValue(name, out result);
        }

        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            dictionary[binder.Name.ToLower()] = value;

            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }

        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);

                stream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(stream);
            }
        }

    }
}

