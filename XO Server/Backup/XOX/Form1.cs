using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOX
{
    
    public partial class Form1 : Form
    {
        System.Windows.Forms.Button[,] btn;

        
        int pl = 0;

        Class1 func = new Class1();
        public Form1()
        {
            InitializeComponent();
        }

        public void buildArr()
        {
            


            btn = new System.Windows.Forms.Button[3, 3];
          
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    btn[i,j] = new System.Windows.Forms.Button();
                    btn[i, j].Location = new System.Drawing.Point(50 + i * 65, 30 + j * 65);
                    btn[i, j].Size = new System.Drawing.Size(50, 30);
                    btn[i, j].Enabled = false;
                    Controls.Add(btn[i, j]);
                    btn[i, j].Click += new EventHandler(btn_Click);
                }
            }
           

        }

        private void btn_Click(object sender, EventArgs e)
        {

            Button btnTemp = (Button)sender;

            if (pl == 0)
            {
                btnTemp.BackColor = Color.Red;
                btnTemp.Text = "X";
                pl = 1;
                label1.Text = "O turn";
            }
            else
            {
                btnTemp.BackColor = Color.Blue;
                btnTemp.Text = "O";
                pl = 0; label1.Text = "X turn";
            }
            btnTemp.Enabled = false;

            if (func.winner(btn) == 1)
            {
                MessageBox.Show("the winner is: " + txt1.Text);

                for (int i = 0; i < 3; i++)                
                    for (int j = 0; j < 3; j++)                 
                        btn[i, j].Enabled = false;
            }

            if (func.winner(btn) == 2)
            {
                MessageBox.Show("the winner is: " + txt2.Text);

                for (int i = 0; i < 3; i++)                
                    for (int j = 0; j < 3; j++)                   
                        btn[i, j].Enabled = false;                    
            }
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {


            txt1.Enabled = false;
            txt2.Enabled = false;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    btn[i, j].Enabled = true;    


            btnStart.Enabled = false;
            btnNew.Visible = true;
            label1.Text = "X turn";

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnNew.Visible = false;
            btnStart.Enabled = true;
            txt1.Enabled = true;
            txt2.Enabled = true;
            
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    btn[i, j].Text = "";
                    btn[i, j].BackColor = Color.Empty;
                }
            pl = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        buildArr();
        }





    }
}