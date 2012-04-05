using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Reactive.Linq
{
#if REACTIVE_2_0
	public static partial class Observable
	{
		public static IObservable<TSource> Repeat<TSource> (this IObservable<TSource> source, int repeatCount)
		{
			if (source == null)
				throw new ArgumentException ("source");
			if (repeatCount < 0)
				throw new ArgumentException ("repeatCount");

			return RepeatInfinitely (source).Take (repeatCount).Concat ();
		}
	}
#endif
}
