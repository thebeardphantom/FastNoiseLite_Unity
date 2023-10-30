using System;
using System.Runtime.CompilerServices;

namespace BeardPhantom.FastNoiseLite
{
    public partial class NoiseGenerator
    {
        #region Fields

        private const int PRIME_X = 501125321;

        private const int PRIME_Y = 1136930381;

        private const int PRIME_Z = 1720413743;

        #endregion

        #region Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float FastMin(float a, float b)
        {
            return a < b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float FastMax(float a, float b)
        {
            return a > b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float FastAbs(float f)
        {
            return f < 0 ? -f : f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float FastSqrt(float f)
        {
            return (float)Math.Sqrt(f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int FastFloor(float f)
        {
            return f >= 0 ? (int)f : (int)f - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int FastRound(float f)
        {
            return f >= 0 ? (int)(f + 0.5f) : (int)(f - 0.5f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Lerp(float a, float b, float t)
        {
            return a + t * (b - a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float InterpHermite(float t)
        {
            return t * t * (3 - 2 * t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float InterpQuintic(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CubicLerp(float a, float b, float c, float d, float t)
        {
            var p = d - c - (a - b);
            return t * t * t * p + t * t * (a - b - p) + t * (c - a) + b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float PingPong(float t)
        {
            t -= (int)(t * 0.5f) * 2;
            return t < 1 ? t : 2 - t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Hash(int seed, int xPrimed, int yPrimed)
        {
            var hash = seed ^ xPrimed ^ yPrimed;

            hash *= 0x27d4eb2d;
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Hash(int seed, int xPrimed, int yPrimed, int zPrimed)
        {
            var hash = seed ^ xPrimed ^ yPrimed ^ zPrimed;

            hash *= 0x27d4eb2d;
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float ValCoord(int seed, int xPrimed, int yPrimed)
        {
            var hash = Hash(seed, xPrimed, yPrimed);

            hash *= hash;
            hash ^= hash << 19;
            return hash * (1 / 2147483648.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float ValCoord(int seed, int xPrimed, int yPrimed, int zPrimed)
        {
            var hash = Hash(seed, xPrimed, yPrimed, zPrimed);

            hash *= hash;
            hash ^= hash << 19;
            return hash * (1 / 2147483648.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GradCoord(int seed, int xPrimed, int yPrimed, float xd, float yd)
        {
            var hash = Hash(seed, xPrimed, yPrimed);
            hash ^= hash >> 15;
            hash &= 127 << 1;

            var xg = _gradients2D[hash];
            var yg = _gradients2D[hash | 1];

            return xd * xg + yd * yg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GradCoord(int seed, int xPrimed, int yPrimed, int zPrimed, float xd, float yd, float zd)
        {
            var hash = Hash(seed, xPrimed, yPrimed, zPrimed);
            hash ^= hash >> 15;
            hash &= 63 << 2;

            var xg = _gradients3D[hash];
            var yg = _gradients3D[hash | 1];
            var zg = _gradients3D[hash | 2];

            return xd * xg + yd * yg + zd * zg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GradCoordOut(int seed, int xPrimed, int yPrimed, out float xo, out float yo)
        {
            var hash = Hash(seed, xPrimed, yPrimed) & (255 << 1);

            xo = _randVecs2D[hash];
            yo = _randVecs2D[hash | 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GradCoordOut(
            int seed,
            int xPrimed,
            int yPrimed,
            int zPrimed,
            out float xo,
            out float yo,
            out float zo)
        {
            var hash = Hash(seed, xPrimed, yPrimed, zPrimed) & (255 << 2);

            xo = _randVecs3D[hash];
            yo = _randVecs3D[hash | 1];
            zo = _randVecs3D[hash | 2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GradCoordDual(int seed, int xPrimed, int yPrimed, float xd, float yd, out float xo, out float yo)
        {
            var hash = Hash(seed, xPrimed, yPrimed);
            var index1 = hash & (127 << 1);
            var index2 = (hash >> 7) & (255 << 1);

            var xg = _gradients2D[index1];
            var yg = _gradients2D[index1 | 1];
            var value = xd * xg + yd * yg;

            var xgo = _randVecs2D[index2];
            var ygo = _randVecs2D[index2 | 1];

            xo = value * xgo;
            yo = value * ygo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GradCoordDual(
            int seed,
            int xPrimed,
            int yPrimed,
            int zPrimed,
            float xd,
            float yd,
            float zd,
            out float xo,
            out float yo,
            out float zo)
        {
            var hash = Hash(seed, xPrimed, yPrimed, zPrimed);
            var index1 = hash & (63 << 2);
            var index2 = (hash >> 6) & (255 << 2);

            var xg = _gradients3D[index1];
            var yg = _gradients3D[index1 | 1];
            var zg = _gradients3D[index1 | 2];
            var value = xd * xg + yd * yg + zd * zg;

            var xgo = _randVecs3D[index2];
            var ygo = _randVecs3D[index2 | 1];
            var zgo = _randVecs3D[index2 | 2];

            xo = value * xgo;
            yo = value * ygo;
            zo = value * zgo;
        }

        private static float SinglePerlin(int seed, float x, float y)
        {
            var x0 = FastFloor(x);
            var y0 = FastFloor(y);

            var xd0 = x - x0;
            var yd0 = y - y0;
            var xd1 = xd0 - 1;
            var yd1 = yd0 - 1;

            var xs = InterpQuintic(xd0);
            var ys = InterpQuintic(yd0);

            x0 *= PRIME_X;
            y0 *= PRIME_Y;
            var x1 = x0 + PRIME_X;
            var y1 = y0 + PRIME_Y;

            var xf0 = Lerp(GradCoord(seed, x0, y0, xd0, yd0), GradCoord(seed, x1, y0, xd1, yd0), xs);
            var xf1 = Lerp(GradCoord(seed, x0, y1, xd0, yd1), GradCoord(seed, x1, y1, xd1, yd1), xs);

            return Lerp(xf0, xf1, ys) * 1.4247691104677813f;
        }

        private static float SinglePerlin(int seed, float x, float y, float z)
        {
            var x0 = FastFloor(x);
            var y0 = FastFloor(y);
            var z0 = FastFloor(z);

            var xd0 = x - x0;
            var yd0 = y - y0;
            var zd0 = z - z0;
            var xd1 = xd0 - 1;
            var yd1 = yd0 - 1;
            var zd1 = zd0 - 1;

            var xs = InterpQuintic(xd0);
            var ys = InterpQuintic(yd0);
            var zs = InterpQuintic(zd0);

            x0 *= PRIME_X;
            y0 *= PRIME_Y;
            z0 *= PRIME_Z;
            var x1 = x0 + PRIME_X;
            var y1 = y0 + PRIME_Y;
            var z1 = z0 + PRIME_Z;

            var xf00 = Lerp(GradCoord(seed, x0, y0, z0, xd0, yd0, zd0), GradCoord(seed, x1, y0, z0, xd1, yd0, zd0), xs);
            var xf10 = Lerp(GradCoord(seed, x0, y1, z0, xd0, yd1, zd0), GradCoord(seed, x1, y1, z0, xd1, yd1, zd0), xs);
            var xf01 = Lerp(GradCoord(seed, x0, y0, z1, xd0, yd0, zd1), GradCoord(seed, x1, y0, z1, xd1, yd0, zd1), xs);
            var xf11 = Lerp(GradCoord(seed, x0, y1, z1, xd0, yd1, zd1), GradCoord(seed, x1, y1, z1, xd1, yd1, zd1), xs);

            var yf0 = Lerp(xf00, xf10, ys);
            var yf1 = Lerp(xf01, xf11, ys);

            return Lerp(yf0, yf1, zs) * 0.964921414852142333984375f;
        }

        private static float SingleValueCubic(int seed, float x, float y)
        {
            var x1 = FastFloor(x);
            var y1 = FastFloor(y);

            var xs = x - x1;
            var ys = y - y1;

            x1 *= PRIME_X;
            y1 *= PRIME_Y;
            var x0 = x1 - PRIME_X;
            var y0 = y1 - PRIME_Y;
            var x2 = x1 + PRIME_X;
            var y2 = y1 + PRIME_Y;
            var x3 = x1 + unchecked(PRIME_X * 2);
            var y3 = y1 + unchecked(PRIME_Y * 2);

            return CubicLerp(
                    CubicLerp(
                        ValCoord(seed, x0, y0),
                        ValCoord(seed, x1, y0),
                        ValCoord(seed, x2, y0),
                        ValCoord(seed, x3, y0),
                        xs),
                    CubicLerp(
                        ValCoord(seed, x0, y1),
                        ValCoord(seed, x1, y1),
                        ValCoord(seed, x2, y1),
                        ValCoord(seed, x3, y1),
                        xs),
                    CubicLerp(
                        ValCoord(seed, x0, y2),
                        ValCoord(seed, x1, y2),
                        ValCoord(seed, x2, y2),
                        ValCoord(seed, x3, y2),
                        xs),
                    CubicLerp(
                        ValCoord(seed, x0, y3),
                        ValCoord(seed, x1, y3),
                        ValCoord(seed, x2, y3),
                        ValCoord(seed, x3, y3),
                        xs),
                    ys)
                * (1 / (1.5f * 1.5f));
        }

        private static float SingleValueCubic(int seed, float x, float y, float z)
        {
            var x1 = FastFloor(x);
            var y1 = FastFloor(y);
            var z1 = FastFloor(z);

            var xs = x - x1;
            var ys = y - y1;
            var zs = z - z1;

            x1 *= PRIME_X;
            y1 *= PRIME_Y;
            z1 *= PRIME_Z;

            var x0 = x1 - PRIME_X;
            var y0 = y1 - PRIME_Y;
            var z0 = z1 - PRIME_Z;
            var x2 = x1 + PRIME_X;
            var y2 = y1 + PRIME_Y;
            var z2 = z1 + PRIME_Z;
            var x3 = x1 + unchecked(PRIME_X * 2);
            var y3 = y1 + unchecked(PRIME_Y * 2);
            var z3 = z1 + unchecked(PRIME_Z * 2);


            return CubicLerp(
                    CubicLerp(
                        CubicLerp(
                            ValCoord(seed, x0, y0, z0),
                            ValCoord(seed, x1, y0, z0),
                            ValCoord(seed, x2, y0, z0),
                            ValCoord(seed, x3, y0, z0),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y1, z0),
                            ValCoord(seed, x1, y1, z0),
                            ValCoord(seed, x2, y1, z0),
                            ValCoord(seed, x3, y1, z0),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y2, z0),
                            ValCoord(seed, x1, y2, z0),
                            ValCoord(seed, x2, y2, z0),
                            ValCoord(seed, x3, y2, z0),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y3, z0),
                            ValCoord(seed, x1, y3, z0),
                            ValCoord(seed, x2, y3, z0),
                            ValCoord(seed, x3, y3, z0),
                            xs),
                        ys),
                    CubicLerp(
                        CubicLerp(
                            ValCoord(seed, x0, y0, z1),
                            ValCoord(seed, x1, y0, z1),
                            ValCoord(seed, x2, y0, z1),
                            ValCoord(seed, x3, y0, z1),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y1, z1),
                            ValCoord(seed, x1, y1, z1),
                            ValCoord(seed, x2, y1, z1),
                            ValCoord(seed, x3, y1, z1),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y2, z1),
                            ValCoord(seed, x1, y2, z1),
                            ValCoord(seed, x2, y2, z1),
                            ValCoord(seed, x3, y2, z1),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y3, z1),
                            ValCoord(seed, x1, y3, z1),
                            ValCoord(seed, x2, y3, z1),
                            ValCoord(seed, x3, y3, z1),
                            xs),
                        ys),
                    CubicLerp(
                        CubicLerp(
                            ValCoord(seed, x0, y0, z2),
                            ValCoord(seed, x1, y0, z2),
                            ValCoord(seed, x2, y0, z2),
                            ValCoord(seed, x3, y0, z2),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y1, z2),
                            ValCoord(seed, x1, y1, z2),
                            ValCoord(seed, x2, y1, z2),
                            ValCoord(seed, x3, y1, z2),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y2, z2),
                            ValCoord(seed, x1, y2, z2),
                            ValCoord(seed, x2, y2, z2),
                            ValCoord(seed, x3, y2, z2),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y3, z2),
                            ValCoord(seed, x1, y3, z2),
                            ValCoord(seed, x2, y3, z2),
                            ValCoord(seed, x3, y3, z2),
                            xs),
                        ys),
                    CubicLerp(
                        CubicLerp(
                            ValCoord(seed, x0, y0, z3),
                            ValCoord(seed, x1, y0, z3),
                            ValCoord(seed, x2, y0, z3),
                            ValCoord(seed, x3, y0, z3),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y1, z3),
                            ValCoord(seed, x1, y1, z3),
                            ValCoord(seed, x2, y1, z3),
                            ValCoord(seed, x3, y1, z3),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y2, z3),
                            ValCoord(seed, x1, y2, z3),
                            ValCoord(seed, x2, y2, z3),
                            ValCoord(seed, x3, y2, z3),
                            xs),
                        CubicLerp(
                            ValCoord(seed, x0, y3, z3),
                            ValCoord(seed, x1, y3, z3),
                            ValCoord(seed, x2, y3, z3),
                            ValCoord(seed, x3, y3, z3),
                            xs),
                        ys),
                    zs)
                * (1 / (1.5f * 1.5f * 1.5f));
        }

        private static float SingleValue(int seed, float x, float y)
        {
            var x0 = FastFloor(x);
            var y0 = FastFloor(y);

            var xs = InterpHermite(x - x0);
            var ys = InterpHermite(y - y0);

            x0 *= PRIME_X;
            y0 *= PRIME_Y;
            var x1 = x0 + PRIME_X;
            var y1 = y0 + PRIME_Y;

            var xf0 = Lerp(ValCoord(seed, x0, y0), ValCoord(seed, x1, y0), xs);
            var xf1 = Lerp(ValCoord(seed, x0, y1), ValCoord(seed, x1, y1), xs);

            return Lerp(xf0, xf1, ys);
        }

        private static float SingleValue(int seed, float x, float y, float z)
        {
            var x0 = FastFloor(x);
            var y0 = FastFloor(y);
            var z0 = FastFloor(z);

            var xs = InterpHermite(x - x0);
            var ys = InterpHermite(y - y0);
            var zs = InterpHermite(z - z0);

            x0 *= PRIME_X;
            y0 *= PRIME_Y;
            z0 *= PRIME_Z;
            var x1 = x0 + PRIME_X;
            var y1 = y0 + PRIME_Y;
            var z1 = z0 + PRIME_Z;

            var xf00 = Lerp(ValCoord(seed, x0, y0, z0), ValCoord(seed, x1, y0, z0), xs);
            var xf10 = Lerp(ValCoord(seed, x0, y1, z0), ValCoord(seed, x1, y1, z0), xs);
            var xf01 = Lerp(ValCoord(seed, x0, y0, z1), ValCoord(seed, x1, y0, z1), xs);
            var xf11 = Lerp(ValCoord(seed, x0, y1, z1), ValCoord(seed, x1, y1, z1), xs);

            var yf0 = Lerp(xf00, xf10, ys);
            var yf1 = Lerp(xf01, xf11, ys);

            return Lerp(yf0, yf1, zs);
        }

        private static float SingleSimplex(int seed, float x, float y)
        {
            // 2D OpenSimplex2 case uses the same algorithm as ordinary Simplex.

            const float SQRT3 = 1.7320508075688772935274463415059f;
            const float G2 = (3 - SQRT3) / 6;

            var i = FastFloor(x);
            var j = FastFloor(y);
            var xi = x - i;
            var yi = y - j;

            var t = (xi + yi) * G2;
            var x0 = xi - t;
            var y0 = yi - t;

            i *= PRIME_X;
            j *= PRIME_Y;

            float n0, n1, n2;

            var a = 0.5f - x0 * x0 - y0 * y0;
            if (a <= 0)
            {
                n0 = 0;
            }
            else
            {
                n0 = a * a * (a * a) * GradCoord(seed, i, j, x0, y0);
            }

            var c = 2 * (1 - 2 * G2) * (1 / G2 - 2) * t + (-2 * (1 - 2 * G2) * (1 - 2 * G2) + a);
            if (c <= 0)
            {
                n2 = 0;
            }
            else
            {
                var x2 = x0 + (2 * G2 - 1);
                var y2 = y0 + (2 * G2 - 1);
                n2 = c * c * (c * c) * GradCoord(seed, i + PRIME_X, j + PRIME_Y, x2, y2);
            }

            if (y0 > x0)
            {
                var x1 = x0 + G2;
                var y1 = y0 + (G2 - 1);
                var b = 0.5f - x1 * x1 - y1 * y1;
                if (b <= 0)
                {
                    n1 = 0;
                }
                else
                {
                    n1 = b * b * (b * b) * GradCoord(seed, i, j + PRIME_Y, x1, y1);
                }
            }
            else
            {
                var x1 = x0 + (G2 - 1);
                var y1 = y0 + G2;
                var b = 0.5f - x1 * x1 - y1 * y1;
                if (b <= 0)
                {
                    n1 = 0;
                }
                else
                {
                    n1 = b * b * (b * b) * GradCoord(seed, i + PRIME_X, j, x1, y1);
                }
            }

            return (n0 + n1 + n2) * 99.83685446303647f;
        }

        private static float SingleOpenSimplex2(int seed, float x, float y, float z)
        {
            // 3D OpenSimplex2 case uses two offset rotated cube grids.

            var i = FastRound(x);
            var j = FastRound(y);
            var k = FastRound(z);
            var x0 = x - i;
            var y0 = y - j;
            var z0 = z - k;

            var xNSign = (int)(-1.0f - x0) | 1;
            var yNSign = (int)(-1.0f - y0) | 1;
            var zNSign = (int)(-1.0f - z0) | 1;

            var ax0 = xNSign * -x0;
            var ay0 = yNSign * -y0;
            var az0 = zNSign * -z0;

            i *= PRIME_X;
            j *= PRIME_Y;
            k *= PRIME_Z;

            float value = 0;
            var a = 0.6f - x0 * x0 - (y0 * y0 + z0 * z0);

            for (var l = 0;; l++)
            {
                if (a > 0)
                {
                    value += a * a * (a * a) * GradCoord(seed, i, j, k, x0, y0, z0);
                }

                if (ax0 >= ay0 && ax0 >= az0)
                {
                    var b = a + ax0 + ax0;
                    if (b > 1)
                    {
                        b -= 1;
                        value += b * b * (b * b) * GradCoord(seed, i - xNSign * PRIME_X, j, k, x0 + xNSign, y0, z0);
                    }
                }
                else if (ay0 > ax0 && ay0 >= az0)
                {
                    var b = a + ay0 + ay0;
                    if (b > 1)
                    {
                        b -= 1;
                        value += b * b * (b * b) * GradCoord(seed, i, j - yNSign * PRIME_Y, k, x0, y0 + yNSign, z0);
                    }
                }
                else
                {
                    var b = a + az0 + az0;
                    if (b > 1)
                    {
                        b -= 1;
                        value += b * b * (b * b) * GradCoord(seed, i, j, k - zNSign * PRIME_Z, x0, y0, z0 + zNSign);
                    }
                }

                if (l == 1)
                {
                    break;
                }

                ax0 = 0.5f - ax0;
                ay0 = 0.5f - ay0;
                az0 = 0.5f - az0;

                x0 = xNSign * ax0;
                y0 = yNSign * ay0;
                z0 = zNSign * az0;

                a += 0.75f - ax0 - (ay0 + az0);

                i += (xNSign >> 1) & PRIME_X;
                j += (yNSign >> 1) & PRIME_Y;
                k += (zNSign >> 1) & PRIME_Z;

                xNSign = -xNSign;
                yNSign = -yNSign;
                zNSign = -zNSign;

                seed = ~seed;
            }

            return value * 32.69428253173828125f;
        }

        private static float SingleOpenSimplex2S(int seed, float x, float y)
        {
            // 2D OpenSimplex2S case is a modified 2D simplex noise.

            const float SQRT3 = (float)1.7320508075688772935274463415059;
            const float G2 = (3 - SQRT3) / 6;

            var i = FastFloor(x);
            var j = FastFloor(y);
            var xi = x - i;
            var yi = y - j;

            i *= PRIME_X;
            j *= PRIME_Y;
            var i1 = i + PRIME_X;
            var j1 = j + PRIME_Y;

            var t = (xi + yi) * G2;
            var x0 = xi - t;
            var y0 = yi - t;

            var a0 = 2.0f / 3.0f - x0 * x0 - y0 * y0;
            var value = a0 * a0 * (a0 * a0) * GradCoord(seed, i, j, x0, y0);

            var a1 = 2 * (1 - 2 * G2) * (1 / G2 - 2) * t + (-2 * (1 - 2 * G2) * (1 - 2 * G2) + a0);
            var x1 = x0 - (1 - 2 * G2);
            var y1 = y0 - (1 - 2 * G2);
            value += a1 * a1 * (a1 * a1) * GradCoord(seed, i1, j1, x1, y1);

            // Nested conditionals were faster than compact bit logic/arithmetic.
            var xmyi = xi - yi;
            if (t > G2)
            {
                if (xi + xmyi > 1)
                {
                    var x2 = x0 + (3 * G2 - 2);
                    var y2 = y0 + (3 * G2 - 1);
                    var a2 = 2.0f / 3.0f - x2 * x2 - y2 * y2;
                    if (a2 > 0)
                    {
                        value += a2 * a2 * (a2 * a2) * GradCoord(seed, i + (PRIME_X << 1), j + PRIME_Y, x2, y2);
                    }
                }
                else
                {
                    var x2 = x0 + G2;
                    var y2 = y0 + (G2 - 1);
                    var a2 = 2.0f / 3.0f - x2 * x2 - y2 * y2;
                    if (a2 > 0)
                    {
                        value += a2 * a2 * (a2 * a2) * GradCoord(seed, i, j + PRIME_Y, x2, y2);
                    }
                }

                if (yi - xmyi > 1)
                {
                    var x3 = x0 + (3 * G2 - 1);
                    var y3 = y0 + (3 * G2 - 2);
                    var a3 = 2.0f / 3.0f - x3 * x3 - y3 * y3;
                    if (a3 > 0)
                    {
                        value += a3 * a3 * (a3 * a3) * GradCoord(seed, i + PRIME_X, j + (PRIME_Y << 1), x3, y3);
                    }
                }
                else
                {
                    var x3 = x0 + (G2 - 1);
                    var y3 = y0 + G2;
                    var a3 = 2.0f / 3.0f - x3 * x3 - y3 * y3;
                    if (a3 > 0)
                    {
                        value += a3 * a3 * (a3 * a3) * GradCoord(seed, i + PRIME_X, j, x3, y3);
                    }
                }
            }
            else
            {
                if (xi + xmyi < 0)
                {
                    var x2 = x0 + (1 - G2);
                    var y2 = y0 - G2;
                    var a2 = 2.0f / 3.0f - x2 * x2 - y2 * y2;
                    if (a2 > 0)
                    {
                        value += a2 * a2 * (a2 * a2) * GradCoord(seed, i - PRIME_X, j, x2, y2);
                    }
                }
                else
                {
                    var x2 = x0 + (G2 - 1);
                    var y2 = y0 + G2;
                    var a2 = 2.0f / 3.0f - x2 * x2 - y2 * y2;
                    if (a2 > 0)
                    {
                        value += a2 * a2 * (a2 * a2) * GradCoord(seed, i + PRIME_X, j, x2, y2);
                    }
                }

                if (yi < xmyi)
                {
                    var x2 = x0 - G2;
                    var y2 = y0 - (G2 - 1);
                    var a2 = 2.0f / 3.0f - x2 * x2 - y2 * y2;
                    if (a2 > 0)
                    {
                        value += a2 * a2 * (a2 * a2) * GradCoord(seed, i, j - PRIME_Y, x2, y2);
                    }
                }
                else
                {
                    var x2 = x0 + G2;
                    var y2 = y0 + (G2 - 1);
                    var a2 = 2.0f / 3.0f - x2 * x2 - y2 * y2;
                    if (a2 > 0)
                    {
                        value += a2 * a2 * (a2 * a2) * GradCoord(seed, i, j + PRIME_Y, x2, y2);
                    }
                }
            }

            return value * 18.24196194486065f;
        }

        private static float SingleOpenSimplex2S(int seed, float x, float y, float z)
        {
            // 3D OpenSimplex2S case uses two offset rotated cube grids.

            var i = FastFloor(x);
            var j = FastFloor(y);
            var k = FastFloor(z);
            var xi = x - i;
            var yi = y - j;
            var zi = z - k;

            i *= PRIME_X;
            j *= PRIME_Y;
            k *= PRIME_Z;
            var seed2 = seed + 1293373;

            var xNMask = (int)(-0.5f - xi);
            var yNMask = (int)(-0.5f - yi);
            var zNMask = (int)(-0.5f - zi);

            var x0 = xi + xNMask;
            var y0 = yi + yNMask;
            var z0 = zi + zNMask;
            var a0 = 0.75f - x0 * x0 - y0 * y0 - z0 * z0;
            var value = a0
                * a0
                * (a0 * a0)
                * GradCoord(
                    seed,
                    i + (xNMask & PRIME_X),
                    j + (yNMask & PRIME_Y),
                    k + (zNMask & PRIME_Z),
                    x0,
                    y0,
                    z0);

            var x1 = xi - 0.5f;
            var y1 = yi - 0.5f;
            var z1 = zi - 0.5f;
            var a1 = 0.75f - x1 * x1 - y1 * y1 - z1 * z1;
            value += a1
                * a1
                * (a1 * a1)
                * GradCoord(
                    seed2,
                    i + PRIME_X,
                    j + PRIME_Y,
                    k + PRIME_Z,
                    x1,
                    y1,
                    z1);

            var xAFlipMask0 = ((xNMask | 1) << 1) * x1;
            var yAFlipMask0 = ((yNMask | 1) << 1) * y1;
            var zAFlipMask0 = ((zNMask | 1) << 1) * z1;
            var xAFlipMask1 = (-2 - (xNMask << 2)) * x1 - 1.0f;
            var yAFlipMask1 = (-2 - (yNMask << 2)) * y1 - 1.0f;
            var zAFlipMask1 = (-2 - (zNMask << 2)) * z1 - 1.0f;

            var skip5 = false;
            var a2 = xAFlipMask0 + a0;
            if (a2 > 0)
            {
                var x2 = x0 - (xNMask | 1);
                var y2 = y0;
                var z2 = z0;
                value += a2
                    * a2
                    * (a2 * a2)
                    * GradCoord(
                        seed,
                        i + (~xNMask & PRIME_X),
                        j + (yNMask & PRIME_Y),
                        k + (zNMask & PRIME_Z),
                        x2,
                        y2,
                        z2);
            }
            else
            {
                var a3 = yAFlipMask0 + zAFlipMask0 + a0;
                if (a3 > 0)
                {
                    var x3 = x0;
                    var y3 = y0 - (yNMask | 1);
                    var z3 = z0 - (zNMask | 1);
                    value += a3
                        * a3
                        * (a3 * a3)
                        * GradCoord(
                            seed,
                            i + (xNMask & PRIME_X),
                            j + (~yNMask & PRIME_Y),
                            k + (~zNMask & PRIME_Z),
                            x3,
                            y3,
                            z3);
                }

                var a4 = xAFlipMask1 + a1;
                if (a4 > 0)
                {
                    var x4 = (xNMask | 1) + x1;
                    var y4 = y1;
                    var z4 = z1;
                    value += a4
                        * a4
                        * (a4 * a4)
                        * GradCoord(
                            seed2,
                            i + (xNMask & (PRIME_X * 2)),
                            j + PRIME_Y,
                            k + PRIME_Z,
                            x4,
                            y4,
                            z4);
                    skip5 = true;
                }
            }

            var skip9 = false;
            var a6 = yAFlipMask0 + a0;
            if (a6 > 0)
            {
                var x6 = x0;
                var y6 = y0 - (yNMask | 1);
                var z6 = z0;
                value += a6
                    * a6
                    * (a6 * a6)
                    * GradCoord(
                        seed,
                        i + (xNMask & PRIME_X),
                        j + (~yNMask & PRIME_Y),
                        k + (zNMask & PRIME_Z),
                        x6,
                        y6,
                        z6);
            }
            else
            {
                var a7 = xAFlipMask0 + zAFlipMask0 + a0;
                if (a7 > 0)
                {
                    var x7 = x0 - (xNMask | 1);
                    var y7 = y0;
                    var z7 = z0 - (zNMask | 1);
                    value += a7
                        * a7
                        * (a7 * a7)
                        * GradCoord(
                            seed,
                            i + (~xNMask & PRIME_X),
                            j + (yNMask & PRIME_Y),
                            k + (~zNMask & PRIME_Z),
                            x7,
                            y7,
                            z7);
                }

                var a8 = yAFlipMask1 + a1;
                if (a8 > 0)
                {
                    var x8 = x1;
                    var y8 = (yNMask | 1) + y1;
                    var z8 = z1;
                    value += a8
                        * a8
                        * (a8 * a8)
                        * GradCoord(
                            seed2,
                            i + PRIME_X,
                            j + (yNMask & (PRIME_Y << 1)),
                            k + PRIME_Z,
                            x8,
                            y8,
                            z8);
                    skip9 = true;
                }
            }

            var skipD = false;
            var aA = zAFlipMask0 + a0;
            if (aA > 0)
            {
                var xA = x0;
                var yA = y0;
                var zA = z0 - (zNMask | 1);
                value += aA
                    * aA
                    * (aA * aA)
                    * GradCoord(
                        seed,
                        i + (xNMask & PRIME_X),
                        j + (yNMask & PRIME_Y),
                        k + (~zNMask & PRIME_Z),
                        xA,
                        yA,
                        zA);
            }
            else
            {
                var aB = xAFlipMask0 + yAFlipMask0 + a0;
                if (aB > 0)
                {
                    var xB = x0 - (xNMask | 1);
                    var yB = y0 - (yNMask | 1);
                    var zB = z0;
                    value += aB
                        * aB
                        * (aB * aB)
                        * GradCoord(
                            seed,
                            i + (~xNMask & PRIME_X),
                            j + (~yNMask & PRIME_Y),
                            k + (zNMask & PRIME_Z),
                            xB,
                            yB,
                            zB);
                }

                var aC = zAFlipMask1 + a1;
                if (aC > 0)
                {
                    var xC = x1;
                    var yC = y1;
                    var zC = (zNMask | 1) + z1;
                    value += aC
                        * aC
                        * (aC * aC)
                        * GradCoord(
                            seed2,
                            i + PRIME_X,
                            j + PRIME_Y,
                            k + (zNMask & (PRIME_Z << 1)),
                            xC,
                            yC,
                            zC);
                    skipD = true;
                }
            }

            if (!skip5)
            {
                var a5 = yAFlipMask1 + zAFlipMask1 + a1;
                if (a5 > 0)
                {
                    var x5 = x1;
                    var y5 = (yNMask | 1) + y1;
                    var z5 = (zNMask | 1) + z1;
                    value += a5
                        * a5
                        * (a5 * a5)
                        * GradCoord(
                            seed2,
                            i + PRIME_X,
                            j + (yNMask & (PRIME_Y << 1)),
                            k + (zNMask & (PRIME_Z << 1)),
                            x5,
                            y5,
                            z5);
                }
            }

            if (!skip9)
            {
                var a9 = xAFlipMask1 + zAFlipMask1 + a1;
                if (a9 > 0)
                {
                    var x9 = (xNMask | 1) + x1;
                    var y9 = y1;
                    var z9 = (zNMask | 1) + z1;
                    value += a9
                        * a9
                        * (a9 * a9)
                        * GradCoord(
                            seed2,
                            i + (xNMask & (PRIME_X * 2)),
                            j + PRIME_Y,
                            k + (zNMask & (PRIME_Z << 1)),
                            x9,
                            y9,
                            z9);
                }
            }

            if (!skipD)
            {
                var aD = xAFlipMask1 + yAFlipMask1 + a1;
                if (aD > 0)
                {
                    var xD = (xNMask | 1) + x1;
                    var yD = (yNMask | 1) + y1;
                    var zD = z1;
                    value += aD
                        * aD
                        * (aD * aD)
                        * GradCoord(
                            seed2,
                            i + (xNMask & (PRIME_X << 1)),
                            j + (yNMask & (PRIME_Y << 1)),
                            k + PRIME_Z,
                            xD,
                            yD,
                            zD);
                }
            }

            return value * 9.046026385208288f;
        }

        private static void SingleDomainWarpBasicGrid(
            int seed,
            float warpAmp,
            float frequency,
            float x,
            float y,
            ref float xr,
            ref float yr)
        {
            var xf = x * frequency;
            var yf = y * frequency;

            var x0 = FastFloor(xf);
            var y0 = FastFloor(yf);

            var xs = InterpHermite(xf - x0);
            var ys = InterpHermite(yf - y0);

            x0 *= PRIME_X;
            y0 *= PRIME_Y;
            var x1 = x0 + PRIME_X;
            var y1 = y0 + PRIME_Y;

            var hash0 = Hash(seed, x0, y0) & (255 << 1);
            var hash1 = Hash(seed, x1, y0) & (255 << 1);

            var lx0x = Lerp(_randVecs2D[hash0], _randVecs2D[hash1], xs);
            var ly0x = Lerp(_randVecs2D[hash0 | 1], _randVecs2D[hash1 | 1], xs);

            hash0 = Hash(seed, x0, y1) & (255 << 1);
            hash1 = Hash(seed, x1, y1) & (255 << 1);

            var lx1x = Lerp(_randVecs2D[hash0], _randVecs2D[hash1], xs);
            var ly1x = Lerp(_randVecs2D[hash0 | 1], _randVecs2D[hash1 | 1], xs);

            xr += Lerp(lx0x, lx1x, ys) * warpAmp;
            yr += Lerp(ly0x, ly1x, ys) * warpAmp;
        }

        private static void SingleDomainWarpBasicGrid(
            int seed,
            float warpAmp,
            float frequency,
            float x,
            float y,
            float z,
            ref float xr,
            ref float yr,
            ref float zr)
        {
            var xf = x * frequency;
            var yf = y * frequency;
            var zf = z * frequency;

            var x0 = FastFloor(xf);
            var y0 = FastFloor(yf);
            var z0 = FastFloor(zf);

            var xs = InterpHermite(xf - x0);
            var ys = InterpHermite(yf - y0);
            var zs = InterpHermite(zf - z0);

            x0 *= PRIME_X;
            y0 *= PRIME_Y;
            z0 *= PRIME_Z;
            var x1 = x0 + PRIME_X;
            var y1 = y0 + PRIME_Y;
            var z1 = z0 + PRIME_Z;

            var hash0 = Hash(seed, x0, y0, z0) & (255 << 2);
            var hash1 = Hash(seed, x1, y0, z0) & (255 << 2);

            var lx0x = Lerp(_randVecs3D[hash0], _randVecs3D[hash1], xs);
            var ly0x = Lerp(_randVecs3D[hash0 | 1], _randVecs3D[hash1 | 1], xs);
            var lz0x = Lerp(_randVecs3D[hash0 | 2], _randVecs3D[hash1 | 2], xs);

            hash0 = Hash(seed, x0, y1, z0) & (255 << 2);
            hash1 = Hash(seed, x1, y1, z0) & (255 << 2);

            var lx1x = Lerp(_randVecs3D[hash0], _randVecs3D[hash1], xs);
            var ly1x = Lerp(_randVecs3D[hash0 | 1], _randVecs3D[hash1 | 1], xs);
            var lz1x = Lerp(_randVecs3D[hash0 | 2], _randVecs3D[hash1 | 2], xs);

            var lx0y = Lerp(lx0x, lx1x, ys);
            var ly0y = Lerp(ly0x, ly1x, ys);
            var lz0y = Lerp(lz0x, lz1x, ys);

            hash0 = Hash(seed, x0, y0, z1) & (255 << 2);
            hash1 = Hash(seed, x1, y0, z1) & (255 << 2);

            lx0x = Lerp(_randVecs3D[hash0], _randVecs3D[hash1], xs);
            ly0x = Lerp(_randVecs3D[hash0 | 1], _randVecs3D[hash1 | 1], xs);
            lz0x = Lerp(_randVecs3D[hash0 | 2], _randVecs3D[hash1 | 2], xs);

            hash0 = Hash(seed, x0, y1, z1) & (255 << 2);
            hash1 = Hash(seed, x1, y1, z1) & (255 << 2);

            lx1x = Lerp(_randVecs3D[hash0], _randVecs3D[hash1], xs);
            ly1x = Lerp(_randVecs3D[hash0 | 1], _randVecs3D[hash1 | 1], xs);
            lz1x = Lerp(_randVecs3D[hash0 | 2], _randVecs3D[hash1 | 2], xs);

            xr += Lerp(lx0y, Lerp(lx0x, lx1x, ys), zs) * warpAmp;
            yr += Lerp(ly0y, Lerp(ly0x, ly1x, ys), zs) * warpAmp;
            zr += Lerp(lz0y, Lerp(lz0x, lz1x, ys), zs) * warpAmp;
        }

        private static void SingleDomainWarpSimplexGradient(
            int seed,
            float warpAmp,
            float frequency,
            float x,
            float y,
            ref float xr,
            ref float yr,
            bool outGradOnly)
        {
            const float SQRT3 = 1.7320508075688772935274463415059f;
            const float G2 = (3 - SQRT3) / 6;

            x *= frequency;
            y *= frequency;

            /*
         * --- Skew moved to TransformNoiseCoordinate method ---
         * const FNfloat F2 = 0.5f * (SQRT3 - 1);
         * FNfloat s = (x + y) * F2;
         * x += s; y += s;
        */

            var i = FastFloor(x);
            var j = FastFloor(y);
            var xi = x - i;
            var yi = y - j;

            var t = (xi + yi) * G2;
            var x0 = xi - t;
            var y0 = yi - t;

            i *= PRIME_X;
            j *= PRIME_Y;

            float vx, vy;
            vx = vy = 0;

            var a = 0.5f - x0 * x0 - y0 * y0;
            if (a > 0)
            {
                var aaaa = a * a * (a * a);
                float xo, yo;
                if (outGradOnly)
                {
                    GradCoordOut(seed, i, j, out xo, out yo);
                }
                else
                {
                    GradCoordDual(seed, i, j, x0, y0, out xo, out yo);
                }

                vx += aaaa * xo;
                vy += aaaa * yo;
            }

            var c = 2 * (1 - 2 * G2) * (1 / G2 - 2) * t + (-2 * (1 - 2 * G2) * (1 - 2 * G2) + a);
            if (c > 0)
            {
                var x2 = x0 + (2 * G2 - 1);
                var y2 = y0 + (2 * G2 - 1);
                var cccc = c * c * (c * c);
                float xo, yo;
                if (outGradOnly)
                {
                    GradCoordOut(seed, i + PRIME_X, j + PRIME_Y, out xo, out yo);
                }
                else
                {
                    GradCoordDual(seed, i + PRIME_X, j + PRIME_Y, x2, y2, out xo, out yo);
                }

                vx += cccc * xo;
                vy += cccc * yo;
            }

            if (y0 > x0)
            {
                var x1 = x0 + G2;
                var y1 = y0 + (G2 - 1);
                var b = 0.5f - x1 * x1 - y1 * y1;
                if (b > 0)
                {
                    var bbbb = b * b * (b * b);
                    float xo, yo;
                    if (outGradOnly)
                    {
                        GradCoordOut(seed, i, j + PRIME_Y, out xo, out yo);
                    }
                    else
                    {
                        GradCoordDual(seed, i, j + PRIME_Y, x1, y1, out xo, out yo);
                    }

                    vx += bbbb * xo;
                    vy += bbbb * yo;
                }
            }
            else
            {
                var x1 = x0 + (G2 - 1);
                var y1 = y0 + G2;
                var b = 0.5f - x1 * x1 - y1 * y1;
                if (b > 0)
                {
                    var bbbb = b * b * (b * b);
                    float xo, yo;
                    if (outGradOnly)
                    {
                        GradCoordOut(seed, i + PRIME_X, j, out xo, out yo);
                    }
                    else
                    {
                        GradCoordDual(seed, i + PRIME_X, j, x1, y1, out xo, out yo);
                    }

                    vx += bbbb * xo;
                    vy += bbbb * yo;
                }
            }

            xr += vx * warpAmp;
            yr += vy * warpAmp;
        }

        private static void SingleDomainWarpOpenSimplex2Gradient(
            int seed,
            float warpAmp,
            float frequency,
            float x,
            float y,
            float z,
            ref float xr,
            ref float yr,
            ref float zr,
            bool outGradOnly)
        {
            x *= frequency;
            y *= frequency;
            z *= frequency;

            /*
         * --- Rotation moved to TransformDomainWarpCoordinate method ---
         * const FNfloat R3 = (FNfloat)(2.0 / 3.0);
         * FNfloat r = (x + y + z) * R3; // Rotation, not skew
         * x = r - x; y = r - y; z = r - z;
        */

            var i = FastRound(x);
            var j = FastRound(y);
            var k = FastRound(z);
            var x0 = x - i;
            var y0 = y - j;
            var z0 = z - k;

            var xNSign = (int)(-x0 - 1.0f) | 1;
            var yNSign = (int)(-y0 - 1.0f) | 1;
            var zNSign = (int)(-z0 - 1.0f) | 1;

            var ax0 = xNSign * -x0;
            var ay0 = yNSign * -y0;
            var az0 = zNSign * -z0;

            i *= PRIME_X;
            j *= PRIME_Y;
            k *= PRIME_Z;

            float vx, vy, vz;
            vx = vy = vz = 0;

            var a = 0.6f - x0 * x0 - (y0 * y0 + z0 * z0);
            for (var l = 0;; l++)
            {
                if (a > 0)
                {
                    var aaaa = a * a * (a * a);
                    float xo, yo, zo;
                    if (outGradOnly)
                    {
                        GradCoordOut(seed, i, j, k, out xo, out yo, out zo);
                    }
                    else
                    {
                        GradCoordDual(seed, i, j, k, x0, y0, z0, out xo, out yo, out zo);
                    }

                    vx += aaaa * xo;
                    vy += aaaa * yo;
                    vz += aaaa * zo;
                }

                var b = a;
                var i1 = i;
                var j1 = j;
                var k1 = k;
                var x1 = x0;
                var y1 = y0;
                var z1 = z0;

                if (ax0 >= ay0 && ax0 >= az0)
                {
                    x1 += xNSign;
                    b = b + ax0 + ax0;
                    i1 -= xNSign * PRIME_X;
                }
                else if (ay0 > ax0 && ay0 >= az0)
                {
                    y1 += yNSign;
                    b = b + ay0 + ay0;
                    j1 -= yNSign * PRIME_Y;
                }
                else
                {
                    z1 += zNSign;
                    b = b + az0 + az0;
                    k1 -= zNSign * PRIME_Z;
                }

                if (b > 1)
                {
                    b -= 1;
                    var bbbb = b * b * (b * b);
                    float xo, yo, zo;
                    if (outGradOnly)
                    {
                        GradCoordOut(seed, i1, j1, k1, out xo, out yo, out zo);
                    }
                    else
                    {
                        GradCoordDual(seed, i1, j1, k1, x1, y1, z1, out xo, out yo, out zo);
                    }

                    vx += bbbb * xo;
                    vy += bbbb * yo;
                    vz += bbbb * zo;
                }

                if (l == 1)
                {
                    break;
                }

                ax0 = 0.5f - ax0;
                ay0 = 0.5f - ay0;
                az0 = 0.5f - az0;

                x0 = xNSign * ax0;
                y0 = yNSign * ay0;
                z0 = zNSign * az0;

                a += 0.75f - ax0 - (ay0 + az0);

                i += (xNSign >> 1) & PRIME_X;
                j += (yNSign >> 1) & PRIME_Y;
                k += (zNSign >> 1) & PRIME_Z;

                xNSign = -xNSign;
                yNSign = -yNSign;
                zNSign = -zNSign;

                seed += 1293373;
            }

            xr += vx * warpAmp;
            yr += vy * warpAmp;
            zr += vz * warpAmp;
        }

        #endregion
    }
}