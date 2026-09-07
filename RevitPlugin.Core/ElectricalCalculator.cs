using System.Collections.Generic;

namespace RevitPlugin.Core
{
    /// <summary>
    /// Данные о нагрузке
    /// </summary>
    public class LoadData
    {
        public string Name { get; set; }
        public double InstalledPower { get; set; }
        public double CalculatedPower { get; set; }
        public double DemandFactor { get; set; }
    }

    /// <summary>
    /// Калькулятор электрической нагрузки (не зависит от Revit API)
    /// </summary>
    public class ElectricalCalculator
    {
        /// <summary>
        /// Расчет нагрузки по формуле Pр = Pуст * Кс
        /// </summary>
        public List<LoadData> CalculateLoads(
            List<(string Name, double Power)> panels, 
            double demandFactor)
        {
            var results = new List<LoadData>();

            foreach (var panel in panels)
            {
                results.Add(new LoadData
                {
                    Name = panel.Name,
                    InstalledPower = panel.Power,
                    CalculatedPower = panel.Power * demandFactor,
                    DemandFactor = demandFactor
                });
            }

            return results;
        }

        /// <summary>
        /// Группировка нагрузок по категориям
        /// </summary>
        public (double lighting, double power, double mechanical, double other) 
            GroupLoadsByCategory(List<LoadData> loads)
        {
            double total = 0;
            foreach (var load in loads)
            {
                total += load.CalculatedPower;
            }
            
            // Для демонстрации распределяем по категориям
            return (
                lighting: total * 0.3,
                power: total * 0.4,
                mechanical: total * 0.2,
                other: total * 0.1
            );
        }
    }
}
