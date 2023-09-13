using UnityEngine;
using UnityEngine.UIElements;

namespace FastNoise.Editor
{
    public class SpeedControlledFloatField : FloatField, ISpeedControlledField
    {
        #region Properties

        public float DragSpeedMultiplier { get; set; } = 1f;

        /// <inheritdoc />
        public Label LabelElement => labelElement;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, float startValue)
        {
            var floatDragSensitivity = SpeedControlUtility.CalculateFloatDragSensitivity(startValue);
            floatDragSensitivity *= DragSpeedMultiplier;
            var acceleration = SpeedControlUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
            var num = SpeedControlUtility.RoundBasedOnMinimumDifference(
                StringToValue(text)
                + SpeedControlUtility.NiceDelta(delta, acceleration)
                * floatDragSensitivity,
                floatDragSensitivity);
            if (isDelayed)
            {
                text = ValueToString(SpeedControlUtility.ClampToFloat(num));
            }
            else
            {
                value = SpeedControlUtility.ClampToFloat(num);
            }
        }

        #endregion
    }
}