using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenouille : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpCooldown = 2f;
    public int damageAmount = 10;

    private Rigidbody2D rb;
    private GameObject joueur;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        joueur = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(JumpRoutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            JumpToPlayer();
            yield return new WaitForSeconds(jumpCooldown);
        }
    }

    private void JumpToPlayer()
    {
        Vector2 direction = joueur.transform.position - transform.position;
        rb.velocity = Vector2.zero; 
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
        rb.AddForce(direction.normalized * jumpForce, ForceMode2D.Impulse); 
    }
}
