using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Concurrency.Tests
{
	[TestFixture]
	public class TaskPoolSchedulerTest
	{
		[Test]
		public void Cancellation ()
		{
			bool raised = false;
			var dis = Scheduler.TaskPool.Schedule<object> (null, TimeSpan.FromMilliseconds (100), (sch, stat) => raised = true);
			Assert.IsFalse (raised, "#1");
			dis.Dispose (); // immediately, to not raise event.
			Thread.Sleep (200);
			Assert.IsFalse (raised, "#2");
		}
		
		[Test]
		public void Order ()
		{
			// It is time-dependent test (i.e. lengthy and inconsistent), which is not very good but we cannot use HistoricalScheduler to test it...
			var s = Scheduler.TaskPool;
			var l = new List<int> ();
			var dis = new CompositeDisposable ();
			try {
				// If the tasks do not run long enough, the (Task) scheduler will not use separate Threads, 
				// therefore Thread.Sleep(x) will block not only current task, but also tasks scheduled to run 
				// in the same Thread
				dis.Add (s.Schedule (() => { Thread.Sleep (2400); l.Add (1); }));
				dis.Add (s.Schedule (() => { Thread.Sleep (1600); l.Add (2); }));
				dis.Add (s.Schedule (() => { Thread.Sleep (50); l.Add (3); }));
				Thread.Sleep (2500);
				Assert.AreEqual (new int [] {3, 2, 1}, l.ToArray (), "#1");
			} finally {
				dis.Dispose ();
			}
		}
	}
}
