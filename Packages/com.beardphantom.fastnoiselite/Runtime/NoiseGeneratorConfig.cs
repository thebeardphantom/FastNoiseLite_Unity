using System;
using UnityEngine;

namespace FastNoise
{
    [Serializable]
    public class NoiseGeneratorConfig
    {
        #region Properties

        /// <summary>
        /// Creates a new instance of a config using the default settings.
        /// </summary>
        public static NoiseGeneratorConfig Default => new();

        /// <summary>
        /// Sets seed used for all noise types
        /// </summary>
        /// <remarks>
        /// Default: 1337
        /// </remarks>
        [field: SerializeField]
        public int Seed { get; private set; } = NoiseGenerator.DEFAULT_SEED;

        /// <summary>
        /// Sets frequency for all noise types
        /// </summary>
        /// <remarks>
        /// Default: 0.01
        /// </remarks>
        [field: SerializeField]
        public float Frequency { get; private set; } = 0.01f;

        /// <summary>
        /// Sets noise algorithm used for GetNoise(...)
        /// </summary>
        /// <remarks>
        /// Default: OpenSimplex2
        /// </remarks>
        [field: SerializeField]
        public NoiseType NoiseType { get; private set; } = NoiseType.OpenSimplex2;

        /// <summary>
        /// Sets method for combining octaves in all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: None
        /// Note: FractalType.DomainWarp... only affects DomainWarp(...)
        /// </remarks>
        [field: SerializeField]
        public FractalType FractalType { get; private set; } = FractalType.None;

        /// <summary>
        /// Sets domain rotation type for 3D Noise and 3D DomainWarp.
        /// Can aid in reducing directional artifacts when sampling a 2D plane in 3D
        /// </summary>
        /// <remarks>
        /// Default: None
        /// </remarks>
        [field: SerializeField]
        public RotationType3D RotationType3D { get; private set; } = RotationType3D.None;

        /// <summary>
        /// Sets octave count for all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: 3
        /// </remarks>
        [field: SerializeField]
        public int Octaves { get; private set; } = 3;

        /// <summary>
        /// Sets octave lacunarity for all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: 2.0
        /// </remarks>
        [field: SerializeField]
        public float Lacunarity { get; private set; } = 2.0f;

        /// <summary>
        /// Sets octave gain for all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: 0.5
        /// </remarks>
        [field: SerializeField]
        public float Gain { get; private set; } = 0.5f;

        /// <summary>
        /// Sets octave weighting for all none DomainWarp fratal types
        /// </summary>
        /// <remarks>
        /// Default: 0.0
        /// Note: Keep between 0...1 to maintain -1...1 output bounding
        /// </remarks>
        [field: SerializeField]
        public float WeightedStrength { get; private set; }

        /// <summary>
        /// Sets strength of the fractal ping pong effect
        /// </summary>
        /// <remarks>
        /// Default: 2.0
        /// </remarks>
        [field: SerializeField]
        public float PingPongStrength { get; private set; } = 2.0f;

        /// <summary>
        /// Sets distance function used in cellular noise calculations
        /// </summary>
        /// <remarks>
        /// Default: Distance
        /// </remarks>
        [field: SerializeField]
        public CellularDistanceFunction CellularDistanceFunction { get; private set; } = CellularDistanceFunction.EuclideanSq;

        /// <summary>
        /// Sets return type from cellular noise calculations
        /// </summary>
        /// <remarks>
        /// Default: EuclideanSq
        /// </remarks>
        [field: SerializeField]
        public CellularReturnType CellularReturnType { get; private set; } = CellularReturnType.Distance;

        /// <summary>
        /// Sets the maximum distance a cellular point can move from it's grid position
        /// </summary>
        /// <remarks>
        /// Default: 1.0
        /// Note: Setting this higher than 1 will cause artifacts
        /// </remarks>
        [field: SerializeField]
        public float CellularJitterModifier { get; private set; } = 1.0f;

