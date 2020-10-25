using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Script_Health : MonoBehaviour
{
    public int hit_points = 2;
    public bool is_enemy = true;
    public GameObject frozen;
    public GameObject fire;

    public Text Lives;

    bool onFire = false;
    int fireCounter = 0;

    static int spawnFrequency = 1000;

    private void Start()
    {
        if (!is_enemy)
        {
            Lives.GetComponent<Text>();
            Lives.text = "Remaining Lifepoints: " + hit_points;
        }
    }

    private void Update()
    {
        if (onFire && fireCounter == 1000)
        {
            hit_points--;
            fireCounter = 0;

            if (hit_points == 0)
            {
                GameObject fireInstance = Instantiate(frozen, transform.position, transform.rotation);
                Destroy(fireInstance, 5f);
                fireInstance.GetComponent<Script_Move>().direction = new Vector2(4, 5);
            }
        }
        fireCounter++;

        spawnFrequency--;
    }

    void OnTriggerEnter2D(Collider2D collider) // When one physics body hits another one we get this
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
            if (collider.gameObject == enemy && !is_enemy)
            {
                hit_points--; ;
                Lives.text = "Remaining Lifepoints: " + hit_points;
                if (hit_points == 0)
                {
                    Destroy(this.gameObject);
                    GameOverScreen();
                }


                return;
            }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyShot");

        foreach (GameObject bullet in bullets)
            if (collider.gameObject == bullet && is_enemy)
                return;

        foreach (GameObject bullet in bullets)
            if (collider.gameObject == bullet && !is_enemy)
            {
                hit_points -= bullet.GetComponent<Script_Shot>().damage;
                Lives.text = "Remaining Lifepoints: " + hit_points;
                if (hit_points == 0)
                {
                    Destroy(this.gameObject);
                    GameOverScreen();
                }

                return;
                
            }

        bullets = GameObject.FindGameObjectsWithTag("PlayerShot");

        foreach (GameObject bullet in bullets)
            if (collider.gameObject == bullet && !is_enemy)
                return;
            
        
        foreach (GameObject bullet in bullets)
            if (collider.gameObject == bullet && is_enemy)
            {

                hit_points -= bullet.GetComponent<Script_Shot>().damage;
                GameObject fireInstance = Instantiate(fire, transform.position, transform.rotation);
                if (hit_points == 0)
                {
                    Destroy(this.gameObject);
                    Destroy(fireInstance, 5f);
                    fireInstance.GetComponent<Script_Move>().direction = new Vector2(-0.5f, -0.5f);
                }
                else
                {
                    onFire = true;
                    fireInstance.GetComponent<Script_Move>().speed = new Vector2(9,9);
                    Destroy(fireInstance, 2f);
                    StartCoroutine(waitFire());
                }
                return;
            }

        bullets = GameObject.FindGameObjectsWithTag("IceShot");

        foreach (GameObject bullet in bullets)
            if (collider.gameObject == bullet && !is_enemy)
                return;

        foreach (GameObject bullet in bullets)
            if (collider.gameObject == bullet && is_enemy)
            {
                hit_points -= bullet.GetComponent<Script_Shot>().damage;
                GameObject frozenInstance = Instantiate(frozen, transform.position, transform.rotation);

                if (hit_points <= 0)
                {
                    Destroy(this.gameObject);
                    Destroy(frozenInstance, 5f);
                    frozenInstance.GetComponent<Script_Move>().direction = new Vector2(4, 5);
                }
                else
                {
                    this.GetComponent<Script_Move>().speed = new Vector2(0, 0);
                    Destroy(frozenInstance, 2f);
                    StartCoroutine(waitIce());
                }
                return;
            }

    }

    IEnumerator waitIce()
    {
        yield return new WaitForSeconds(2f);
        this.GetComponent<Script_Move>().speed = new Vector2(10, 10);
        this.gameObject.SetActive(true);
        
    }

    IEnumerator waitFire()
    {
        yield return new WaitForSeconds(2f);
        onFire = false;
        fireCounter = 0;
    }

    void GameOverScreen()
    {
        SceneManager.LoadScene("Level_1");
    }
}
