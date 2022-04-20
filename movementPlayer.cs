using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andrehard.CharacterStats;
public class movementPlayer : MonoBehaviour
{
    public CharacterController controller;
    public Joystick joystick;
    public Character player;
    
    

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
       
    void Update()
    {
        /*float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");*/

        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        float speed = player.Speed.BaseValue;
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
