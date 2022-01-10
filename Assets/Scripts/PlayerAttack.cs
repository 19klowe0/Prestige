using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayers;

    public int attackDamage;

    public float attackRate;
    private float nextAttackTime = 0;

    public ParticleSystem slashEffect;
    public ParticleSystem slashEffectUpsideDown;
    bool attack1 = true;

   



    private void Update()
    {
        
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                CountSwitch();
            }

        }
        
    }
    void Attack()
    {

        //play animation right animation 
        //anim.Play("Melee1");
        if (attack1)
        {

            anim.SetTrigger("Attack1");
            slashEffect.Play();

        }
        if (!attack1)
        {

            anim.SetTrigger("Attack2");
            slashEffectUpsideDown.Play();

        }
        Debug.Log("attack ");
        EnemyDetection();
        

    }

    void EnemyDetection()
    {
        //detect enemies 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayers);

        //damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    void CountSwitch()
    {
        attack1 = !attack1;
        anim.SetBool("isStriking", attack1);
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
