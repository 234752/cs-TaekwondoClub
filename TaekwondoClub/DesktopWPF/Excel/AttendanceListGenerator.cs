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
    public List<Attendance> Attendances { get; set; }
    public AttendanceListGenerator(ExcelWorksheet worksheet, List<Event> events, List<Attendance> attendances)
    {
        ExcelWorksheet = worksheet;
        Events = events;
        Attendances = attendances;
    }
    public void GenerateAttendanceList()
    {
        var customers = Attendances.Select(a => a.Customer).Distinct().ToList();
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
        for (int i = 0; i < dates.Count(); i++) 
        {
            ExcelWorksheet.Cells[2, i + 4].Value = dates[i].ToString("dd/MM/yyyy");
        }
        var attendanceTable = new List<List<string>>();
        for (int i = 0; i < customers.Count(); i++)
        {
            var customer = customers[i];
            var row = new List<string>();

            row.Add((i +1).ToString());
            row.Add(customer.Name);
            row.Add(customer.Surname);
            for (int date = 0; date < dates.Count(); date++)
            {
                
                var ev = Events.Where(e => e.Date == dates[date]).First();
                var isPresent = Attendances.Where(a => a.EventId == ev.Id && a.CustomerId == customer.Id).FirstOrDefault();
                if(isPresent != null)
                {
                    row.Add("-");
                }
                else
                {
                    row.Add(" ");
                }
            }
            attendanceTable.Add(row);
        }
        for (int i = 0; i < attendanceTable.Count(); i++)
        {
            for (int j = 0; j < attendanceTable[i].Count(); j++)
            {
                ExcelWorksheet.Cells[i + 3, j + 1].Value = attendanceTable[i][j];

            }
        }
    }
}
