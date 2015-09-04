using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Web.Script.Serialization;

namespace TreeBuilder
{
    public partial class TreeBuilder : Form
    {
        public class SuperNode : TreeNode
        {

        }
        public class Node
        {
            public string parent = "";
            public string text = "";
            public string key = "";
            public List<string> data;

            public Node(string parent, string key, string text = "")
            {
                this.parent = parent;
                this.text = text;
                this.key = key;
            }
            //  public List<Node> children = new List<Node>();
        }
        public TreeBuilder()
        {
            InitializeComponent();
        }
        List<Node> list = new List<Node>();

        public void ListSort(List<Node> listNodes, string keySort)
        {
          List<Node> temp = new List<Node>();
            temp.AddRange(listNodes.FindAll(xx=>xx.parent==keySort));
            if (temp.Count > 0)
            {
                temp.ForEach(AddToTree);
                foreach (Node item in temp)
                {
                    ListSort(listNodes, item.key);
                }
            }
            else return;
        }

        public bool Checker(TreeNodeCollection nodes, string key, ref string path)
        {
            bool result = false;
            if (nodes.Count > 0)
                if (!nodes.ContainsKey(key))
                    foreach (TreeNode node in nodes)
                    {
                        path += " " + node.Text;
                        bool t = Checker(node.Nodes, key, ref path);
                        if (t) { result = t; break; }

                    }
                else { result = true; path += " " + key; }
            else result= false;
            return result;
        }
        public void AddToTree(Node node)
        {
            string s = " ";
            if (Checker(treeView1.Nodes, node.parent, ref s))
            {
                s = s.Substring(1);
                string[] strHelp = s.Split(' ');
                TreeNodeCollection nodeCollection = treeView1.Nodes;
                for (int i = 1; i < strHelp.Length; i++)
                {
                    if (strHelp[i]!="")
                    nodeCollection = nodeCollection[strHelp[i]].Nodes;
                }
                nodeCollection.Add(node.key, node.text);
            }
            else treeView1.Nodes.Add(node.key, node.text);
        }
        public void AddToTree(string parent, string key, string text = "")
        {
            if (treeView1.Nodes.ContainsKey(parent))
                treeView1.Nodes[parent].Nodes.Add(key, text);
            else treeView1.Nodes.Add(key, text);
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
        private void btnEat_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer j = new JavaScriptSerializer();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string help = tbJson.Text.Remove(tbJson.Text.Length - 1);
            help = help.Substring(1);
            string tabcount = "";
            string temp = "";
            Node tempNode = new Node("", "");
            bool check = false;

            for (int i = 0; i < help.Length; i++)
            {

                char item = help[i];
                if (check)
                    temp += item;
                if (item == '"')
                {
                    if (!check)
                    {
                        check = true;
                        temp += item;
                    }
                    else
                    {
                        check = false;
                        if (temp == tbKey.Text)
                        {
                            temp = "";
                            for (int f = i + 2; f < help.Length; f++)
                            {
                                char h = help[f];
                                if (h != ',')
                                    temp += h;
                                else break;
                            }
                            tempNode.key = temp;
                            tempNode.text = temp;
                        }
                        else
                        {
                            if (temp == tbParent.Text)
                            {
                                temp = "";
                                for (int k = i + 2; k < help.Length; k++)
                                {
                                    char h = help[k];
                                    if (h != ',')
                                        temp += h;
                                    else break;
                                }
                                tempNode.parent = temp;
                                list.Add(tempNode);
                                // AddToTree(tempNode);
                                tempNode = new Node("", "");
                            }

                        }
                        temp = "";
                    }
                }
                if (item == '[')
                {
                    if (help[i - 1] == ':')
                        Console.WriteLine();

                    tabcount += "\t";
                    Console.WriteLine(tabcount + item);
                    Console.Write(tabcount);
                }
                else
                    if (item == '{')
                    {
                        if (i - 1 >= 0)
                            if (help[i - 1] == ':')
                                Console.WriteLine();
                        Console.WriteLine(tabcount + item);
                        tabcount += "\t";

                        Console.Write(tabcount);
                    }
                    else if (item == ']')
                    {
                        Console.WriteLine();
                        tabcount = tabcount.Remove(tabcount.Length - 1);
                        Console.Write(tabcount + item);
                        Console.Write(tabcount);
                    }
                    else if (item == '}')
                    {
                        Console.WriteLine();
                        tabcount = tabcount.Remove(tabcount.Length - 1);
                        Console.Write(tabcount + item);
                        Console.Write(tabcount);
                    }
                    else if (item == ',')
                    {
                        if (help[i - 1] == '}')
                            Console.WriteLine();
                        else
                        {
                            Console.Write(item + "\r");
                            Console.Write(tabcount);
                        }

                    }
                    else Console.Write(item);
            }
          //  treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // treeView1.ExpandAll();
            tbJson.Text = "";
            //   Console.WriteLine(Serialise(list));
        }

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        public async Task<dynamic> ReqTask()
        {
            string result = "";
            try
            {
                HttpWebRequest h = (HttpWebRequest)WebRequest.Create(tbAsk.Text + ((!String.IsNullOrEmpty(tbQuery.Text)) ?  ("?" + tbQuery.Text):""));
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                h.ContentType = "multipart/form-data; boundary=" + boundary;
                h.Method = "GET";
                h.KeepAlive = true;
                h.Credentials = CredentialCache.DefaultCredentials;
                h.Headers.Add(HttpRequestHeader.Authorization,
                    "Basic YWthZG9tQGdtYWlsLmNvbTpZVGcxWmpBMU1tUTFNelZoTW1JeFpESTVabUZqTmpZNVpUazBPVFUyTWprNk1tUQ");
                h.ServicePoint.Expect100Continue = false;
                HttpWebResponse httpResponse = h.GetResponse() as HttpWebResponse;
                if (httpResponse == null) return "";                            
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
                Dictionary<string, string> headers = new Dictionary<string, string>();
                for (int i = 0; i < httpResponse.Headers.Count; ++i)
                    headers.Add(httpResponse.Headers.Keys[i], httpResponse.Headers[i]);
                if (headers.ContainsKey("X-Pagination-Page-Count"))
                {
                    progressBar1.Step = 1;
                    progressBar1.Maximum = Convert.ToInt32(headers["X-Pagination-Page-Count"]);
                    tbJson.Text = result;
                    btnEat.PerformClick();
                    for (int i = 2; i < Convert.ToInt32(headers["X-Pagination-Page-Count"]); i++)
                    {
                        string help = ReqTaskPage(i).Result.ToString();
                        tbJson.Text = help;
                        btnEat.PerformClick();
                        progressBar1.Increment(1);
                    }
                }
            }
            catch (WebException exception)
            {
                Console.WriteLine(exception);
            }
            return result;
        }
        public async Task<dynamic> ReqTaskPage(int page)
        {
            string result = "";
            try
            {
                HttpWebRequest h = (HttpWebRequest)WebRequest.Create(tbAsk.Text + ((!String.IsNullOrEmpty(tbQuery.Text)) ? ("?" + tbQuery.Text + "&page=" + page.ToString()) : ("?page=" + page.ToString())));
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                h.ContentType = "multipart/form-data; boundary=" + boundary;
                h.Method = "GET";
                h.KeepAlive = true;
                h.Credentials = CredentialCache.DefaultCredentials;
                h.Headers.Add(HttpRequestHeader.Authorization,
                    "Basic YWthZG9tQGdtYWlsLmNvbTpZVGcxWmpBMU1tUTFNelZoTW1JeFpESTVabUZqTmpZNVpUazBPVFUyTWprNk1tUQ");
                h.ServicePoint.Expect100Continue = false;
                HttpWebResponse httpResponse = h.GetResponse() as HttpWebResponse;



                if (httpResponse == null) return "";

                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
                Dictionary<string, string> headers = new Dictionary<string, string>();
                for (int i = 0; i < httpResponse.Headers.Count; ++i)
                    headers.Add(httpResponse.Headers.Keys[i], httpResponse.Headers[i]);
            }
            catch (WebException exception)
            {
                Console.WriteLine(exception);
            }
            return result;
        }
        private void btnBuild_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            AddToTree("",tbStart.Text,"Root");
            //list.ForEach((x)=>Console.WriteLine(x.parent+" "+x.key+" "+x.text));
            ListSort(list,tbStart.Text); 
           /* Console.WriteLine();
            list.ForEach((x) => Console.WriteLine(x.parent + " " + x.key + " " + x.text));
            list.ForEach(AddToTree);*/
        }

        private void btnSendRequest_Click(object sender, EventArgs e)
        {
            tbJson.Text = ReqTask().Result as string;
        }
    }
}
