using System;
using System.Diagnostics;

namespace DataAccessLayer
{
    /// <summary>
    /// Bruges til at få DateTime.Now og som StopUr
    /// Men dette gør recorder testbar
    /// </summary>
    public interface ITimeProvider : IStopWatch
    {
        DateTime GetDateTime();
    }

    /// <summary>
    /// Bruges til stopur
    /// Gør at recorder er testbar
    /// </summary>
    public interface IStopWatch
    {

        void Start();
        void Stop();
        void Reset();

        /// <summary>
        /// Printer total elapsed time measured by the current instance i Debug.WriteLine
        /// </summary>
        void PrintElapsed();
        /// <summary>
        /// Denne metode starter uret på ny og udskriver at den er started i Debug.WriteLine
        /// </summary>
        void StartTimer();
        /// <summary>
        /// Dette er en kombi, da den både kan stoppe, Printe i Debug.WriteLine og Resette stopuret
        /// </summary>
        /// <param name="print">True, hvis det skal printes til Debug.WriteLine</param>
        /// <param name="reset">True, hvis uret ska resettes til 0</param>
        void StopTimer(bool print, bool reset);
    }

    public class RealTimeProvider : Stopwatch, ITimeProvider
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public void PrintElapsed()
        {
            TimeSpan ts = Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.WriteLine("RunTime " + elapsedTime);
        }

        public void StopTimer(bool print, bool reset)
        {
            Stop();
            if (print)
            {
                PrintElapsed();
            }
            if (reset)
            {
                Reset();

            }
        }

        public void StartTimer()
        {
            Start();
            Debug.WriteLine("Måling er startet");
        }
    }
}
