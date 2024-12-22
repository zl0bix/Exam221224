using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam221224
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WorkBook book = new WorkBook();
            book.Work();
        }
    }

    class DictionaryBook
    {
        public string WordEn { get;  set; }
        public string WordRu { get;  set; }
        public DictionaryBook(string WordEn, string WordRU)
        {
            this.WordEn = WordEn;
            this.WordRu = WordRU;
        }
    }

    class WorkBook
    {
        List<DictionaryBook> _listBook = new List<DictionaryBook>();

        string path = @"C:/txt/fileToTest.txt";

        public string OpenFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public void WriteTxt(string str1, string str2)
        {           
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter sr = new StreamWriter(fs))
                {
                    sr.WriteLine(str1 + " " + str2);
                }
            }
        }

        public void CreateDictionary()
        {
            string strTmp1 , strTmp2;

            Console.Write("Введите английское слово -> ");
            strTmp1 = Console.ReadLine();
            Console.Write($"Введите русский перевод слова {strTmp1} -> ");
            strTmp2 = Console.ReadLine();

            _listBook.Add(new DictionaryBook(strTmp1, strTmp2));

             WriteTxt(strTmp1,strTmp2);
        }

        public List<DictionaryBook> ReturnListFromFile()
        {
            List<DictionaryBook> bookTmp = new List<DictionaryBook>();

            string strTmp = OpenFile(path);

            char[] chArr1 = strTmp.ToCharArray();
            string[] strArr2 = new string[2];

            string strTmpChar1 = "";
            string strTmpChar2 = "";

            for (int i = 0; i < chArr1.Length; i++)
            {
                if (chArr1[i] != '\n')

                    strTmpChar1 += chArr1[i];

                /*else if (chArr[i] != '\n' && chArr[i] != ' ')

                    strTmpChar2 += chArr[i];*/

                else
                {
                    strArr2 = strTmp.Split(' ');
                    bookTmp.Add(new DictionaryBook(strArr2[0], strArr2[2]));
                    strTmpChar1 = "";
                    //strTmpChar2 = "";
                }
            }

            return bookTmp;
        }

        public void ShowListFile()
        {
            List<DictionaryBook> list = ReturnListFromFile();

            foreach (DictionaryBook book in list)
            {
                Console.WriteLine(book.WordEn + " " + book.WordRu);
            }
        }

        public int ReturnIndex(string str)
        {
            for (int i = 0; i < _listBook.Count;i++)
            {
                if(_listBook[i].WordEn.ToLower() == str.ToLower())

                     return i; 
            }
            return -1;
        }


        public void DelElList(int num)
        {
            _listBook.RemoveAt(num);
        }

       public void Work()
        {
            int num = 0;
            string logOut = "exit";
            string str = "";
            Console.WriteLine("Что хотите сделать:\n1: Add words\n2: Show All words\n3: Delete for index\n4: Name search\n5: Exit(or Write exit)");

            while (str.ToLower() != logOut.ToLower())
            {
                Console.WriteLine("Что хотите сделать:\n1: Add words\n2: Show All words\n3: Delete for index\n4: Name search\n5: Exit(or Write exit)");

                str = Console.ReadLine();
                switch (str)
                {
                        case "1":

                        CreateDictionary();

                        break;

                        case "2":

                        ShowListFile(); 

                        break;

                        case "3":

                        ShowListFile();

                        Console.Write("Enter index to delete -> ");
                        int numDel = Convert.ToInt32(Console.ReadLine());
                        DelElList(numDel);
                        break;

                        case "4":
                        Console.WriteLine("Enter word -> ");
                        Console.WriteLine( _listBook[ReturnIndex(Console.ReadLine())].WordEn + " " + _listBook[ReturnIndex(Console.ReadLine())].WordRu);
                        break;

                        case "5":

                        str = "exit";
                        break;

                            case "exit":

                            str = "exit";

                            break;
                }
            }
        }

    }
}
