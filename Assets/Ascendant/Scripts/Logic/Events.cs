using System;
using System.Collections.Generic;

namespace Ascendant.Scripts.Logic {
	// TODO: Maybe actually bring back interface/abstract class for "data" types to send to handlers
	// Although maybe events shouldn't need data to be passed?
	public delegate void EventDelegate(object data);
	public delegate void UnregisterListenerDelegate();

	/// <summary>
	/// Register events to listen to.
	/// You MUST unregister a listener or there WILL be memory leaks and/or broken code
	/// You can do so by calling the function returned by the <c>Listen</c> method or by calling
	/// <c>Unregister</c> with the key and method you used. Doing the latter will mean you'll have to
	/// not use lambdas.
	/// </summary>
	public static class Events {
		private static Dictionary<string, List<EventDelegate>> EventMap = new Dictionary<string, List<EventDelegate>>();

		/// <summary>
		/// Listen to an event
		/// </summary>
		/// <param name="eventKey">The event to listen to</param>
		/// <param name="eventDelegate">The handler</param>
		/// <returns></returns>
		public static UnregisterListenerDelegate Listen(string eventKey, EventDelegate eventDelegate) {
			if (!EventMap.ContainsKey(eventKey)) {
				EventMap[eventKey] = new List<EventDelegate>();
			}
			EventMap[eventKey].Add(eventDelegate);
			string s = "hello";
			s.EndsWith("");
			return () => { Unregister(eventKey, eventDelegate); };
		}

		/// <summary>
		/// Fires an event
		/// </summary>
		/// <param name="eventKey">The event to fire</param>
		/// <param name="data">Data to send the event</param>
		public static void Fire(string eventKey, object data = null) {
			List<EventDelegate> delegates;
			EventMap.TryGetValue(eventKey, out delegates);
			if (delegates != null) {
				foreach (EventDelegate EventDelegate in delegates) {
					EventDelegate(data);
				}
			}
		}

		public static bool Unregister(string eventKey, EventDelegate eventDelegate) {
			return EventMap[eventKey].Remove(eventDelegate);
		}

		public static void UnregisterAll(string eventKey) {
			EventMap[eventKey].Clear();
		}
	}
}
