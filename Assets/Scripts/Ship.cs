using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100.0f;
    [SerializeField]
    private float thrust = 0.75f;
    [SerializeField]
    private float thrustCost = 2.5f;

    [SerializeField]
    private float health = 100.0f;
    float horAxis, verAxis;
    public Rigidbody2D shipRb;
    [SerializeField]
    private float energyLimit = 1000.0f;
    [SerializeField]
    private float energy = 1000.0f;
    [SerializeField]
    private float energyGain = 0.2f;

    private float cargoLimit = 256.0f;
    private float cargo = 0.0f;

    private Inventory shipInventory;
    public Inventory ShipInventory
    {
        get { return shipInventory; }
        set { shipInventory = value; }
    }

    public void setCargo(float addedValue)
    {
        cargo = addedValue;
    }

    public float getCargo()
    {
        return cargo;
    }

    public void setCargoLimit(float addedValue)
    {
        cargoLimit = addedValue;
    }

    public float getCargoLimit()
    {
        return cargoLimit;
    }
    public void setThrust(float addedValue)
    {
        thrust = addedValue;
    }

    public float getThrust()
    {
        return thrust;
    }

    public void setThrustCost(float addedValue)
    {
        thrustCost = addedValue;
    }

    public float getThrustCost()
    {
        return thrustCost;
    }
    public void setEnergyGain(float addedValue)
    {
        energyGain = addedValue;
    }

    public float getEnergyGain()
    {
        return energyGain;
    }
    public void setEnergy(float addedValue)
    {
        energy = addedValue;
    }

    public float getEnergy()
    {
        return energy;
    }
    public void setEnergyLimit(float addedValue)
    {
        energyLimit = addedValue;
    }

    public float getEnergyLimit()
    {
        return energyLimit;
    }

    private void Awake()
    {
        ShipInventory = gameObject.GetComponent<Inventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shipRb = GetComponent<Rigidbody2D>();
        shipRb.rotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        horAxis = Input.GetAxisRaw("Horizontal");
        verAxis = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shipRb.velocity = new Vector2(0f, 0f);
        }
        CalcEnergyGain();
    }
    void FixedUpdate()
    {
        if (horAxis != 0f) shipRb.rotation = shipRb.rotation + -horAxis * rotationSpeed * Time.fixedDeltaTime;
        if (energy > 0f && verAxis != 0)
        {
            PushShip();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        shipRb.angularVelocity = 0;
    }

    private void PushShip()
    {
        shipRb.AddForce(transform.up * verAxis * thrust);
        energy = energy - thrustCost * Time.fixedDeltaTime * Mathf.Abs(verAxis);
    }

    private void CalcEnergyGain()
    {
        if (energy <= energyLimit) energy += energyGain * Time.deltaTime;
        else energy = energyLimit;
    }
}
