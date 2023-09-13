using UnityEngine;

namespace FastNoise
{
    [CreateAssetMenu(menuName = "CUSTOM/" + nameof(NoiseGeneratorAsset))]
    public class NoiseGeneratorAsset : ScriptableObject, INoiseGenerator
    {
        #region Fields

        private readonly NoiseGenerator _generator = NoiseGenerator.Default;

        #endregion

        #region Properties

        [field: SerializeField]
        public NoiseGeneratorConfig Config { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public float GetNoise(float x, float y, int seedOffset)
        {
            Config.ApplyTo(_generator);
            return _generator.GetNoise(x, y, seedOffset);
        }

        /// <inheritdoc />
        public float GetNoise(float x, float y, float z, int seedOffset)
        {
            Config.ApplyTo(_generator);
            return _generator.GetNoise(x, y, z, seedOffset);
        }

        /// <inheritdoc />
        public float GetNoise(float x, float y)
        {
            Config.ApplyTo(_generator);
            return _generator.GetNoise(x, y);
        }

        /// <inheritdoc />
        public float GetNoise(float x, float y, float z)
        {
            Config.ApplyTo(_generator);
            return _generator.GetNoise(x, y, z);
        }

        #endregion
    }
}