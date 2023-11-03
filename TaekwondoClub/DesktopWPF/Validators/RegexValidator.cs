using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DesktopWPF.Validators;

internal static class RegexValidator
{
    private const string NumericPattern = @"^\d+$";
    private const string MonthYearPattern = @"^(0[1-9]|1[0-2])/\d{4}$";

    private static readonly Regex _numericRegex = new Regex(NumericPattern, RegexOptions.Compiled);
    private static readonly Regex _monthYearRegex = new Regex(MonthYearPattern, RegexOptions.Compiled);

    public static bool IsNumeric(string text)
    {
        return _numericRegex.IsMatch(text);
    }

    public static bool IsMonthYearPattern(string text)
    {
        return _monthYearRegex.IsMatch(text);
    }
}
