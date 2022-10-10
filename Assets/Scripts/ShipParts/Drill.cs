using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    private Ship ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = gameObject.GetComponentInParent<Ship>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DigAsteroid(collision);
    }

    public void DigAsteroid(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            if (ship.getCargo() <= ship.getCargoLimit())
            {
                ship.setCargo(ship.getCargo() + 1);

                SquareItemSlot newItem = collision.gameObject.GetComponent<SquareItemSlot>();

                ship.ShipInventory.AddItem(newItem.Item, Random.Range(1, 4));

                Destroy(collision.gameObject);
            }
        }
    }
}
