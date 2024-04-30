using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDemageble
{
    [SerializeField] private int healfh;

    public void ApplyDamage(int damageValue)
    {
        healfh -= damageValue;
    }
}