        /// <summary>
        /// Sets the warp algorithm when using DomainWarp(...)
        /// </summary>
        /// <remarks>
        /// Default: OpenSimplex2
        /// </remarks>
        [field: SerializeField]
        public DomainWarpType DomainWarpType { get; private set; } = DomainWarpType.OpenSimplex2;

        /// <summary>
        /// Sets the maximum warp distance from original position when using DomainWarp(...)
        /// </summary>
        /// <remarks>
        /// Default: 1.0
        /// </remarks>
        [field: SerializeField]
        public float DomainWarpAmp { get; private set; } = 1.0f;

        #endregion

        #region Constructors

        public NoiseGeneratorConfig() { }

        /// <summary>
        /// Creates a new config with an optional seed value.
        /// </summary>
        /// <param name="seed">The seed to use.</param>
        public NoiseGeneratorConfig(int seed)
        {
            Seed = seed;
        }

        /// <summary>
        /// Creates a new FastNoiseLiteConfig based on the config of an existing generator.
        /// </summary>
        /// <param name="generator">The generator to copy the config from.</param>
        public NoiseGeneratorConfig(NoiseGenerator generator)
        {
            Seed = generator.Seed;
            Frequency = generator.Frequency;
            NoiseType = generator.NoiseType;
            FractalType = generator.FractalType;
            RotationType3D = generator.RotationType3D;
            Octaves = generator.Octaves;
            Lacunarity = generator.Lacunarity;
            Gain = generator.Gain;
            WeightedStrength = generator.WeightedStrength;
            PingPongStrength = generator.PingPongStrength;
            CellularDistanceFunction = generator.CellularDistanceFunction;
            CellularReturnType = generator.CellularReturnType;
            CellularJitterModifier = generator.CellularJitterModifier;
            DomainWarpType = generator.DomainWarpType;
            DomainWarpAmp = generator.DomainWarpAmp;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new generator using the settings from this config.
        /// </summary>
        /// <returns>A new generator utilizing the settings from this config.</returns>
        public NoiseGenerator CreateNewGenerator()
        {
            var fastNoiseLite = new NoiseGenerator();
            ApplyTo(fastNoiseLite);
            return fastNoiseLite;
        }

        /// <summary>
        /// Overwrites this config's properties based on the config of a generator.
        /// </summary>
        /// <param name="generator">The generator to source the config from.</param>
        public void CopyFrom(NoiseGenerator generator)
        {
            Seed = generator.Seed;
            Frequency = generator.Frequency;
            NoiseType = generator.NoiseType;
            FractalType = generator.FractalType;
            RotationType3D = generator.RotationType3D;
            Octaves = generator.Octaves;
            Lacunarity = generator.Lacunarity;
            Gain = generator.Gain;
            WeightedStrength = generator.WeightedStrength;
            PingPongStrength = generator.PingPongStrength;
            CellularDistanceFunction = generator.CellularDistanceFunction;
            CellularReturnType = generator.CellularReturnType;
            CellularJitterModifier = generator.CellularJitterModifier;
            DomainWarpType = generator.DomainWarpType;
            DomainWarpAmp = generator.DomainWarpAmp;
        }

        /// <summary>
        /// Applies a config to a generator, overwriting its properties.
        /// </summary>
        /// <param name="generator">The generator to apply the config to.</param>
        public void ApplyTo(NoiseGenerator generator)
        {
            generator.Seed = Seed;
            generator.Frequency = Frequency;
            generator.NoiseType = NoiseType;
            generator.FractalType = FractalType;
            generator.RotationType3D = RotationType3D;
            generator.Octaves = Octaves;
            generator.Lacunarity = Lacunarity;
            generator.Gain = Gain;
            generator.WeightedStrength = WeightedStrength;
            generator.PingPongStrength = PingPongStrength;
            generator.CellularDistanceFunction = CellularDistanceFunction;
            generator.CellularReturnType = CellularReturnType;
            generator.CellularJitterModifier = CellularJitterModifier;
            generator.DomainWarpType = DomainWarpType;
            generator.DomainWarpAmp = DomainWarpAmp;
        }

        #endregion
    }
}