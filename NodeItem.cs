using System;
using System.Drawing;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Windows.Forms;

namespace TreeBuilder
{
    class NodeItem : Button
    {
        private string NameButton;
        public NodeItem(string name, string text, Point pt)
        {
            NameButton = name;
            Name = name;
            Text = text;
            Location = pt;
            BackColor = System.Drawing.Color.DarkSalmon;
            Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            ForeColor = System.Drawing.Color.White;
            Size = new System.Drawing.Size(194, 54);
            UseVisualStyleBackColor = false;
            MouseDown += ClickToButton;
        }

        private void ClickToButton(object o, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Clipboard.SetText(NameButton);
                Console.WriteLine("RIGHT");
            }
        }
    }
}
