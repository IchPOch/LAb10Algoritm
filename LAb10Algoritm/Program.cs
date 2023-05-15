using BackpackTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackpackTask
{
    class Rock
    {

        public double weigth { get; set; }


        public Rock( double _weigth)
        {
            weigth = _weigth;
        }

    }
    class Backpack
    {
        private List<Rock> bestItems = null;

        private double maxW;

        private double bestWeigth;

        public Backpack(double _maxW)
        {
            maxW = _maxW;
        }
        //вычисляет общий вес набора предметов
        public double CalcWeigth(List<Rock> items)
        {
            double sumW = 0;

            foreach (Rock i in items)
            {
                sumW += i.weigth;
            }

            return sumW;
        }
        public void MaxW(List<Rock> items)
        {
            maxW = CalcWeigth(items) / 2;
        }

        //проверка, является ли данный набор лучшим решением задачи
        private void CheckSet(List<Rock> items)
        {
            if (bestItems == null)
            {
                if (CalcWeigth(items) <= maxW)
                {
                    bestItems = items;
                    bestWeigth = CalcWeigth(items);
                }
            }
            else
            {
                if (CalcWeigth(items) <= maxW && CalcWeigth(items) > bestWeigth)
                {
                    if (Math.Abs(maxW - CalcWeigth(items)) < Math.Abs(maxW - bestWeigth))
                    {
                        bestItems = items;
                        bestWeigth = CalcWeigth(items);
                    }
                }
            }
        }
        //создание всех наборов перестановок значений
        public void MakeAllSets(List<Rock> items)
        {
            if (items.Count > 0)
                CheckSet(items);

            for (int i = 0; i < items.Count; i++)
            {
                List<Rock> newSet = new List<Rock>(items);

                newSet.RemoveAt(i);

                MakeAllSets(newSet);
            }

        }
        //возвращает решение задачи (набор предметов)
        public List<Rock> GetBestSet()
        {
            return bestItems;
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        List<Rock> items;
        items = new List<Rock>();
        int l;
        while (true)
        {
            Console.WriteLine("Нажмите 1 что бы добавить камень и введите его вес, решение задачи нажмите 2");
            l = Convert.ToInt16(Console.ReadLine());
            if(l == 1)
            {
                items.Add(new Rock(Convert.ToDouble(Console.ReadLine())));
            }
            if (l == 2) break;
            //items.Add(new Rock("1", 5));
            //items.Add(new Rock(5));
            //items.Add(new Rock(8));
            //items.Add(new Rock(13));
            //items.Add(new Rock(27));
            //items.Add(new Rock(14));
        }
        Backpack bp = new Backpack(0);
        bp.MaxW(items);
        bp.MakeAllSets(items);
        List<Rock> solve = bp.GetBestSet();
        Console.WriteLine("Weigth obschii = " + bp.CalcWeigth(items));
        Console.WriteLine("Weigth best = " + bp.CalcWeigth(solve));
        for(int i = 0; i < items.Count; i++)
        {
            for (int j = 0; j < solve.Count; j++)
            {
                if (items[i] == solve[j])
                {
                    items.Remove(solve[j]);
                }
            }
        }
        Console.WriteLine("Weigth drugoi polovini =  " + bp.CalcWeigth(items));
        double resh = Math.Abs(bp.CalcWeigth(solve) - bp.CalcWeigth(items));
        Console.WriteLine("Raznica = " + resh);
    }
}