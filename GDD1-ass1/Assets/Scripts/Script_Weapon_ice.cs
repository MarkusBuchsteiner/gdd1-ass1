using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Weapon_ice : MonoBehaviour
{
    public Transform shot_prefab; // Projectile prefab for Ice shooting
    public float shoot_rate = 0.25f; // pause between 2 shots

    // pause section (gun should not be fired to often):

    private float shoot_pause;

    void Start()
    {
       shoot_pause = 0f;
    }

    void Update()
    {
       if (shoot_pause > 0)
       {
          shoot_pause = shoot_pause - Time.deltaTime;
       }
    }

    // shooting from another script:

    public bool can_attack // check if the weapon is ready to create a new projectile
    {
      get
      {
        return shoot_pause <= 0f;
      }
    }

    public void Attack(bool is_enemy)
    {
      if (can_attack)
      {
        shoot_pause = shoot_rate;
        
        var shot_transform = Instantiate(shot_prefab) as Transform; // Create a new shot

        shot_transform.position = transform.position; // Assign position

        Script_Shot shot = shot_transform.gameObject.GetComponent<Script_Shot>(); // Is enemy property

        if (shot != null)
        {
          shot.enemy_is_shot = is_enemy;
        }

        // make the weapon shot always towards it
        Script_Move move = shot_transform.gameObject.GetComponent<Script_Move>();

        if (move != null)
        {
          move.direction = this.transform.right; // towards in 2D space is the right of the sprite
        } 
      }
    }
}
