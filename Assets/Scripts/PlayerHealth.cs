using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public FloatVariable currentHealth;
    public FloatVariable maxHealth;

    public VoidEventChannelSO onPlayerDeath;
    public VoidEventChannelSO onValueUpdated;
    public Animator animator;

    public SpriteRenderer spriteRenderer;

    private bool isInvincible = false;
    public float invincibilityFlashDelay = 0.2f;
    public float invincibilityTimeAfterHit = 2.5f;
    public VoidEventChannelSO OnEventLastCheckpointSO;


    [Tooltip("Please uncheck it on production")]
    public bool needResetHP = true;

    private void Awake()
    {
        if (needResetHP || currentHealth.CurrentValue <= 0)
        {
            currentHealth.CurrentValue = maxHealth.CurrentValue;
        }
    }
    private void OnEnable(){
        OnEventLastCheckpointSO.OnEventRaised += RefillHealth;
    }

    private void RefillHealth(){
        currentHealth.CurrentValue = maxHealth.CurrentValue;
        onValueUpdated.Raise();
    }

    private void OnDisable() {
        OnEventLastCheckpointSO.OnEventRaised -= RefillHealth;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            TakeDamage(0);
        }
#endif
    }
 
    public void TakeDamage(float damage)
    {
        if (isInvincible && damage < float.MaxValue) return;
        currentHealth.CurrentValue -= damage;
        onValueUpdated.Raise();
        if (currentHealth.CurrentValue <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(HandleInvincibilityDelay());
            StartCoroutine(InvincibilityFlash());
        }
    }

    private void Die()
    {
        onPlayerDeath.Raise();
        transform.Rotate(0f, 0f, 45f);
        animator.SetTrigger("OnPlayerDeath");
    }

    public void OnPlayerDeathAnimationCallback()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}
