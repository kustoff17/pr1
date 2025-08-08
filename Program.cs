using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

// проект практическая работа номер 1
public interface IMcdonalds
{
    void DisplayInfo();

     string[] GetTableData();
   
    void ShowClassName();
}

public abstract class Burger : IMcdonalds
{
    public abstract void DisplayInfo();

    public virtual void ShowClassName()
    {
        Console.WriteLine($"Это класс {GetType().Name} ");
    }

    public float Cost { get; set; } // свойство цена

    public int QuontityC { get; set; } // кол-во котлет

    public int QuontityS { get; set; } // кол-во сыра

    public abstract string[] GetTableData(); // инфа в табличке

    
    ~Burger()
    {
        Console.WriteLine("Бургер успешно удален!");
    }
    


    
}



class BigMac: Burger
{
    private readonly float cost_; // поле со стоимостью (только для чтения)

    public const int MaxCotlets = 3; // поле с максимальным кол-вом котлет


    
    // свойства
   
    public string typeCotlet; //котлета куриная или говяжья

    public string typeCheese; // тип сыра

    public double Weight;

    public bool IsBecon;

    public BigMac() : this( 0, 0, 0,  false, "Неизвестный","Неизвестный", 0) { } // конструктор по умолчанию

    public BigMac(float cost, int quontitys, int quontityc,  bool isbecon, string typecotlet, string typecheese, double weight)
    {
        cost_ = cost;
        QuontityS = quontitys;
        QuontityC = quontityc;
        IsBecon = isbecon;
        typeCotlet = typecotlet;
        typeCheese = typecheese;
        Weight = weight;
    }

    //копирующий конструктор
    public BigMac(BigMac other)
    : this(other.cost_, other.QuontityS, other.QuontityC, other.IsBecon, other.typeCotlet, other.typeCheese, other.Weight)
    { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Бургер стоимостью {cost_}, с {typeCotlet} котлетой и {typeCheese} сыром, кол-во котлет: {QuontityC}, кол-во сыра: {QuontityS}," +
            $"вес: {Weight}");
    }


    public override string[] GetTableData()
    {
        return new string[]
        {
            "BigMac",
            Cost.ToString("0.00"),
            QuontityC.ToString(),
            QuontityS.ToString(),
            IsBecon.ToString(),
            typeCotlet,
            typeCheese,
            Weight.ToString("0.00")
            
        };
    }


    public void Eat()
    {
        if (Weight > 0)
        {
            Console.WriteLine($"Вы съели бургер весом {Weight} г. Было очень вкусно!");
            Weight = 0;
        }
        else
        {
            Console.WriteLine("Бургер уже съеден!");
        }
    }

    public void Sell(int kolvo)
    {
        Console.WriteLine($"Продано {kolvo} бургеров.");
    }

    public void Heat()
    {
        Console.WriteLine("Бургер подогрет!");
    }

    public static void SingSong()
    {
        Console.WriteLine("Две мясных котлеты-гриль, специальный соус, сыр, огурцы, салат и лук, всё на булочке с кунжутом, только так и это - биг-мак");
    }

    // Перегрузка метода ToString

    public override string ToString()
    {
        return $"Бургер стоимостью {cost_}, с {typeCotlet} котлетой и {typeCheese} сыром, кол-во котлет: {QuontityC}, кол-во сыра: {QuontityS},\" +\r\n       " +
            $"     $\"вес: {Weight}";
    }

    // продолжаю с перегрузки арифметических операторов

    public static BigMac operator +(BigMac a, BigMac b)
    {
        return new BigMac(a.Cost + b.Cost, a.QuontityS + b.QuontityS, a.QuontityC + b.QuontityC, a.IsBecon || b.IsBecon, $" Котлеты двух видов: {a.typeCotlet} и {b.typeCotlet}",
            $"Сыр двух видов: {a.typeCheese} и {b.typeCheese}", a.Weight + b.Weight);
    }

    public static bool operator ==(BigMac a, BigMac b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false; 

        return a.Cost == b.Cost  && a.QuontityS == b.QuontityS && a.IsBecon == b.IsBecon 
            && a.typeCotlet == b.typeCotlet && a.typeCheese == b.typeCheese && a.Weight == b.Weight;
    }

    public static bool operator !=(BigMac a, BigMac b)
    {
        return !(a == b);
    }


    public override bool Equals(object obj)
    {
        return obj is BigMac bigmac && this == bigmac; // сравнение с другим бургером
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Cost,  QuontityS, IsBecon, typeCotlet, typeCheese, Weight);
    }
}

