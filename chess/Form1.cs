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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Timer timer = new Timer();
        bool started = false, rakir = false, newfig = false;
        char turn = 'w';
        int koxm = 80, w_hr = 01, w_mt = 30, w_sc = 00, nfig_return;
        int[,] matrixx = new int[8, 8];
        PictureBox stepping_fig = new PictureBox();
        string[] clr = new string[5] { "n", "b", "w", "b", "w"};
        int[] mvd = new int[2] { 0, 0 };
        char[] alps = new char[8] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        Bitmap[] b_figs = new Bitmap[9] { Properties.Resources.tower_b, Properties.Resources.horse_b, Properties.Resources.knight_b, Properties.Resources.queen_b, Properties.Resources.king_b, Properties.Resources.knight_b, Properties.Resources.horse_b, Properties.Resources.tower_b, Properties.Resources.soldier_b };
        Bitmap[] w_figs = new Bitmap[9] { Properties.Resources.tower_w, Properties.Resources.horse_w, Properties.Resources.knight_w, Properties.Resources.queen_w, Properties.Resources.king_w, Properties.Resources.knight_w, Properties.Resources.horse_w, Properties.Resources.tower_w, Properties.Resources.soldier_w };
        string[] fig_names = new String[9] { "tower", "horse", "knight", "queen", "king", "knight", "horse", "tower", "soldier" };
        private void Form1_Load(object sender, EventArgs e)
        {
            //matrici tesq
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    matrixx[i, j] = 0;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            // form
            this.MaximumSize = new Size(0, 0);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point(0, 0);
            koxm = (this.Size.Height - 20) / 8;
            f_control("panel1").Size = new Size(this.Size.Width - koxm * 8, this.Size.Height);
            f_control("panel1").Location = new Point(koxm * 8 + 20, 8);

            ///

                                                  ///                         Time start                       ///

            //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\

            Button start = new Button()
            {
                BackColor = Color.WhiteSmoke,
                Font = new Font(Font.FontFamily, 16),
                Size = new Size(80, 33),
                Location = new Point(48, 20),
                Name = "start",
                Text = "Start",
            };
            start.Click += start_Click;
            Button reset = new Button()
            {
                BackColor = Color.WhiteSmoke,
                Font = new Font(Font.FontFamily, 16),
                Size = new Size(80, 33),
                Location = new Point(260, 20),
                Name = "reset",
                Text = "Reset",
                Enabled = false,
            };
            reset.Click += reset_Click;
            Button resign = new Button()
            {
                BackColor = Color.WhiteSmoke,
                Font = new Font(Font.FontFamily, 12),
                Size = new Size(80, 33),
                Location = new Point(260, 65),
                Name = "resign",
                Text = "Resign",
            };
            resign.Click += resign_Click;
            Button exit = new Button()
            {
                BackColor = Color.WhiteSmoke,
                Font = new Font(Font.FontFamily, 16),
                Size = new Size(80, 33),
                Location = new Point(462, 20),
                Name = "exit",
                Text = "Exit",
            };
            exit.Click += exit_Click;

            //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\


            TextBox w_t_hour = new TextBox()
            {
                MaxLength = 2,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font(Font.FontFamily, 16),
                Size = new Size(40, 20),
                Location = new Point(50, 100),
                Name = "w_t_hour"
            };
            Label w_m_tick = new Label()
            {
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 17, FontStyle.Bold),
                Size = new Size(20, 30),
                Location = new Point(90, 100),
                Name = "w_m_tick"
            };
            TextBox w_t_minute = new TextBox()
            {
                MaxLength = 2,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font(Font.FontFamily, 16),
                Size = new Size(40, 20),
                Location = new Point(110, 100),
                Name = "w_t_minute"
            };
            Label w_s_tick = new Label()
            {
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 17, FontStyle.Bold),
                Size = new Size(20, 30),
                Location = new Point(150, 100),
                Name = "w_s_tick"
            };
            TextBox w_t_sec = new TextBox()
            {
                MaxLength = 2,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font(Font.FontFamily, 16),
                Size = new Size(40, 20),
                Location = new Point(170, 100),
                Name = "w_t_sec"
            };
            TextBox b_t_hour = new TextBox()
            {
                MaxLength = 2,
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font(Font.FontFamily, 16),
                Size = new Size(40, 20),
                Location = new Point(380, 100),
                Name = "b_t_hour"
            };
            Label b_m_tick = new Label()
            {
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 17, FontStyle.Bold),
                Size = new Size(20, 30),
                Location = new Point(420, 100),
                Name = "b_m_tick"
            };
            TextBox b_t_minute = new TextBox()
            {
                MaxLength = 2,
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font(Font.FontFamily, 16),
                Size = new Size(40, 20),
                Location = new Point(440, 100),
                Name = "b_t_minute"
            };
            Label b_s_tick = new Label()
            {
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 17, FontStyle.Bold),
                Size = new Size(20, 30),
                Location = new Point(480, 100),
                Name = "b_s_tick"
            };
            TextBox b_t_sec = new TextBox()
            {
                MaxLength = 2,
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font(Font.FontFamily, 16),
                Size = new Size(40, 20),
                Location = new Point(500, 100),
                Name = "b_t_sec"
            };
            w_t_hour.TextChanged += new EventHandler(text_synch);
            w_t_minute.TextChanged += new EventHandler(text_synch);
            w_t_sec.TextChanged += new EventHandler(text_synch);
            panel1.Controls.Add(start);
            panel1.Controls.Add(reset);
            panel1.Controls.Add(resign);
            panel1.Controls.Add(exit);
            panel1.Controls.Add(w_s_tick);
            panel1.Controls.Add(w_m_tick);
            panel1.Controls.Add(b_s_tick);
            panel1.Controls.Add(b_m_tick);
            panel1.Controls.Add(w_t_hour);
            panel1.Controls.Add(w_t_minute);
            panel1.Controls.Add(w_t_sec);
            panel1.Controls.Add(b_t_hour);
            panel1.Controls.Add(b_t_minute);
            panel1.Controls.Add(b_t_sec);

            ///

                                            ///                         Time end                       ///

            ///      Labels



            Label white = new Label()
            {
                Name = "white_text",
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 27, FontStyle.Bold),
                Text = "WHITE",
                Size = new Size(200, 50),
                Location = new Point(60, 160),
            };
            Label black = new Label()
            {
                Name = "black_text",
                Font = new Font(Font.FontFamily, 27, FontStyle.Bold),
                Text = "BLACK",
                Size = new Size(200, 50),
                Location = new Point(390, 160),
            };
            Label wcount = new Label()
            {
                Name = "b_count",
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 27, FontStyle.Bold),
                Text = "0",
                Size = new Size(200, 50),
                Location = new Point(110, 220),
            };
            Label bcount = new Label()
            {
                Name = "w_count",
                Font = new Font(Font.FontFamily, 27, FontStyle.Bold),
                Text = "0",
                Size = new Size(200, 50),
                Location = new Point(430, 220),
            };

            panel1.Controls.Add(black);
            panel1.Controls.Add(white);
            panel1.Controls.Add(bcount);
            panel1.Controls.Add(wcount);


            for (int i = 0; i < 8; i++)
            {

                Label lb = new Label()
                {
                    Location = new Point(koxm * i, koxm * 8),
                    Size = new Size(koxm, 20),
                    Name = "lbl_" + alps[i].ToString(),
                    Text = alps[i].ToString(),
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold),
                    AutoSize = false,
                    TextAlign = System.Drawing.ContentAlignment.TopCenter,
                    BackColor = Color.Transparent
                };
                this.Controls.Add(lb);
                lb.BringToFront();
                Label llb = new Label()
                {
                    Size = new Size(20, koxm),
                    Location = new Point(koxm * 8, koxm * i),
                    Name = "lbl_" + (1 + i).ToString(),
                    Text = (8 - i).ToString(),
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold),
                    AutoSize = false,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    BackColor = Color.Transparent
                };
                llb.BringToFront();
                this.Controls.Add(llb);
            };
            reset_Click(null, null);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor,
                true);
            Graphics g = e.Graphics;
            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 4; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Brown), (i * 2 + 1) * koxm, j * 2 * koxm, koxm, koxm);
                    g.FillRectangle(new SolidBrush(Color.Brown), (i * 2 * 1) * koxm, (j * 2 + 1) * koxm, koxm, koxm);
                }
        }
        private void text_synch(object sender, EventArgs e)
        {
            int time;
            if (!started)
            {
                TextBox tm_tb = (TextBox)sender;
                if (tm_tb.Text != "")
                {
                    if (!(Int32.TryParse(tm_tb.Text, out time)) || time < 0)
                    {
                        tm_tb.Text = "00";
                        return;
                    }
                    time = Int32.Parse(tm_tb.Text);
                    if (time > 60)
                    {
                        tm_tb.Text = "60";
                        return;
                    }
                }
                f_control("b_t_hour").Text = f_control("w_t_hour").Text;
                f_control("b_t_minute").Text = f_control("w_t_minute").Text;
                f_control("b_t_sec").Text = f_control("w_t_sec").Text;
                if (f_control("w_t_hour").Text == "" || f_control("w_t_sec").Text == "" || f_control("w_t_minute").Text == "")
                {
                    f_control("start").Enabled = false;
                }
                else
                {
                    f_control("start").Enabled = true;
                }
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            int hour = Int32.Parse(f_control(turn.ToString() + "_t_hour").Text);
            int min = Int32.Parse(f_control(turn.ToString() + "_t_minute").Text);
            int sec = Int32.Parse(f_control(turn.ToString() + "_t_sec").Text);
            sec--;
            if (sec < 0)
            {
                min--;
                if (min < 0)
                {
                    hour--;
                    if (hour < 0)
                    {
                        hour = 0;
                        timer.Stop();
                        MessageBox.Show(turn.ToString() + " lost");
                        return;
                    }
                    min = 59;
                }
                sec = 59;
            }
            f_control(turn.ToString() + "_t_hour").Text = hour.ToString();
            f_control(turn.ToString() + "_t_minute").Text = min.ToString();
            f_control(turn.ToString() + "_t_sec").Text = sec.ToString();
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / koxm;
            int y = e.Y / koxm;
            if (stepping_fig.Name != "" && x < 8 && y < 8)
            {
                //if (turn == 'b') rnd(); else
                go(stepping_fig.Name, x, y);
            }
            return;
        }
        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }
        void reset_Click(object sender, EventArgs e)
        {
            timer.Stop();
            started = false;
            turn = 'w';
            f_control("w_t_hour").Enabled = true;
            f_control("w_t_minute").Enabled = true;
            f_control("w_t_sec").Enabled = true;
            f_control("start").Enabled = true;
            f_control("reset").Enabled = false;
            f_control("resign").Enabled = false;
            f_control("w_t_hour").Text = w_hr.ToString();
            f_control("w_t_minute").Text = w_mt.ToString();
            f_control("w_t_sec").Text = w_sc.ToString();
            f_control("b_t_hour").Text = w_hr.ToString();
            f_control("b_t_minute").Text = w_mt.ToString();
            f_control("b_t_sec").Text = w_sc.ToString();
            f_control("w_m_tick").Text = ":";
            f_control("w_s_tick").Text = ":";
            f_control("b_m_tick").Text = ":";
            f_control("b_s_tick").Text = ":";
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (matrixx[i, j] != 0)
                    {
                        f_control(matrix_toname(matrixx[i, j])).Dispose();
                        matrixx[i, j] = 0;
                    }

            // black figs
            for (int i = 0; i < 8; i++)
            {
                int j;
                if (i > 3)
                {
                    j = i - 4;
                    do
                    {
                        PictureBox s_berd_1 = new PictureBox()
                        {
                            Location = new Point(koxm * j, koxm),
                            Size = new Size(koxm, koxm),
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Cursor = System.Windows.Forms.Cursors.Hand,
                            BackColor = System.Drawing.Color.Transparent,
                            Name = "b_" + fig_names[8] + "_" + (30 + j).ToString(),
                            Image = Properties.Resources.soldier_b,
                        };
                        s_berd_1.Click += new EventHandler(fig_click);
                        this.Controls.Add(s_berd_1);
                        matrixx[j, 1] = 30 + j;
                        j = 7 - j;
                    } while (j == 7 - i + 4);
                    continue;
                }
                j = i;
                do
                {
                    PictureBox s_berd_1 = new PictureBox()
                    {
                        Location = new Point(koxm * j, 0),
                        Size = new Size(koxm, koxm),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Cursor = System.Windows.Forms.Cursors.Hand,
                        BackColor = System.Drawing.Color.Transparent,
                        Name = "b_" + fig_names[j] + "_" + (10 + j).ToString(),
                        Image = b_figs[j],
                    };
                    s_berd_1.Click += new EventHandler(fig_click);
                    this.Controls.Add(s_berd_1);
                    matrixx[j, 0] = 10 + j;
                    j = 7 - j;
                } while (j == 7 - i);
            }
            //white figs
            for (int i = 0; i < 8; i++)
            {
                int j;
                if (i > 3)
                {
                    j = i - 4;
                    do
                    {
                        PictureBox s_berd_1 = new PictureBox()
                        {
                            Location = new Point(koxm * j, koxm * 6),
                            Size = new Size(koxm, koxm),
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Cursor = System.Windows.Forms.Cursors.Hand,
                            BackColor = System.Drawing.Color.Transparent,
                            Name = "w_" + fig_names[8] + "_" + (40 + j).ToString(),
                            Image = Properties.Resources.soldier_w,
                        };
                        s_berd_1.Click += new EventHandler(fig_click);
                        this.Controls.Add(s_berd_1);
                        matrixx[j, 6] = 40 + j;
                        j = 7 - j;
                    } while (j == 7 - i + 4);
                    continue;
                }
                j = i;
                do
                {
                    PictureBox s_berd_1 = new PictureBox()
                    {
                        Location = new Point(koxm * j, koxm * 7),
                        Size = new Size(koxm, koxm),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Cursor = System.Windows.Forms.Cursors.Hand,
                        BackColor = System.Drawing.Color.Transparent,
                        Name = "w_" + fig_names[j] + "_" + (20 + j).ToString(),
                        Image = w_figs[j],
                    };
                    s_berd_1.Click += new EventHandler(fig_click);
                    this.Controls.Add(s_berd_1);
                    matrixx[j, 7] = 20 + j;
                    j = 7 - j;
                } while (j == 7 - i);
            }
            f_control("start").Select();
            return;
        }
        private void resign_Click(object sender, EventArgs e)
        {
            f_control(turn + "_count").Text = (Int32.Parse(f_control(turn + "_count").Text) + 1).ToString();
            reset_Click(null, null);
        }
        void start_Click(object sender, EventArgs e)
        {
            w_hr = Int32.Parse(f_control("w_t_hour").Text);
            w_mt = Int32.Parse(f_control("w_t_minute").Text);
            w_sc = Int32.Parse(f_control("w_t_sec").Text);
            started = true;
            f_control("b_t_hour").Enabled = false;
            f_control("b_t_minute").Enabled = false;
            f_control("b_t_sec").Enabled = false;
            f_control("w_t_hour").Enabled = false;
            f_control("w_t_minute").Enabled = false;
            f_control("w_t_sec").Enabled = false;
            f_control("start").Enabled = false;
            f_control("reset").Enabled = true;
            f_control("resign").Enabled = true;
            timer.Start();
            f_control("reset").Select();
        }
        private void fig_click(object sender, EventArgs e)
        {
            PictureBox fig = (PictureBox)sender;
            if (fig.Name.Split('_')[0] == turn.ToString())
            {
                stepping_fig.BackColor = Color.Transparent;
                stepping_fig = fig;
                stepping_fig.BackColor = Color.Green;
            }
            else if (stepping_fig.Name != "")
            {
                int x = fig.Location.X / koxm;
                int y = fig.Location.Y / koxm;
                go(stepping_fig.Name, x, y);
            }
            return;
        }
        private void go(string name, int x, int y)
        {
            bool take = false; newfig = false; rakir = false;
            if (matrixx[x, y] != 0)
                 take = true;
            int cur_x = f_control(name).Location.X / koxm;
            int cur_y = f_control(name).Location.Y / koxm;
            if (started && can(x, y, cur_x, cur_y, matrixx) && not_check(x, y, cur_x, cur_y))
            {
                if (take)
                    f_control(matrix_toname(matrixx[x, y])).Dispose();
                matrixx[x, y] = matrixx[cur_x, cur_y];
                matrixx[cur_x, cur_y] = 0;
                stepping_fig.Location = new Point(koxm * x, koxm * y);
                stepping_fig.BackColor = Color.Transparent;
                if (rakir)
                {
                    if ((cur_x - x) > 0)
                    {
                        f_control(turn + "_" + "tower" + "_" + ((turn == 'b') ? 1 : 2).ToString() + 0).Location = new Point((x + 1) * koxm, ((turn == 'b') ? 0 : 7)*koxm);
                    }
                    else
                    {
                        f_control(turn + "_" + "tower" + "_" + ((turn == 'b') ? 1 : 2).ToString() + 7).Location = new Point((x - 1) * koxm, ((turn == 'b') ? 0 : 7) * koxm);
                    }
                    mvd[(turn == 'w') ? 0 : 1] = 1;
                    rakir = false;
                }
                if (newfig)
                {
                    using (Form2 form2 = new Form2(turn))
                    {
                        if (form2.ShowDialog() == DialogResult.OK)
                        {
                            nfig_return = form2.TheValue;
                        }
                    }
                    if (nfig_return != 8 )
                    {
                        if (turn == 'b')
                        {
                            stepping_fig.Image = b_figs[nfig_return];
                            stepping_fig.Name = "b_" + fig_names[nfig_return] + "_" + (300 + nfig_return *10 + Int32.Parse(stepping_fig.Name.Split('_')[2]) - 30).ToString();
                        }
                        else
                        {
                            stepping_fig.Image = w_figs[nfig_return];
                            stepping_fig.Name = "w_" + fig_names[nfig_return] + "_" + (400 + nfig_return * 10 + Int32.Parse(stepping_fig.Name.Split('_')[2]) - 40).ToString();
                        }
                        matrixx[x, y] = Int32.Parse(stepping_fig.Name.Split('_')[2]);
                    }
                    newfig = false;
                }
                stepping_fig = new PictureBox();
                if (turn == 'w')
                {
                    turn = 'b';
                }
                else
                    turn = 'w';
            };
            return;
        }
        private bool can(int x, int y, int cur_x, int cur_y, int[,] matrix, bool nfc = true) // nfc - not for chess-checking
        {
            bool can = false, take = false;
            string[] parts = matrix_toname(matrix[cur_x, cur_y]).Split('_');
            if (matrix[x, y] != 0)
                take = true;
            if (take && parts[0] == matrix_toname(matrix[x, y]).Split('_')[0])
                return false;
            if (parts[1] == fig_names[8])
            {
                if ((parts[0] == "w") && ((!take && cur_x == x && (cur_y - y == 1 || (cur_y == 6 && cur_y - y == 2))) || (take && Math.Abs(cur_x - x) == 1 && cur_y - y == 1)))
                {
                    can = true;
                }
                else if ((parts[0] == "b") && ((!take && cur_x == x && (cur_y - y == -1 || (cur_y == 1 && cur_y - y == -2))) || (take && Math.Abs(cur_x - x) == 1 && cur_y - y == -1)))
                {
                    can = true;
                };
                if (can && ((parts[0] == "b" && cur_y == 6) || (parts[0] == "w" && cur_y == 1)) && nfc)
                {
                    newfig = true;
                }
            }
            else if (parts[1] == fig_names[0])
            {
                if (cur_y == y)
                {
                    can = true;
                    int dir = Math.Abs(cur_x - x) / (cur_x - x);
                    for (int i = x+dir; i != cur_x; i += dir)
                        if (matrix[i, cur_y] != 0)
                            return false;
                }
                else if (cur_x == x)
                {
                    can = true;
                    int dir = Math.Abs(cur_y - y) / (cur_y - y);
                    for (int i = y+dir; i != cur_y; i += dir)
                        if (matrix[cur_x, i] != 0)
                            return false;
                }
            }
            else if (parts[1] == fig_names[1])
            {
                if ((Math.Abs(cur_x - x) == 1 && Math.Abs(cur_y - y) == 2) || (Math.Abs(cur_x - x) == 2 && Math.Abs(cur_y - y) == 1))
                {
                    can = true;
                }
            }
            else if (parts[1] == fig_names[2])
            {
                if (Math.Abs(cur_x - x) == Math.Abs(cur_y - y))
                {
                    can = true;
                    int dir_y = Math.Abs(cur_y - y) / (cur_y - y);
                    int dir_x = Math.Abs(cur_x - x) / (cur_x - x);
                    for (int i = 1; i != Math.Abs(cur_y - y); i++)
                        if (matrix[x + i * dir_x, y + dir_y * i] != 0)
                            return false;
                }
            }
            else if (parts[1] == fig_names[4])
            {
                if (((cur_y == y && Math.Abs(cur_x - x) == 1) || (cur_x == x && Math.Abs(cur_y - y) == 1) || (Math.Abs(cur_x - x) == 1 && Math.Abs(cur_y - y) == 1)) || (mvd[(turn == 'w') ? 0 : 1] == 0 && Math.Abs(cur_x - x) == 2 && cur_y - y == 0 && ((cur_x - x > 0 && matrixx[0, (turn == 'b') ? 0 : 7] == ((turn == 'b') ? 10 : 20)) || (cur_x - x < 0 && matrixx[7, (turn == 'b') ? 0 : 7] == ((turn == 'b') ? 17 : 27)))))
                {
                    if (Math.Abs(cur_x - x) == 2 && nfc)
                    {
                        rakir = true;
                    }
                    can = true;
                }
                //MessageBox.Show(matrixx[0, 7].ToString());
            }
            else if (parts[1] == fig_names[3])
            {
                if (Math.Abs(cur_x - x) == Math.Abs(cur_y - y))
                {
                    can = true;
                    int dir_y = Math.Abs(cur_y - y) / (cur_y - y);
                    int dir_x = Math.Abs(cur_x - x) / (cur_x - x);
                    for (int i = 1; i != Math.Abs(cur_y - y); i++)
                        if (matrix[x + i * dir_x, y + dir_y * i] != 0)
                            return false;
                }
                else if (cur_y == y)
                {
                    can = true;
                    int dir = Math.Abs(cur_x - x) / (cur_x - x);
                    for (int i = x+dir; i != cur_x; i += dir)
                        if (matrix[i, cur_y] != 0)
                            return false;
                }
                else if (cur_x == x)
                {
                    can = true;
                    int dir = Math.Abs(cur_y - y) / (cur_y - y);
                    for (int i = y+dir; i != cur_y; i += dir)
                        if (matrix[cur_x, i] != 0)
                            return false;
                }
            };
            return can;
        }
        private bool not_check(int x, int y, int cur_x, int cur_y)
        {
            int[,] ex_matrix = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    ex_matrix[i, j] = matrixx[i, j];
            int k_x = 0, k_y = 0;
            ex_matrix[x, y] = ex_matrix[cur_x, cur_y];
            ex_matrix[cur_x, cur_y] = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    string[] parts = matrix_toname(ex_matrix[i, j]).Split('_');
                    if (parts[0] == turn.ToString() && parts[1] == fig_names[4])
                    {
                        k_x = i;
                        k_y = j;
                        i = 8;
                        break;
                    }
                };
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if ((ex_matrix[i, j] != 0 && matrix_toname(ex_matrix[i, j]).Split('_')[0] != turn.ToString()) && can(k_x, k_y, i, j, ex_matrix, false))
                        return false;
            return true;
        }
        private string matrix_toname(int matrix)
        {
            return clr[(matrix / 100 > 1) ? matrix / 100 : matrix / 10] + "_" + fig_names[(matrix / 100 > 1) ? ((matrix - (matrix / 100) * 100) / 10) : (matrix / 10 > 2) ? 8 : (matrix - (matrix / 10) * 10)] + "_" + matrix.ToString();
        }
        private Control f_control(string s)
        {
            return this.Controls.Find(s, true)[0];
        }
    }
}
