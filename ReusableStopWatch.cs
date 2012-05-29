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
		
		const int Totals = 60;
		public long[] PreviousTotals = new long[Totals];
		
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
			long total = _stopWatch.ElapsedTicks;
			for (int i = 0; i < Totals - 1; i++)
				PreviousTotals[i] = PreviousTotals[i + 1];
			PreviousTotals[Totals - 1] = total;
			
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

