using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace FastNoise.Editor
{
    [CustomEditor(typeof(NoiseGeneratorAsset))]
    public class NoiseGeneratorAssetEditor : UnityEditor.Editor
    {
        #region Fields

        private static readonly string _configBackingField = $"<{nameof(NoiseGeneratorAsset.Config)}>k__BackingField";

        #endregion

        #region Methods

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var scriptPropertyField = new PropertyField(serializedObject.FindProperty("m_Script"));
            scriptPropertyField.SetEnabled(false);
            root.Add(scriptPropertyField);

            var serializedProperty = serializedObject.FindProperty(_configBackingField);
            foreach (SerializedProperty childProperty in serializedProperty)
            {
                root.Add(new PropertyField(childProperty));
            }

            return root;
        }

        #endregion
    }
}