using System;
using System.Collections.Generic;
using System.Threading;

namespace BarSimHW
{
    class Program
    {
        private static DateTime timeThreadStarted;
      //  public static List<string> elapsedThreadTime = new List<string>();
        static void Main(string[] args)
        {
            Bar bar = new Bar();
            Random random = new Random();
            for (int i = 1; i <= 20; i++)
            {
                int rP = random.Next(300, 5000);
                float price = (float)rP / 100;
                bar.drinks.Add(new Drinks(i.ToString(), price, random.Next(1, 10)));
            }
            
            List<Thread> studentThreads = new List<Thread>(); 
            for (int i = 1; i<100; i++)
            {
                int age = random.Next(10, 50);
                int rB = random.Next(1000, 15000);
                float budget = (float)rB / 100; 

                var students = new Students(i.ToString(), bar, age, budget);
                var thread = new Thread(students.PaintTheTownRed);
                thread.Start();
                studentThreads.Add(thread);
              //  timeThreadStarted = DateTime.Now;
               // elapsedThreadTime.Add(new string(timeThreadStarted.ToString()));
            }

            foreach (var t in studentThreads) t.Join();
            Console.WriteLine();
            Console.WriteLine("The party is over.");
            bar.CloseBar();
            Console.WriteLine();
            bar.SaleReport();
            Console.ReadLine();
        }
    }
}
