using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    Image bar;

    [SerializeField]
    private Ship ship;

    void Start()
    {
        bar = GetComponent<Image>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        bar.fillAmount = ship.getEnergy() / ship.getEnergyLimit();
    }
}
