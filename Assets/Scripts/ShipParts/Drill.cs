using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    private Ship ship;
    [SerializeField]
    private Item item;

    // Start is called before the first frame update
    void Start()
    {
        ship = gameObject.GetComponentInParent<Ship>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DigAsteroid(collision);
    }

    public void DigAsteroid(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            if (ship.getCargo() <= ship.getCargoLimit())
            {
                ship.setCargo(ship.getCargo() + 1);

                ship.ShipInventory.AddItem(item, 1);

                Destroy(collision.gameObject);
            }
        }
    }
}
