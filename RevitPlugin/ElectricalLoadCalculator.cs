using System;
using System.Collections.Generic;
using System.Linq;
using RevitPlugin.Core;

#if WINDOWS
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endif

namespace RevitPlugin
{
#if WINDOWS
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ElectricalLoadCalculator : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            try
            {
                var panels = CollectPanels(doc);
                
                if (panels.Count == 0)
                {
                    TaskDialog.Show("Информация", "Электрические щиты не найдены!");
                    return Result.Cancelled;
                }

                var panelData = new List<(string Name, double Power)>();
                foreach (var panel in panels)
                {
                    double power = GetInstalledPower(panel);
                    panelData.Add((panel.Name, power));
                }

                double demandFactor = 0.7;
                var calculator = new ElectricalCalculator();
                var results = calculator.CalculateLoads(panelData, demandFactor);

                using (Transaction trans = new Transaction(doc, "Расчет нагрузки"))
                {
                    trans.Start();

                    foreach (var result in results)
                    {
                        TaskDialog.Show("Результат", 
                            $"Щит: {result.Name}\n" +
                            $"Pуст: {result.InstalledPower:F2} Вт\n" +
                            $"Pр: {result.CalculatedPower:F2} Вт\n" +
                            $"Кс: {result.DemandFactor:F2}");
                    }

                    trans.Commit();
                }

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }

        private List<FamilyInstance> CollectPanels(Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var allEquipment = collector
                .OfClass(typeof(FamilyInstance))
                .OfCategory(BuiltInCategory.OST_ElectricalEquipment)
                .Cast<FamilyInstance>()
                .ToList();

            var panels = new List<FamilyInstance>();
            foreach (var inst in allEquipment)
            {
                string familyName = inst.Symbol.Family.Name;
                if (familyName.Contains("Щит") || 
                    familyName.Contains("Panel") || 
                    familyName.Contains("Distribution"))
                {
                    panels.Add(inst);
                }
            }

            return panels;
        }

        private double GetInstalledPower(FamilyInstance element)
        {
            // Revit 2026 - пробуем разные варианты параметров
            Parameter powerParam = null;
            
            // Список возможных имен параметров
            string[] paramNames = new string[]
            {
                "Electrical Load",
                "Power",
                "Apparent Load",
                "Nominal Power",
                "RBS_ELEC_APPARENT_LOAD",
                "РБС_ЭЛЕКТРИЧЕСКАЯ_НАГРУЗКА"
            };
            
            foreach (string name in paramNames)
            {
                powerParam = element.LookupParameter(name);
                if (powerParam != null && powerParam.HasValue)
                    break;
            }
            
            // Если не нашли, пробуем BuiltInParameter
            if (powerParam == null || !powerParam.HasValue)
            {
                powerParam = element.get_Parameter(BuiltInParameter.RBS_ELEC_APPARENT_LOAD);
            }
            
            if (powerParam == null || !powerParam.HasValue)
                return 0;

            double power = powerParam.AsDouble();
            if (power > 0 && power < 1) 
                power *= 1000;
            
            return power;
        }
    }
#else
    public class ElectricalLoadCalculator
    {
        public void Test()
        {
            Console.WriteLine("🧪 Тестирование логики на macOS");
            
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
            foreach (var result in results)
            {
                Console.WriteLine($"{result.Name}: Pуст={result.InstalledPower:F2} Вт, Pр={result.CalculatedPower:F2} Вт, Кс={result.DemandFactor:F2}");
            }
        }
    }
#endif
}
