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

            package.Save();
        }
    }
}
