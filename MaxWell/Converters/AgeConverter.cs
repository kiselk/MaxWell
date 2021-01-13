using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MaxWell.Converters
{
 
        class AgeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string result="";
            //value is set to null
                try
                {
                    if (value == null)
                        result = "Im Null";
                    else
                    {
                        DateTime due = (DateTime) value;

                        TimeSpan ts = DateTime.Now.Subtract(due);
                        if (ts.Milliseconds > 0)
                        {
                            int NumberOfDays = (int) ts.TotalDays;
                            int NumberOfMonths = (int) (Math.Floor((ts.TotalDays / 31)));
                            int NumberOfYears = (int) (Math.Floor(ts.TotalDays / 365));
                            if (NumberOfYears > 0)
                            {
                                result += NumberOfYears + " лет";
                                if (NumberOfMonths > 0) result += " " + (NumberOfMonths - NumberOfYears * 12) + " месяцев";
                            }
                            else
                            {
                                if (NumberOfMonths > 0)
                                {
                                    result += "" + NumberOfMonths + " месяцев";

                                    if (NumberOfDays > 0) result += " " + (NumberOfDays - NumberOfMonths * 30) + " дней";
                                }
                                else
                                {
                                    if (ts.Hours> 0)
                                    result = ts.Hours + " часов";
                                    else result = ts.Minutes + " минут";
                            }
                            }


                        }
                    }

                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                return result;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DateTime.Now;
                //  throw new NotImplementedException();
            }
        }
 
}
