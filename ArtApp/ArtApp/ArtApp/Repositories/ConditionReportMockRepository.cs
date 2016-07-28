using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtApp.Model;

namespace ArtApp.Repositories
{
    public class ConditionReportMockRepository : IDisposable
    {
        public async Task<List<ConditionReport>> GetConditionReportsAsync()
        {
            return new List<ConditionReport>()
            {
                new ConditionReport()
                {
                    ConditionReportId = 1,
                    Title = "CR1",
                    Date = DateTime.Now,
                    Lux = 1,
                    RH = 1,
                    Temperature = 23,
                    FrontProtection = Protection.Glass,
                    BackProtection = Protection.Tablex,
                    Handling = Handling.Cotton_gloves,
                    HandlingPosition = HandlingPosition.Horizontal,
                    MadeBy = "Manel",
                    Work = new Work()
                    {
                        Title = "Guernica"
                    },
                    Notes = "None"
                },
                new ConditionReport()
                {
                    ConditionReportId = 1,
                    Title = "CR2",
                    Date = DateTime.Now,
                    Lux = 1,
                    RH = 1,
                    Temperature = 23,
                    FrontProtection = Protection.Glass,
                    BackProtection = Protection.Tablex,
                    Handling = Handling.Cotton_gloves,
                    HandlingPosition = HandlingPosition.Horizontal,
                    MadeBy = "Manel",
                    Work = new Work()
                    {
                        Title = "Lilly Pads"
                    },
                    Notes = "None"
                },
                new ConditionReport()
                {
                    ConditionReportId = 1,
                    Title = "CR3",
                    Date = DateTime.Now,
                    Lux = 1,
                    RH = 1,
                    Temperature = 23,
                    FrontProtection = Protection.Glass,
                    BackProtection = Protection.Tablex,
                    Handling = Handling.Cotton_gloves,
                    HandlingPosition = HandlingPosition.Horizontal,
                    MadeBy = "Manel",
                    Work = new Work()
                    {
                        Title = "Mona"
                    },
                    Notes = "None"
                },
            };
        }

        public async Task<ConditionReport> GetConditionReportAsync(int id)
        {
            ConditionReport conditionReport = new ConditionReport()
            {
                ConditionReportId = 1,
                Title = "CR1",
                Date = DateTime.Now,
                Lux = 1,
                RH = 1,
                Temperature = 23,
                FrontProtection = Protection.Glass,
                BackProtection = Protection.Tablex,
                Handling = Handling.Cotton_gloves,
                HandlingPosition = HandlingPosition.Horizontal,
                MadeBy = "Manel",
                Work = new Work()
                {
                    Title = "Guernica"
                },
                Notes = "None"
            };

            return conditionReport;
        }

        public async Task<ConditionReport> PostConditionReportAsync(ConditionReport conditionReport)
        {
            if (conditionReport != null)
            {
                return new ConditionReport()
                {
                    ConditionReportId = 1,
                    Title = "CR1",
                    Date = DateTime.Now,
                    Lux = 1,
                    RH = 1,
                    Temperature = 23,
                    FrontProtection = Protection.Glass,
                    BackProtection = Protection.Tablex,
                    Handling = Handling.Cotton_gloves,
                    HandlingPosition = HandlingPosition.Horizontal,
                    MadeBy = "Manel",
                    Work = new Work()
                    {
                        Title = "Guernica"
                    },
                    Notes = "None"
                };
            }
            return null;

        }

        public async Task<ConditionReport> PutConditionReportAsync(string id, ConditionReport conditionReport)
        {
            if (id.Equals("1"))
            {
                return new ConditionReport()
                {
                    ConditionReportId = 1,
                    Title = "CR1",
                    Date = DateTime.Now,
                    Lux = 1,
                    RH = 1,
                    Temperature = 23,
                    FrontProtection = Protection.Glass,
                    BackProtection = Protection.Tablex,
                    Handling = Handling.Cotton_gloves,
                    HandlingPosition = HandlingPosition.Horizontal,
                    MadeBy = "Manel",
                    Work = new Work()
                    {
                        Title = "Guernica"
                    },
                    Notes = "None"
                };
            }
            return null;

        }

        public async Task DeleteConditionReportAsync(string id)
        {
        }

        public void Dispose()
        {
        }
    }
}
