using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName="Character_", menuName="Character Preset")]
public class CustomerData : ScriptableObject
{
    public Sprite[] randomHeadSprites;
    public Sprite[] randomTorsoSprites;

    [Tooltip("Acceptable range of price change in relation to original price")]
    public Vector2 targetRelativePriceRange;
    public float frequency;

    [Multiline][SerializeField] private string _randomRequestTexts;

    [ReadOnly] public string[] randomRequestTexts;

    public void PreInit()
    {
        randomRequestTexts = _randomRequestTexts.Split('\n');
    }
}
