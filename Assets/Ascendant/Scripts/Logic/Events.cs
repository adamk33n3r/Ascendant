using System;
using System.Collections.Generic;

namespace Ascendant.Scripts.Logic {
	public delegate void EventDelegate(object data = null);
	public delegate void UnregisterListenerDelegate();

	public enum Events {
		CARD_DRAWN,
		CARD_PLAYED,
		CARD_DISCARDED
	}

	/// <summary>
	/// Register events to listen to.
	/// You MUST unregister a listener or there WILL be memory leaks and/or broken code
	/// You can do so by calling the function returned by the <c>Listen</c> method or by calling
	/// <c>Unregister</c> with the key and method you used. Doing the latter will mean you'll have to
	/// not use lambdas.
	/// </summary>
	public static class EventManager {
		private static Dictionary<Events, List<EventDelegate>> EventMap = new Dictionary<Events, List<EventDelegate>>();

		/// <summary>
		/// Listen to an event
		/// </summary>
		/// <param name="eventKey">The event to listen to</param>
		/// <param name="eventDelegate">The handler</param>
		/// <returns></returns>
		public static UnregisterListenerDelegate Listen(Events eventKey, EventDelegate eventDelegate) {
			if (!EventMap.ContainsKey(eventKey)) {
				EventMap[eventKey] = new List<EventDelegate>();
			}
			EventMap[eventKey].Add(eventDelegate);
			return () => { Unregister(eventKey, eventDelegate); };
		}

		/// <summary>
		/// Fires an event
		/// </summary>
		/// <param name="eventKey">The event to fire</param>
		public static void Fire(Events eventKey, object data = null) {
			List<EventDelegate> delegates;
			EventMap.TryGetValue(eventKey, out delegates);
			if (delegates != null) {
				foreach (EventDelegate EventDelegate in delegates) {
					EventDelegate(data);
				}
			}
		}

		public static bool Unregister(Events eventKey, EventDelegate eventDelegate) {
			return EventMap[eventKey].Remove(eventDelegate);
		}

		public static void UnregisterAll(Events eventKey) {
			EventMap[eventKey].Clear();
		}
	}
}
