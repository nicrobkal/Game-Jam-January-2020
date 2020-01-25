using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoCapacity = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PC")
        {
            collision.gameObject.GetComponent<PlayerStats>().currentAmmoCount += ammoCapacity;
            if(collision.gameObject.GetComponent<PlayerStats>().currentAmmoCount > collision.gameObject.GetComponent<PlayerStats>().ammoCapacity)
            {
                collision.gameObject.GetComponent<PlayerStats>().currentAmmoCount = collision.gameObject.GetComponent<PlayerStats>().ammoCapacity;
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
