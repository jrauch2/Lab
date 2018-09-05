using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GPAWithIO
{
    class Program
    {
        static void Main(string[] args)
        {
            int classCount = 0;
            double totalGrade = 0.0;

            string filePath = "gradeData.txt";
            int selection = 0;

            do
            {
                Console.WriteLine("1) Read file and display grades.");
                Console.WriteLine("2) Add grades to file.");
                Console.WriteLine("3) Exit");
                try
                {
                    selection = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                

                if (selection == 1)
                {
                    if (File.Exists(filePath))
                    {
                        StreamReader sr = null;
                        try
                        {
                            sr = new StreamReader(filePath);

                            // Read lines from file until no more lines to read
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();

                                // Math|B
                                string classname = line.Substring(0, line.IndexOf("|"));
                                string letterGrade = line.Substring(line.IndexOf("|") + 1);
                                switch (letterGrade.ToUpper())
                                {
                                    case "A":
                                        totalGrade += 4;
                                        break;
                                    case "B":
                                        totalGrade += 3;
                                        break;
                                    case "C":
                                        totalGrade += 2;
                                        break;
                                    case "D":
                                        totalGrade++;
                                        break;
                                    case "F":
                                    default:
                                        Console.WriteLine("No points added.");
                                        break;
                                }

                                classCount++;

                                Console.WriteLine("{0} - {1}", classname, letterGrade);
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Source);
                            Console.WriteLine(e.Message);
                        }
                        finally
                        {
                            if (sr != null)
                                sr.Close();
                        }
                        
                        Console.WriteLine("{0} - {1}", totalGrade, classCount);
                        if (classCount > 0)
                            Console.WriteLine("Your GPA is: {0}", totalGrade / classCount);
                        else
                            Console.WriteLine("Your GPA is: 0");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("File does not exist");
                    }
                }
                else if (selection == 2)
                {
                    StreamWriter DrewsFileStorage = new StreamWriter(filePath);
                    string moreClasses = "";
                    do
                    {
                        // Get class name
                        Console.WriteLine("Enter the class name");
                        string className = Console.ReadLine();
                        Console.WriteLine("Enter the letter grade");
                        string letterGrade = Console.ReadLine().ToUpper();

                        DrewsFileStorage.WriteLine("{0}|{1}", className, letterGrade);

                        Console.WriteLine("Enter more classes");
                        moreClasses = Console.ReadLine();
                        moreClasses = moreClasses.ToUpper();

                    } while (moreClasses == "Y");
                    DrewsFileStorage.Close();
                }
                else
                {
                    Console.WriteLine("Exiting...");
                }
            } while (selection != 3);
            Console.ReadLine();
        }
    }
}
