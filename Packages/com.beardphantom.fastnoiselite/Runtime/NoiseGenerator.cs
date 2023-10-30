// MIT License
//
// Copyright(c) 2020 Jordan Peck (jordan.me2@gmail.com)
// Copyright(c) 2020 Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// .'',;:cldxkO00KKXXNNWWWNNXKOkxdollcc::::::;:::ccllloooolllllllllooollc:,'...        ...........',;cldxkO000Okxdlc::;;;,,;;;::cclllllll
// ..',;:ldxO0KXXNNNNNNNNXXK0kxdolcc::::::;;;,,,,,,;;;;;;;;;;:::cclllllc:;'....       ...........',;:ldxO0KXXXK0Okxdolc::;;;;::cllodddddo
// ...',:loxO0KXNNNNNXXKK0Okxdolc::;::::::::;;;,,'''''.....''',;:clllllc:;,'............''''''''',;:loxO0KXNNNNNXK0Okxdollccccllodxxxxxxd
// ....';:ldkO0KXXXKK00Okxdolcc:;;;;;::cclllcc:;;,''..... ....',;clooddolcc:;;;;,,;;;;;::::;;;;;;:cloxk0KXNWWWWWWNXKK0Okxddoooddxxkkkkkxx
// .....';:ldxkOOOOOkxxdolcc:;;;,,,;;:cllooooolcc:;'...      ..,:codxkkkxddooollloooooooollcc:::::clodkO0KXNWWWWWWNNXK00Okxxxxxxxxkkkkxxx
// . ....';:cloddddo___________,,,,;;:clooddddoolc:,...      ..,:ldx__00OOOkkk___kkkkkkxxdollc::::cclodkO0KXXNNNNNNXXK0OOkxxxxxxxxxxxxddd
// .......',;:cccc:|           |,,,;;:cclooddddoll:;'..     ..';cox|  \KKK000|   |KK00OOkxdocc___;::clldxxkO0KKKKK00Okkxdddddddddddddddoo
// .......'',,,,,''|   ________|',,;;::cclloooooolc:;'......___:ldk|   \KK000|   |XKKK0Okxolc|   |;;::cclodxxkkkkxxdoolllcclllooodddooooo
// ''......''''....|   |  ....'',,,,;;;::cclloooollc:;,''.'|   |oxk|    \OOO0|   |KKK00Oxdoll|___|;;;;;::ccllllllcc::;;,,;;;:cclloooooooo
// ;;,''.......... |   |_____',,;;;____:___cllo________.___|   |___|     \xkk|   |KK_______ool___:::;________;;;_______...'',;;:ccclllloo
// c:;,''......... |         |:::/     '   |lo/        |           |      \dx|   |0/       \d|   |cc/        |'/       \......',,;;:ccllo
// ol:;,'..........|    _____|ll/    __    |o/   ______|____    ___|   |   \o|   |/   ___   \|   |o/   ______|/   ___   \ .......'',;:clo
// dlc;,...........|   |::clooo|    /  |   |x\___   \KXKKK0|   |dol|   |\   \|   |   |   |   |   |d\___   \..|   |  /   /       ....',:cl
// xoc;'...  .....'|   |llodddd|    \__|   |_____\   \KKK0O|   |lc:|   |'\       |   |___|   |   |_____\   \.|   |_/___/...      ...',;:c
// dlc;'... ....',;|   |oddddddo\          |          |Okkx|   |::;|   |..\      |\         /|   |          | \         |...    ....',;:c
// ol:,'.......',:c|___|xxxddollc\_____,___|_________/ddoll|___|,,,|___|...\_____|:\ ______/l|___|_________/...\________|'........',;::cc
// c:;'.......';:codxxkkkkxxolc::;::clodxkOO0OOkkxdollc::;;,,''''',,,,''''''''''',,'''''',;:loxkkOOkxol:;,'''',,;:ccllcc:;,'''''',;::ccll
// ;,'.......',:codxkOO0OOkxdlc:;,,;;:cldxxkkxxdolc:;;,,''.....'',;;:::;;,,,'''''........,;cldkO0KK0Okdoc::;;::cloodddoolc:;;;;;::ccllooo
// .........',;:lodxOO0000Okdoc:,,',,;:clloddoolc:;,''.......'',;:clooollc:;;,,''.......',:ldkOKXNNXX0Oxdolllloddxxxxxxdolccccccllooodddd
// .    .....';:cldxkO0000Okxol:;,''',,;::cccc:;,,'.......'',;:cldxxkkxxdolc:;;,'.......';coxOKXNWWWNXKOkxddddxxkkkkkkxdoollllooddxxxxkkk
//       ....',;:codxkO000OOxdoc:;,''',,,;;;;,''.......',,;:clodkO00000Okxolc::;,,''..',;:ldxOKXNWWWNNK0OkkkkkkkkkkkxxddooooodxxkOOOOO000
//       ....',;;clodxkkOOOkkdolc:;,,,,,,,,'..........,;:clodxkO0KKXKK0Okxdolcc::;;,,,;;:codkO0XXNNNNXKK0OOOOOkkkkxxdoollloodxkO0KKKXXXXX
//
// VERSION: 1.0.1
// https://github.com/Auburn/FastNoise

