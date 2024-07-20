using UnityEngine;

[CreateAssetMenu(fileName="Character_", menuName="Character Preset")]
public class CustomerData : ScriptableObject
{
    public Sprite[] randomHeadSprites;
    public Sprite[] randomTorsoSprites;

    [Tooltip("Acceptable range of price change in relation to original price")]
    public Vector2 targetRelativePriceRange;
    public float frequency;
}
