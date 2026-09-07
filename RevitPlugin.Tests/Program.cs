using System;
using System.Collections.Generic;
using RevitPlugin.Core;

namespace RevitPlugin.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🧪 Тестирование Core логики на macOS\n");

            var panels = new List<(string Name, double Power)>
            {
                ("Щит №1", 100.0),
                ("Щит №2", 250.5),
                ("Щит №3", 75.2)
            };

            double demandFactor = 0.7;
            var calculator = new ElectricalCalculator();
            var results = calculator.CalculateLoads(panels, demandFactor);

            Console.WriteLine("📊 Результаты расчета:");
            Console.WriteLine("----------------------------------------");
            
            double totalInstalled = 0;
            double totalCalculated = 0;
            
            foreach (var result in results)
            {
                Console.WriteLine($"{result.Name}:");
                Console.WriteLine($"  Pуст = {result.InstalledPower:F2} Вт");
                Console.WriteLine($"  Pр   = {result.CalculatedPower:F2} Вт");
                Console.WriteLine($"  Кс   = {result.DemandFactor:F2}");
                Console.WriteLine();
                
                totalInstalled += result.InstalledPower;
                totalCalculated += result.CalculatedPower;
            }

            // Группировка
            var grouped = calculator.GroupLoadsByCategory(results);
            Console.WriteLine("📋 Группировка по категориям:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Освещение:    {grouped.lighting:F2} Вт ({grouped.lighting/1000:F2} кВт)");
            Console.WriteLine($"Розетки:      {grouped.power:F2} Вт ({grouped.power/1000:F2} кВт)");
            Console.WriteLine($"Оборудование: {grouped.mechanical:F2} Вт ({grouped.mechanical/1000:F2} кВт)");
            Console.WriteLine($"Прочее:       {grouped.other:F2} Вт ({grouped.other/1000:F2} кВт)");
            
            Console.WriteLine();
            Console.WriteLine("📈 Итоги:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Суммарная установленная мощность: {totalInstalled:F2} Вт ({totalInstalled/1000:F2} кВт)");
            Console.WriteLine($"Суммарная расчетная мощность:     {totalCalculated:F2} Вт ({totalCalculated/1000:F2} кВт)");
            Console.WriteLine($"Средний коэффициент спроса:       {totalCalculated/totalInstalled:F2}");

            Console.WriteLine("\n✅ Тестирование завершено!");
        }
    }
}
