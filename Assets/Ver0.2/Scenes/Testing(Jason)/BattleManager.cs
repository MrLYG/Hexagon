using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;

    public GameObject getWeapon(string weaponName)
    {
        foreach (GameObject gameObject in weapons)
        {
            if (gameObject.name.Equals(weaponName))
                return gameObject;
        }
        return null;
    }
}
