using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData projectileData;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float liveTime;
    [SerializeField] private float dmg;
    private Vector2 direction;
    
    private void OnEnable() {
        moveSpeed = projectileData.MoveSpeed;
        liveTime = projectileData.LiveTime;
        dmg = projectileData.Dmg;

        StartCoroutine(MoveProjectile());
    }

    private void OnDisable() {
        StopCoroutine(MoveProjectile());
    }

    private IEnumerator MoveProjectile()
    {
        while (liveTime > 0f)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            liveTime -= Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log($"collide with {other.gameObject.name}");
    }
}
