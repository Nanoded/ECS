using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    [SerializeField] private GameObject _prefabThrowingWeapon;
    [SerializeField] private GameObject _weapon;

    private void Start()
    {
        _weapon.SetActive(false);
    }

    /// <summary>
    /// For transition animation between throw animation and idle animation
    /// </summary>
    public void GetWeapon()
    {
        _weapon.SetActive(true);
    }

    /// <summary>
    /// For transition animation between idle animation and throw animation
    /// </summary>
    public void HideWeapon()
    {
        _weapon.SetActive(false);
    }

    /// <summary>
    /// For throw animation
    /// </summary>
    public void Throw()
    {
        GameObject newThrowingWeapon = Instantiate(_prefabThrowingWeapon, _weapon.transform.position, Quaternion.identity);
    }
}
