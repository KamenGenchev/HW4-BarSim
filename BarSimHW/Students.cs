using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarSimHW
{
    class Students
    {
        enum NightlifeActivities { Walk, VisitBar, GoHome};
        enum BarActivities { Drink, Dance, Leave};

        Random random = new Random();
        

        public string Name { get; set; }
        public Bar Bar { get; set; }
        public int Age { get; set; }
        public float Budget { get; set; }


        private NightlifeActivities GetRandomNightlifeActivity()
        {
            int n = random.Next(10);
            if (n < 3) return NightlifeActivities.Walk;
            if (n < 8) return NightlifeActivities.VisitBar;
            return NightlifeActivities.GoHome;
        }
        
        private BarActivities GetRandomBarActivity()
        {
            int n = random.Next(10);
            if (n < 4) return BarActivities.Dance;
            if (n < 9) return BarActivities.Drink;
            return BarActivities.Leave;
        }

        public void WalkOut()
        {
            Console.WriteLine($"{Name} is walking in the streets.");
            Thread.Sleep(100);
        }

        private void VisitBar()
        {
            if (Bar.BarIsOpen)
            {
                Console.WriteLine($"{Name} is getting in line to enter the bar.");
                
                if (Bar.Enter(this))
                {
                    Console.WriteLine($"{Name} entered the bar!");
                    bool staysAtBar = true;
                    while (staysAtBar)
                    {
                        var NextActivity = GetRandomBarActivity();
                        if (Bar.BarIsOpen == false)
                            NextActivity = BarActivities.Leave;

                        switch (NextActivity)
                        {
                            case BarActivities.Dance:
                                Console.WriteLine($"{Name} dances.");
                                Thread.Sleep(100);
                                break;
                            case BarActivities.Drink:
                                Bar.BuyDrink(this, Bar.drinks[random.Next(Bar.drinks.Count)]);
                                Thread.Sleep(100);
                                break;
                            case BarActivities.Leave:
                                Console.WriteLine($"{Name} leaves the bar.");
                                Bar.Leave(this);
                                staysAtBar = false;
                                break;
                            default: throw new NotImplementedException();
                        }
                    }
                }
            }
            else PaintTheTownRed();
        }

        public void PaintTheTownRed()
        {
            WalkOut();
            bool staysOut = true;
            while (staysOut)
            {
                var NextActivity = GetRandomNightlifeActivity();
                switch (NextActivity)
                {
                    case NightlifeActivities.Walk:
                        WalkOut();
                        break;
                    case NightlifeActivities.VisitBar:
                        VisitBar();
                        staysOut = false;
                        break;
                    case NightlifeActivities.GoHome:
                        staysOut = false;
                        break;
                    default: throw new NotImplementedException();
                }
            }
            Console.WriteLine($"{Name} is going back home.");
        }

        public Students(string name, Bar bar, int age, float budget)
        {
            Name = name;
            Bar = bar;
            Age = age;
            Budget = budget;
        }
    }
}
