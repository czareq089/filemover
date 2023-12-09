using System;
using System.IO;
using System.Net;

namespace FileMover;

class MainClass
{
    static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                Logo.Print();
                Console.WriteLine("Welcome to FileMover!");
                Console.WriteLine("The program moves files from one directory to another based on a filetype.");
                Console.WriteLine(
                    @"Please choose the directory you want to move files from: (leave blank for current \downloads)");
                string from = Console.ReadLine();
                Console.WriteLine(
                    "=============================================================================================");
                Console.WriteLine("Here are the files in the directory you chose:");
                Console.WriteLine("");
                ListFiles(from);
                Console.WriteLine("");
                Console.WriteLine(
                    @"Please choose the directory you want to move files to: (leave blank for current \desktop\folder)");
                string to = Console.ReadLine();
                Console.WriteLine("Please enter the filetype you want to exclude: (leave blank for none)");
                Console.WriteLine("");
                string exclude = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("=============================================================================================");
                Console.WriteLine("Are you sure you want to move these files? (y/n)");
                Console.WriteLine("");
                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "y":
                        Console.WriteLine("Moving files...");
                        MoveFiles(from, to, exclude);
                        Console.WriteLine("Done!");
                        break;
                    case "n":
                        Console.WriteLine("Aborting...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid choice, aborting...");
                        Console.ReadKey();
                        break;
                }
                Console.ReadKey();
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
    }
    static void MoveFiles(string from, string to, string exclude)
    {
        string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        if (from == "")
        {
            from = downloadsPath;
        }
        if (to == "")
        {
            to = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\FileMover";
        }
        if (exclude == "")
        {
            exclude = "none";
        }
        string[] files = Directory.GetFiles(from);
        foreach (var file in files)
        {
            string fileName = Path.GetFileName(file);
            string fileExtension = Path.GetExtension(file);
            if (fileExtension == exclude)
            {
                Console.WriteLine("Excluding " + fileName + "...");
            }
            else
            {
                Console.WriteLine("Moving " + fileName + "...");
                File.Move(file, to + "\\" + fileName);
            }
        }
    }
    static void ListFiles(string from)
    {
        string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        if (from == "")
        {
            from = downloadsPath;
        }
        string[] files = Directory.GetFiles(from);
        foreach (var file in files)
        {
            Console.WriteLine(Path.GetFileName(file));
        }
    }
}