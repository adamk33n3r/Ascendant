using System;
using System.Collections.Generic;

namespace Utils {
    public static class Array {
        public static void ForEach<T>(T[] array, Action<T> callback) {
            foreach (T item in array) {
                callback(item);
            }
        }
    }
}