using UnityEngine;

namespace FastNoise
{
    public class SpeedControlledValueAttribute : PropertyAttribute
    {
        #region Fields

        public readonly float DragSpeedMultiplier;

        #endregion

        #region Constructors

        public SpeedControlledValueAttribute(float dragSpeedMultiplier)
        {
            DragSpeedMultiplier = dragSpeedMultiplier;
        }

        #endregion
    }
}