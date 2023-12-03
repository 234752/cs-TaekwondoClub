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
    public void GeneratePlannedExpensesSummary()
    {
        ExcelWorksheet.Cells[1, 2].Value = "Type of Costs";
        ExcelWorksheet.Cells[1, 3].Value = "Number of units";
        ExcelWorksheet.Cells[1, 4].Value = "Unit cost";
        ExcelWorksheet.Cells[1, 5].Value = "Total cost";

        decimal totalCost = 0.0M;
        for (int i = 0; i < Payments.Count; i++)
        {
            var cost = Payments[i].Amount * Payments[i].Price;
            ExcelWorksheet.Cells[i + 2, 1].Value = i + 1;
            ExcelWorksheet.Cells[i + 2, 2].Value = Payments[i].Name;
            ExcelWorksheet.Cells[i + 2, 3].Value = Payments[i].Amount;
            ExcelWorksheet.Cells[i + 2, 4].Value = Payments[i].Price;
            ExcelWorksheet.Cells[i + 2, 5].Value = cost;
            totalCost += cost;
        }
        ExcelWorksheet.Cells[Payments.Count + 2, 1].Value = "Total:";
        ExcelWorksheet.Cells[Payments.Count + 2, 1, Payments.Count + 2, 4].Merge = true;
        ExcelWorksheet.Cells[Payments.Count + 2, 5].Value = totalCost;
    }
}
