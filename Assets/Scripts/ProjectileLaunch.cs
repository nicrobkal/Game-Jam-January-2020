using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 100;
    public float attackSpeed = 5f;
    private float coolDown = 0;
    public float numBursts = 3;
    private float currBurstNum;
    public float burstInterval = 0.1f;
    private float currBurstInterval;
    public float pushbackForce = 20f;
    public float pushbackShake = 1;
    private PlayerStats playerStats;
    public AudioClip bulletNoise;

    // Start is called before the first frame update
    void Start()
    {
        currBurstNum = numBursts;
        currBurstInterval = 0;
        playerStats = GetComponent<PlayerStats>();
    }

    void Fire(bool isFirstShotInBurst)
    {
        Rigidbody2D bulletInstance = Instantiate(bulletPrefab, new Vector3(transform.localPosition.x, transform.localPosition.y), transform.rotation) as Rigidbody2D;

        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        bulletInstance.velocity = transform.right * bulletSpeed * -1;

        GetComponent<AudioSource>().PlayOneShot(bulletNoise);

        GetComponent<Rigidbody2D>().AddForce(transform.right * pushbackForce * pushbackShake);

        if (isFirstShotInBurst)
        {
            pushbackShake = 1;
            currBurstNum = 1;
        }
        else
        {
            pushbackShake *= -1;
        }

        playerStats.currentAmmoCount--;
    }

    // Update is called once per frame
    void Update()
    {
        if (0 > coolDown)
        {
            if (Input.GetMouseButtonDown(0) && playerStats.currentAmmoCount > 0)
            {
                Fire(true);
                coolDown = attackSpeed;
            }
        }

        if(coolDown >= 0)
        {
            coolDown -= Time.deltaTime;
        }

        if(numBursts > 1 && currBurstNum < numBursts)
        {
            if(currBurstInterval < burstInterval)
            {
                currBurstInterval += Time.deltaTime;
            }
            else
            {
                Fire(false);

                currBurstInterval = 0;
                currBurstNum++;
            }
        }
    }
}
