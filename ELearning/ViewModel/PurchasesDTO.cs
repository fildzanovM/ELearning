using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.ViewModel
{

    //Post new Purchase Data Transfer Object
    public class PurchasesDTO
    {
        public int CourseId { get; set; }
        public DateTime  PurchaseDate { get; set; }
    }

    //"billaccess": [
    //{
    //  "active": 5172,
    //  "nonsolr": 0,
    //  "weekdate": "11/02/2019",
    //  "weekstart": "2019-02-11T00:00:00+00:00"
    //},
    public class Weekly_Purchases_ByCategory
    {
        public List<string> CategoryName { get; set; }
    }

    public class Weekly_Purchases
    {
        public DateTime WeekDate { get; set; }
        public DateTime WeekStart { get; set; }
        public Weekly_Purchases_ByCategory Categories { get; set; }
        public int CourseId { get; set; }
    }

    public class Purchases_by_Weeks
    {
        public ICollection<Weekly_Purchases> WeeklyPurchases { get; set; }
          //   = new List<Weekly_Purchases>();
    }

    //Get All Purchases Data Transfer Object
    public class GetAllPurchases
    {
        public int PurchaseId { get; set; }
        public string CourseName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string CategoryName { get; set; }
    }

    //Sorting Purchases Data Transfer Object
    public class Sorted_Purchases_DTO
    {
        public string PurchaseId { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    //Filter DTO
    public class FilterDTO
    {
        public int Page_index { get; set; }
        public int Page_size { get; set; }
        public string Sort_column { get; set; }
        public string Sort_direction { get; set; }
        public DateTime? Start_date { get; set; }
        public DateTime? End_date { get; set; }
    }

    public class Purchase_Filter : FilterDTO
    {
        public string  PurchaseId { get; set; }
        public string CourseName { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int CourseId { get; set; }
        public DateTime PurchaseDate { get; set; }

    }

    public class PagedList<T>
    {
        /// <summary>
        /// Gets or sets one page od Items.
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets total item count.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets the total page count
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }


    public class Week
    {
        public DateTime Start { get; set; }                                 // Start date of the week
        public DateTime End { get; set; }                                   // End date of the week
        public int Number { get; set; }                                     // Number of week
        public int Days { get { return End.Subtract(Start).Days + 1; } }    // How many days does the week have
        public bool IsOdd { get { return Days < 7; } }                   // A week is odd if it has less than 7 days
        public int Year { get { return Start.Year; } }                      // Which year does this week belong to

        public Week(DateTime start, DateTime end, int weekNumber)
        {
            this.Start = new DateTime(start.Year, start.Month, start.Day);
            this.End = new DateTime(end.Year, end.Month, end.Day);
            this.Number = weekNumber;
        }


        public bool Contains(DateTime date)
        {
            DateTime tmp = new DateTime(date.Year, date.Month, date.Day);
            return Start <= tmp && tmp <= End;
        }


        public static List<Week> GetAllWeeks(
            DateTime start,
            DateTime end,
            CalendarWeekRule rule,
            DayOfWeek firstDayOfWeek,
            GregorianCalendarTypes calendarType)
        {
            GregorianCalendar calendar = new GregorianCalendar(calendarType);
            List<Week> result = new List<Week>();
            DateTime currentDate = start;
            // Find start date of current week of year
            int currentWeekNumber = calendar.GetWeekOfYear(currentDate, rule, firstDayOfWeek);
            do
            {
                currentDate = currentDate.AddDays(-1);
            } while (currentWeekNumber == calendar.GetWeekOfYear(currentDate, rule, firstDayOfWeek));
            currentDate = currentDate.AddDays(1);
            Week currentWeek = null;
            DateTime startDateOfWeek = currentDate;
            int nextWeekNumber = currentWeekNumber;
            while (currentDate < end)
            {
                currentDate = currentDate.AddDays(1);
                if (currentWeekNumber != calendar.GetWeekOfYear(currentDate, rule, firstDayOfWeek))
                {
                    currentWeek = new Week(startDateOfWeek, currentDate.AddDays(-1), currentWeekNumber);
                    result.Add(currentWeek);
                    currentWeekNumber = calendar.GetWeekOfYear(currentDate, rule, firstDayOfWeek);
                    startDateOfWeek = currentDate;
                }
            }
            // We need to add another week if none are in the result or if we have a startDateOfWeek
            // different from our last result
            if (result.Count == 0 || startDateOfWeek != result.Last().Start)
            {
                // Now lets seek to the end of the week
                while (currentWeekNumber == calendar.GetWeekOfYear(currentDate, rule, firstDayOfWeek))
                {
                    currentDate = currentDate.AddDays(1);
                }
                result.Add(new Week(startDateOfWeek, currentDate.AddDays(-1), currentWeekNumber));
            }
            return result;
        }
    }

}
