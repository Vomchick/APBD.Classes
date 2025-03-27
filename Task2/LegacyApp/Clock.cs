using System;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class Clock : IClock
    {
        private readonly DateTime? _now;

        public Clock(DateTime? now = null)
        {
            _now = now;
        }

        public DateTime Now()
        {
            return _now ?? DateTime.Now;
        }
    }
}
