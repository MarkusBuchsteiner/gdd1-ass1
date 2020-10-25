using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    public int damage = 1; // how much damage the shot can do

    public Vector2 speed = new Vector2(50, 50);

    void Update() // Update is called once per frame
    {
        float y_in = Input.GetAxis ("Vertical");
        float x_in = Input.GetAxis ("Horizontal");

        Vector3 move = new Vector3(speed.x * x_in, speed.y * y_in, 0); // z == 0
        move = move * Time.deltaTime; // smooth movement on screen
        transform.Translate(move); // move sprite itself

        // CODE FOR FIRE SHOT:

        bool shoot = Input.GetButtonDown("Fire1");
        // shoot |= Input.GetButtonDown("Fire2");

        if (shoot)
        {
          Script_Weapon weapon = GetComponent<Script_Weapon>();
          if (weapon != null)
          {
            weapon.Attack(false);
          }
        }

        // CODE FOR ICE SHOT:

        bool shoot_ice = Input.GetButtonDown("Fire2");
        if (shoot_ice)
        {
          Script_Weapon_ice weapon = GetComponent<Script_Weapon_ice>();
          if (weapon != null)
          {
            weapon.Attack(false);
          }
        }
    }
}
