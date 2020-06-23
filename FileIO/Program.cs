using System;
using System.Collections.Generic;
using System.IO;

namespace FileIO
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Console Input
            Console.WriteLine("Waiting...");
            string input = Console.ReadLine();
            Console.WriteLine("User Input : " + input);

            string workingDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(workingDirectory).FullName).Parent.FullName;
            Console.WriteLine(workingDirectory);
            Console.WriteLine(projectDirectory);

            FileIO fileIO = new FileIO();

            List<string> writeData = new List<string>();
            writeData.Add("abc");
            writeData.Add("ABC");
            writeData.Add("123");
            writeData.Add("가나다");
            writeData.Add("456");

            fileIO.WriteFile(projectDirectory + "\\test.txt", writeData);


            // File 읽기 -- 1
            List<string> readData = new List<string>();
            readData = fileIO.ReadFile(projectDirectory + "\\test.txt");

            foreach(string data in readData)
            {
                Console.WriteLine(data);
            }

            // File 읽기 -- 2
            string[] readFile = File.ReadAllText(projectDirectory + "\\test.txt").Split("\n");
            //string[] readFile = File.ReadAllLines(projectDirectory + "\\test.txt");
            foreach(string item in readFile)
            {
                Console.WriteLine(item);
            }

            // File 및 폴더 검색(하위 폴더 포함)
            //string[] files = Directory.GetFiles(@"D:\호비\", "*.MP4", SearchOption.AllDirectories);
            string[] files = Directory.GetFiles(@"D:\호비\", "", SearchOption.AllDirectories);
            foreach(string file in files)
            {
                if (Directory.Exists(file)) Console.WriteLine("============");
                Console.WriteLine(file);
            }

            //// File Write
            //StreamWriter sw = new StreamWriter(new FileStream("a.txt", FileMode.Create));
            //sw.WriteLine("abc");
            //sw.WriteLine("123");
            //sw.WriteLine("ABC");
            //sw.WriteLine("가나다");
            //sw.Close();

            //// File Read
            //StreamReader sr = new StreamReader(new FileStream("a.txt", FileMode.Open));
            //string line;
            //while((line = sr.ReadLine()) != null)
            //{
            //    Console.WriteLine(line);
            //}
            //sr.Close();
        }
    }

    public class FileIO
    {
        public List<string> ReadFile(string aPath)
        {
            List<string> list = new List<string>();

            StreamReader sr = new StreamReader(new FileStream(aPath, FileMode.Open));
            string line;
            while((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
            sr.Close();

            return list;
        }

        public void WriteFile(string aPath, List<string> aData)
        {
            StreamWriter sw = new StreamWriter(new FileStream(aPath, FileMode.OpenOrCreate));

            foreach(string data in aData)
            {
                sw.WriteLine(data);
            }

            sw.Close();
        }
    }
}
