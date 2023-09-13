using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace FastNoise.Editor
{
    [CustomPropertyDrawer(typeof(SpeedControlledValueAttribute))]
    public class SpeedControlledValuePropertyDrawer : PropertyDrawer
    {
        #region Methods

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var field = GetSpeedControlledField(property);
            var typedAttribute = (SpeedControlledValueAttribute)attribute;
            field.DragSpeedMultiplier = typedAttribute.DragSpeedMultiplier;

            var visualElement = (VisualElement)field;
            visualElement.AddToClassList(BaseField<float>.alignedFieldUssClassName);
            field.LabelElement.AddToClassList(BaseField<float>.labelUssClassName);
            field.BindProperty(property);
            return visualElement;
        }

        private ISpeedControlledField GetSpeedControlledField(SerializedProperty property)
        {
            if (fieldInfo.FieldType == typeof(float))
            {
                return new SpeedControlledFloatField
                {
                    label = property.displayName,
                    value = property.floatValue
                };
            }

            throw new ArgumentException(
                $"Invalid FieldType {fieldInfo.FieldType} for attribute on field {fieldInfo.Name} on type {fieldInfo.DeclaringType}",
                nameof(fieldInfo.FieldType));
        }

        #endregion
    }
}