﻿using DB.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWPF.Excel;

internal static class ExcelFileManager
{
    internal static void GenerateTimetable(string filepath, List<Event> events)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage($"{filepath}.xlsx"))
        {
            var timetableSheet = package.Workbook.Worksheets.Add("Weekly Timetable");
            var generator = new TimetableGenerator(timetableSheet, events);
            generator.GenerateWeeklyTimetable();

            var classesListSheet = package.Workbook.Worksheets.Add("Classes List");
            generator.ExcelWorksheet = classesListSheet;
            generator.GenerateListOfClasses();

            package.Save();
        }
    }
    internal static void GenerateAttendanceList(string filepath, List<Event> events)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage($"{filepath}.xlsx"))
        {
            var attendanceSheet = package.Workbook.Worksheets.Add("Attendance List");
            var generator = new AttendanceListGenerator(attendanceSheet, events);
            generator.GenerateAttendanceList();

            package.Save();
        }
    }
}
