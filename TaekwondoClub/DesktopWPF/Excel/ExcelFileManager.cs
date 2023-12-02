using DB.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWPF.Excel;

internal static class ExcelFileManager
{
    internal static void WriteMessage(string folderPath, string filename)
    {
        string fullPath = System.IO.Path.Combine(folderPath, filename);
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage($"{fullPath}.xlsx"))
        {
            var sheet = package.Workbook.Worksheets.Add("My Sheet");
            sheet.Cells["A1"].Value = "hello";
            sheet.Cells[1, 1, 1, 2].Merge = true;

            package.Save();
        }
    }
    internal static void GenerateTimetable(string filepath, List<Event> events)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage($"{filepath}.xlsx"))
        {
            var sheet = package.Workbook.Worksheets.Add("Weekly Timetable");
            var generator = new TimetableGenerator(sheet, events);
            generator.Generate();

            package.Save();
        }
    }
}
