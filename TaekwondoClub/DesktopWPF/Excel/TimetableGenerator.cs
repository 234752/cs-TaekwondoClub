using DB.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public void Generate()
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

    }
}