public class CheeseBurger: Burger
{
    public int ColCucumber { get; set; } // кол-во маринованных огурцов

    // конструктор с параметрами
    public CheeseBurger(int colCucumber, float cost, int quontityS, int quontityC)
    {
        ColCucumber = colCucumber;
        Cost = cost;
        QuontityS = quontityS; // Кол-во сыра
        QuontityC = quontityC; // кол-во котлет
   
    }

    public static void Reccomend()
    {
        Console.Write("Ешьте целиком, не снимайте обертку и не торопитесь. Приятного аппетита!");
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Чизбургер стоимостью {Cost}, кол-во котлет: {QuontityC}," +
            $"кол-во ломтиков сыра:{QuontityS}, кол-во маринованных огурцов:{ColCucumber},  ");
    }

    public override string[] GetTableData()
    {
        return new string[]
        {
            "CheeseBurger",
            Cost.ToString("0.00"),
            QuontityC.ToString(),
            QuontityS.ToString(),
            ColCucumber.ToString(),
        };
    }



}


public class BigTasty : Burger
{
    public string Sause { get; set; } // соус для бургера

    // конструктор с параметрами
    public BigTasty(string sous, float cost, int quontityS, int quontityC) 
    {
        Sause = sous;
        Cost = cost;
        QuontityS = quontityS; // Кол-во сыра
        QuontityC = quontityC; //кол-во котлет

    }

    public void Calorie(string sous,int quontityC, int qountityS)
    {
        string sous_ = sous.ToLower();
        switch (sous_)
        {
            case "кетчуп":
                float baseCalories = 300;
                float calories = quontityC * 250 + QuontityS * 80 + baseCalories;
                Console.WriteLine($"калорийность бургера bigtasty: {calories}");
                break;

            case "майонез":
                float baseCalories_ = 500;
                float calories_ = quontityC * 250 + QuontityS * 80 + baseCalories_;
                Console.WriteLine($"калорийность бургера bigtasty: {calories_}");
                break;
            default:
                float _baseCalories_ = 400;
                float _calories_ = quontityC * 250 + QuontityS * 80 + _baseCalories_;
                Console.WriteLine($"калорийность бургера bigtasty: {_calories_}");
                break;
        }
    }


    public override void DisplayInfo()
    {
        Console.WriteLine($"Бигтейсти стоимостью {Cost}, кол-во котлет: {QuontityC}," +
            $"кол-во ломтиков сыра: {QuontityS}, соус:{Sause}");
    }

    public override string[] GetTableData()
    {
        return new string[]
        {
            "BigMac",
            Cost.ToString("0.00"),
            QuontityC.ToString(),
            QuontityS.ToString(),
            Sause
        };
    }


}


public sealed class Muffin : Burger
{
    public string Filling{ get; set; } // начинка для макмаффина
    // конструктор с параметрами
    public Muffin(string filling, float cost)
    {
        Filling = filling;
        Cost = cost;

    }

    public void Drink()
    {
        Console.WriteLine("К маффину не помешает купить вкусный кофе!");
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Макмаффин с начинкой: {Filling}, стоимостью {Cost}");
    }

    public override string[] GetTableData()
    {
        return new string[]
        {
            "BigMac",
            Cost.ToString("0.00"),
            Filling
        };
    }


}

class Programm
{
    static List<IMcdonalds> Food = new List<IMcdonalds>(); // список для хранения еды

