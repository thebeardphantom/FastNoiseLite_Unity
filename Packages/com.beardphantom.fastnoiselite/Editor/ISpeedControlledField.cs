using UnityEngine.UIElements;

namespace BeardPhantom.FastNoiseLite.Editor
{
    public interface ISpeedControlledField : IBindable
    {
        #region Properties

        Label LabelElement { get; }

        float DragSpeedMultiplier { get; set; }

        #endregion
    }
}