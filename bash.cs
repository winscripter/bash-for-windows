/* Online C# Compiler and Editor */
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

class Program
{
    static void RunCommand(string CommandName)
    {
        string output = Regex.Replace(CommandName.Split()[0], @"[^0-9a-zA-Z\ ]+", "");
        
        if (output == "mkdir")
        {
            string __mkdir_folder = "";
            try
            {
            __mkdir_folder = Regex.Replace(CommandName.Split()[1], @"[^0-9a-zA-Z\ ] +", "");
            }
            catch (Exception)
            {
                Console.WriteLine("mkdir: missing operand\n*Hint: Try: \'mkdir \"folder name here\"\'\n");
                LinuxTerminal();
            }
            if (Directory.Exists(@__mkdir_folder))
            {
                Console.WriteLine("mkdir: cannot create directory '" + __mkdir_folder + "': File exists");
            } else {
                if (File.Exists(@__mkdir_folder))
                {
                    Console.WriteLine("mkdir: cannot create directory '" + __mkdir_folder + "': File exists");
                    LinuxTerminal();
                } else {
                    Directory.CreateDirectory(__mkdir_folder);
                }
            }
            LinuxTerminal();
        }
        
        if (output == "pwd")
        {
            string __current_directory = Directory.GetCurrentDirectory();
            Console.WriteLine(__current_directory);
            LinuxTerminal();
        }
        
        if (output == "touch")
        {
            string __touch_command_file = "";
            try
            {
                __touch_command_file = Regex.Replace(CommandName.Split()[1], @"[^0-9a-zA-Z\ ] +", "");
            }
            catch (Exception)
            {
                Console.WriteLine("touch: missing file operand\n*Hint: Try: \'touch \"file name here\"\'\n");
                LinuxTerminal();
            }
            
            if (__touch_command_file == "--help")
            {
                Console.WriteLine("Creates an empty file.\ntouch \"filename\"\n\nIf a file exists, it will get deleted first. This is because the script generates a \"fatal error\" if a file exists already.\n");
                LinuxTerminal();
                
            }
            if (File.Exists(__touch_command_file)) File.Delete(__touch_command_file);
            File.Create(__touch_command_file);
            Console.WriteLine("" + "\n");
            LinuxTerminal();
        }
        
        if (output == "echo")
        {
            if (CommandName == "echo")
            {
                LinuxTerminal();
            }
            string word = CommandName;
            if (word.Length > 0)
            {
                int i = word.IndexOf(" ")+1;
                string str=word.Substring(i);
                if (CommandName.Contains(">>"))
                {
                    string ToFile = str.Substring(str.LastIndexOf(">>") + 1);
                    try
                    {
                        File.AppendAllText(ToFile, str);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unknown error.");
                        LinuxTerminal();
                    }
                    LinuxTerminal();
                } else if (CommandName.Contains(">"))
                {
                    
                    string ToFile = str.Substring(str.LastIndexOf('>') + 1);
                    try
                    {
                        File.Delete(ToFile);
                        File.AppendAllText(ToFile, str);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unknown error.");
                        LinuxTerminal();
                    }
                    LinuxTerminal();
                } else {
                    Console.WriteLine(str + "\n");
                }
            }
            LinuxTerminal();
        }
        
        if (output == "ls")
        {
            string dir_current = Directory.GetCurrentDirectory();
            try
            {
                Console.WriteLine(string.Join("\n", Directory.GetFiles(@dir_current)));
            }
            catch (Exception)
            {
                Console.WriteLine("Unknown exception!");
            }
            if (CommandName.Contains("-hl") || CommandName.Contains("-h") || CommandName.Contains("-l") || CommandName.Contains("-lh"))
            {
                Console.WriteLine("*** Little notice: Although given parameters are correct, they do not make any sense in this case.\n");
            }
            LinuxTerminal();
        }
        
        if (output == "ps")
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                try
                {
                    Console.WriteLine(p.ProcessName);
                }
                catch (Exception)
                {
                    Console.WriteLine("Could not display this process: Process exited in time.");
                }
            }
            
