using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_Scrolling_BG : MonoBehaviour
{
    public Vector2 speed = new Vector2(7, 7);
    public Vector2 direction = new Vector2(-1, 1);
    public bool linked_to_camera = false;
    public bool is_looping = false;

    private List<SpriteRenderer> bg_part;
   
    void Start()
    {
      if (is_looping == true)
      {
        bg_part = new List<SpriteRenderer>();

        for (int i = 0; i < transform.childCount; i++)
        {
          Transform child = transform.GetChild(i);
          SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();

           if (renderer != null)
           {
             bg_part.Add(renderer);
           }
        }

        bg_part = bg_part.OrderBy(t => t.transform.position.x).ToList();
      }
    }


    // Update is called once per frame
    void Update()
    {
        // Moving:
        Vector3 moving = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);
        moving = moving * Time.deltaTime;
        transform.Translate(moving);

        // Camera Moving:
        if (linked_to_camera == true)
        {
          Camera.main.transform.Translate(moving);
        }

        if (is_looping == true)
        {
          SpriteRenderer firstChild = bg_part.FirstOrDefault();
   
          if (firstChild != null)
          {
            if (firstChild.transform.position.x < Camera.main.transform.position.x)
            {
               if (firstChild.IsVisibleFrom(Camera.main) == false)
               {
                 SpriteRenderer lastChild = bg_part.LastOrDefault();

                 Vector3 last_position = lastChild.transform.position;
                 Vector3 last_size = (lastChild.bounds.max - lastChild.bounds.min);

                 firstChild.transform.position = new Vector3(last_position.x + last_size.x, firstChild.transform.position.y, firstChild.transform.position.z);

                 bg_part.Remove(firstChild);
                 bg_part.Add(firstChild);
               }
            }
          }
        }
    }
}
