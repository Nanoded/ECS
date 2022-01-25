using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject", order = 10)]
public class DataScriptable : ScriptableObject
{
    public string LoadString;
    public Sprite LoadSprite;
}
