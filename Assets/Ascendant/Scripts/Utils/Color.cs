using UnityEngine;

namespace Utils {
    public static class Color {
        public static UnityEngine.Color FromDecimal(int r, int g, int b) {
            return FromDecimal(r, g, b, 255);
        }

        public static UnityEngine.Color FromDecimal(int r, int g, int b, int a) {
            return new UnityEngine.Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        public static UnityEngine.Color GetRandom(float alpha = 1f) {
            return new UnityEngine.Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), alpha);
        }
    }
}
