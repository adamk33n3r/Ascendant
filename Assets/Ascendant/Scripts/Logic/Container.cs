using System;
using System.Collections.Generic;

public static class Container {
	private static Dictionary<string, object> classMap;

	static Container() {
		classMap = new Dictionary<string, object>();
	}

	public static void Register<T>(T value) {
		Register(typeof(T).ToString(), value);
	}

	//public static void Register<T>(T value, string alias) {
	//	Register(value);
	//	Register(alias, value);
	//}

	public static T Get<T>() {
		return (T)Get(typeof(T).ToString());
	}

	public static object Get(string key) {
		object obj;
		if (!classMap.TryGetValue(key, out obj)) {
			throw new Exception(string.Format("{0} not found in the container", key));
		}
		return obj;
	}

	public static void Dump() {
		foreach (KeyValuePair<string, object> entry in classMap) {
			UnityEngine.Debug.Log(string.Format("name: {0}, value: {1}", entry.Key, entry.Value));
		}
	}

	private static void Register(string key, object value) {
		classMap[key] = value;
	}
}
