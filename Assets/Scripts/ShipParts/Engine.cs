using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private Ship ship;
    [SerializeField]
    private float engineSpeed = 0.25f;
    [SerializeField]
    private float engineCost = 0.5f;
    [SerializeField]
    Sprite newTexture, oldTexture;

    SpriteRenderer sprRender;

    // Start is called before the first frame update
    private void Awake()
    {
        sprRender = gameObject.GetComponent<SpriteRenderer>();
        ship = gameObject.GetComponentInParent<Ship>();
        ship.setThrust(ship.getThrust() + engineSpeed);
        ship.setThrustCost(ship.getThrustCost() + engineCost);
    }

    private void Update()
    {
        float verAxis = Input.GetAxisRaw("Vertical");
        if (verAxis > 0) sprRender.sprite = newTexture;
        else sprRender.sprite = oldTexture;
    }
}
