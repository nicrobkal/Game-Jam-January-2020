using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float walkSpeed;
    private float sprintSpeed;
    private float curSpeed;
    private float maxSpeed;
    public float staminaCooldown = 5f;
    private float currStaminaCooldown = 0f;
    private bool staminaRecharging = true;
    private float horizontalMovementSpeed;
    private float verticalMovementSpeed;

    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        maxSpeed = sprintSpeed;
        walkSpeed = playerStats.speed;
        sprintSpeed = walkSpeed + (walkSpeed / 2);

        Physics2D.IgnoreLayerCollision(10, 12);
        Physics2D.IgnoreLayerCollision(9, 12);
    }

    void Update()
    {
        horizontalMovementSpeed = Input.GetAxis("Horizontal");
        verticalMovementSpeed = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            curSpeed = sprintSpeed;
            playerStats.currStamina -= Time.deltaTime;
            staminaRecharging = false;

            if (playerStats.currStamina <= 0)
            {
                playerStats.currStamina = 0;
                curSpeed = walkSpeed;
            }
        }
        else
        {
            curSpeed = walkSpeed;
            staminaRecharging = true;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, horizontalMovementSpeed * curSpeed, 0.8f),
            Mathf.Lerp(0, verticalMovementSpeed * curSpeed, 0.8f));

        if(staminaRecharging && currStaminaCooldown < staminaCooldown)
        {
            currStaminaCooldown += Time.deltaTime;
        }

        if(currStaminaCooldown >= staminaCooldown)
        {
            playerStats.currStamina += playerStats.staminaRechargeRate * Time.deltaTime;
        }

        if(playerStats.currStamina > playerStats.maxStamina)
        {
            playerStats.currStamina = playerStats.maxStamina;
        }
    }
}
