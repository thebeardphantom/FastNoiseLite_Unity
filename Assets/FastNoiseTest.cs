using FastNoise;
using System.Runtime.InteropServices;
using UnityEngine;

public class FastNoiseTest : MonoBehaviour
{
    #region Properties

    [field: SerializeField]
    private NoiseGeneratorConfig NoiseGeneratorConfig { get; set; }

    [field: SerializeField]
    private NoiseGeneratorAsset NoiseGeneratorAsset { get; set; }

    #endregion

    #region Methods

    private void Start()
    {
        var position = transform.position;
        Debug.Log(NoiseGeneratorAsset.GetNoise01(position.x, position.y, position.z));
    }

    #endregion
}