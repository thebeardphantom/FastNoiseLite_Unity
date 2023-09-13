using UnityEngine.UIElements;

namespace FastNoise.Editor
{
    public interface ISpeedControlledField : IBindable
    {
        #region Properties

        Label LabelElement { get; }

        float DragSpeedMultiplier { get; set; }

        #endregion
    }
}