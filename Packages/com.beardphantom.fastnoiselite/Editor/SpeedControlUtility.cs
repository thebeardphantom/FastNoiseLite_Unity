using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FastNoise.Editor
{
    public static class SpeedControlUtility
    {
        #region Fields

        private static readonly MethodInfo _calculateFloatDragSensitivityMethod;

        private static readonly MethodInfo _accelerationMethod;

        private static readonly MethodInfo _niceDeltaMethod;

        private static readonly MethodInfo _calculateIntDragSensitivityMethod;

        private static readonly object[] _oneArgArray = new object[1];

        private static readonly object[] _twoArgArray = new object[2];

        #endregion

        #region Constructors

        static SpeedControlUtility()
        {
            var unityEngineAssembly = typeof(Mathf).Assembly;
            var numericFieldDraggerUtilityType = unityEngineAssembly.GetType("UnityEngine.NumericFieldDraggerUtility");
            var methods = numericFieldDraggerUtilityType.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            _calculateFloatDragSensitivityMethod = methods
                .First(m => m.Name == "CalculateFloatDragSensitivity" && m.GetParameters().Length == 1);
            _calculateIntDragSensitivityMethod = methods
                .First(m => m.Name == "CalculateIntDragSensitivity" && m.GetParameters().Length == 1);
            _accelerationMethod = methods.First(m => m.Name == "Acceleration");
            _niceDeltaMethod = methods.First(m => m.Name == "NiceDelta");
        }

        #endregion

        #region Methods

        public static long CalculateIntDragSensitivity(long startValue)
        {
            _oneArgArray[0] = startValue;
            return (long)_calculateIntDragSensitivityMethod.Invoke(null, _oneArgArray);
        }

        internal static double CalculateFloatDragSensitivity(double startValue)
        {
            _oneArgArray[0] = startValue;
            return (double)_calculateFloatDragSensitivityMethod.Invoke(null, _oneArgArray);
        }

        internal static float NiceDelta(Vector2 deviceDelta, float acceleration)
        {
            _twoArgArray[0] = deviceDelta;
            _twoArgArray[1] = acceleration;
            return (float)_niceDeltaMethod.Invoke(null, _twoArgArray);
        }

        internal static float Acceleration(bool shiftPressed, bool altPressed)
        {
            _twoArgArray[0] = shiftPressed;
            _twoArgArray[1] = altPressed;
            return (float)_accelerationMethod.Invoke(null, _twoArgArray);
        }

        internal static double RoundBasedOnMinimumDifference(double valueToRound, double minDifference)
        {
            return minDifference == 0.0
                ? DiscardLeastSignificantDecimal(valueToRound)
                : Math.Round(
                    valueToRound,
                    GetNumberOfDecimalsForMinimumDifference(minDifference),
                    MidpointRounding.AwayFromZero);
        }

        internal static float ClampToFloat(double value)
        {
            if (double.IsPositiveInfinity(value))
            {
                return float.PositiveInfinity;
            }

            if (double.IsNegativeInfinity(value))
            {
                return float.NegativeInfinity;
            }

            if (value < -3.4028234663852886E+38)
            {
                return float.MinValue;
            }

            return value > 3.4028234663852886E+38 ? float.MaxValue : (float)value;
        }

        internal static int ClampToInt(long value)
        {
            if (value < int.MinValue)
            {
                return int.MinValue;
            }

            return value > int.MaxValue ? int.MaxValue : (int)value;
        }

        private static double DiscardLeastSignificantDecimal(double v)
        {
            var digits = Math.Max(0, (int)(5.0 - Math.Log10(Math.Abs(v))));
            try
            {
                return Math.Round(v, digits);
            }
            catch (ArgumentOutOfRangeException)
            {
                return 0.0;
            }
        }

        private static int GetNumberOfDecimalsForMinimumDifference(double minDifference)
        {
            return (int)Math.Max(0.0, -Math.Floor(Math.Log10(Math.Abs(minDifference))));
        }

        #endregion
    }
}