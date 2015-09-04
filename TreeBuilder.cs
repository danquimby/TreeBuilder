using System;
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
        private List<Button> ListItems = new List<Button>();
        List<string> HistoryNode = new List<string>();
        public TreeBuilder()
        {
            InitializeComponent();
            btnBack.Enabled = HistoryNode.Count > 0;
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
            return ev;
        }

        private void btnSendRequest_Click_1(object sender, EventArgs e)
        {
            HistoryNode.Add(tbValue.Text);
            btnBack.Enabled = HistoryNode.Count > 1;

            string reqest = tbAsk.Text;
            reqest += "?advancedFilter=[{";
            reqest += tbParent.Text + ": ";
            reqest += tbValue.Text;
            reqest += "}]";
            CreateTreeNodes(tbValue.Text, SendRequset(reqest));
        }

        private string GenerateRequest(string id)
        {
            string reqest = tbAsk.Text;
            reqest += "?advancedFilter=[{";
            reqest += tbParent.Text + ": ";
            reqest += id;
            reqest += "}]";
            return reqest;
        }
        private void CreateTreeNodes(string rootNumber, dynamic data)
        {
            MainPanel.Controls.Clear();
            ListItems.Clear();
            NodeItem itemNode = new NodeItem("root", rootNumber, new Point(10, 10));
            itemNode.MouseClick += ClickToNode;
            MainPanel.Controls.Add(itemNode);
            ListItems.Add(itemNode);
            if (data == null) return;
            int StepW = 1;
            int StepH = 1;
            RecurceMakeTree(data, ref StepW, ref StepH);
        }

        private void RecurceMakeTree(dynamic data, ref int StepW, ref int StepH)
        {
            foreach (var item in data.items)
            {
                NodeItem node = new NodeItem("root", item["categories_id"].ToString(), new Point(10 + StepW * 100, 50 * StepH));
                node.MouseUp += ClickToNode;
                MainPanel.Controls.Add(node);
                StepH++;
            }
        }
        private void ClickToNode(object o, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("eee");
            }
            else
            {
                NodeItem item = o as NodeItem;
                if (item == null) return;
                tbValue.Text = item.Text;
                btnSendRequest_Click_1(null, null);
            }
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
