using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using NUnit.Framework;

namespace System.Reactive.Linq.Tests
{
#if REACTIVE_2_0
	[TestFixture]
	public class ObservableRx2Tests
	{
		[Test]
		public void RepeatExtensionMethodWithCount ()
		{
			var list = new List<int> ();
			Observable.Range (1, 3).Repeat (2).Subscribe (list.Add);
			Assert.AreEqual (new[] { 1, 2, 3, 1, 2, 3 }, list.ToArray ());
		}
	}
#endif
}
