using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace PssSpritePerformanceTests
{
	public class ReusableStopWatch
	{
		public List<long> Readings = new List<long>(20);
		Stopwatch _stopWatch = new Stopwatch();
		long _lastReading;
		
		public ReusableStopWatch ()
		{
		}
		
		public void Start()
		{
			Readings.Clear();
			_stopWatch.Start();
			_lastReading = 0;
		}
		
		public void Stop()
		{
			_stopWatch.Stop();
			_stopWatch.Reset();
		}
		
		public void TakeReading()
		{
			long reading = _stopWatch.ElapsedTicks;
			Readings.Add(reading - _lastReading);
			_lastReading = reading;
		}
	}
}

