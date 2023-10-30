using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.FastNoiseLite
{
    public static class NoiseGeneratorExtensions
    {
        #region Fields

        private static readonly Color _black = Color.black;

        private static readonly Color _white = Color.white;

        private static readonly Color32 _black32 = new(0, 0, 0, byte.MaxValue);

        private static readonly Color32 _white32 = new(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

        #endregion

        #region Methods

        /// <summary>
        /// 2D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded in range [0, 1]
        /// </returns>
        public static float GetNoise01(this INoiseGenerator noiseGenerator, float x, float y, int seedOverride)
        {
            var noise = noiseGenerator.GetNoise(x, y, seedOverride);
            noise = RemapTo01(noise);
            return noise;
        }

        /// <summary>
        /// 3D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded in range [0, 1]
        /// </returns>
        public static float GetNoise01(this INoiseGenerator noiseGenerator, float x, float y, float z, int seedOverride)
        {
            var noise = noiseGenerator.GetNoise(x, y, z, seedOverride);
            noise = RemapTo01(noise);
            return noise;
        }

        /// <summary>
        /// 2D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded in range [0, 1]
        /// </returns>
        public static float GetNoise01(this INoiseGenerator noiseGenerator, float x, float y)
        {
            var noise = noiseGenerator.GetNoise(x, y);
            noise = RemapTo01(noise);
            return noise;
        }

        /// <summary>
        /// 3D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded in range [0, 1]
        /// </returns>
        public static float GetNoise01(this INoiseGenerator noiseGenerator, float x, float y, float z)
        {
            var noise = noiseGenerator.GetNoise(x, y, z);
            noise = RemapTo01(noise);
            return noise;
        }

        public static Color32 SampleColor32(this INoiseGenerator noiseGenerator, float x, float y, int seedOverride)
        {
            var noise = noiseGenerator.GetNoise01(x, y, seedOverride);
            return Color32.Lerp(_black32, _white32, noise);
        }

        public static Color32 SampleColor32(this INoiseGenerator noiseGenerator, float x, float y)
        {
            var noise = noiseGenerator.GetNoise01(x, y);
            return Color32.Lerp(_black32, _white32, noise);
        }

        public static Color SampleColor(this INoiseGenerator noiseGenerator, float x, float y, int seedOverride)
        {
            var noise = noiseGenerator.GetNoise01(x, y, seedOverride);
            return Color.Lerp(_black, _white, noise);
        }

        public static Color SampleColor(this INoiseGenerator noiseGenerator, float x, float y)
        {
            var noise = noiseGenerator.GetNoise01(x, y);
            return Color.Lerp(_black, _white, noise);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float RemapTo01(float value)
        {
            value += 1f;
            return value / 2f;
        }

        #endregion
    }
}