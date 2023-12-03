using DB.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWPF.Excel;

internal class ExpensesSummaryGenerator
{
    public ExcelWorksheet ExcelWorksheet { get; set; }
    public List<Payment> Payments { get; set; }
    public ExpensesSummaryGenerator(ExcelWorksheet worksheet, List<Payment> payments)
    {
        ExcelWorksheet = worksheet;
        Payments = payments;
    }
    public void GenerateExpensesSummary()
    {
        ExcelWorksheet.Cells[1, 1].Value = "No.";
        ExcelWorksheet.Cells[1, 2].Value = "Type of Costs";
        ExcelWorksheet.Cells[1, 3].Value = "Number of units";
        ExcelWorksheet.Cells[1, 4].Value = "Unit cost";
        ExcelWorksheet.Cells[1, 5].Value = "Total cost";
        ExcelWorksheet.Cells[1, 6].Value = "Units paid";
        ExcelWorksheet.Cells[1, 7].Value = "Total paid";


        decimal totalCost = 0.0M;
        decimal totalPaid = 0.0M;
        for (int i = 0; i < Payments.Count; i++)
        {
            var cost = Payments[i].Amount * Payments[i].Price;
            ExcelWorksheet.Cells[i + 2, 1].Value = i + 1;
            ExcelWorksheet.Cells[i + 2, 2].Value = Payments[i].Name;
            ExcelWorksheet.Cells[i + 2, 3].Value = Payments[i].Amount;
            ExcelWorksheet.Cells[i + 2, 4].Value = Payments[i].Price;
            ExcelWorksheet.Cells[i + 2, 5].Value = cost;
            totalCost += cost;
            if (int.TryParse(Payments[i].Paid, out int units))
            {
                var paid = units * Payments[i].Price;
                ExcelWorksheet.Cells[i + 2, 6].Value = units;
                ExcelWorksheet.Cells[i + 2, 7].Value = paid;
                totalPaid += paid;
            }
            else
            {
                ExcelWorksheet.Cells[i + 2, 6].Value = 0;
                ExcelWorksheet.Cells[i + 2, 7].Value = 0;
            }
        }
        ExcelWorksheet.Cells[Payments.Count + 2, 1].Value = "Total:";
        ExcelWorksheet.Cells[Payments.Count + 2, 1, Payments.Count + 2, 4].Merge = true;
        ExcelWorksheet.Cells[Payments.Count + 2, 5].Value = totalCost;
        ExcelWorksheet.Cells[Payments.Count + 2, 7].Value = totalPaid;
    }
}
