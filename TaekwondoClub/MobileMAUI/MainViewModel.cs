using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Entities;

namespace MobileMAUI;

public class MainViewModel
{
    public ObservableCollection<Customer> Customers { get; set; }
    public ObservableCollection<Event> Events { get; set; }
    public ObservableCollection<Payment> Payments { get; set; }
    public ObservableCollection<Payment> Costs { get; set; }
    public ObservableCollection<Payment> Reminders { get; set; }
    private RestService _restService { get; set; }
    public MainViewModel(RestService restService)
    {
        _restService = restService;
        Customers = new ObservableCollection<Customer>(restService.Customers);
        Events = new ObservableCollection<Event>(restService.Events.Where(e => e.Type == "One Time"));
        Payments = new ObservableCollection<Payment>(restService.Payments.Where(p => p.Customer != null));
        Costs = new ObservableCollection<Payment>(restService.Payments.Where(p => p.Customer == null));
        Reminders = new ObservableCollection<Payment>(restService.Payments.Where(p => p.Type == "Reminder"));
        StartDate = DateTime.Now;
        EndDate = StartDate.AddDays(30);
        EndMonthYear = DateTime.Now;
        StartMonthYear = EndMonthYear.AddMonths(-1);
        FilterEvents();
        FilterPayments();
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime StartMonthYear { get; set; }
    public DateTime EndMonthYear { get; set; }
    public bool UnpaidOnly { get; set; }

    #region data filters
    public void FilterEvents()
    {
        Events.Clear();
        foreach (var e in _restService.Events.Where(e => e.Date >= StartDate && e.Date <= EndDate))
        {
            Events.Add(e);
        }
    }

    public void FilterPayments()
    {
        int MonthYearToMonth(string monthYear)
        {
            var month = monthYear.Substring(0, 2);
            return int.Parse(month);
        }
        int MonthYearToYear(string monthYear)
        {
            var year = monthYear.Substring(3, 4);
            return int.Parse(year);
        }
        bool IsInRange(string monthYear)
        {
            try
            {
                if (MonthYearToYear(monthYear) < StartMonthYear.Year || MonthYearToYear(monthYear) > EndMonthYear.Year) return false;
                if (MonthYearToYear(monthYear) == StartMonthYear.Year && MonthYearToMonth(monthYear) < StartMonthYear.Month) return false;
                if (MonthYearToYear(monthYear) == EndMonthYear.Year && MonthYearToMonth(monthYear) > EndMonthYear.Month) return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        Payments.Clear();
        foreach (var p in _restService.Payments.Where(p => IsInRange(p.Type) && (!UnpaidOnly || p.Paid=="no")))
        {
            Payments.Add(p);
        }
    }
    #endregion

    #region email notifications

    public async Task SendEmailToDuePayments()
    {
        await _restService.SendEmailToDuePayments(Payments.ToList());
    }
    public async Task SendEmailToEventParticipants()
    {
        await _restService.SendEmailToEventParticipants(Events.ToList());
    }


    #endregion
}
