using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class movement : MonoBehaviour
{
    public float movingSpeed;
    public float WalkingSpeed = 5f;
    public float SneakSpeed = 2.5f;
    public float SprintSpeed = 7f;
    public float maxStamina;
    public float stamina = 5f;
    public Rigidbody2D rb;
    public Vector2 movemented;
    bool staminaRecharge = false;
    public Slider staminaSlider;
    public Image SliderFill;

    // Update is called once per frame
    void Update()
    {
        movemented.x = Input.GetAxisRaw("Horizontal");
        movemented.y = Input.GetAxisRaw("Vertical");

        StaminaManagement();

        if (Input.GetButton("Sneak"))
        {
            movingSpeed = SneakSpeed;
        }else if (Input.GetButton("Sprint") && stamina > 0 && !staminaRecharge && movemented != Vector2.zero)
        {
            movingSpeed = SprintSpeed;
            stamina -= Time.fixedDeltaTime;
            if(stamina <= 0)
            {
                staminaRecharge = true;
                movingSpeed = 5;
            }
        }
        else
        {
            movingSpeed = WalkingSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movemented * movingSpeed * Time.fixedDeltaTime);
    }


    public void StaminaManagement()
    {
        if (stamina <= maxStamina && !Input.GetButton("Sprint") || staminaRecharge || movemented == Vector2.zero)
        {
            stamina += Time.fixedDeltaTime;
        }
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        if (stamina >= maxStamina)
        {
            staminaRecharge = false;
        }
        staminaSlider.value = stamina / maxStamina;
        if (staminaRecharge)
        {
            SliderFill.color = new Color(1f, 0f, 0f);
        }
        else
        {
            SliderFill.color = new Color(1 - stamina / maxStamina, stamina / maxStamina, 0f);
        }
    }
}