using System.Runtime.CompilerServices;
// Switch between using floats or doubles for input position

//using FNLfloat = System.Double;

namespace BeardPhantom.FastNoiseLite
{
    public partial class NoiseGenerator : INoiseGenerator
    {
        #region Fields

        public const int DEFAULT_SEED = 1337;

        private NoiseType _noiseType = NoiseType.OpenSimplex2;

        private TransformType3D _transformType3D = TransformType3D.DefaultOpenSimplex2;

        private int _octaves = 3;

        private float _gain = 0.5f;

        private float _fractalBounding = 1.0f / 1.75f;

        private DomainWarpType _domainWarpType = DomainWarpType.OpenSimplex2;

        private TransformType3D _warpTransformType3D = TransformType3D.DefaultOpenSimplex2;

        private RotationType3D _rotationType3D = RotationType3D.None;

        #endregion

        #region Properties

        /// <summary>
        /// Creates a new instance of a generator using the default settings.
        /// </summary>
        public static NoiseGenerator Default => new();

        /// <summary>
        /// Sets seed used for all noise types
        /// </summary>
        /// <remarks>
        /// Default: 1337
        /// </remarks>
        public int Seed { get; set; }

        /// <summary>
        /// Sets frequency for all noise types
        /// </summary>
        /// <remarks>
        /// Default: 0.01
        /// </remarks>
        public float Frequency { get; set; } = 0.01f;

        /// <summary>
        /// Sets noise algorithm used for GetNoise(...)
        /// </summary>
        /// <remarks>
        /// Default: OpenSimplex2
        /// </remarks>
        public NoiseType NoiseType
        {
            get => _noiseType;
            set
            {
                _noiseType = value;
                UpdateTransformType3D();
            }
        }

        /// <summary>
        /// Sets method for combining octaves in all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: None
        /// Note: FractalType.DomainWarp... only affects DomainWarp(...)
        /// </remarks>
        public FractalType FractalType { get; set; } = FractalType.None;

        /// <summary>
        /// Sets domain rotation type for 3D Noise and 3D DomainWarp.
        /// Can aid in reducing directional artifacts when sampling a 2D plane in 3D
        /// </summary>
        /// <remarks>
        /// Default: None
        /// </remarks>
        public RotationType3D RotationType3D
        {
            get => _rotationType3D;
            set
            {
                _rotationType3D = value;
                UpdateTransformType3D();
                UpdateWarpTransformType3D();
            }
        }

        /// <summary>
        /// Sets octave count for all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: 3
        /// </remarks>
        public int Octaves
        {
            get => _octaves;
            set
            {
                _octaves = value;
                CalculateFractalBounding();
            }
        }

        /// <summary>
        /// Sets octave lacunarity for all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: 2.0
        /// </remarks>
        public float Lacunarity { get; set; } = 2.0f;

        /// <summary>
        /// Sets octave gain for all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: 0.5
        /// </remarks>
        public float Gain
        {
            get => _gain;
            set
            {
                _gain = value;
                CalculateFractalBounding();
            }
        }

        /// <summary>
        /// Sets octave weighting for all none DomainWarp fratal types
        /// </summary>
        /// <remarks>
        /// Default: 0.0
        /// Note: Keep between 0...1 to maintain -1...1 output bounding
        /// </remarks>
        public float WeightedStrength { get; set; } = default;

        /// <summary>
        /// Sets strength of the fractal ping pong effect
        /// </summary>
        /// <remarks>
        /// Default: 2.0
        /// </remarks>
        public float PingPongStrength { get; set; } = 2.0f;

        /// <summary>
        /// Sets distance function used in cellular noise calculations
        /// </summary>
        /// <remarks>
        /// Default: Distance
        /// </remarks>
        public CellularDistanceFunction CellularDistanceFunction { get; set; } = CellularDistanceFunction.EuclideanSq;

        /// <summary>
        /// Sets return type from cellular noise calculations
        /// </summary>
        /// <remarks>
        /// Default: EuclideanSq
        /// </remarks>
        public CellularReturnType CellularReturnType { get; set; } = CellularReturnType.Distance;

        /// <summary>
        /// Sets the maximum distance a cellular point can move from it's grid position
        /// </summary>
        /// <remarks>
        /// Default: 1.0
        /// Note: Setting this higher than 1 will cause artifacts
        /// </remarks>
        public float CellularJitterModifier { get; set; } = 1.0f;

