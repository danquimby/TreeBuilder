using System.Drawing;
using System.Windows.Forms;

namespace TreeBuilder
{
    class NodeItem : Button
    {
        public NodeItem(string name, string text, Point pt)
        {
            Name = name;
            Text = text;
            Location = pt;
            BackColor = System.Drawing.Color.DarkSalmon;
            Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            ForeColor = System.Drawing.Color.White;
            Size = new System.Drawing.Size(94, 34);
            UseVisualStyleBackColor = false;

        }
    }
}