    static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Задать параметры бургера");
            Console.WriteLine("2. Вывести свойства бургера");
            Console.WriteLine("3. Съесть бургер");
            Console.WriteLine("4. Продать бургер");
            Console.WriteLine("5. Подогреть бургер");
            Console.WriteLine("6. Спеть песенку про бигмак (статический метод)");
            Console.WriteLine("7. Добавить новый класс в список (CheeseBurger, BigTasty, Muffin)");
            Console.WriteLine("8. Сгенерировать элемнты списка");
            Console.WriteLine("9. Вывести все элементы из списка");
            Console.WriteLine("10. Выйти"); 
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine(); //получения выбора пользователя

            switch (choice)
            {
                case "1":
                    Burger mybigmac = CreateBigMac(); // Создание бургера
                    if (mybigmac != null)
                    {
                        Food.Add(mybigmac); // добавление бургера в список
                    }
                    break;
                case "2":
                    DisplayProperties(); // свойств бургера
                    break;

                case "3":
                    EatBigMac(); // метод поедания бигмака
                    break;

                case "4":
                    Console.Write("Сколько бургеров продать?");
                    int kolvo = int.Parse(Console.ReadLine());
                    SellBigMac(kolvo); //продать бигмак
                    break;

                case "5":
                    HeatBigMac(); // подоргеть бигмак
                    break;

                case "6":
                    BigMac.SingSong();
                    break;
                case "7":
                    AddBurgers(); //добавление бургера в список
                    break;
                case "8":
                    GenerateRandomBurgers(); // генератор элементво списка
                    break;
                case "9":
                    DisplayAllBurgers(); //вывод всех бургеров из списка
                    break;
                case "10":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Некорректные данные для ввода. Попробуйте заново!");
                    break;
            }

        }

        static BigMac CreateBigMac()
        {
            Console.Write("Введите вес бургера: ");
            double weight = double.Parse(Console.ReadLine() ?? "0"); //получение веса (присвой считываемое значение, иначе присвой 0)
            Console.Write("Введите стоимость бургера: ");
            float cost = float.Parse(Console.ReadLine() ?? "0"); // стоимость бургера (присвой считываемое значение, иначе присвой 0)
            Console.Write("Введите кол-во сыра в  бургере: ");
            int quontitys = int.Parse(Console.ReadLine() ?? "0"); // кол-во сыра в бургере
            Console.Write("Введите кол-во котлет в бургере: ");
            int quontityс = int.Parse(Console.ReadLine() ?? "0"); // кол-во котлет в бургере
            Console.Write("Введите тип котлеты (говяжья/куриная): ");
            string typecotlet = Console.ReadLine(); // тип котлеты
            Console.Write("Введите тип сыра (голландский, маасдам, российский, камамбер): ");
            string typecheese = Console.ReadLine(); // тип сыра
            Console.Write("Есть ли бекон? (да/нет): ");
            bool isbecon = Console.ReadLine()?.ToLower() == "да"; // проверка на наличие бекона

            return new BigMac(cost, quontitys, quontityс, isbecon, typecotlet, typecheese, weight);

        }

        static void DisplayProperties()
        {
            foreach (var burger in Food)
            {
                if (burger is BigMac bigmac) 
                {
                    bigmac.DisplayInfo(); // Вывод информации о бигмаке
                }
            }
        }
        static void EatBigMac()
        {
            foreach (var burger in Food)
            {
                if (burger is BigMac bigmac) 
                {
                    bigmac.Eat(); 
                    return; 
                }
            }
            Console.WriteLine("Нет бигмаков для поедания."); 
        }

        static void SellBigMac(int kolvo)
        {
            foreach (var burger in Food)
            {
                if (burger is BigMac bigmac)
                {
                    bigmac.Sell(kolvo);
                    return;
                }
            }
            Console.WriteLine("Нет бигмаков для продажи.");
        }

        static void HeatBigMac()
        {
            foreach (var burger in Food)
            {
                if (burger is BigMac bigmac)
                {
                    bigmac.Heat(); 
                }
            }
        }

        static void AddBurgers()
        {
            Console.WriteLine("Выберите тип фастфуда, который хотите добавить: 1) CheeseBurger, 2) BigTasty, 3) McMuffin");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                Console.Write("Введите стоимость чизбургера: ");
                float cost = float.Parse(Console.ReadLine());
                Console.Write("Введите кол-во котлет: ");
                int quontityc=int.Parse(Console.ReadLine());
                Console.Write("Введите кол-во ломтиков сыра: ");
                int quontitys = int.Parse(Console.ReadLine());
                Console.Write("Введите кол-во маринованных огурцов");
                int colcucumber  = int.Parse(Console.ReadLine());

                CheeseBurger cheeseburger = new CheeseBurger(colcucumber, cost, quontitys,quontityc);
                Food.Add(cheeseburger);
                Console.WriteLine("Чизбургер добавлен в список!");

            }
       
            if (choice == 2)
            {
                Console.Write("Введите стоимость бургера BigTasty: ");
                float cost = float.Parse(Console.ReadLine());
                Console.Write("Введите кол-во котлет: ");
                int quontityc = int.Parse(Console.ReadLine());
                Console.Write("Введите кол-во ломтиков сыра: ");
                int quontitys = int.Parse(Console.ReadLine());
                Console.Write("Введите соус: ");
                string sous = Console.ReadLine();
                BigTasty bigtasty = new BigTasty(sous, cost, quontitys, quontityc);
                Food.Add(bigtasty);
                Console.WriteLine("BigTasty добавлен в список!");

            }
            if (choice == 3)
            {
                Console.Write("Введите стоимость маффина: ");
                float cost = float.Parse(Console.ReadLine());
      
                Console.Write("Введите начинку");
                string filling = Console.ReadLine();

                 Muffin muffin = new Muffin( filling, cost);
                Food.Add(muffin);
                Console.WriteLine("Маффин добавлен в список!");

            }


        }

        static void DisplayAllBurgers()
        {
            
            foreach (var burger in Food)
            {
                burger.DisplayInfo();
                burger.ShowClassName();
            }
        }



    static void GenerateRandomBurgers()
        {
            Console.Write("Сколько еды сгенерировать? ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.WriteLine("Некорректное число.");
                return;
            }

            Random rnd = new Random();

            string[] cutletTypes = { "Говяжья", "Куриная" };
            string[] cheeseTypes = { "Чеддер", "Маасдам", "Российский", "Голландский" };
            string[] sauces = { "Кетчуп", "Майонез", "Барбекю" };
            string[] fillings = { "Сыр", "Яичница", "Ветчина", "Шоколад" };

            for (int i = 0; i < count; i++)
            {
                int type = rnd.Next(4);
                switch (type)
                {
                    case 0:
                        Food.Add(new BigMac(
                            cost: (float)(rnd.NextDouble() * 10 + 5),
                            quontitys: rnd.Next(1, 3),
                            quontityc: rnd.Next(1, BigMac.MaxCotlets + 1),
                            isbecon: rnd.Next(2) == 0,
                            typecotlet: cutletTypes[rnd.Next(cutletTypes.Length)],
                            typecheese: cheeseTypes[rnd.Next(cheeseTypes.Length)],
                            weight: rnd.Next(150, 301)
                        ));
                        break;
                    case 1:
                        Food.Add(new CheeseBurger(
                            colCucumber: rnd.Next(1, 4),
                            cost: (float)(rnd.NextDouble() * 6 + 2),
                            quontityS: rnd.Next(1, 3),
                            quontityC: rnd.Next(1, 2)
                        ));
                        break;
                    case 2:
                        Food.Add(new BigTasty(
                            sous: sauces[rnd.Next(sauces.Length)],
                            cost: (float)(rnd.NextDouble() * 9 + 6),
                            quontityS: rnd.Next(1, 3),
                            quontityC: rnd.Next(1, 3)
                        ));
                        break;
                    case 3:
                        Food.Add(new Muffin(
                            filling: fillings[rnd.Next(fillings.Length)],
                            cost: (float)(rnd.NextDouble() * 3 + 1)
                        ));
                        break;
                }
            }

            Console.WriteLine($"{count} случайных элементов добавлены!");
        }


    }
}