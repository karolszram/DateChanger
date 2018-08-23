using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DateChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            string Path = "C:\\Users\\karol.szram\\Desktop\\DateChanger";
            Console.Write("Default Path is: ");
            Console.WriteLine(Path);

            Console.WriteLine("If you want to change it press 'c', else press any key");
            char key = Console.ReadKey(true).KeyChar;
            if (key == 'c') Path = getNewPath();

            if (!Directory.Exists(Path))
            {
                Console.WriteLine("Path does not exists!");
                Path = getNewPath();
            }

            string date = File.GetCreationTimeUtc(Path).ToShortDateString();
            DirectoryInfo dirInf = new DirectoryInfo(@Path);
            FileInfo[] Files = dirInf.GetFiles();
            int i = 1;
            foreach (FileInfo file in Files)
            {
                string extension = file.Extension;
                string OldName = Path + "\\" + file.Name;
                string CreationTime = File.GetCreationTimeUtc(OldName).ToShortDateString();
                CreationTime +=" " + File.GetCreationTimeUtc(OldName).ToShortTimeString();
                CreationTime = CreationTime.Replace(":" , "_").Replace("/","_");

                string newName = Path + "\\" + CreationTime + extension;
                try
                {
                    File.Move(OldName, newName);
                }
                catch
                {
                    newName = Path + "\\" + CreationTime + "("+i.ToString()+")" + extension;
                    
                    File.Move(OldName, newName);
                    i++;
                }

            }
            try
            {
                if (Files.First() != null) Console.WriteLine("It is done");
            }
            catch
            { 
                Console.WriteLine("No files in folder");
            }
        }

        static string getNewPath()
        {
            string Path = "";
            bool isCorrect = false;
            Console.WriteLine();
            do
            {
                Console.WriteLine("Please give new correct path");
                Path = Console.ReadLine();
                Console.WriteLine();
                isCorrect = Directory.Exists(Path);
                if (!isCorrect) Console.WriteLine("Incorrect path!");

            }
            while (isCorrect == false);
            return Path;
        }

    }

}
