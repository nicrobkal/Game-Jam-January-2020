using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health = 25;
    public float speed = 1.6f;
    public int damage = 5;
    public float damageCooldown = 0.5f;
    private float currDamageCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AIPath>().maxSpeed = speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PC" && currDamageCooldown >= damageCooldown)
        {
            collision.gameObject.GetComponent<PlayerStats>().currHealth -= damage;
            currDamageCooldown = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health -= collision.gameObject.GetComponent<Projectile>().damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currDamageCooldown < damageCooldown)
        {
            currDamageCooldown += Time.deltaTime;
        }

        if(health <= 0)
        {
            //Play Death Animation
            Destroy(gameObject);
        }
    }
}
