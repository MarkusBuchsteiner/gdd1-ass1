using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Shot : MonoBehaviour // is used for both: good and bad characters
{
    public int damage = 1; // how much damage the shot can do
    public bool enemy_is_shot = false;

    void Start() // Start is called before the first frame update
    {
        Destroy(gameObject, 20); // Shot is destroyed automatically after 20 sec
    }
}
