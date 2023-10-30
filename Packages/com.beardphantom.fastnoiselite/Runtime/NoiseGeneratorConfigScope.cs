using System;

namespace BeardPhantom.FastNoiseLite
{
    public readonly struct NoiseGeneratorConfigScope : IDisposable
    {
        #region Fields

        private readonly NoiseGenerator _generator;

        private readonly NoiseGeneratorConfig _configOriginal;

        #endregion

        #region Constructors

        public NoiseGeneratorConfigScope(NoiseGenerator generator, NoiseGeneratorConfig config)
        {
            _generator = generator;
            _configOriginal = new NoiseGeneratorConfig(generator);
            config.ApplyTo(_generator);
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Dispose()
        {
            _configOriginal.ApplyTo(_generator);
        }

        #endregion
    }
}