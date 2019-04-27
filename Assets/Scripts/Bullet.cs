using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] Rigidbody rb;
    [SerializeField] float startSpeed = 10.0f;
    [SerializeField] Headquarter headquarter = null;
    [SerializeField] int hp = 1;

    private void OnValidate() {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start() {
        rb.velocity = transform.forward * startSpeed;
        //rb.AddForce(transform.forward * 10);
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (hp > 0) {
            hp--;
            var zombie = other.GetComponentInParent<Zombie>();
            zombie.ApplyDamage(transform.position);
            headquarter.IncreaseHit();
            if (hp <= 0) {
                Destroy(gameObject);
            }
        }
        
    }

    internal void SetHeadquarter(Headquarter headquarter) {
        this.headquarter = headquarter;
    }
}
