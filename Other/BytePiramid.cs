using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {

        static void Piramid_1(string arr)
        {
            try
            {
                Console.WriteLine("    ^");
                Console.WriteLine("   /" + arr[0] + "\\");
                Console.WriteLine("  *---*  ");
                
            }
            catch { }
        }


        static void Piramid_A1(string arr)
        {
            try
            {
                Console.WriteLine("    ^");
                Console.WriteLine("   /" + arr[3] + "\\");
                Console.WriteLine("  *---*  ");
                Console.WriteLine(" /" + arr[2] + "\\" + arr[1] + "/" + arr[0] + "\\");
                Console.WriteLine("*---*---*");

            }
            catch { }
        }


        static string TransformMain(string arr)
        {
            if (arr == "0000") arr = "0000";
            else
             if (arr == "0001") arr = "1000";
            else
             if (arr == "0010") arr = "0001";
            else
            if (arr == "0011") arr = "0010";
            else
            if (arr == "0100") arr = "0000";
            else
             if (arr == "0101") arr = "0010";
            else
             if (arr == "0110") arr = "1011";
            else
              if (arr == "0111") arr = "1011";
            else
             if (arr == "1000") arr = "0100";
            else
             if (arr == "1001") arr = "0101";
            else
              if (arr == "1010") arr = "0111";
            else
             if (arr == "1011") arr = "1111";
            else
            if (arr == "1100") arr = "1101";
            else
            if (arr == "1101") arr = "1110";
            else
            if (arr == "1110") arr = "0111";
            else
            if (arr == "1111") arr = "1111";

            return arr;
        }

        static string TestS(string arr)
        {
            if (arr == "1111" || arr == "0000")
                return "true";
            else return "false";
        }

        static void Piramid_AA1(string arr, int left, int top)
        {
            try
            {

                Console.SetCursorPosition(left  - 1, top + 1);
                Console.WriteLine("/" + arr[3] + "\\");

                Console.SetCursorPosition(left  - 2, top + 2);
                Console.WriteLine("*---*");

                Console.SetCursorPosition(left  - 3, top + 3);
                Console.WriteLine("/" + arr[2] + "\\" + arr[1] + "/" + arr[0] + "\\");

                Console.SetCursorPosition(left  - 4, top + 4);
                Console.WriteLine("*---*---*");

                Console.SetCursorPosition(left  - 5, top + 5);
                Console.WriteLine("/" + arr[7] + "\\" + arr[9] + "/" + arr[10] + "\\" + arr[11] + "/" + arr[15] + "\\");

                Console.SetCursorPosition(left  - 6, top + 6);
                Console.WriteLine("*---*---*---*");

                Console.SetCursorPosition(left  - 7, top + 7);
                Console.WriteLine("/" + arr[6] + "\\" + arr[5] + "/" + arr[4] + "\\" + arr[8] + "/" + arr[14] + "\\" + arr[13] + "/" + arr[12] + "\\");

                Console.SetCursorPosition(left - 8, top + 8);
                Console.WriteLine("*---*---*---*---*");

            }
            catch { }
        }


        static void Piramid_AA2(string arr, int left, int top)
        {
            try
            {

                Console.SetCursorPosition(left + 1+1, top + 1);
                Console.WriteLine(arr[5] + "/" + arr[6] + "\\" + arr[7] + "/" + arr[11] + "\\" + arr[13] + "/" + arr[14] + "\\" + arr[15]);

                Console.SetCursorPosition(left  + 2 + 1, top + 2);
                Console.WriteLine("---*---*---");

                Console.SetCursorPosition(left  + 3 + 1, top + 3);
                Console.WriteLine(arr[4] + "/" + arr[10] + "\\" + arr[9] + "/" + arr[8] + "\\" + arr[12]);

                Console.SetCursorPosition(left + 4 + 1, top + 4);
                Console.WriteLine("---*---");

                Console.SetCursorPosition(left  + 5 + 1, top + 5);
                Console.WriteLine(arr[1] + "/" + arr[2] + "\\" + arr[3]);

                Console.SetCursorPosition(left + 6 + 1, top + 6);
                Console.WriteLine("---");

                Console.SetCursorPosition(left + 7 + 1, top + 7);
                Console.WriteLine(arr[0]);

                Console.SetCursorPosition(left + 8 + 1, top + 8);
                Console.WriteLine("*");
            }
            catch { }
        }


        static void Main(string[] args)
        {
            Console.Write("Array:");
            string arr = Console.ReadLine();
            Console.Clear();

            string arrT = null;
            string testarrM = null;
            string testarr = null;
            string arrMain = null;


            Console.WriteLine("Length:" + arr.Length);


            //Транформирование 

            for (int i = 0; i < arr.Length / 4; i++)
            {
                string arrP = String.Concat(arr[i * 4], arr[i * 4 + 1], arr[i * 4 + 2], arr[i * 4 + 3]);
                arrT = string.Concat(arrT, TransformMain(arrP));

                arrP = null;
            }
            //-----------------


            Console.WriteLine("Length:" + arrT.Length);


            //Проверка на сокращение
            for (int i = 0; i < arrT.Length / 4; i++)
            {
                string arrP = String.Concat(arrT[i * 4], arrT[i * 4 + 1], arrT[i * 4 + 2], arrT[i * 4 + 3]);
                testarr = string.Concat(testarr, TestS(arrP));
                testarrM = string.Concat(testarrM + "true");
            }

            if (testarr == testarrM)
            {
            back:
                for (int i = 0; i < arrT.Length / 4; i++)
                {
                    string arrP = String.Concat(arrT[i * 4], arrT[i * 4 + 1], arrT[i * 4 + 2], arrT[i * 4 + 3]);
                    if (arrP == "1111")
                        arrMain = String.Concat(arrMain + "1");
                    else
                        if (arrP == "0000")
                        arrMain = String.Concat(arrMain + "0");
                }

                arrT = arrMain;

                if (arrT == "1111") goto back;
                if (arrT == "0000") goto back;
            }


            Console.WriteLine("Length:" + arrT.Length);


            //-------------------------

            arrMain = arrT;

            //Console.WriteLine("arrMain:" + arrMain);

            if (arrMain.Length == 1) Piramid_1(arrMain);
            else
            if (arrMain.Length == 4) Piramid_A1(arrMain);
            else
            if (arrMain.Length == 16)
            {
                Console.SetCursorPosition(1 + arrMain.Length / 2, 0);
                Console.WriteLine("^");
                Piramid_AA1(arrMain, arrMain.Length / 2 + 1, 0);
            }
            else
            {

                int AllPir = arrMain.Length / 16;
                int iter = 3;
                int iMass = 16;

                while (AllPir >= iter)
                {
                    AllPir = AllPir - iter;
                    iter += 2;
                }

                iter -= 2;

               // Console.WriteLine("iter:" + iter); //64 - 3; 256 - 7;

                int iter2 = iter;

                int maxI = 1;

                while (iter2 >= maxI)
                {
                    iter2 = iter2 - maxI;
                    maxI += 2;
                }

                maxI -= 2;

                Console.WriteLine("maxI:" + maxI); //64 - 1; //256 - 3

                Console.WriteLine("iter:" + iter); //64 - 3; 256 - 7;

                Console.WriteLine("Length:" + arrMain.Length);

                Thread.Sleep(1000);
                Console.Clear();


                int left = (maxI+1)*8;
                int left2 = 0;
                int left3 = 0;
                int top = 0;

                Console.SetCursorPosition(left, top);
                Console.WriteLine("^");

                string arrP_1 = String.Concat(arrMain[0], arrMain[1], arrMain[2], arrMain[3], arrMain[4], arrMain[5], arrMain[6], arrMain[7], arrMain[8], arrMain[9], arrMain[10], arrMain[11], arrMain[12], arrMain[13], arrMain[14], arrMain[15]);
                Piramid_AA1(arrP_1, left, top);

                top = 8;

                try{
                    for (int i = 1; i <= maxI; i++)
                    {
                        int ji = 0;

                        left = left - 8;

                        string arrP_2 = String.Concat(arrMain[iMass + 0], arrMain[iMass + 1], arrMain[iMass + 2], arrMain[iMass + 3], arrMain[iMass + 4], arrMain[iMass + 5], arrMain[iMass + 6], arrMain[iMass + 7], arrMain[iMass + 8], arrMain[iMass + 9],
                                arrMain[iMass + 10], arrMain[iMass + 11], arrMain[iMass + 12], arrMain[iMass + 13], arrMain[iMass + 14], arrMain[iMass + 15]);
                        Piramid_AA1(arrP_2, left, top);

                        iMass += 16;

                        for (int j = 1; j <= i; j++)
                        {
                            left2 = left + ji;

                            string arrP_3 = String.Concat(arrMain[iMass + 0], arrMain[iMass + 1], arrMain[iMass + 2], arrMain[iMass + 3], arrMain[iMass + 4], arrMain[iMass + 5], arrMain[iMass + 6], arrMain[iMass + 7], arrMain[iMass + 8], arrMain[iMass + 9],
                                arrMain[iMass + 10], arrMain[iMass + 11], arrMain[iMass + 12], arrMain[iMass + 13], arrMain[iMass + 14], arrMain[iMass + 15]);
                            Piramid_AA2(arrP_3, left2, top);

                            iMass += 16;

                            ji += 16;

                            left3 = left2 + 16;

                            //Console.WriteLine("iMass"+ iMass);

                            string arrP_4 = String.Concat(arrMain[iMass + 0], arrMain[iMass + 1], arrMain[iMass + 2], arrMain[iMass + 3], arrMain[iMass + 4], arrMain[iMass + 5], arrMain[iMass + 6], arrMain[iMass + 7], arrMain[iMass + 8], arrMain[iMass + 9],
                                arrMain[iMass + 10], arrMain[iMass + 11], arrMain[iMass + 12], arrMain[iMass + 13], arrMain[iMass + 14], arrMain[iMass + 15]);
                            Piramid_AA1(arrP_4, left3, top);

                            iMass += 16;

                        }

                        top += 8;
                    }
                }catch { };
        };

            // 1010101010101010101010101010101010101010101010101010101010101010 - 64
            
            Console.Read();
        }
    }
}

