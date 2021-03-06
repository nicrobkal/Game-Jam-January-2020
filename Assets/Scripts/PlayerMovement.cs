﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    void Update()
    {
        Debug.Log(Time.frameCount);
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

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
