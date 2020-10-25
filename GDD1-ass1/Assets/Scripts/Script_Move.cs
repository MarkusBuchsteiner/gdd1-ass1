using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Move : MonoBehaviour
{
    public Vector2 direction = new Vector2(-1, 0);
    public Vector2 speed = new Vector2(10, 10);
    
    void Update() // Update is called once per frame
    {
        Vector3 move = new Vector3(speed.x * direction.x, speed.y * direction.y, 0); // z == 0

        move = move * Time.deltaTime; // smooth movement on screen

        transform.Translate(move); // move sprite itself
    }
}
