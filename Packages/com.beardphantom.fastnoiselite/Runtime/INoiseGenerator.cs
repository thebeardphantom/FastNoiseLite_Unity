namespace FastNoise
{
    public interface INoiseGenerator
    {
        #region Methods

        /// <summary>
        /// 2D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        float GetNoise(float x, float y, int seedOffset);

        /// <summary>
        /// 3D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        float GetNoise(float x, float y, float z, int seedOffset);

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