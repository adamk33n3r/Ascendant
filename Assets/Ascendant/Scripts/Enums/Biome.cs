using UnityEngine;

namespace Ascendant.Scripts.Enums {
    public enum Biome {
        Neutral,
        Primal,
        Divine,
        Artificial,
    }

    internal static class BiomeMethods {
        public static Color ToColor(this Biome biome) {
            switch (biome) {
                case Biome.Neutral:
                    return Color.grey;
                case Biome.Primal:
                    return Color.green;
                case Biome.Divine:
                    return Color.cyan;
                case Biome.Artificial:
                    return Color.red;
                default:
                    return Color.magenta;
            }
        }
    }
}
