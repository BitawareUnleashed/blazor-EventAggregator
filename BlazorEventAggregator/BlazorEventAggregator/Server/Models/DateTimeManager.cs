using BlazorEventAggregator.Models;
using BlazorEventAggregator.Shared.Models.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorEventAggregator.Models;

public class DateTimeManager
{
    private readonly IEventAggregator eventAggregator;

    #region FILEDS
    private DateTime StartingDateSeconds = DateTime.Now;
    #endregion

    private System.Timers.Timer? aTimer;

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemWatch"/> class.
    /// </summary>
    /// <param name="interval">The interval in milliseconds.</param>
    public DateTimeManager(IEventAggregator eventAggregator)
    {
        int interval = 1000;
        // Create a timer and set a two second interval.
        if (aTimer == null)
        {
            aTimer = new System.Timers.Timer();
            aTimer.Interval = interval;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed -= ATimer_Elapsed!;
            aTimer.Elapsed += ATimer_Elapsed!;

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;
        }

        this.eventAggregator = eventAggregator;
        
    }

    private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        eventAggregator.Publish(new TimeValueChanged(e.SignalTime.ToLongDateString() + " - " + e.SignalTime.Second.ToString("00")));
    }

    #region IDisposable
    // To detect redundant calls
    private bool disposed = false;
    
    protected virtual void Dispose(bool disposing)
    {
        // If already disposed then exit
        if (disposed) { return; }
        // Dispose managed state (managed objects).
        if (disposing) { aTimer!.Dispose(); }
        disposed = true;
    }

    public void Dispose()
    {
        // Dispose of unmanaged resources.
        Dispose(true);
        // Suppress finalization.
        GC.SuppressFinalize(this);
    }
    #endregion
}
