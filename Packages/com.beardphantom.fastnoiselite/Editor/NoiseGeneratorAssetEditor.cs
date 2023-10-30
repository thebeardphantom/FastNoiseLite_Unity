using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BeardPhantom.FastNoiseLite.Editor
{
    [CustomEditor(typeof(NoiseGeneratorAsset))]
    public class NoiseGeneratorAssetEditor : UnityEditor.Editor
    {
        #region Fields

        private const int RESOLUTION = 128;

        private static readonly string _configBackingField = $"<{nameof(NoiseGeneratorAsset.Config)}>k__BackingField";

        private static bool _usePreviewSeedOverride;

        private static int _previewSeedOverride = 1;

        private static float _previewScale = 1f;

        private static Vector2 _sampleOffset;

        private readonly Color32[] _colors = new Color32[RESOLUTION * RESOLUTION];

        private Texture2D _previewTexture;

        private IntegerField _previewSeedField;

        private Vector2Field _previewOffsetField;

        private FloatField _previewScaleField;

        private Toggle _usePreviewSeedOverrideToggle;
        private VisualElement _root;
        private SerializedProperty _frequencyProperty;
        private SpeedControlledFloatField _frequencyFloatField;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override bool HasPreviewGUI()
        {
            if (_previewTexture == null)
            {
                _previewTexture = new Texture2D(RESOLUTION, RESOLUTION);
            }

            return true;
        }

        /// <inheritdoc />
        public override GUIContent GetPreviewTitle()
        {
            return new GUIContent("Noise Preview");
        }

        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();
            _root.RegisterCallback<SerializedPropertyChangeEvent>(OnSerializedPropertyChangeEvent);
            _root.RegisterCallback<IMGUIEvent>(OnIMGUIEvent);

            var scriptPropertyField = new PropertyField(serializedObject.FindProperty("m_Script"));
            scriptPropertyField.SetEnabled(false);
            _root.Add(scriptPropertyField);

            var serializedProperty = serializedObject.FindProperty(_configBackingField);
            foreach (SerializedProperty childProperty in serializedProperty)
            {
                _root.Add(
                    new PropertyField(childProperty)
                    {
                        name = childProperty.propertyPath,
                        userData = childProperty
                    });
            }

            // Preview seed
            var previewSeedRoot = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    marginTop = new Length(16f, LengthUnit.Pixel)
                }
            };
            {
                _usePreviewSeedOverrideToggle = new Toggle
                {
                    value = _usePreviewSeedOverride
                };
                _usePreviewSeedOverrideToggle.RegisterValueChangedCallback(OnUsePreviewSeedOverrideValueChanged);
                previewSeedRoot.Add(_usePreviewSeedOverrideToggle);

                _previewSeedField = new IntegerField("Preview Seed")
                {
                    value = _previewSeedOverride,
                    style =
                    {
                        paddingLeft = new Length(8, LengthUnit.Pixel),
                        flexGrow = 1f
                    }
                };
                _previewSeedField.SetEnabled(_usePreviewSeedOverride);
                _previewSeedField.RegisterValueChangedCallback(OnSeedFieldValueChanged);
                previewSeedRoot.Add(_previewSeedField);
            }
            _root.Add(previewSeedRoot);

            // Preview offset
            _previewOffsetField = new Vector2Field("Preview Offset")
            {
                value = _sampleOffset
            };
            _previewOffsetField.RegisterValueChangedCallback(OnSampleOffsetFieldValueChanged);
            _root.Add(_previewOffsetField);

            // Preview scale
            _previewScaleField = new FloatField("Preview Scale")
            {
                value = _previewScale
            };
            _previewScaleField.RegisterValueChangedCallback(OnZoomFieldValueChanged);
            _root.Add(_previewScaleField);

            return _root;
        }

        /// <inheritdoc />
        public override void DrawPreview(Rect previewArea)
        {
            GUI.DrawTexture(previewArea, _previewTexture, ScaleMode.ScaleToFit);
        }

        private void OnIMGUIEvent(IMGUIEvent evt)
        {
            Debug.Log(evt.imguiEvent.type);
        }

        private void OnUsePreviewSeedOverrideValueChanged(ChangeEvent<bool> evt)
        {
            _usePreviewSeedOverride = evt.newValue;
            _previewSeedField.SetEnabled(_usePreviewSeedOverride);
            RegenerateTexture();
        }

        private void OnZoomFieldValueChanged(ChangeEvent<float> evt)
        {
            _previewScale = evt.newValue;
            RegenerateTexture();
        }

        private void OnSampleOffsetFieldValueChanged(ChangeEvent<Vector2> evt)
        {
            _sampleOffset = evt.newValue;
            RegenerateTexture();
        }

        private void OnSeedFieldValueChanged(ChangeEvent<int> evt)
        {
            _previewSeedOverride = evt.newValue;
            RegenerateTexture();
        }

        private void OnSerializedPropertyChangeEvent(SerializedPropertyChangeEvent evt)
        {
            RegenerateTexture();
        }

        private void OnEnable()
        {
            _previewTexture = new Texture2D(RESOLUTION, RESOLUTION, TextureFormat.RGBA32, false);
        }

        private void OnDestroy()
        {
            if (_previewTexture != null)
            {
                DestroyImmediate(_previewTexture);
            }
        }

        private void RegenerateTexture()
        {
            var noiseGenerator = (NoiseGeneratorAsset)target;
            var index = 0;
            for (var y = 0; y < RESOLUTION; y++)
            {
                for (var x = 0; x < RESOLUTION; x++)
                {
                    var sampleX = (x + _sampleOffset.x) * _previewScale;
                    var sampleY = (y + _sampleOffset.y) * _previewScale;
                    if (_usePreviewSeedOverride)
                    {
                        _colors[index++] = noiseGenerator.SampleColor32(sampleX, sampleY, _previewSeedOverride);
                    }
                    else
                    {
                        _colors[index++] = noiseGenerator.SampleColor32(sampleX, sampleY);
                    }
                }
            }

            _previewTexture.SetPixels32(_colors);
            _previewTexture.Apply(false);
        }

        #endregion
    }
}