            if (CommandName.Contains("-a") || CommandName.Contains("-aux"))
            {
                Console.WriteLine("*** Little notice: Although given parameters are correct, they do not make any sense in this case.\n");
            }
            LinuxTerminal();
        }
        
        if (output == "kill")
        {
            string word2 = "0";
            try
            {
                word2 = Regex.Replace(CommandName.Split()[1], @"[^0-9a-zA-Z\ ] +", "");
            }
            catch (Exception)
            {
                Console.WriteLine("*KILL: Sends a signal to a process to forcefully end it.\nkill [7 | 9] [process name]\n\nInformation: This isn't the screen that will be actually displayed when using the KILL command incorrectly in bash.\n\nIf the number is not 7 or 9, nothing happens.\n\nAlso, even if you do use Windows, you should not append .EXE in a process name. This won't work anyway. For example, to end chrome.exe, use \"kill 9 chrome\" or \"kill 7 chrome\" command.");
                LinuxTerminal();
            }
            
            if (word2 == "7" || word2 == "9")
            {
                try
                {
                    string SpecifiedProcessName = Regex.Replace(CommandName.Split()[2], @"[^0-9a-zA-Z\ ] +", "");
                    var pname = Process.GetProcesses().
                    Where(pr => pr.ProcessName == SpecifiedProcessName);
        
                    try
                    {
                        foreach (var process in pname)
                        {
                            process.Kill();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unable to kill the specified process.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Process name must be entered. Type the kill command without any arguments for more information.");
                }
            }
            LinuxTerminal();
        }
        
        if (output == "rm")
        {
            string arg1 = "";
            try
            {
                arg1 = Regex.Replace(CommandName.Split()[1], @"[^0-9a-zA-Z\ ] +", "");
            }
            catch (Exception)
            {
                Console.WriteLine("rm: missing operand\n*Hint: Try: \"rm -f [Filename]\", \"rm -r [Foldername]\", or \"rm -rf [Foldername]\".\n\nBAD:\nrm [Foldername]\nGOOD:\nrm -r [Foldername\n\nBAD:\nrm -f -r [Foldername]\nGOOD: rm -rf [Foldername]");
                LinuxTerminal();
            }
            
            string ObjToRemove = "";
            try
            {
                ObjToRemove = Regex.Replace(CommandName.Split()[2], @"[^0-9a-zA-Z\ ] +", "");
            }
            catch (Exception)
            {
                Console.WriteLine("rm: missing operand\n*Hint: Try: \"rm -f [Filename]\", \"rm -r [Foldername]\", or \"rm -rf [Foldername]\".\n\nBAD:\nrm [Foldername]\nGOOD:\nrm -r [Foldername\n\nBAD:\nrm -f -r [Foldername]\nGOOD: rm -rf [Foldername]");
                LinuxTerminal();
            }
            
            if (arg1 == "-f")
            {
                if (!File.Exists(ObjToRemove))
                {
                    Console.WriteLine("rm: cannot remove \'" + ObjToRemove + "\': No such file or directory");
                    LinuxTerminal();
                } else {
                    try
                    {
                        File.Delete(ObjToRemove);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("rm: cannot remove \'" + ObjToRemove + "\': Unknown exception\n*Hint: Make sure the program has permissions to delete your file, or it is not being in use by another process.");
                        LinuxTerminal();
                    }
                }
            } else if (arg1.Contains("-r"))
            {
                bool deleteRecursively = false;
                if (arg1.Contains("-rf")) deleteRecursively = true;
                
                if (!Directory.Exists(ObjToRemove))
                {
                    Console.WriteLine("rm: cannot remove \'" + ObjToRemove + "\': No such file or directory");
                    LinuxTerminal();
                } else {
                    try
                    {
                        Directory.Delete(@ObjToRemove, deleteRecursively);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("rm: cannot remove \'" + ObjToRemove + "\': Unknown exception\n*Hint: Make sure the program has permissions to delete your file, or it is not being in use by another process.");
                        LinuxTerminal();
                    }
                }
            }
            LinuxTerminal();
        }
        
        if (output == "cd")
        {
            Console.WriteLine("C# won\'t let you to use the cd command :(\n\nAn upcoming version of Bash for Windows may address this problem.");
            LinuxTerminal();
        }
        
        if (output == "cat")
        {
            string Object = "";
            try
            {
                Object = Regex.Replace(CommandName.Split()[1], @"[^0-9a-zA-Z\ ] +", "");
            }
            catch (Exception)
            {
                LinuxTerminal();
            }
            
            string content = "";
            if (!File.Exists(Object))
            {
                Console.WriteLine(Object + " not found.");
                LinuxTerminal();
            }
            try
            {
                content = File.ReadAllText(@Object);
                Console.WriteLine(content);
            }
            catch (Exception)
            {
                Console.WriteLine("Unspecified error.\n\nTIP: Make sure the program has permissions to manipulate your file, or no process is using it.");
                LinuxTerminal();
            }
        }
        
        Console.WriteLine("\n");
        LinuxTerminal();
    }
    
    static void LinuxTerminal()
    {
        Console.Write("$ ");
        string Current = Console.ReadLine();

        RunCommand(Current);
    }
    static void Main()
    {
        Console.WriteLine("Linux Terminal (Bash) for Windows, v1.0\nBy winscripter --> https://www.tiktok.com/@winscripter\n");
        LinuxTerminal();
    }
}
