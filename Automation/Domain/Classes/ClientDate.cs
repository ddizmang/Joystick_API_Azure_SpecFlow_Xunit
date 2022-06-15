using System;
using System.Collections.Generic;
using System.Linq;
using Automation.Domain.Enums;
using Epoch.net;

namespace Automation.Domain.Classes
{
    public static class ClientDate
    {
        #region Properties
        public static ClientTimeZone ClientTimeZone { get; set; }
        #endregion

        #region Methods 
        //TODO: Need to refactor/fix
        private static DateTime GetDateTime()
        {
            //var timeZone = ClientTimeZone.GetDescription();
            //var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            //return TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            return DateTime.Now;
        }
        public static string GetDate(string expectedDateFormat)
        {
            var convertedDateTime = ClientDate.GetDateTime();
            var formattedDate = convertedDateTime.ToString(expectedDateFormat);
            return formattedDate;
        }
        public static string GetTime(string expectedTimeFormat)
        {
            var convertedDateTime = ClientDate.GetDateTime();
            var formattedTime = convertedDateTime.ToString(expectedTimeFormat);
            return formattedTime;

        }
        public static string GetTomorrowDate(string expectedDateFormat)
        {
            return GetDateTime().AddDays(1).ToString(expectedDateFormat);
        }
        public static string GetDayAfterTomorrowDate(string expectedDateFormat)
        {
            return GetDateTime().AddDays(2).ToString(expectedDateFormat);
        }
        public static string GetYesterdayDate(string expectedDateFormat)
        {
            return GetDateTime().AddDays(-1).ToString(expectedDateFormat);
        }
        public static string GetDayBeforeYesterdayDate(string expectedDateFormat)
        {
            return GetDateTime().AddDays(-2).ToString(expectedDateFormat);
        }
        public static string GetFutureDate(string expectedDateFormat, int daysToBeAdded)
        {
            return GetDateTime().AddDays(daysToBeAdded).ToString(expectedDateFormat);
        }
        public static string GetPastDate(string expectedDateFormat, int daysToBeNegotiated)
        {
            return GetDateTime().AddDays(-daysToBeNegotiated).ToString(expectedDateFormat);
        }
        public static string GetTimeOneHourPost(string expectedTimeFormat)
        {
            return GetDateTime().AddHours(1).ToString(expectedTimeFormat);
        }
        public static string GetTimeOneHourPrior(string expectedTimeFormat)
        {
            return GetDateTime().AddHours(-1).ToString(expectedTimeFormat);
        }
        public static string ConvertTodayToDateString(string todayString)
        {
            return todayString.Replace("Today", GetDate("M/dd/yyyy")).ToString();
        }

        public static void GetMonthInformation(int month, int year, out DateTime firstDayOfMonth, out DateTime lastDayOfMonth)
        {
            DateTime first;
            DateTime last;

            first = new DateTime(year, month, 1);
            last = first.AddMonths(1).AddSeconds(-1);

            firstDayOfMonth = first;
            lastDayOfMonth = last;
        }


        public static DateTime CreateUTCEventDateTime()
        {
            return TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
        }

        public static string GetUnixEpochDate(DateTime date)
        {
            return date.ToLongEpochTime().Epoch.ToString();
        }

        public static string GetUnixEpochDateForToday()
        {
            TimeSpan t = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1));
            string millisecondsSinceEpoch = t.Ticks.ToString();
            return millisecondsSinceEpoch;
        }

        public static string GetUnixEpochDateForFiveMinsAgo()
        {
            //TimeSpan t = (DateTime.Now.AddMinutes(-5).ToUniversalTime() - new DateTime(1970, 1, 1));
            //string millisecondsSinceEpoch = t.Ticks.ToString();
            //return millisecondsSinceEpoch;

            return DateTime.Now.AddMinutes(-5).ToUniversalTime().ToEpochTime().ToString();
        }
        public static string GetUnixEpochDateForFiveMinsFromNow()
        {
            return DateTime.Now.AddMinutes(5).ToUniversalTime().ToEpochTime().ToString();
        }
        #endregion
    }
}
