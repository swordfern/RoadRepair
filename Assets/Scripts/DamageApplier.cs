using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageApplier : MonoBehaviour
{
    [SerializeField] private int _damageAmount;
    public int DamageAmount => _damageAmount;
}
