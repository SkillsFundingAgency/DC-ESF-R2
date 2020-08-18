using System.Collections.Generic;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;

namespace ESFA.DC.ESF.R2.ReportingService.Tests.FundingSummary
{
    public abstract class AbstractFundingSummaryReportTests
    {
        public IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> EsfValuesDictionary()
        {
            return new Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>
            {
                {
                    2020,  new Dictionary<string, IEnumerable<PeriodisedValue>>
                    {
                        {
                            "ST01", new List<PeriodisedValue>
                            {
                                new PeriodisedValue("ConRef1", "ST01", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
                            }
                        }
                    }
                },
                {
                    2018,  new Dictionary<string, IEnumerable<PeriodisedValue>>
                    {
                        {
                            "ST01", new List<PeriodisedValue>
                            {
                                new PeriodisedValue("ConRef1", "ST01", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
                            }
                        }
                    }
                }
            };
        }

        public IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> IlrValuesDictionary()
        {
            return new Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>
            {
                {
                    2020,  new Dictionary<string, IEnumerable<PeriodisedValue>>
                    {
                        {
                            "ST01", new List<PeriodisedValue>
                            {
                                new PeriodisedValue("ConRef1", "ST01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                                new PeriodisedValue("ConRef1", "ST01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                            }
                        }
                    }
                },
                {
                    2019,  new Dictionary<string, IEnumerable<PeriodisedValue>>
                    {
                        {
                            "ST01", new List<PeriodisedValue>
                            {
                                new PeriodisedValue("ConRef1", "ST01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                                new PeriodisedValue("ConRef1", "ST01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                            }
                        }
                    }
                }
            };
        }

        public PeriodisedReportValue ZeroFundedPeriodisedValues(string title) => new PeriodisedReportValue(title, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m);

        public string[] HeaderStringArray() => new string[]
       {
            "August 2020",
            "September 2020",
            "October 2020",
            "November 2020",
            "December 2020",
            "January 2021",
            "February 2021",
            "March 2021",
            "April 2021",
            "May 2021",
            "June 2021",
            "July 2021"
       };

        public GroupHeader Year2020GroupHeader(string title) => new GroupHeader(
            title,
            "August 2020",
            "September 2020",
            "October 2020",
            "November 2020",
            "December 2020",
            "January 2021",
            "February 2021",
            "March 2021",
            "April 2021",
            "May 2021",
            "June 2021",
            "July 2021");

        public GroupHeader Year2019GroupHeader(string title) => new GroupHeader(
            title,
            "August 2019",
            "September 2019",
            "October 2019",
            "November 2019",
            "December 2019",
            "January 2020",
            "February 2020",
            "March 2020",
            "April 2020",
            "May 2020",
            "June 2020",
            "July 2020");

        public GroupHeader Year2018GroupHeader(string title) => new GroupHeader(
            title,
            "August 2018",
            "September 2018",
            "October 2018",
            "November 2018",
            "December 2018",
            "January 2019",
            "February 2019",
            "March 2019",
            "April 2019",
            "May 2019",
            "June 2019",
            "July 2019");

        public IDictionary<int, string[]> HeaderDictionary()
        {
            return new Dictionary<int, string[]>
            {
                {
                    2020, new string[]
                    {
                        "August 2020",
                        "September 2020",
                        "October 2020",
                        "November 2020",
                        "December 2020",
                        "January 2021",
                        "February 2021",
                        "March 2021",
                        "April 2021",
                        "May 2021",
                        "June 2021",
                        "July 2021"
                    }
                },
                {
                    2019, new string[]
                    {
                        "August 2019",
                        "September 2019",
                        "October 2019",
                        "November 2019",
                        "December 2019",
                        "January 2020",
                        "February 2020",
                        "March 2020",
                        "April 2020",
                        "May 2020",
                        "June 2020",
                        "July 2020"
                    }
                },
                {
                    2018, new string[]
                    {
                        "August 2018",
                        "September 2018",
                        "October 2018",
                        "November 2018",
                        "December 2018",
                        "January 2019",
                        "February 2019",
                        "March 2019",
                        "April 2019",
                        "May 2019",
                        "June 2019",
                        "July 2019"
                    }
                }
            };
        }
    }
}
