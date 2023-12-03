using DB.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWPF.Excel;

internal class AttendanceListGenerator
{
    public ExcelWorksheet ExcelWorksheet { get; set; }
    public List<Event> Events { get; set; }
    public AttendanceListGenerator(ExcelWorksheet worksheet, List<Event> events)
    {
        ExcelWorksheet = worksheet;
        Events = events;
    }
    public void GenerateAttendanceList()
    {
        var customers = Events.Select(e => e.Attendances.Select(a => a.Customer)).ToList();
        var months = Events.Select(e => e.Date.Month).Distinct().ToList();
        var dates = Events.Select(e => e.Date).Distinct().ToList();

        ExcelWorksheet.Cells[2, 1].Value = "No.";
        ExcelWorksheet.Cells[2, 2].Value = "Name";
        ExcelWorksheet.Cells[2, 3].Value = "Surname";
        var datesUntilNow = 0;
        for (int i = 0; i < months.Count(); i++)
        {
            var datesThisMonth = dates.Where(d => d.Month == months[i]);
            var monthName = new DateTime(2023, months[i], 1).ToString("MMMM");
            ExcelWorksheet.Cells[1, 4 + datesUntilNow].Value = monthName;
            ExcelWorksheet.Cells[1, 4 + datesUntilNow, 1, 4 + datesUntilNow + datesThisMonth.Count() - 1].Merge = true;
            datesUntilNow += datesThisMonth.Count();
        }
        for(int i = 0; i < dates.Count(); i++) 
        {
            ExcelWorksheet.Cells[2, i + 4].Value = dates[i].ToString("dd/MM/yyyy");
        }

    }
}
