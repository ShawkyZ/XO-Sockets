using System;
using System.Collections.Generic;
using System.Text;

namespace XOX
{
    class Class1
    {
        int count=0;
        public int winner(System.Windows.Forms.Button [,] btnTemp)
        {
            for (int i = 0; i < btnTemp.GetLength(0); i++)
            {
                for (int j = 0; j < btnTemp.GetLength(1); j++)
                {
                    if (btnTemp[i, j].Text == "X")
                        count++;
                    if (btnTemp[i, j].Text == "O")
                        count--;
                }
                if (count == 3)
                    return 1;
                if (count == -3)
                    return 2;
                count = 0;
            }
            count = 0;
            for (int j = 0; j < btnTemp.GetLength(1); j++)
            {
                for (int i = 0; i < btnTemp.GetLength(0); i++)
                {
                    if (btnTemp[i, j].Text == "X")
                        count++;
                    if (btnTemp[i, j].Text == "O")
                        count--;
                }
                if (count == 3)
                    return 1;
                if (count == -3)
                    return 2;
                count = 0;
            }


            if (btnTemp[0, 0].Text == btnTemp[1, 1].Text && btnTemp[0, 0].Text == btnTemp[2, 2].Text && btnTemp[0, 0].Text== "X")
                return 1;
            if (btnTemp[0, 0].Text == btnTemp[1, 1].Text && btnTemp[0, 0].Text == btnTemp[2, 2].Text && btnTemp[0, 0].Text == "0")
                return 2;
            if (btnTemp[2, 0].Text == btnTemp[1, 1].Text && btnTemp[2, 0].Text == btnTemp[0, 2].Text && btnTemp[2, 0].Text == "X")
                return 1;
            if (btnTemp[2, 0].Text == btnTemp[1, 1].Text && btnTemp[2, 0].Text == btnTemp[0, 2].Text && btnTemp[2, 0].Text == "O")
                return 2;       

            return 0;
            
        }
    }
}
