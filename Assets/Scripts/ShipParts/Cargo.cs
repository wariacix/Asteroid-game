using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    [SerializeField]
    private int addedLimit = 40;
    private Ship ship;
    // Start is called before the first frame update
    void Awake()
    {
        ship = gameObject.GetComponentInParent<Ship>();
        ship.setCargoLimit(ship.getCargoLimit() + addedLimit);
    }
}
