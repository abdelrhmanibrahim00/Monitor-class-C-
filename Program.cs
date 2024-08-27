using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace L1_conccurent_programming
{

    class Program
    {
       


        
        public static void write2Monitor(string[] Array, Dmonitor d)

        { 
                if (Array.Length != 0)
                {

                    foreach (string s in Array)
                    {
                        string[] lines = s.Split(',');
                        string name = lines[0];
                        int number = int.Parse(lines[1]);
                        double fees = double.Parse(lines[2]);
                        field r = new field(name, number, fees);
                        d.AddItem(r);
                    }

                }

        }

        static string[] ReadDataFromFile(string filePath)
        {
            try
            {
                // Read all lines from the file and store them in a string array
                string[] lines = File.ReadAllLines(filePath);


                return lines;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new string[0]; // Return an empty array in case of an error
            }
        }

        static void WriteStudentTableToFile(Rmonitor students, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(" Number | Name                        | Credits    | GPA   ");

                    for (int i = 0; i < students.GetSize(); i++)
                    {
                        var studentData = students.Get(i).ToString();
                        var columns = studentData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Ensure there are at least three elements (first name, last name, GPA)
                        if (columns.Length >= 3)
                        {
                            string numberColumn = $"{i + 1,-6}|";
                            string firstName = columns[0];
                            string lastName = columns[1];
                            string fullName = $"{firstName} {lastName}";
                            string creditsColumn = $"{columns[2],-12}|";
                            string gpaColumn = $"{columns[3],-7}";

                            writer.WriteLine($"{numberColumn} {fullName,-30} {creditsColumn} {gpaColumn}");
                        }
                        else
                        {
                            // Handle incomplete data (at least three elements are expected)
                            Console.WriteLine($"Incomplete data for student: {studentData}");
                        }
                    }
                }

                Console.WriteLine($"Data has been written to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }






        private static void conditon(Dmonitor d, Rmonitor r)
        {
            try
            {
                while (true)
                {
                    field x= d.Removeitem();

                    if (x == null)
                    {
                        break;
                    }

                   Boolean computed = ResultComputation(x);
                    if (computed)
                    {
                        r.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
            }
        }


        private static Boolean ResultComputation(field student)
        {
           

            if (student.GetGpa() > 6 && student.GetNumber() >200)
            {
                return true;
            } 
            else
            {
                return false;
            }
            
        }



        static void Main(string[] args)
        {
           // Console.Write("Enter the name of the text file (including extension, e.g., myfile.txt): ");
            string filePath = "f3.txt";

            string[] dataArray = ReadDataFromFile(filePath);
            int Dmonitorsize = (int)Math.Round(dataArray.Length * 0.5);
            int max = 300;
            Dmonitor d = new Dmonitor();
            Rmonitor r = new Rmonitor(max);
            Console.WriteLine(Dmonitorsize + "Here is the size ");

            Thread[] threads = new Thread[4];
            
            for (int i = 0; i < 4; i++)
            {
                threads[i] = new Thread(() => conditon(d, r));
                threads[i].Start();
                Console.WriteLine("thread number : {0}", i);
            }

            write2Monitor(dataArray, d);

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine("====================================================");
            Console.WriteLine("Size of the result monitor is :{0} ",r.GetSize());
            WriteStudentTableToFile(r, "output.txt");









 







            Console.ReadLine();



        }
    }


    
}
