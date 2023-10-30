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

        public int Seed
        {
            get => _generator.Seed;
            set => _generator.Seed = value;
        }

        [field: SerializeField]
        public NoiseGeneratorConfig Config { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public float GetNoise(float x, float y, int seedOverride)
        {
            Config.ApplyTo(_generator);
            return _generator.GetNoise(x, y, seedOverride);
        }

        /// <inheritdoc />
        public float GetNoise(float x, float y, float z, int seedOverride)
        {
            Config.ApplyTo(_generator);
            return _generator.GetNoise(x, y, z, seedOverride);
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