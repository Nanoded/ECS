using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class BehaviourManager : MonoBehaviour
{
    public List<MonoBehaviour> AllBehaviours;
    [HideInInspector] public IBehaviour CurrentBehave;
}