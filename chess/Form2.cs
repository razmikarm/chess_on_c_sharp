using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess
{
    public partial class Form2 : Form
    {
        public Form2(char turn)
        {
            InitializeComponent();
        }
        int nmb;
        private void Form2_Load(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            nmb = Int32.Parse(pb.Tag.ToString()); //(pb.Location.X - 30) / 135;
            button1.PerformClick();
        }
        public int TheValue
        {
            get { return nmb; }
        }
    }
}
