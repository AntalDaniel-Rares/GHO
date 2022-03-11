using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace GHO
{
    public partial class Form1 : Form
    {
        int moves = 10;
        int moves_counter = 0;
        int color_choice = 0;
        GroupBox label_group = new GroupBox();
        Font Font1 = new Font("Times New Roman", 18);
        Label RED_team = new Label();
        Label BLUE_team = new Label();
        bool initial_phase = true;
        TextBox player1 = new TextBox();
        TextBox player2 = new TextBox();
        int[] labels_clicked = new int[50];
        int counter = 0;
        Label player1_score = new Label();
        Label player2_score = new Label();
        int red_team_score = 0;
        int blue_team_score = 0;

        //Stopwatch watch = new Stopwatch();
        //public System.Threading.AutoResetEvent thread1done = new System.Threading.AutoResetEvent(false);
        public Form1()
        {
            InitializeComponent();
            pct1.Controls.Add(label_group);
            CenterControlInParent(btn_new_game, 0, 0);
            CenterControlInParent(pct1, 0, -50);
            CenterControlInParent(btn_quit, 0, 50);
            pct1.Visible = false;
            pct1.SendToBack();
            label_group.Location = new Point(0, 0);
            label_group.Size = pct1.Size;
            label_group.BackColor = Color.Transparent;
            label_group.Paint += PaintBorderlessGroupBox;

        }

        private void btn_new_game_Click(object sender, EventArgs e)
        {
            btn_new_game.Hide();
            btn_quit.Hide();
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.TopMost = true;
            pct1.Visible = true;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            //this.MinimumSize = this.Size;

            New_game();
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterControlInParent(btn_new_game, 0, 0);
            CenterControlInParent(pct1, 0, -50);
            CenterControlInParent(btn_quit, 0, 50);
            //pct1.Invalidate();
            
        }
        private void CenterControlInParent(Control ctrlToCenter, int delay_width, int delay_height)
        {
            ctrlToCenter.Left = (ctrlToCenter.Parent.Width - ctrlToCenter.Width) / 2 + delay_width;
            ctrlToCenter.Top = (ctrlToCenter.Parent.Height - ctrlToCenter.Height) / 2 + delay_height;
        }

        private void pct1_Click(object sender, EventArgs e)
        {
            //sound effect
        }

        private void New_game()
        {
            Settings();
            //Label_Create();
        }

        private void Label_Create()
        {

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Label lbl = new Label();

                    lbl.Size = new Size(12, 12);
                    lbl.Location = new Point(222 + i * 38, 69 + j * 35);
                    //lbl.Location = new Point(0, 0);
                    lbl.BackColor = Color.White;
                    lbl.Click += lbl_Click;

                    label_group.Controls.Add(lbl);
                    lbl.BringToFront();
                }
                //pct1.Hide();
            }
        }

        private void PaintBorderlessGroupBox(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            DrawGroupBox(box, e.Graphics, Color.Peru);
        }

        private void DrawGroupBox(GroupBox box, Graphics g, Color borderColor)
        {
            if (box != null)
            {
                //Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush, 13);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                //g.Clear(Color.Transparent);

                // Draw text
                //g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            if ((lbl.BackColor == Color.White) && moves_counter < moves)
            {
                if ((moves_counter + color_choice) % 2 != 0)
                    lbl.BackColor = Color.Red;
                else lbl.BackColor = Color.Blue;

                moves_counter++;
             
                Win_condition(lbl.Parent.Controls.GetChildIndex(lbl), lbl.BackColor);
                labels_clicked[++counter] = lbl.Parent.Controls.GetChildIndex(lbl);
                Score();
            }
            //MessageBox.Show(lbl.Parent.Controls.GetChildIndex(lbl).ToString());
        }
        private void Settings()
        {
            Button btn_roll = new Button();

            btn_roll.Text = "Roll";
            btn_roll.Location = new Point(96, 164);
            btn_roll.AutoSize = true;
            btn_roll.Font = new Font("Times New Roman", 13);
            btn_roll.BackColor = Color.White;

            pct1.Controls.Add(btn_roll);
            btn_roll.BringToFront();

            btn_roll.Click += btn_roll_Click;

            Label Choose_team = new Label();
            Choose_team.Location = new Point(96, 54);
            Choose_team.Text = "Choose a starting team:";
            Choose_team.Font = Font1;
            Choose_team.AutoSize = true;
            Choose_team.BackColor = Color.Transparent;

            // Add this label to form
            pct1.Controls.Add(Choose_team);
            Choose_team.BringToFront();
            //Mylablel.BringToFront();

            RED_team.Location = new Point(96, 89);
            RED_team.Text = "RED TEAM";
            RED_team.Font = Font1;
            RED_team.AutoSize = true;
            RED_team.BackColor = Color.Transparent;
            RED_team.ForeColor = Color.Red;
            RED_team.MouseEnter += RED_team_MouseEnter;
            RED_team.MouseLeave += RED_team_MouseLeave;
            RED_team.MouseClick += RED_team_MouseClick;

            pct1.Controls.Add(RED_team);
            RED_team.BringToFront();

            BLUE_team.Location = new Point(96, 124);
            BLUE_team.Text = "BLUE TEAM";
            BLUE_team.Font = Font1;
            BLUE_team.AutoSize = true;
            BLUE_team.BackColor = Color.Transparent;
            BLUE_team.ForeColor = Color.Blue;
            BLUE_team.MouseEnter += BLUE_team_MouseEnter;
            BLUE_team.MouseLeave += BLUE_team_MouseLeave;
            BLUE_team.MouseClick += BLUE_team_MouseClick;

            pct1.Controls.Add(BLUE_team);
            BLUE_team.BringToFront();

            Label Round_select = new Label();
            Round_select.Location = new Point(96, 214);
            Round_select.Text = "Select number of moves:";
            Round_select.Font = Font1;
            Round_select.AutoSize = true;
            Round_select.BackColor = Color.Transparent;

            // Add this label to form
            pct1.Controls.Add(Round_select);
            Round_select.BringToFront();

            RadioButton rounds_10 = new RadioButton();
            rounds_10.Location = new Point(96, 244);
            rounds_10.Text = "10 moves";
            rounds_10.Font = Font1;
            rounds_10.Checked = true;
            rounds_10.BackColor = Color.Transparent;
            rounds_10.ForeColor = Color.GreenYellow;
            rounds_10.AutoSize = true;
            rounds_10.Click += Rounds_10_Click;

            pct1.Controls.Add(rounds_10);
            rounds_10.BringToFront();

            RadioButton rounds_25 = new RadioButton();
            rounds_25.Location = new Point(96, 274);
            rounds_25.Text = "25 moves";
            rounds_25.Font = Font1;
            rounds_25.BackColor = Color.Transparent;
            rounds_25.ForeColor = Color.Yellow;
            rounds_25.AutoSize = true;
            rounds_25.Click += Rounds_25_Click;

            pct1.Controls.Add(rounds_25);
            rounds_25.BringToFront();

            RadioButton rounds_50 = new RadioButton();
            rounds_50.Location = new Point(96, 304);
            rounds_50.Text = "50 moves";
            rounds_50.Font = Font1;
            rounds_50.BackColor = Color.Transparent;
            rounds_50.ForeColor = Color.Orange;
            rounds_50.AutoSize = true;
            rounds_50.Click += Rounds_50_Click;

            pct1.Controls.Add(rounds_50);
            rounds_50.BringToFront();

            Label Enter_name = new Label();
            Enter_name.Location = new Point(96, 354);
            Enter_name.Text = "Enter player names:";
            Enter_name.Font = Font1;
            Enter_name.AutoSize = true;
            Enter_name.BackColor = Color.Transparent;

            pct1.Controls.Add(Enter_name);
            Enter_name.BringToFront();

            Label Red_name = new Label();
            Red_name.Location = new Point(96, 384);
            Red_name.Text = "Red team:";
            Red_name.Font = new Font("Times New Roman", 22);
            Red_name.AutoSize = true;
            Red_name.ForeColor = Color.Red;
            Red_name.BackColor = Color.Transparent;

            pct1.Controls.Add(Red_name);
            Red_name.BringToFront();

            player1.Location = new Point(235, 384);
            player1.Text = "Player1";
            player1.Font = Font1;

            pct1.Controls.Add(player1);
            player1.BringToFront();

            Label Blue_name = new Label();
            Blue_name.Location = new Point(96, 424);
            Blue_name.Text = "Blue team:";
            Blue_name.Font = new Font("Times New Roman", 22);
            Blue_name.AutoSize = true;
            Blue_name.ForeColor = Color.Blue;
            Blue_name.BackColor = Color.Transparent;

            pct1.Controls.Add(Blue_name);
            Blue_name.BringToFront();

            player2.Location = new Point(240, 424);
            player2.Text = "Player2";
            player2.Font = Font1;

            pct1.Controls.Add(player2);
            player2.BringToFront();

            Button btn_start = new Button();

            btn_start.Text = "Start";
            btn_start.AutoSize = true;
            btn_start.Font = new Font("Times New Roman", 13);
            btn_start.BackColor = Color.White;

            pct1.Controls.Add(btn_start);
            btn_start.BringToFront();
            CenterControlInParent(btn_start, 0, 230);
            btn_start.Click += Btn_start_Click1;
        }

        private void Btn_start_Click1(object sender, EventArgs e)
        {
            //thread1done.Set();
            //T1.Join();
            //Clear_pct1();
            //thread1done.WaitOne();
            //T1.Abort();

            //Thread T2 = new Thread(new ThreadStart(Clear_pct1));
            Clear_pct1();
            //if(T1.ThreadState == ThreadState.WaitSleepJoin)
            //T2.Start();
            //T2.Join();
            //MessageBox.Show(T1.ThreadState.ToString());

            Thread T1 = new Thread(new ThreadStart(Grid_create));
            T1.Start();

            Label_Create();
            pct1.Invalidate();

            Button btn_throw = new Button();
            btn_throw.Text = "Throw game";
            btn_throw.AutoSize = true;            
            pct1.Controls.Add(btn_throw);
            CenterControlInParent(btn_throw, -330, 20);
            btn_throw.Font = new Font("Times New Roman", 15);
            btn_throw.BringToFront();
            btn_throw.Click += Btn_throw_Click;

            pct1.Controls.Add(player1_score);
            player1_score.Text = player1.Text + "\n\n" + red_team_score.ToString();
            CenterControlInParent(player1_score, -320, -160);
            player1_score.BackColor = Color.Transparent;
            player1_score.ForeColor = Color.Red;
            player1_score.Font = new Font("Times New Roman", 30);
            player1_score.BringToFront();
            player1_score.AutoSize = true;

            pct1.Controls.Add(player2_score);
            player2_score.Text = blue_team_score.ToString() + "\n\n" + player2.Text;
            CenterControlInParent(player2_score, -320, 90);
            player2_score.BackColor = Color.Transparent;
            player2_score.ForeColor = Color.Blue;
            player2_score.Font = new Font("Times New Roman", 30);
            player2_score.BringToFront();
            player2_score.AutoSize = true;

            pct1.Invalidate();
        }

        private void Score()
        {
            blue_team_score = 0;
            red_team_score = 0;

            foreach (Control c in label_group.Controls)
            {
                if (c.BackColor == Color.RoyalBlue)
                    blue_team_score++;
                else if(c.BackColor == Color.PaleVioletRed)
                    red_team_score++;
            }

            player1_score.Text = player1.Text + "\n\n" + red_team_score.ToString();
            player2_score.Text = blue_team_score.ToString() + "\n\n" + player2.Text;
        }

        private void Btn_throw_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Win_condition(int tab_index, Color lbl_color)
        {
            int j = tab_index / 16, k = tab_index % 16;
            Color new_color, bad_color;
            bool bad_color_TRUE = false;

            if (lbl_color == Color.Red)
            {
                new_color = Color.PaleVioletRed;
                bad_color = Color.RoyalBlue;
            }
            else
            {
                new_color = Color.RoyalBlue;
                bad_color = Color.PaleVioletRed;
            }

            for(int i = k + 1; i < 16; i++)
            {
                if (bad_color_TRUE == false)
                    color_line(new_color, bad_color, lbl_color, j, i, ref bad_color_TRUE, true);
            }

            bad_color_TRUE = false;

            for (int i = k - 1; i >= 0; i--)
            {
                if(bad_color_TRUE == false)
                    color_line(new_color, bad_color, lbl_color, j, i, ref bad_color_TRUE, true);
                //MessageBox.Show(k.ToString());
            }

            bad_color_TRUE = false;

            for (int i = j + 1; i < 16; i++)
            {
                if (bad_color_TRUE == false)
                    color_line(new_color, bad_color, lbl_color, i, k, ref bad_color_TRUE, false);
            }

            bad_color_TRUE = false;

            for (int i = j - 1; i >= 0; i--)
            {
                if (bad_color_TRUE == false)
                    color_line(new_color, bad_color, lbl_color, i, k, ref bad_color_TRUE, false);
            }

        }

        private void color_line(Color new_color, Color bad_color, Color lbl_color, int i, int k, ref bool bad_color_TRUE, bool collum_or_row)
        {
            if (label_group.Controls[(16 * i) + k].BackColor == Color.White && bad_color_TRUE == false)
            {
                label_group.Controls[(16 * i) + k].BackColor = new_color;
            }
            else if (label_group.Controls[(16 * i) + k].BackColor == bad_color && bad_color_TRUE == false)
            {
                label_group.Controls[(16 * i) + k].BackColor = Color.Purple;
                if (collum_or_row == false)
                    Recolor_collum(i, k, lbl_color, new_color);
                else Recolor_row(i, k, lbl_color, new_color);

                bad_color_TRUE = true;
            }
            else if (label_group.Controls[(16 * i) + k].BackColor == Color.Purple)
            {
                bad_color_TRUE = true;
            }
            else return;
            //MessageBox.Show(counter.ToString());

        }

        private void Recolor_collum(int i, int k, Color lbl_color, Color new_color)
        {
            Color other_color;
            bool Up = false;

            if (lbl_color == Color.Blue)
                other_color = Color.Red;
            else other_color = Color.Blue;

            if (k > 8)
            {
                for (int j = k + 1; j < 16; j++)
                {
                    if (label_group.Controls[(16 * i) + j].BackColor == other_color)
                    {
                        Up = true;
                    }
                    else if (label_group.Controls[(16 * i) + j].BackColor == Color.Purple)
                        break;
                }
            }
            else
            {
                Up = true;
                for (int j = k - 1; j >= 0; j--)
                {
                    if (label_group.Controls[(16 * i) + j].BackColor == other_color)
                    {
                        Up = false;
                        //MessageBox.Show(((16 * i) + j).ToString());
                    }
                    else if (label_group.Controls[(16 * i) + j].BackColor == Color.Purple)
                        break;
                }

            }
            //MessageBox.Show("pause");

            if (Up == true)
            {
                for (int f = k - 1; f >= 0; f--)
                {
                    //MessageBox.Show(f.ToString());
                    if (f - 1 > 0)
                    {
                        if (label_group.Controls[(16 * i) + f].BackColor == Color.Purple && label_group.Controls[(16 * i) + f - 1].BackColor != Color.White)
                            break;
                    }
                    label_group.Controls[(16 * i) + f].BackColor = Color.White;
                }
            }
            else
            {
                for (int f = k + 1; f < 16; f++)
                {
                    //MessageBox.Show(f.ToString() + label_group.Controls[(16 * i) + f + 1].BackColor.ToString());
                    if (f + 1 < 16)
                    {
                        if (label_group.Controls[(16 * i) + f].BackColor == Color.Purple && label_group.Controls[(16 * i) + f + 1].BackColor != Color.White)
                            break;
                    }
                    label_group.Controls[(16 * i) + f].BackColor = Color.White;
                }
            }

            Redraw();

            //label_group.Controls[(16 * i) + k + 1].BackColor = Color.Black;
        }

        private void Recolor_row(int i, int k, Color lbl_color, Color new_color)
        {
            Color other_color;
            bool Up = false;

            if (lbl_color == Color.Blue)
                other_color = Color.Red;
            else other_color = Color.Blue;

            if (i > 8)
            {
                for (int j = i + 1; j < 16; j++)
                {
                    if (label_group.Controls[(16 * j) + k].BackColor == other_color)
                    {
                        Up = true;
                    }
                    else if (label_group.Controls[(16 * j) + k].BackColor == Color.Purple)
                        break;
                }
            }
            else
            {
                Up = true;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (label_group.Controls[(16 * j) + k].BackColor == other_color)
                    {
                        Up = false;
                        //MessageBox.Show(((16 * i) + j).ToString());
                    }
                    else if (label_group.Controls[(16 * j) + k].BackColor == Color.Purple)
                        break;
                }
            }
            //MessageBox.Show("pause");

            if (Up == true)
            {
                for (int f = i - 1; f >= 0; f--)
                {
                    //if(f!=0)
                    //MessageBox.Show(f.ToString() + label_group.Controls[(16 * f) + k].BackColor.ToString() + label_group.Controls[(16 * (f - 1)) + k].BackColor.ToString());
                    //MessageBox.Show(f.ToString());
                    if (f - 1 > 0)
                    {
                        if (label_group.Controls[(16 * f) + k].BackColor == Color.Purple && label_group.Controls[(16 * (f - 1)) + k].BackColor != Color.White)
                            break;
                    }
                    label_group.Controls[(16 * f) + k].BackColor = Color.White;
                }
            }
            else
            {
                for (int f = i + 1; f < 16; f++)
                {
                    //MessageBox.Show(f.ToString());
                    if (f + 1 < 16)
                    {
                        if (label_group.Controls[(16 * f) + k].BackColor == Color.Purple && label_group.Controls[(16 * (f + 1)) + k].BackColor != Color.White)
                            break;
                    }
                    label_group.Controls[(16 * f) + k].BackColor = Color.White;
                }
            }

            Redraw();
        }

        private void Redraw()
        {
            for (int j = 1; j <= counter; j++)
                if ((j + color_choice) % 2 == 0)
                {
                    //MessageBox.Show("pause");
                    //repeat = false;
                    Win_condition(labels_clicked[j], Color.Red);
                }
                else
                {
                    //MessageBox.Show("pause");
                    //repeat = false;
                    Win_condition(labels_clicked[j], Color.Blue);
                }
        }

        private void Clear_pct1()
        {
            //watch.Start();
            foreach (Control c in pct1.Controls)
            {
                /*if (c.InvokeRequired)
                {
                    c.Invoke(new MethodInvoker(Clear_pct1));
                    return;
                }*/
                //MessageBox.Show(c.GetType().ToString());
                if(c.GetType() != typeof(GroupBox))
                    c.Visible = false;

                //Thread.Sleep(2000);
            }

            //thread1done.Set();
            //watch.Stop();
        }

        private void pct1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Black, 8.0F);

            PointF point1 = new PointF(223.5F, 75.0F);
            PointF point2 = new PointF(799.0F, 75.0F);

            PointF point3 = new PointF(228.0F, 71.0F);
            PointF point4 = new PointF(228.0F, 604.0F);

            for (int i = 0; i < 16; i++)
            {
                g.DrawLine(pen, point1 + new Size(0, 35 * i), point2 + new Size(0, 35 * i));
                g.DrawLine(pen, point3 + new Size(38 * i, 0), point4 + new Size(38 * i, 0));
            }
            //pct1.Refresh();
        }

        private void Grid_create()
        {
            //Thread.Sleep(10000);
            pct1.Paint += this.pct1_Paint;
            //MessageBox.Show("thread2");
        }

        private void Rounds_50_Click(object sender, EventArgs e)
        {
            moves = 50;
            Array.Resize(ref labels_clicked, moves);
        }

        private void Rounds_25_Click(object sender, EventArgs e)
        {
            moves = 25;
            Array.Resize(ref labels_clicked, moves);
        }

        private void Rounds_10_Click(object sender, EventArgs e)
        {
            moves = 10;
            Array.Resize(ref labels_clicked, moves);
        }

        private void RED_team_MouseClick(object sender, MouseEventArgs e)
        {
            color_choice = 1;

            Label Red = (Label)sender;
            Red.ForeColor = Color.Red;
            Red.Font = new Font("Times New Roman", 22);

            Red.MouseLeave -= RED_team_MouseLeave;
            Red.MouseEnter -= RED_team_MouseEnter;

            BLUE_team.ForeColor = Color.Blue;

            if (initial_phase == false && BLUE_team.Font != Font1)
            {   
                BLUE_team.MouseEnter += BLUE_team_MouseEnter;
                BLUE_team.MouseLeave += BLUE_team_MouseLeave;
            }

            BLUE_team.Font = Font1;
            initial_phase = false;
        }

        private void RED_team_MouseLeave(object sender, EventArgs e)
        {
            Label Red = (Label)sender;
            Red.ForeColor = Color.Red;
            Red.Font = new Font("Times New Roman", 18);
        }

        private void RED_team_MouseEnter(object sender, EventArgs e)
        {
            Label Red = (Label)sender;
            Red.ForeColor = Color.DarkRed;
            Red.Font = new Font("Times New Roman", 22); 
        }
        private void BLUE_team_MouseClick(object sender, MouseEventArgs e)
        {
            color_choice = 0;

            Label Blue = (Label)sender;
            Blue.ForeColor = Color.Blue;
            Blue.Font = new Font("Times New Roman", 22);

            Blue.MouseLeave -= BLUE_team_MouseLeave;
            Blue.MouseEnter -= BLUE_team_MouseEnter;

            RED_team.ForeColor = Color.Red;
            
            if (initial_phase == false && RED_team.Font != Font1)
            {
                RED_team.MouseEnter += RED_team_MouseEnter;
                RED_team.MouseLeave += RED_team_MouseLeave;
            }

            RED_team.Font = Font1;
            initial_phase = false;
        }

        private void BLUE_team_MouseLeave(object sender, EventArgs e)
        {
            Label Blue = (Label)sender;
            Blue.ForeColor = Color.Blue;
            Blue.Font = new Font("Times New Roman", 18);
        }

        private void BLUE_team_MouseEnter(object sender, EventArgs e)
        {
            Label Blue = (Label)sender;
            Blue.ForeColor = Color.DarkBlue;
            Blue.Font = new Font("Times New Roman", 22);
        }

        private void btn_roll_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            int team = random.Next(0, 2);

            if (team == 0)
            {
                //MessageBox.Show("Red is starting");
                RED_team_MouseClick(RED_team, null);

            }
            else
            {
                //MessageBox.Show("Blue is starting");
                BLUE_team_MouseClick(BLUE_team, null);
            }


        }
    }
}

