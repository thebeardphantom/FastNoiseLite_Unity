using System.Runtime.CompilerServices;

namespace FastNoise
{
    public static class NoiseGeneratorExtensions
    {
        #region Methods

        /// <summary>
        /// 2D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded in range [0, 1]
        /// </returns>
        public static float GetNoise01(this INoiseGenerator noiseGenerator, float x, float y, int seedOffset)
        {
            var noise = noiseGenerator.GetNoise(x, y, seedOffset);
            noise = RemapTo01(noise);
            return noise;
        }

        /// <summary>
        /// 3D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded in range [0, 1]
        /// </returns>
        public static float GetNoise01(this INoiseGenerator noiseGenerator, float x, float y, float z, int seedOffset)
        {
            var noise = noiseGenerator.GetNoise(x, y, z, seedOffset);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float RemapTo01(float value)
        {
            value += 1f;
            return value / 2f;
        }

        #endregion
    }
}