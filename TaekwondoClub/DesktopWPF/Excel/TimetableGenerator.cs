using DB.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace DesktopWPF.Excel;

class TimetableGenerator
{
    public ExcelWorksheet ExcelWorksheet { get; set; }
    public List<Event> Events { get; set; }
    public TimetableGenerator(ExcelWorksheet worksheet, List<Event> events) 
    {
        ExcelWorksheet = worksheet;
        Events = events;
    }
    public void GenerateWeeklyTimetable()
    {
        ExcelWorksheet.Cells[1, 1].Value = "No."; ExcelWorksheet.Cells[1, 1, 2, 1].Merge = true;
        ExcelWorksheet.Cells[1, 2].Value = "Hours"; ExcelWorksheet.Cells[1, 2, 2, 2].Merge = true;
        ExcelWorksheet.Cells[1, 3].Value = "Subjects"; ExcelWorksheet.Cells[1, 3, 1, 9].Merge = true;
        ExcelWorksheet.Cells[2, 3].Value = "Monday";
        ExcelWorksheet.Cells[2, 4].Value = "Tuesday";
        ExcelWorksheet.Cells[2, 5].Value = "Wednesday";
        ExcelWorksheet.Cells[2, 6].Value = "Thursday";
        ExcelWorksheet.Cells[2, 7].Value = "Friday";
        ExcelWorksheet.Cells[2, 8].Value = "Saturday";
        ExcelWorksheet.Cells[2, 9].Value = "Sunday";

        var timeList = Events.Select(e => e.Date.ToString("HH:mm")).Distinct().OrderBy(t => t).ToList();
        var timetable = CreateTimetable(Events, timeList);
        for(int hour = 0; hour < timeList.Count; hour++)
        {
            ExcelWorksheet.Cells[hour + 3, 1].Value = hour + 1;
            for (int day = 0; day <= 7; day++)
            {
                ExcelWorksheet.Cells[hour + 3, day + 2].Value = timetable[hour][day];
            }
        }

    }
    List<List<string>> CreateTimetable(List<Event> events, List<string> timeList)
    {
        List<List<string>> eventTable = new List<List<string>>();

        foreach (var time in timeList)
        {
            var row = new List<string> { time };
            row.AddRange(Enumerable.Repeat(string.Empty, 7));

            foreach (var dayOfWeek in events.Select(e => (int)e.Date.DayOfWeek).Distinct())
            {
                var eventAtTimeAndDay = events.FirstOrDefault(e => e.Date.ToString("HH:mm") == time && (int)e.Date.DayOfWeek == dayOfWeek);

                if (eventAtTimeAndDay != null)
                {
                    if(dayOfWeek == 0) row[7] = eventAtTimeAndDay.Name;
                    else row[dayOfWeek] = eventAtTimeAndDay.Name;
                }
            }

            eventTable.Add(row);
        }
        return eventTable;
    }

    public void GenerateListOfClasses()
    {
        ExcelWorksheet.Cells[1, 1].Value = "No.";
        ExcelWorksheet.Cells[1, 2].Value = "Date";
        ExcelWorksheet.Cells[1, 3].Value = "Classes content";
        ExcelWorksheet.Cells[1, 4].Value = "Number of people";
        ExcelWorksheet.Cells[1, 5].Value = "Instructor signature";

        for (int i = 0; i < Events.Count; i++)
        {
            ExcelWorksheet.Cells[i + 2, 1].Value = i + 1;
            ExcelWorksheet.Cells[i + 2, 2].Value = Events[i].Date.ToString("dd/MM/yyyy");
            ExcelWorksheet.Cells[i + 2, 3].Value = Events[i].Name;
            ExcelWorksheet.Cells[i + 2, 4].Value = Events[i].Attendances.Count;
        }
    }
}
