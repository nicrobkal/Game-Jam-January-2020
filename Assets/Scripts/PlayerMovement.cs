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

    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        walkSpeed = (float)(playerStats.speed + (playerStats.agility / 5));
        sprintSpeed = walkSpeed + (walkSpeed / 2);
    }

    void FixedUpdate()
    {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        // Move
        if (Input.GetKey(KeyCode.LeftShift) && playerStats.currStamina > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * sprintSpeed, 0.8f),
                                             Mathf.Lerp(0, Input.GetAxis("Vertical") * sprintSpeed, 0.8f));
            playerStats.currStamina -= Time.deltaTime;
            staminaRecharging = false;

            if(playerStats.currStamina <= 0)
            {
                playerStats.currStamina = 0;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f),
                                             Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
            staminaRecharging = true;
        }

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
