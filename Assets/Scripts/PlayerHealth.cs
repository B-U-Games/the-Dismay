using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float value = 100;
    public Image HealthStatus;
    public GameObject GameplayUI;
    public GameObject GameOverScreen;
    public PlayerMove Controller;
    public FireballCaster FireballCaster;
    public GameObject Camera;
    private float _maxHealth;
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
    void Die()
    {
        GameplayUI.SetActive(false);
        GameOverScreen.SetActive(true);
        Controller.enabled = false;
        FireballCaster.enabled = false;
        Camera.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
