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
    public ObservableCollection<Event> Reminders { get; set; }
    private RestService _restService { get; set; }
    public MainViewModel(RestService restService)
    {
        _restService = restService;
        Customers = new ObservableCollection<Customer>(restService.Customers);
        Events = new ObservableCollection<Event>(restService.Events);
        Payments = new ObservableCollection<Payment>(restService.Payments);
        Costs = new ObservableCollection<Payment>(restService.Payments);
        Reminders = new ObservableCollection<Event>(restService.Events.Where(p => p.Type == "Reminder" && p.Date >= DateTime.Now).OrderBy(r => r.Date));
        StartDate = DateTime.Now;
        EndDate = StartDate.AddDays(30);
        EndMonthYear = DateTime.Now;
        StartMonthYear = EndMonthYear.AddMonths(-1);
        FilterCollections();
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime StartMonthYear { get; set; }
    public DateTime EndMonthYear { get; set; }
    public bool UnpaidOnly { get; set; }

    #region data filters
    public void FilterCollections()
    {
        FilterEvents();
        FilterPayments();
        FilterCosts();
    }
    private void FilterEvents()
    {
        var allEvents = _restService.Events.Where(e => e.Type == "One Time");
        Events.Clear();
        foreach (var e in allEvents.Where(e => e.Date >= StartDate && e.Date <= EndDate))
        {
            Events.Add(e);
        }
    }
    private void FilterPayments()
    {
        var allPayments = _restService.Payments.Where(p => p.Customer != null);
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
        foreach (var p in allPayments.Where(p => IsInRange(p.Type) && (!UnpaidOnly || p.Paid=="no")))
        {
            Payments.Add(p);
        }
    }
    private void FilterCosts()
    {
        var allCosts = _restService.Payments.Where(p => p.Customer == null);
        Costs.Clear();
        foreach (var p in allCosts.Where(p => p.DueDate >= StartDate && p.DueDate <= EndDate))
        {
            Costs.Add(p);
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