        /// <summary>
        /// Sets the warp algorithm when using DomainWarp(...)
        /// </summary>
        /// <remarks>
        /// Default: OpenSimplex2
        /// </remarks>
        public DomainWarpType DomainWarpType
        {
            get => _domainWarpType;
            set
            {
                _domainWarpType = value;
                UpdateWarpTransformType3D();
            }
        }

        /// <summary>
        /// Sets the maximum warp distance from original position when using DomainWarp(...)
        /// </summary>
        /// <remarks>
        /// Default: 1.0
        /// </remarks>
        public float DomainWarpAmp { get; set; } = 1.0f;

        #endregion

        #region Constructors

        /// <summary>
        /// Create new FastNoise object with optional seed
        /// </summary>
        public NoiseGenerator(int seed = DEFAULT_SEED)
        {
            Seed = seed;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// 2D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetNoise(float x, float y, int seedOverride)
        {
            var seedOriginal = Seed;
            Seed = seedOverride;
            var noise = GetNoise(x, y);
            Seed = seedOriginal;
            return noise;
        }

        /// <summary>
        /// 3D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetNoise(float x, float y, float z, int seedOverride)
        {
            var seedOriginal = Seed;
            Seed = seedOverride;
            var noise = GetNoise(x, y, z);
            Seed = seedOriginal;
            return noise;
        }

        /// <summary>
        /// 2D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetNoise(float x, float y)
        {
            TransformNoiseCoordinate(ref x, ref y);

            switch (FractalType)
            {
                default:
                {
                    return GenNoiseSingle(Seed, x, y);
                }
                case FractalType.FBm:
                {
                    return GenFractalFBm(x, y);
                }
                case FractalType.Ridged:
                {
                    return GenFractalRidged(x, y);
                }
                case FractalType.PingPong:
                {
                    return GenFractalPingPong(x, y);
                }
            }
        }

        /// <summary>
        /// 3D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between [-1, 1]
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetNoise(float x, float y, float z)
        {
            TransformNoiseCoordinate(ref x, ref y, ref z);

            switch (FractalType)
            {
                case FractalType.FBm:
                {
                    return GenFractalFBm(x, y, z);
                }
                case FractalType.Ridged:
                {
                    return GenFractalRidged(x, y, z);
                }
                case FractalType.PingPong:
                {
                    return GenFractalPingPong(x, y, z);
                }
                default:
                {
                    return GenNoiseSingle(Seed, x, y, z);
                }
            }
        }

        /// <summary>
        /// 2D warps the input position using current domain warp settings
        /// </summary>
        /// <example>
        /// Example usage with GetNoise
        /// <code>DomainWarp(ref x, ref y)
        /// noise = GetNoise(x, y)</code>
        /// </example>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DomainWarp(ref float x, ref float y)
        {
            switch (FractalType)
            {
                case FractalType.DomainWarpProgressive:
                {
                    DomainWarpFractalProgressive(ref x, ref y);
                    break;
                }
                case FractalType.DomainWarpIndependent:
                {
                    DomainWarpFractalIndependent(ref x, ref y);
                    break;
                }
                default:
                {
                    DomainWarpSingle(ref x, ref y);
                    break;
                }
            }
        }

        /// <summary>
        /// 3D warps the input position using current domain warp settings
        /// </summary>
        /// <example>
        /// Example usage with GetNoise
        /// <code>DomainWarp(ref x, ref y, ref z)
        /// noise = GetNoise(x, y, z)</code>
        /// </example>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DomainWarp(ref float x, ref float y, ref float z)
        {
            switch (FractalType)
            {
                case FractalType.DomainWarpProgressive:
                {
                    DomainWarpFractalProgressive(ref x, ref y, ref z);
                    break;
                }
                case FractalType.DomainWarpIndependent:
                {
                    DomainWarpFractalIndependent(ref x, ref y, ref z);
                    break;
                }
                default:
                {
                    DomainWarpSingle(ref x, ref y, ref z);
                    break;
                }
            }
        }

        private void CalculateFractalBounding()
        {
            var gain = FastAbs(_gain);
            var amp = gain;
            var ampFractal = 1.0f;
            for (var i = 1; i < _octaves; i++)
            {
                ampFractal += amp;
                amp *= gain;
            }

            _fractalBounding = 1 / ampFractal;
        }

        // Generic noise gen

        private float GenNoiseSingle(int seed, float x, float y)
        {
            switch (_noiseType)
            {
                case NoiseType.OpenSimplex2:
                {
                    return SingleSimplex(seed, x, y);
                }
                case NoiseType.OpenSimplex2S:
                {
                    return SingleOpenSimplex2S(seed, x, y);
                }
                case NoiseType.Cellular:
                {
                    return SingleCellular(seed, x, y);
                }
                case NoiseType.Perlin:
                {
                    return SinglePerlin(seed, x, y);
                }
                case NoiseType.ValueCubic:
                {
                    return SingleValueCubic(seed, x, y);
                }
                case NoiseType.Value:
                {
                    return SingleValue(seed, x, y);
                }
                default:
                {
                    return 0;
                }
            }
        }

        private float GenNoiseSingle(int seed, float x, float y, float z)
        {
            switch (_noiseType)
            {
                case NoiseType.OpenSimplex2:
                {
                    return SingleOpenSimplex2(seed, x, y, z);
                }
                case NoiseType.OpenSimplex2S:
                {
                    return SingleOpenSimplex2S(seed, x, y, z);
                }
                case NoiseType.Cellular:
                {
                    return SingleCellular(seed, x, y, z);
                }
                case NoiseType.Perlin:
                {
                    return SinglePerlin(seed, x, y, z);
                }
                case NoiseType.ValueCubic:
                {
                    return SingleValueCubic(seed, x, y, z);
                }
                case NoiseType.Value:
                {
                    return SingleValue(seed, x, y, z);
                }
                default:
                {
                    return 0;
                }
            }
        }

        // Noise Coordinate Transforms (frequency, and possible skew or rotation)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TransformNoiseCoordinate(ref float x, ref float y)
        {
            x *= Frequency;
            y *= Frequency;

            switch (_noiseType)
            {
                case NoiseType.OpenSimplex2:
                case NoiseType.OpenSimplex2S:
                {
                    const float SQRT3 = (float)1.7320508075688772935274463415059;
                    const float F2 = 0.5f * (SQRT3 - 1);
                    var t = (x + y) * F2;
                    x += t;
                    y += t;
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TransformNoiseCoordinate(ref float x, ref float y, ref float z)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;

            switch (_transformType3D)
            {
                case TransformType3D.ImproveXYPlanes:
                {
                    var xy = x + y;
                    var s2 = xy * -(float)0.211324865405187;
                    z *= (float)0.577350269189626;
                    x += s2 - z;
                    y = y + s2 - z;
                    z += xy * (float)0.577350269189626;
                    break;
                }
                case TransformType3D.ImproveXZPlanes:
                {
                    var xz = x + z;
                    var s2 = xz * -(float)0.211324865405187;
                    y *= (float)0.577350269189626;
                    x += s2 - y;
                    z += s2 - y;
                    y += xz * (float)0.577350269189626;
                    break;
                }
                case TransformType3D.DefaultOpenSimplex2:
                {
                    const float R3 = (float)(2.0 / 3.0);
                    // Rotation, not skew
                    var r = (x + y + z) * R3;
                    x = r - x;
                    y = r - y;
                    z = r - z;
                    break;
                }
            }
        }

        private void UpdateTransformType3D()
        {
            switch (RotationType3D)
            {
                case RotationType3D.ImproveXYPlanes:
                {
                    _transformType3D = TransformType3D.ImproveXYPlanes;
                    break;
                }
                case RotationType3D.ImproveXZPlanes:
                {
                    _transformType3D = TransformType3D.ImproveXZPlanes;
                    break;
                }
                default:
                {
                    switch (_noiseType)
                    {
                        case NoiseType.OpenSimplex2:
                        case NoiseType.OpenSimplex2S:
                        {
                            _transformType3D = TransformType3D.DefaultOpenSimplex2;
                            break;
                        }
                        default:
                        {
                            _transformType3D = TransformType3D.None;
                            break;
                        }
                    }

                    break;
                }
            }
        }

        // Domain Warp Coordinate Transforms

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TransformDomainWarpCoordinate(ref float x, ref float y)
        {
            switch (_domainWarpType)
            {
                case DomainWarpType.OpenSimplex2:
                case DomainWarpType.OpenSimplex2Reduced:
                {
                    const float SQRT3 = (float)1.7320508075688772935274463415059;
                    const float F2 = 0.5f * (SQRT3 - 1);
                    var t = (x + y) * F2;
                    x += t;
                    y += t;
                }
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TransformDomainWarpCoordinate(ref float x, ref float y, ref float z)
        {
            switch (_warpTransformType3D)
            {
                case TransformType3D.ImproveXYPlanes:
                {
                    var xy = x + y;
                    var s2 = xy * -(float)0.211324865405187;
                    z *= (float)0.577350269189626;
                    x += s2 - z;
                    y = y + s2 - z;
                    z += xy * (float)0.577350269189626;
                    break;
                }
                case TransformType3D.ImproveXZPlanes:
                {
                    var xz = x + z;
                    var s2 = xz * -(float)0.211324865405187;
                    y *= (float)0.577350269189626;
                    x += s2 - y;
                    z += s2 - y;
                    y += xz * (float)0.577350269189626;
                    break;
                }
                case TransformType3D.DefaultOpenSimplex2:
                {
                    const float R3 = (float)(2.0 / 3.0);
                    var r = (x + y + z) * R3; // Rotation, not skew
                    x = r - x;
                    y = r - y;
                    z = r - z;
                    break;
                }
            }
        }

        private void UpdateWarpTransformType3D()
        {
            switch (RotationType3D)
            {
                case RotationType3D.ImproveXYPlanes:
                {
                    _warpTransformType3D = TransformType3D.ImproveXYPlanes;
                    break;
                }
                case RotationType3D.ImproveXZPlanes:
                {
                    _warpTransformType3D = TransformType3D.ImproveXZPlanes;
                    break;
                }
                default:
                {
                    switch (_domainWarpType)
                    {
                        case DomainWarpType.OpenSimplex2:
                        case DomainWarpType.OpenSimplex2Reduced:
                        {
                            _warpTransformType3D = TransformType3D.DefaultOpenSimplex2;
                            break;
                        }
                        default:
                        {
                            _warpTransformType3D = TransformType3D.None;
                            break;
                        }
                    }

                    break;
                }
            }
        }

        // Fractal FBm

        private float GenFractalFBm(float x, float y)
        {
            var seed = Seed;
            float sum = 0;
            var amp = _fractalBounding;

            for (var i = 0; i < _octaves; i++)
            {
                var noise = GenNoiseSingle(seed++, x, y);
                sum += noise * amp;
                amp *= Lerp(1.0f, FastMin(noise + 1, 2) * 0.5f, WeightedStrength);

                x *= Lacunarity;
                y *= Lacunarity;
                amp *= _gain;
            }

            return sum;
        }

        private float GenFractalFBm(float x, float y, float z)
        {
            var seed = Seed;
            float sum = 0;
            var amp = _fractalBounding;

            for (var i = 0; i < _octaves; i++)
            {
                var noise = GenNoiseSingle(seed++, x, y, z);
                sum += noise * amp;
                amp *= Lerp(1.0f, (noise + 1) * 0.5f, WeightedStrength);

                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                amp *= _gain;
            }

            return sum;
        }

        // Fractal Ridged

        private float GenFractalRidged(float x, float y)
        {
            var seed = Seed;
            float sum = 0;
            var amp = _fractalBounding;

            for (var i = 0; i < _octaves; i++)
            {
                var noise = FastAbs(GenNoiseSingle(seed++, x, y));
                sum += (noise * -2 + 1) * amp;
                amp *= Lerp(1.0f, 1 - noise, WeightedStrength);

                x *= Lacunarity;
                y *= Lacunarity;
                amp *= _gain;
            }

            return sum;
        }

        private float GenFractalRidged(float x, float y, float z)
        {
            var seed = Seed;
            float sum = 0;
            var amp = _fractalBounding;

            for (var i = 0; i < _octaves; i++)
            {
                var noise = FastAbs(GenNoiseSingle(seed++, x, y, z));
                sum += (noise * -2 + 1) * amp;
                amp *= Lerp(1.0f, 1 - noise, WeightedStrength);

                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                amp *= _gain;
            }

            return sum;
        }

        // Fractal PingPong 

        private float GenFractalPingPong(float x, float y)
        {
            var seed = Seed;
            float sum = 0;
            var amp = _fractalBounding;

            for (var i = 0; i < _octaves; i++)
            {
                var noise = PingPong((GenNoiseSingle(seed++, x, y) + 1) * PingPongStrength);
                sum += (noise - 0.5f) * 2 * amp;
                amp *= Lerp(1.0f, noise, WeightedStrength);

                x *= Lacunarity;
                y *= Lacunarity;
                amp *= _gain;
            }

            return sum;
        }

        private float GenFractalPingPong(float x, float y, float z)
        {
            var seed = Seed;
            var sum = 0f;
            var amp = _fractalBounding;

            for (var i = 0; i < _octaves; i++)
            {
                var noise = PingPong((GenNoiseSingle(seed++, x, y, z) + 1) * PingPongStrength);
                sum += (noise - 0.5f) * 2 * amp;
                amp *= Lerp(1.0f, noise, WeightedStrength);

                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                amp *= _gain;
            }

            return sum;
        }

        private float SingleCellular(int seed, float x, float y)
        {
            var xr = FastRound(x);
            var yr = FastRound(y);

            var distance0 = float.MaxValue;
            var distance1 = float.MaxValue;
            var closestHash = 0;

            var cellularJitter = 0.43701595f * CellularJitterModifier;

            var xPrimed = (xr - 1) * PRIME_X;
            var yPrimedBase = (yr - 1) * PRIME_Y;

            switch (CellularDistanceFunction)
            {
                case CellularDistanceFunction.Euclidean:
                case CellularDistanceFunction.EuclideanSq:
                default:
                {
                    for (var xi = xr - 1; xi <= xr + 1; xi++)
                    {
                        var yPrimed = yPrimedBase;

                        for (var yi = yr - 1; yi <= yr + 1; yi++)
                        {
                            var hash = Hash(seed, xPrimed, yPrimed);
                            var idx = hash & (255 << 1);

                            var vecX = xi - x + _randVecs2D[idx] * cellularJitter;
                            var vecY = yi - y + _randVecs2D[idx | 1] * cellularJitter;

                            var newDistance = vecX * vecX + vecY * vecY;

                            distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                            if (newDistance < distance0)
                            {
                                distance0 = newDistance;
                                closestHash = hash;
                            }

                            yPrimed += PRIME_Y;
                        }

                        xPrimed += PRIME_X;
                    }

                    break;
                }
                case CellularDistanceFunction.Manhattan:
                {
                    for (var xi = xr - 1; xi <= xr + 1; xi++)
                    {
                        var yPrimed = yPrimedBase;

                        for (var yi = yr - 1; yi <= yr + 1; yi++)
                        {
                            var hash = Hash(seed, xPrimed, yPrimed);
                            var idx = hash & (255 << 1);

                            var vecX = xi - x + _randVecs2D[idx] * cellularJitter;
                            var vecY = yi - y + _randVecs2D[idx | 1] * cellularJitter;

                            var newDistance = FastAbs(vecX) + FastAbs(vecY);

                            distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                            if (newDistance < distance0)
                            {
                                distance0 = newDistance;
                                closestHash = hash;
                            }

                            yPrimed += PRIME_Y;
                        }

                        xPrimed += PRIME_X;
                    }

                    break;
                }
                case CellularDistanceFunction.Hybrid:
                {
                    for (var xi = xr - 1; xi <= xr + 1; xi++)
                    {
                        var yPrimed = yPrimedBase;

                        for (var yi = yr - 1; yi <= yr + 1; yi++)
                        {
                            var hash = Hash(seed, xPrimed, yPrimed);
                            var idx = hash & (255 << 1);

                            var vecX = xi - x + _randVecs2D[idx] * cellularJitter;
                            var vecY = yi - y + _randVecs2D[idx | 1] * cellularJitter;

                            var newDistance = FastAbs(vecX) + FastAbs(vecY) + (vecX * vecX + vecY * vecY);

                            distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                            if (newDistance < distance0)
                            {
                                distance0 = newDistance;
                                closestHash = hash;
                            }

                            yPrimed += PRIME_Y;
                        }

                        xPrimed += PRIME_X;
                    }

                    break;
                }
            }

            if (CellularDistanceFunction == CellularDistanceFunction.Euclidean
                && CellularReturnType >= CellularReturnType.Distance)
            {
                distance0 = FastSqrt(distance0);

                if (CellularReturnType >= CellularReturnType.Distance2)
                {
                    distance1 = FastSqrt(distance1);
                }
            }

            switch (CellularReturnType)
            {
                case CellularReturnType.CellValue:
                {
                    return closestHash * (1 / 2147483648.0f);
                }
                case CellularReturnType.Distance:
                {
                    return distance0 - 1;
                }
                case CellularReturnType.Distance2:
                {
                    return distance1 - 1;
                }
                case CellularReturnType.Distance2Add:
                {
                    return (distance1 + distance0) * 0.5f - 1;
                }
                case CellularReturnType.Distance2Sub:
                {
                    return distance1 - distance0 - 1;
                }
                case CellularReturnType.Distance2Mul:
                {
                    return distance1 * distance0 * 0.5f - 1;
                }
                case CellularReturnType.Distance2Div:
                {
                    return distance0 / distance1 - 1;
                }
                default:
                {
                    return 0;
                }
            }
        }

        private float SingleCellular(int seed, float x, float y, float z)
        {
            var xr = FastRound(x);
            var yr = FastRound(y);
            var zr = FastRound(z);

            var distance0 = float.MaxValue;
            var distance1 = float.MaxValue;
            var closestHash = 0;

            var cellularJitter = 0.39614353f * CellularJitterModifier;

            var xPrimed = (xr - 1) * PRIME_X;
            var yPrimedBase = (yr - 1) * PRIME_Y;
            var zPrimedBase = (zr - 1) * PRIME_Z;

            switch (CellularDistanceFunction)
            {
                case CellularDistanceFunction.Euclidean:
                case CellularDistanceFunction.EuclideanSq:
                {
                    for (var xi = xr - 1; xi <= xr + 1; xi++)
                    {
                        var yPrimed = yPrimedBase;

                        for (var yi = yr - 1; yi <= yr + 1; yi++)
                        {
                            var zPrimed = zPrimedBase;

                            for (var zi = zr - 1; zi <= zr + 1; zi++)
                            {
                                var hash = Hash(seed, xPrimed, yPrimed, zPrimed);
                                var idx = hash & (255 << 2);

                                var vecX = xi - x + _randVecs3D[idx] * cellularJitter;
                                var vecY = yi - y + _randVecs3D[idx | 1] * cellularJitter;
                                var vecZ = zi - z + _randVecs3D[idx | 2] * cellularJitter;

                                var newDistance = vecX * vecX + vecY * vecY + vecZ * vecZ;

                                distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                                if (newDistance < distance0)
                                {
                                    distance0 = newDistance;
                                    closestHash = hash;
                                }

                                zPrimed += PRIME_Z;
                            }

                            yPrimed += PRIME_Y;
                        }

                        xPrimed += PRIME_X;
                    }

                    break;
                }
                case CellularDistanceFunction.Manhattan:
                {
                    for (var xi = xr - 1; xi <= xr + 1; xi++)
                    {
                        var yPrimed = yPrimedBase;

                        for (var yi = yr - 1; yi <= yr + 1; yi++)
                        {
                            var zPrimed = zPrimedBase;

                            for (var zi = zr - 1; zi <= zr + 1; zi++)
                            {
                                var hash = Hash(seed, xPrimed, yPrimed, zPrimed);
                                var idx = hash & (255 << 2);

                                var vecX = xi - x + _randVecs3D[idx] * cellularJitter;
                                var vecY = yi - y + _randVecs3D[idx | 1] * cellularJitter;
                                var vecZ = zi - z + _randVecs3D[idx | 2] * cellularJitter;

                                var newDistance = FastAbs(vecX) + FastAbs(vecY) + FastAbs(vecZ);

                                distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                                if (newDistance < distance0)
                                {
                                    distance0 = newDistance;
                                    closestHash = hash;
                                }

                                zPrimed += PRIME_Z;
                            }

                            yPrimed += PRIME_Y;
                        }

                        xPrimed += PRIME_X;
                    }

                    break;
                }
                case CellularDistanceFunction.Hybrid:
                {
                    for (var xi = xr - 1; xi <= xr + 1; xi++)
                    {
                        var yPrimed = yPrimedBase;

                        for (var yi = yr - 1; yi <= yr + 1; yi++)
                        {
                            var zPrimed = zPrimedBase;

                            for (var zi = zr - 1; zi <= zr + 1; zi++)
                            {
                                var hash = Hash(seed, xPrimed, yPrimed, zPrimed);
                                var idx = hash & (255 << 2);

                                var vecX = xi - x + _randVecs3D[idx] * cellularJitter;
                                var vecY = yi - y + _randVecs3D[idx | 1] * cellularJitter;
                                var vecZ = zi - z + _randVecs3D[idx | 2] * cellularJitter;

                                var newDistance = FastAbs(vecX)
                                    + FastAbs(vecY)
                                    + FastAbs(vecZ)
                                    + (vecX * vecX + vecY * vecY + vecZ * vecZ);

                                distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                                if (newDistance < distance0)
                                {
                                    distance0 = newDistance;
                                    closestHash = hash;
                                }

                                zPrimed += PRIME_Z;
                            }

                            yPrimed += PRIME_Y;
                        }

                        xPrimed += PRIME_X;
                    }

                    break;
                }
            }

            if (CellularDistanceFunction == CellularDistanceFunction.Euclidean
                && CellularReturnType >= CellularReturnType.Distance)
            {
                distance0 = FastSqrt(distance0);

                if (CellularReturnType >= CellularReturnType.Distance2)
                {
                    distance1 = FastSqrt(distance1);
                }
            }

            switch (CellularReturnType)
            {
                case CellularReturnType.CellValue:
                {
                    return closestHash * (1 / 2147483648.0f);
                }
                case CellularReturnType.Distance:
                {
                    return distance0 - 1;
                }
                case CellularReturnType.Distance2:
                {
                    return distance1 - 1;
                }
                case CellularReturnType.Distance2Add:
                {
                    return (distance1 + distance0) * 0.5f - 1;
                }
                case CellularReturnType.Distance2Sub:
                {
                    return distance1 - distance0 - 1;
                }
                case CellularReturnType.Distance2Mul:
                {
                    return distance1 * distance0 * 0.5f - 1;
                }
                case CellularReturnType.Distance2Div:
                {
                    return distance0 / distance1 - 1;
                }
                default:
                {
                    return 0;
                }
            }
        }

        private void DoSingleDomainWarp(int seed, float amp, float freq, float x, float y, ref float xr, ref float yr)
        {
            switch (_domainWarpType)
            {
                case DomainWarpType.OpenSimplex2:
                {
                    SingleDomainWarpSimplexGradient(seed, amp * 38.283687591552734375f, freq, x, y, ref xr, ref yr, false);
                    break;
                }
                case DomainWarpType.OpenSimplex2Reduced:
                {
                    SingleDomainWarpSimplexGradient(seed, amp * 16.0f, freq, x, y, ref xr, ref yr, true);
                    break;
                }
                case DomainWarpType.BasicGrid:
                {
                    SingleDomainWarpBasicGrid(seed, amp, freq, x, y, ref xr, ref yr);
                    break;
                }
            }
        }

        private void DoSingleDomainWarp(
            int seed,
            float amp,
            float freq,
            float x,
            float y,
            float z,
            ref float xr,
            ref float yr,
            ref float zr)
        {
            switch (_domainWarpType)
            {
                case DomainWarpType.OpenSimplex2:
                {
                    SingleDomainWarpOpenSimplex2Gradient(
                        seed,
                        amp * 32.69428253173828125f,
                        freq,
                        x,
                        y,
                        z,
                        ref xr,
                        ref yr,
                        ref zr,
                        false);
                    break;
                }
                case DomainWarpType.OpenSimplex2Reduced:
                {
                    SingleDomainWarpOpenSimplex2Gradient(
                        seed,
                        amp * 7.71604938271605f,
                        freq,
                        x,
                        y,
                        z,
                        ref xr,
                        ref yr,
                        ref zr,
                        true);
                    break;
                }
                case DomainWarpType.BasicGrid:
                {
                    SingleDomainWarpBasicGrid(seed, amp, freq, x, y, z, ref xr, ref yr, ref zr);
                    break;
                }
            }
        }

        // Domain Warp Single Wrapper

        private void DomainWarpSingle(ref float x, ref float y)
        {
            var seed = Seed;
            var amp = DomainWarpAmp * _fractalBounding;
            var freq = Frequency;

            var xs = x;
            var ys = y;
            TransformDomainWarpCoordinate(ref xs, ref ys);

            DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y);
        }

        private void DomainWarpSingle(ref float x, ref float y, ref float z)
        {
            var seed = Seed;
            var amp = DomainWarpAmp * _fractalBounding;
            var freq = Frequency;

            var xs = x;
            var ys = y;
            var zs = z;
            TransformDomainWarpCoordinate(ref xs, ref ys, ref zs);

            DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z);
        }

        // Domain Warp Fractal Progressive

        private void DomainWarpFractalProgressive(ref float x, ref float y)
        {
            var seed = Seed;
            var amp = DomainWarpAmp * _fractalBounding;
            var freq = Frequency;

            for (var i = 0; i < _octaves; i++)
            {
                var xs = x;
                var ys = y;
                TransformDomainWarpCoordinate(ref xs, ref ys);

                DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y);

                seed++;
                amp *= _gain;
                freq *= Lacunarity;
            }
        }

        private void DomainWarpFractalProgressive(ref float x, ref float y, ref float z)
        {
            var seed = Seed;
            var amp = DomainWarpAmp * _fractalBounding;
            var freq = Frequency;

            for (var i = 0; i < _octaves; i++)
            {
                var xs = x;
                var ys = y;
                var zs = z;
                TransformDomainWarpCoordinate(ref xs, ref ys, ref zs);

                DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z);

                seed++;
                amp *= _gain;
                freq *= Lacunarity;
            }
        }

        // Domain Warp Fractal Independant
        private void DomainWarpFractalIndependent(ref float x, ref float y)
        {
            var xs = x;
            var ys = y;
            TransformDomainWarpCoordinate(ref xs, ref ys);

            var seed = Seed;
            var amp = DomainWarpAmp * _fractalBounding;
            var freq = Frequency;

            for (var i = 0; i < _octaves; i++)
            {
                DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y);

                seed++;
                amp *= _gain;
                freq *= Lacunarity;
            }
        }

        private void DomainWarpFractalIndependent(ref float x, ref float y, ref float z)
        {
            var xs = x;
            var ys = y;
            var zs = z;
            TransformDomainWarpCoordinate(ref xs, ref ys, ref zs);

            var seed = Seed;
            var amp = DomainWarpAmp * _fractalBounding;
            var freq = Frequency;

            for (var i = 0; i < _octaves; i++)
            {
                DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z);

                seed++;
                amp *= _gain;
                freq *= Lacunarity;
            }
        }

        #endregion

        // Domain Warp Basic Grid
    }
}