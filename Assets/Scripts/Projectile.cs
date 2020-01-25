using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan = 5f;
    private float currLifespan = 0;
    public int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject ammoPickup in GameObject.FindGameObjectsWithTag("Ammo Pickup"))
        {
            Physics2D.IgnoreCollision(ammoPickup.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currLifespan += Time.deltaTime;

        if(currLifespan > lifespan)
        {
            Destroy(gameObject);
        }
    }
}
