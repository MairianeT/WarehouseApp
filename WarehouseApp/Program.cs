using WarehouseApp.Entities;

namespace WarehouseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pallet> pallets = new List<Pallet>();
            Console.WriteLine("Выберите способ получения данных:");
            Console.WriteLine("1. Генерация данных");
            Console.WriteLine("2. Пользовательский ввод");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine() ?? throw new InvalidOperationException();
            
            switch(choice)
            {
                case "1": 
                    pallets = GenerateData();
                    break;
                case "2":
                    Console.WriteLine("Введите данные паллеты:");
                    Console.Write("Ширина: ");
                    int palletWidth = int.Parse(Console.ReadLine());
                    Console.Write("Высота: ");
                    int palletHeight = int.Parse(Console.ReadLine());
                    Console.Write("Глубина: ");
                    int palletDepth = int.Parse(Console.ReadLine());

                    var pallet = new Pallet( palletWidth, palletHeight, palletDepth);
                    pallets.Add(pallet);

                    Console.WriteLine("Добавлена паллета с ID: " + pallet.Id);
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
            
            bool addMore = true;
            while (addMore)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить коробку");
                Console.WriteLine("2. Добавить паллету");
                Console.WriteLine("3. Завершить добавление");
                Console.Write("Ваш выбор: ");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        Console.WriteLine("Введите данные коробки:");
                        Console.Write("Ширина: ");
                        int boxWidth = int.Parse(Console.ReadLine());
                        Console.Write("Высота: ");
                        int boxHeight = int.Parse(Console.ReadLine());
                        Console.Write("Глубина: ");
                        int boxDepth = int.Parse(Console.ReadLine());
                        Console.Write("Вес: ");
                        int boxWeight = int.Parse(Console.ReadLine());
                        Console.Write("Дата производства (дд.мм.гггг): ");
                        DateTime productionDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);
                        
                        Console.WriteLine("Список Id доступных паллет:");
                        for (int i = 0; i < pallets.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}). {pallets[i].Id}");
                        }
                        
                        Console.Write("Введите номер паллеты, в которую добавить коробку: ");
                        int selectedPalletNumber = int.Parse(Console.ReadLine());

                        if (selectedPalletNumber >= 1 && selectedPalletNumber <= pallets.Count)
                        {
                            var selectedPallet = pallets[selectedPalletNumber - 1];
                            var box = new Box( boxWidth, boxHeight, boxDepth, boxWeight, productionDate);
                            try
                            {
                                selectedPallet.AddBox(box);
                                Console.WriteLine("Коробка добавлена в паллету.");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine("Ошибка: " + ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверный номер паллеты.");
                        }

                        break;

                    case "2":
                        Console.WriteLine("Введите данные паллеты:");
                        Console.Write("Ширина: ");
                        int newPalletWidth = int.Parse(Console.ReadLine());
                        Console.Write("Высота: ");
                        int newPalletHeight = int.Parse(Console.ReadLine());
                        Console.Write("Глубина: ");
                        int newPalletDepth = int.Parse(Console.ReadLine());

                        var newPallet = new Pallet( newPalletWidth, newPalletHeight, newPalletDepth);
                        pallets.Add(newPallet);

                        Console.WriteLine("Добавлена новая паллета с ID: " + newPallet.Id);
                        break;

                    case "3":
                        addMore = false;
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
            
            // Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу.
            var groupedPallets = pallets.GroupBy(p => p.GetExpiryDate())
                .OrderBy(g => g.Key);
            Console.WriteLine("Сгруппированные паллеты по сроку годности:");
            foreach (var group in groupedPallets)
            {
                Console.WriteLine($"Срок годности: {group.Key}");
                var sortedPallets = group.OrderBy(p => p.Weight);

                foreach (var pallet in sortedPallets)
                {
                    Console.WriteLine($"  Паллета ID: {pallet.Id}, Вес: {pallet.Weight} кг");
                }
            }
            Console.WriteLine();
            
            // 3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.
            var topPallets = pallets.OrderByDescending(p => p.GetMaxBoxExpiryDate())
                .Take(3)
                .OrderBy(p => p.GetTotalVolume());

            Console.WriteLine("Топ 3 паллеты, которые содержат коробки с наибольшим сроком годности:");

            foreach (var pallet in topPallets)
            {
                Console.WriteLine($"Паллета ID: {pallet.Id}, Срок годности коробки: {pallet.GetMaxBoxExpiryDate()}, Объем: {pallet.GetTotalVolume()}");
            }

            Console.ReadLine();
        }
        
        // Генерация в приложении
        static List<Pallet> GenerateData()
        {
            var pallets = new List<Pallet>();

            // Паллета 1
            var box1 = new Box( 5, 5, 5, 45, new DateTime(2023, 1, 1));
            var box2 = new Box(4, 4, 4, 20, new DateTime(2023, 1, 5));
            var pallet1 = new Pallet(100, 100, 100);
            pallet1.AddBox(box1);
            pallet1.AddBox(box2);
            pallets.Add(pallet1);

            // Паллета 2
            var box3 = new Box(3, 3, 3, 17, new DateTime(2023, 2, 1));
            var pallet2 = new Pallet(90, 90, 90);
            pallet2.AddBox(box3);
            pallets.Add(pallet2);

            // Паллета 3
            var box4 = new Box(2, 2, 2, 8, new DateTime(2023, 2, 1));
            var pallet3 = new Pallet(80, 80, 80);
            pallet3.AddBox(box4);
            pallets.Add(pallet3);

            // Паллета 4
            var box5 = new Box(1, 1, 1, 7, new DateTime(2023, 4, 1));
            var box6 = new Box( 1, 1, 1, 5, new DateTime(2023, 4, 1));
            var box7 = new Box( 10, 10, 15, 50, new DateTime(2023, 4, 5));
            var box8 = new Box( 20, 15, 15, 75, new DateTime(2023, 4, 5));
            var pallet4 = new Pallet(70, 70, 70);
            pallet4.AddBox(box5);
            pallet4.AddBox(box6);
            pallet4.AddBox(box7);
            pallet4.AddBox(box8);
            pallets.Add(pallet4);

            return pallets;
        }
    }
}