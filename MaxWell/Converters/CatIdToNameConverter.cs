using MaxWell.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace MaxWell.Converters
{


        public class CatIdToNameConverter : IValueConverter
        {





            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {

                string result = "";
                //value is set to null
                if (value == null)
                    result = "Не найдено";
                else
                {
                    var task = Task.Run(() => ((GetName((int)value))));
                    return new TaskCompletionNotifier<string>(task);

            }


                return result;
            }
            private async Task<string> GetName(int id)
            {
                    try
                {

                    Cat cat = await App.Database2.GetItemAsync(id);

                    if (cat!=null)
                        return cat.Text;
                    else
                    {
                        return "Не найдено";
                    }
                }
                catch (Exception e)
                {
                    return null;
                }


            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return null;
            }


         
        }
        public sealed class TaskCompletionNotifier<TResult> : INotifyPropertyChanged
        {
            public TaskCompletionNotifier(Task<TResult> task)
            {
                Task = task;
                if (!task.IsCompleted)
                {
                    var scheduler = (SynchronizationContext.Current == null) ? TaskScheduler.Current : TaskScheduler.FromCurrentSynchronizationContext();
                    task.ContinueWith(t =>
                    {
                        var propertyChanged = PropertyChanged;
                        if (propertyChanged != null)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
                            if (t.IsCanceled)
                            {
                                propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
                            }
                            else if (t.IsFaulted)
                            {
                                propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
                                propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
                            }
                            else
                            {
                                propertyChanged(this, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
                                propertyChanged(this, new PropertyChangedEventArgs("Result"));
                            }
                        }
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously,
                    scheduler);
                }
            }

            // Gets the task being watched. This property never changes and is never <c>null</c>.
            public Task<TResult> Task { get; private set; }



            // Gets the result of the task. Returns the default value of TResult if the task has not completed successfully.
            public TResult Result { get { return (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult); } }

            // Gets whether the task has completed.
            public bool IsCompleted { get { return Task.IsCompleted; } }

            // Gets whether the task has completed successfully.
            public bool IsSuccessfullyCompleted { get { return Task.Status == TaskStatus.RanToCompletion; } }

            // Gets whether the task has been canceled.
            public bool IsCanceled { get { return Task.IsCanceled; } }

            // Gets whether the task has faulted.
            public bool IsFaulted { get { return Task.IsFaulted; } }


            public event PropertyChangedEventHandler PropertyChanged;
        }
    }




