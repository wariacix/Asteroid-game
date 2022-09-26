using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGain : MonoBehaviour
{
    [SerializeField]
    private float energyGainLocal = 0.5f;

    Ship ship;

    // Start is called before the first frame update
    void Awake()
    {
        ship = gameObject.GetComponentInParent<Ship>();
        ship.setEnergyGain(ship.getEnergyGain() + energyGainLocal);
    }
}
