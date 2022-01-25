using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class WeaponComponent : MonoBehaviour
{ 
    public GameObject BulletPrefab;

    public void Shoot(Transform transform, Quaternion quaternion)
    {
        var newBullet = Instantiate(BulletPrefab, transform.localPosition + new Vector3(0, 0.5f, 0), quaternion);
    }
}



