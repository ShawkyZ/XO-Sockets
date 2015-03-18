using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
namespace Tic_Tac_Toe
{
    public partial class frmTicTacToe : Form
    {
        Socket _socket;
        Socket clientSocket;
        byte[] buffer = new byte[1024];
        public frmTicTacToe() 
        { 
            InitializeComponent();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);        
        }
        bool game;
        string p1Name, p2Name;
        char player = 'X';
        int xScore = 1, oScore = 1;
        List<int> X = new List<int>();
        List<int> O = new List<int>();

        public void Bind(int port)
        {
            _socket.Bind(new IPEndPoint(IPAddress.Any, port));
        }
        public void Listen(int backlog)
        {
            _socket.Listen(backlog);
        }
        public void Accept()
        {
            _socket.BeginAccept(AcceptCallBack, null);
        }
        private void AcceptCallBack(IAsyncResult result)
        {
            this.Invoke(new Action(() => State.Text = "Client Connected!"));
            this.Invoke(new Action(() => panel1.Show()));
            clientSocket = _socket.EndAccept(result);
            buffer = new byte[1024];
            Accept();
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, RecieveCallBack, clientSocket);
        }
        private void RecieveCallBack(IAsyncResult result)
        {
            clientSocket = result.AsyncState as Socket;
            int bufferSize = clientSocket.EndReceive(result);
            byte[] packet = new byte[bufferSize];
            Array.Copy(buffer, packet, packet.Length);
            string msg = Encoding.ASCII.GetString(packet);
            if (msg.Contains("coordinate"))
            {
                check(int.Parse(msg.Replace("coordinate", "")));
            }
            buffer = new byte[1024];
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, RecieveCallBack, clientSocket);
        }
        public void SendMsg(byte[] msg)
        {
            clientSocket.Send(msg);
        }
        public void setNames()
        {
            p1Name = "Server";
            p2Name = "Client";
            lblP1Name.Text = p1Name + ":";
            lblP2Name.Text = p2Name + ":";
        }
        void check(int getSlotNr)
        {
            game = true;
            this.panel1.Controls["pbSlot" + getSlotNr.ToString()].Text = player.ToString();
            this.panel1.Controls["pbSlot" + getSlotNr.ToString()].Enabled = false;
            if (player == 'X') X.Add(getSlotNr);
            else O.Add(getSlotNr);
            result();
            if (game == true) player = (player == 'X') ? 'O' : 'X';
        }

        public void result()
        {
            int[,] win = new int[,]    {
                      {1,2,3}, {4,5,6}, {7,8,9},
                      {1,4,7}, {2,5,8}, {3,6,9},
                      {1,5,9}, {7,5,3}
               };
            List<int>[] p = new List<int>[] { X, O };
            int listPos = (player == 'X') ? 0 : 1;

            for (int i = 0; i < 8; i++)
            {
                if (p[listPos].Contains(win[i, 0]) && p[listPos].Contains(win[i, 1]) && p[listPos].Contains(win[i, 2]))
                {
                    ((Label)this.panel1.Controls["lblP" + player.ToString() + "Score"]).Text = ((player == 'X') ? xScore++ : oScore++).ToString();
                    string winner = (player == 'X') ? p1Name : p2Name;
                    MessageBox.Show(winner + " won!", "Result");
                    clearBoard();
                }
            }
            if (X.Count + O.Count == 9)
            {
                MessageBox.Show("It's a draw!", "Result");
                clearBoard();
            }
        }
        void clearBoard()
        {
            for (int i = 1; i <= 9; i++)
            {
                this.panel1.Controls["pbSlot" + i.ToString()].Text = "";
                this.panel1.Controls["pbSlot" + i.ToString()].Enabled = true;
            }
            game = false;
            player = 'X';
            X.Clear();
            O.Clear();
        }

        private void pbSlot1_Click(object sender, EventArgs e) 
        {
            if (player == 'X')
            {
                check(1);
                clientSocket.Send(Encoding.UTF8.GetBytes("coordinate1"));
            }
        }
        private void pbSlot2_Click(object sender, EventArgs e) 
        {
            if (player == 'X')
            {
                check(2);
                clientSocket.Send(Encoding.UTF8.GetBytes("coordinate2"));
            }
        }
        private void pbSlot3_Click(object sender, EventArgs e) 
        {
            if (player == 'X')
            {
                check(3);
                clientSocket.Send(Encoding.UTF8.GetBytes("coordinate3"));
            }
        }
        private void pbSlot4_Click(object sender, EventArgs e) 
        {
            if (player == 'X')
            {
                check(4);
                clientSocket.Send(Encoding.UTF8.GetBytes("coordinate4"));
            }
        }
        private void pbSlot5_Click(object sender, EventArgs e) 
        {
            if (player == 'X')
            {
                check(5);
                clientSocket.Send(Encoding.UTF8.GetBytes("coordinate5"));
            }
        }
        private void pbSlot6_Click(object sender, EventArgs e) 
        {
            if (player == 'X')
            {
                check(6);
                clientSocket.Send(Encoding.UTF8.GetBytes("coordinate6"));
            }
        }
        private void pbSlot7_Click(object sender, EventArgs e) 
        {
            if (player == 'X')
            {
                check(7);
                clientSocket.Send(Encoding.UTF8.GetBytes("coordinate7"));
            }
        }
        private void pbSlot8_Click(object sender, EventArgs e) 
        {
            if (player == 'X')
            {
                check(8);
                clientSocket.Send(Encoding.UTF8.GetBytes("coordinate8"));
            }
        }
        private void pbSlot9_Click(object sender, EventArgs e) 
        {
            if (player == 'X')
            {
                check(9);
                clientSocket.Send(Encoding.UTF8.GetBytes("coordinate9"));
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            clearBoard();
            setNames();
            lblPXScore.Text = "0";
            lblPOScore.Text = "0";
            xScore = 1;
            oScore = 1;
        }
        private void frmTicTacToe_Load(object sender, EventArgs e) 
        {
            panel1.Hide();
            Bind(6556);
            Listen(500);
            Accept();
            setNames();
            
        }
    }
}