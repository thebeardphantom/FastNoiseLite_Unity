using BeardPhantom.FastNoiseLite;
using System.Collections.Generic;
using System.Linq;
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
        var occurances = new Dictionary<float, int>();
        var position = transform.position;
        var seedBase = NoiseGeneratorAsset.Config.Seed;
        for (var i = 0; i < 1000; i++)
        {
            var noise01 = NoiseGeneratorAsset.GetNoise(position.x, position.y, position.z, seedBase + i);
            occurances.TryGetValue(noise01, out var count);
            count++;
            occurances[noise01] = count;
        }

        Debug.Log(string.Join("\n", occurances.OrderByDescending(kvp => kvp.Value)));
    }

    #endregion
}