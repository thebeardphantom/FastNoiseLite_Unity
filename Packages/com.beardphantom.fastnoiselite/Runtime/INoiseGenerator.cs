namespace BeardPhantom.FastNoiseLite
{
    public interface INoiseGenerator
    {
        #region Properties

        int Seed { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 2D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        float GetNoise(float x, float y, int seedOverride);

        /// <summary>
        /// 3D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        float GetNoise(float x, float y, float z, int seedOverride);

        /// <summary>
        /// 2D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        float GetNoise(float x, float y);

        /// <summary>
        /// 3D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        float GetNoise(float x, float y, float z);

        #endregion
    }
}