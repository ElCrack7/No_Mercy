using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator anim;
    public float totalHealth;
    private bool dead;
    public float currentHealth;
    public float expGranted;
    public float atkDamage;
    public float atkSpeed;
    public float moveSpeed;

    public GameObject[] Players;
    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    public void GetHit(float damage)
    {
        if (dead) return;
        anim.SetInteger("Condition", 3);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(RecoverFromHit());

    }


    void Die()
    {
        dead = true;
        DropLoot();
        foreach (GameObject pc in Players)
        {
            pc.GetComponent<PlayerController>().SetExperience(expGranted/Players.Length);
        }
        anim.SetInteger("Condition", 4);
        GameObject.Destroy(this.gameObject, 3);
    }

    void DropLoot()
    {
        print("You get the bounty");
    }
    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition",0);
    }
}
