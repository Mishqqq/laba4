using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    enum Results
    {
        Help,
        good,
        fail
    };
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                CopyFilesRecursively("Лабораторные", "Лабораторные - copy");
            }
            else
            {
                bool isHelp = false;
                for(var i=0;i<args.Length; i++)
                {
                    if (args[i] == "-h" || args[i] == "-help" || args[i] == "-HELP") 
                    {
                        isHelp = true;
                    }
                }
                if (isHelp)
                {
                    Console.WriteLine("Help ");
                    Console.WriteLine("ConsoleApp12.exe \"C:\\Users\\Миша\\Лабораторные\" \"C:\\Users\\Миша\\Лабораторные\\1\" ");
                    Console.WriteLine("ConsoleApp12.exe \"C:\\Users\\Миша\\Лабораторные\" \"C:\\Users\\Миша\\Лабораторные\\1\" *.exe *.docx ");
                    return (int)Results.Help;
                }
                if(args.Length >= 2)
                {
                    string source = args[0];
                    string destination = args[1];
                    //CopyFilesRecursively(source, destination);
                    if(args.Length==2)
                        {
                        CopyFilesRecursively(source, destination);
                    }
                    List<string> fileformats = new List<string>();
                    if(args.Length >= 3)
                    {
                        for (var i = 2; i < args.Length; i++)
                        {
                            Console.WriteLine(args[i]);
                            fileformats.Add(args[i]);

                        }
                        CopyFilesRecursively(source, destination,fileformats);
                    }
                }
                else
                {
                    Console.WriteLine("Not enought args ");
                    return (int)Results.fail;
                }
            }
            return (int)Results.good;
        }
        static void CopyFilesRecursively(string sourcePath, string targetPath, List<string> fileformats = null)
        {
            System.IO.Directory.CreateDirectory(targetPath);
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                DirectoryInfo sourceDirectory = new DirectoryInfo(dirPath);
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                DirectoryInfo targetDirectory = new DirectoryInfo(dirPath.Replace(sourcePath, targetPath));
                targetDirectory.Attributes = sourceDirectory.Attributes;
            }
            if (fileformats == null)
            {
                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }
            }
            else
            {
                foreach (var fileformat in fileformats)
                {
                    foreach (string newPath in Directory.GetFiles(sourcePath, fileformat, SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                    }
                }
            }
            
        }

    }
}
