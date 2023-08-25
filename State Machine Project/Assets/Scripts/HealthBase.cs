using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife;

    [SerializeField]
    private float currentLife;

    public SliderController healthSlider;

    public Action onDamage;
    public Action onKill;

    public void Start()
    {
        Reset();
    }

    public void Reset()
    {
        currentLife = startLife;
    }

    public void OnKill()
    {
        onKill?.Invoke();
    }

    public void OnDamage(float damage)
    {
        Debug.Log("health damage");

        currentLife -= damage;

        UpdateLife();

        if (currentLife <= 0)
        {
            OnKill();
            return;
        }

        onDamage?.Invoke();
    }

    private void UpdateLife()
    {
        healthSlider.UpdateSlider(currentLife);
    }
}
