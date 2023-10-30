namespace FastNoise
{
    public class NoiseGeneratorRng
    {
        #region Fields

        private readonly INoiseGenerator _generator;

        #endregion

        #region Properties

        public uint Position { get; set; }

        public int Seed
        {
            get => _generator.Seed;
            set => _generator.Seed = value;
        }

        #endregion

        #region Constructors

        public NoiseGeneratorRng() : this(NoiseGeneratorConfig.Default) { }

        public NoiseGeneratorRng(NoiseGeneratorConfig config)
        {
            Position = default;
            _generator = config.CreateNewGenerator();
        }

        public NoiseGeneratorRng(INoiseGenerator noiseGenerator)
        {
            _generator = noiseGenerator;
        }

        #endregion

        #region Methods

        public float Next()
        {
            var noise = _generator.GetNoise(Position, Position);
            Position++;
            return noise;
        }

        public float Next01()
        {
            var noise = _generator.GetNoise01(Position, Position);
            Position++;
            return noise;
        }

        public float Next(float minInclusive, float maxInclusive)
        {
            var value = Next01();
            return NoiseGenerator.Lerp(minInclusive, maxInclusive, value);
        }

        public int Next(int minInclusive, int maxExclusive)
        {
            var value = Next01();
            return (int)NoiseGenerator.Lerp(minInclusive, maxExclusive, value);
        }

        #endregion
    }
}