using UnityEngine;

[CreateAssetMenu(fileName="Character_", menuName="Character Preset")]
public class CustomerData : ScriptableObject
{
    public Sprite[] randomHeadSprites;

    [Tooltip("Acceptable range of price change in relation to original price")]
    public Vector2 targetRelativePriceRange;
    public float frequency;

    [Multiline(50)][SerializeField] private string _randomRequestTexts;

    [HideInInspector] public string[] randomRequestTexts;

    public void PreInit()
    {
        randomRequestTexts = _randomRequestTexts.Split('\n');
    }
}
