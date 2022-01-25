using UnityEngine;
using Zenject;

public class LoadInstaller : MonoInstaller
{
    [SerializeField] private DummyScript _dummyScript;
    [SerializeField] private DataScriptable _ScriptableObject;
    [SerializeField] bool _fromScriptableObject;
    public override void InstallBindings()
    {
        if (_fromScriptableObject == true)
        {
            Container.Bind<string>().FromInstance(_ScriptableObject.LoadString);
            Container.Bind<Sprite>().FromInstance(_ScriptableObject.LoadSprite);
        }
        else
        {
            Container.Bind<string>().FromInstance(_dummyScript.LoadString);
            Container.Bind<Sprite>().FromInstance(_dummyScript.LoadSprite);
        }
    }
}