using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Enemy : MonoBehaviour
{
    private Script_Weapon[] weapons;

    // Start is called before the first frame update
    void Awake()
    {
        weapons = GetComponentsInChildren<Script_Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Script_Weapon weapon in weapons)
        {
          if (weapon != null && weapon.can_attack)
          {
            weapon.Attack(true);
          }
        }
        
    }
}
