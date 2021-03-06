﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooPooled : MonoBehaviour
{
    private float lifeTime;
    private readonly float maxLifetime = 15f;
    private string enemyTag;
    private float Damage;
    private SpriteRenderer spr;

    private void LateUpdate()
    {
        if (this.isActiveAndEnabled)
        {
            // Move to the right which is facing the direction the player is facing
            float direction = (spr.flipX ? -1f : 1f);

            transform.position += transform.right * direction * 7.5f * Time.deltaTime;

            if (lifeTime > maxLifetime)
            {
                GooPool.Instance.ReturnToPool(this);
            }

            lifeTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");

        if (collision.CompareTag(enemyTag))
        {
           

            ITakeDamage _takeDamage = collision.GetComponent<ITakeDamage>();

            if (_takeDamage is ITakeDamage)
            {
                _takeDamage.TakeDamage(Damage);
            }

            Die();
        }
    }

    private void Die()
    {
        GooPool.Instance.ReturnToPool(this);
    }

    public void Init(Transform player, string _enemyTag, float _damage)
    {
        enemyTag = _enemyTag;
        Damage = _damage;
        // Change the color of the GOO to the player it was fired by and also look towards where the player was looking
        spr = GetComponent<SpriteRenderer>();
        SpriteRenderer playerSPR = player.GetComponent<SpriteRenderer>();
        spr.color = playerSPR.color;
        spr.flipX = playerSPR.flipX;

        lifeTime = 0f;
    }
}
