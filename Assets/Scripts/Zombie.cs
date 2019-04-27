using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    [SerializeField] float moveSpeed = 3.0f;
    [SerializeField] int hp = 2;
    [SerializeField] Material[] materialList = null;
    [SerializeField] Renderer zombieRenderer = null;
    [SerializeField] int matIndex = 0;
    //[SerializeField] GameObject flarePrefab = null;

    private void Start() {
        zombieRenderer.material = materialList[matIndex];
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * moveSpeed);
        Vector3 vel = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), ref vel, 0.05f);
        transform.LookAt(Vector3.zero);
    }

    public void SetMoveSpeed(float moveSpeed, int matIndex) {
        this.moveSpeed = moveSpeed;
        this.matIndex = matIndex;
    }

    internal void ApplyDamage(Vector3 applierPosition) {
        hp--;
        //Destroy(Instantiate(flarePrefab, applierPosition, Quaternion.identity), 1.0f);
        if (hp <= 0) {
            Destroy(gameObject);
        } else {
            // knockback
            var d = transform.position - applierPosition;
            transform.position += d;
        }
    }
}
