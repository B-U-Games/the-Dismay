using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float value = 100;
    public Image HealthStatus;
    public GameObject GameplayUI;
    public GameObject GameOverScreen;
    public GameObject Camera;
    private float _maxHealth;
    public TextMeshProUGUI DeathCause;
    public void DealDamage(float damage)
    {
        value -= damage;
        if (value <= 0)
        {
            Die();
        }
    }
    void Start()
    {
        _maxHealth = value;
    }
    void Update()
    {
        HealthStatus.fillAmount = value / _maxHealth;
    }

    public void Die(string cause)
    {
        GetComponent<Pause>().enabled = false;
        GameplayUI.SetActive(false);
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        GameOverScreen.SetActive(true);
        GetComponent<ThirdPersonController>().enabled = false;
        Camera.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(CausePrint(cause, 0.015f));
    }

    private void Die()
    {
        GetComponent<Pause>().enabled = false;
        GameplayUI.SetActive(false);
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        GameOverScreen.SetActive(true);
        GetComponent<ThirdPersonController>().enabled = false;
        Camera.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(CausePrint("skill issue", 0.015f));
    }

    private IEnumerator CausePrint(string str, float delay)
    {
        foreach (var sym in str)
        {
            DeathCause.text += sym;
            yield return new WaitForSeconds(delay);
        }
    }
}
