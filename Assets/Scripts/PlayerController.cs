using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    // private int level = 1;
    // private Text levelText;
    public float experience;
    // public float experience { get; private set; }
    // private Transform experienceBar;

    [Header("Movement")]
    private bool canMove = true;
    public float movementSpeed;
    public float velocity;
    public Rigidbody rb;
    public bool active = false;

    [Header("Combat")]
    private List<Transform> enemiesInRange = new List<Transform>();
    private bool canAttack = true;
    private bool attacking;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;

    // Use this for initialization
    void Start()
    {
        //AnimationEvents.OnSlashAnimationHit += DealDamage;
        //experienceBar = UIController.instance.transform.Find("Background/Experience");
        //levelText = UIController.instance.transform.Find("Background/Level_Text").GetComponent<Text>();
        //SetExperience(0);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
    }

    /*private void FixedUpdate()
    {     
        HandleAttacks();
        ResetValue();
    }*/
    void GetInput()
    {
        //Attack

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                NPCController npcController = hit.transform.GetComponent<NPCController>();
                if (npcController != null)
                {
                    npcController.ShowDialogue();
                    npcController.dialogueIndex++;
                    return;
                }
            }


            active = true;
            Attack();


        }
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            SetVelocity(-1);
            print("left");
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            SetVelocity(0);
            anim.SetInteger("Condition", 0);
        }
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            SetVelocity(1);
            print("right");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            SetVelocity(0);
            anim.SetInteger("Condition", 0);
        }
    }

    #region MOVEMENT
    void Move()
    {
        /*if(!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            rb.MovePosition(transform.position + (Vector3.right * velocity * movementSpeed * Time.deltaTime));
        }*/

        if (velocity == 0)
        {
            //anim.SetInteger("Condition", 0);
            return;
        }
        else
        {
            /*if (!attacking)
            {
                anim.SetInteger("Condition", 1);
                rb.MovePosition(transform.position + (Vector3.right * velocity * movementSpeed * Time.deltaTime));
            }*/
            if (canMove && !attacking)
            {
                anim.SetInteger("Condition", 1);
                rb.MovePosition(transform.position + (Vector3.right * velocity * movementSpeed * Time.deltaTime));
            }
        }

    }
    void SetVelocity(float dir)
    {
        //Look left or right 
        canMove = true;
        if (dir < 0) transform.LookAt(transform.position + Vector3.left);
        else if (dir > 0) transform.LookAt(transform.position + Vector3.right);
        velocity = dir;

    }
    #endregion

    #region COMBAT
    void Attack()
    {
        if (!canAttack)
            return;
        anim.SetInteger("Condition", 3);
        StartCoroutine(AttackRoutine());
        StartCoroutine(AttackCooldown());
    }


    //void DealDamage()
    //{
    //    print("deal damage!");
    //    GetEnemiesInRange();
    //    foreach (Transform enemy in enemiesInRange)
    //    {
    //        EnemyController ec = enemy.GetComponent<EnemyController>();
    //        if(ec == null) continue;
    //        ec.GetHit(attackDamage);
    //    }
    //}
    IEnumerator AttackRoutine()
    {
        attacking = true;
        canMove = false;
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition", 2);
        GetEnemiesInRange();
        foreach (Transform enemy in enemiesInRange)
        {
            EnemyController ec = enemy.GetComponent<EnemyController>();
            if (ec == null) continue;
            ec.GetHit(attackDamage);
        }
        //DealDamage();

        yield return new WaitForSeconds(0.65f);
        canMove = true;

        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition", 0);
        yield return new WaitForSeconds(1);
        attacking = false;
    }
    IEnumerator AttackCooldown()
    {
        canAttack = true;
        yield return new WaitForSeconds(1 / attackSpeed);
        canAttack = false;
    }

    void GetEnemiesInRange()
    {
        enemiesInRange.Clear();
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * 0.5f), 0.5f))
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                enemiesInRange.Add(c.transform);
            }
        }

    }
    #endregion

    public void SetExperience(float exp)
    {
        experience += exp;
        //    float experienceNeeded = GameLogic.ExperienceForNextLevel(level);
        //    float previousExperience = GameLogic.ExperienceForNextLevel(level - 1);
        //    // Level Up
        //    if (experience >= experienceNeeded)
        //    {
        //        LevelUp();
        //        experienceNeeded = GameLogic.ExperienceForNextLevel(level);
        //        previousExperience = GameLogic.ExperienceForNextLevel(level - 1);
        //    }

        //    experienceBar.Find("Fill").GetComponent<Image>().fillAmount = (experience - previousExperience) / (experienceNeeded - previousExperience);
    }

    //void LevelUp()
    //{
    //    level++;
    //    levelText.text = "lvl " + level.ToString("00");
    //}
}
