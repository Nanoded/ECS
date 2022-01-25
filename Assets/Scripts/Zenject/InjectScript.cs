using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InjectScript : MonoBehaviour
{
    [Inject] private void LoadData(string LoadString, Sprite LoadSprite)
    {
        GetComponent<Image>().overrideSprite = LoadSprite;
        Debug.Log(LoadString);
    }
}
