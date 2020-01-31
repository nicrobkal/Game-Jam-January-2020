using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public ParticleSystem BloodPoolPrefab;
    public ParticleSystem BloodSpurtPrefab;
    public int health = 25;
    public float speed = 1.6f;
    public int damage = 5;
    public float damageCooldown = 0.5f;
    public float approachPlayerDistance = 0.5f;
    private float currDamageCooldown = 0;
    public bool hasBeenProvoked = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AIPath>().maxSpeed = speed;

        foreach(GameObject ammoPickup in GameObject.FindGameObjectsWithTag("Ammo Pickup"))
        {
            Physics2D.IgnoreCollision(ammoPickup.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
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
            hasBeenProvoked = true;
            health -= collision.gameObject.GetComponent<Projectile>().damage;
            ParticleSystem BloodPoolInstance = Instantiate(BloodPoolPrefab, new Vector3(transform.localPosition.x, transform.localPosition.y), transform.rotation) as ParticleSystem;
            BloodPoolInstance.Play();
            ParticleSystem BloodSpurtInstance = Instantiate(BloodSpurtPrefab, new Vector3(transform.localPosition.x, transform.localPosition.y), transform.rotation) as ParticleSystem;
            BloodSpurtInstance.Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("PC").transform.position) <= approachPlayerDistance || hasBeenProvoked)
        {
            GetComponent<AIPath>().canSearch = true;
            hasBeenProvoked = true;
        }
        else
        {
            GetComponent<AIPath>().canSearch = false;
        }

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
