using System;
using System.Collections.Generic;
using System.Threading;

namespace BarSimHW
{
    class Bar
    {
        List<Students> students = new List<Students>();
        public List<Drinks> drinks = new List<Drinks>();
        Semaphore semaphore = new Semaphore(10, 11);
        Random r = new Random();
        decimal BarRevenue = 0;
        public bool BarIsOpen = true;
        public bool Enter(Students student)
        {
                if(r.Next(0,10)==9 && BarIsOpen == true)
            {
                CloseBar();
            }    

                semaphore.WaitOne();
                lock (students)
                {

                    if (BarIsOpen)
                    {
                        if (student.Age > 18)
                        {
                            students.Add(student);
                            return true;
                        }
                        else
                        {
                            Console.WriteLine($"{student.Name} is a Minor!");
                            return false;
                        }
                    }
                    else return false;
                }         
        }

        public void Leave(Students student)
        {
            lock (students)
            {
                students.Remove(student);
            }
            semaphore.Release();
        }

        public void BuyDrink (Students students, Drinks drinks)
        {
            bool QuantityNot0 = true;
            if (QuantityNot0)
            {
                if (students.Budget > drinks.Price)
                {
                    Console.WriteLine($"{ students.Name} ordered drink {drinks.Name}");
                    students.Budget -= drinks.Price;
                    BarRevenue += (decimal)drinks.Price;
                    drinks.Quantity -= 1;
                    if (drinks.Quantity == 0) QuantityNot0 = false;
                    
                }
                else
                {
                    Console.WriteLine($"{students.Name} tried to order drink {drinks.Name}, but didn't have enough money.");
                }
            }
            else Console.WriteLine($"{drinks.Name} is out of stock!");
        } 

        public void CloseBar()
        {
            lock (students)
            {
                foreach (var stud in students)
                {
                    Console.WriteLine($"{stud.Name} leaves the closing bar to go home.");
                    semaphore.Release();            
                }
                BarIsOpen = false;
                students.Clear();
                Console.WriteLine("Bar is closed!");
            }
        }
        public void SaleReport()
        {
            foreach (var drink in drinks)
            {
                if(drink.Quantity==0)
                    Console.WriteLine($"{drink.Name} is out of stock!");
            }
            
            Console.WriteLine($"Total revenue: $ {BarRevenue}");
        }
    }
}
