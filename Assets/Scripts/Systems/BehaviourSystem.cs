using Unity.Entities;
using UnityEngine;

public partial class BehaviourSystem : SystemBase
{
    private float _timer;
    private float _maxPriority = 0;
    protected override void OnUpdate()
    {
        _timer += Time.DeltaTime;
        if (Mathf.Round(_timer) % 2 == 0)
        {
            CheckBehaviour();
        }
    }

    private void CheckBehaviour()
    {
        Entities.ForEach(
            (Entity entity, in BehaviourManager behaviourManager) =>
            {
                _maxPriority = 0;
                foreach(var behaviour in behaviourManager.AllBehaviours)
                {
                    if (behaviour is IBehaviour iBehaviour)
                    {
                        if (iBehaviour.Importance() > _maxPriority)
                        {
                            _maxPriority = iBehaviour.Importance();
                            
                            behaviourManager.CurrentBehave = iBehaviour;
                        }
                    }
                }

                behaviourManager.CurrentBehave.Execution();

            }).WithoutBurst().Run();
    }
}
