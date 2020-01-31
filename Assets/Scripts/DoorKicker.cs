using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKicker : MonoBehaviour
{
    public float kickForce = 200f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PC")
        {
            float angle = 45;
            if ((collision.relativeVelocity.y > 1 || collision.relativeVelocity.x > 1) && Vector3.Angle(-collision.gameObject.transform.right, transform.position - collision.gameObject.transform.position) < angle)
            {

                float playerDir = collision.gameObject.transform.rotation.x;

                gameObject.layer = LayerMask.NameToLayer("Litter");
                GetComponent<Rigidbody2D>().isKinematic = false;

                GetComponent<AudioSource>().Play();

                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

                // Calculate Angle Between the collision point and the player
                Vector2 dir = collision.GetContact(0).point - new Vector2(Mathf.Round(transform.position.x / 90) * 90, Mathf.Round(transform.position.y / 90) * 90);

                // We then get the opposite (-Vector3) and normalize it
                dir = dir.normalized;

                // And finally we add force in the direction of dir and multiply it by force. 
                // This will push back the player
                GetComponent<Rigidbody2D>().velocity = dir * kickForce;

                Vector2 size = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 4, transform.localScale.z);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
