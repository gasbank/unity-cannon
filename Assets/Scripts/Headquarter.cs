using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Headquarter : MonoBehaviour {
    [SerializeField] Transform yawAxis = null;
    [SerializeField] Transform pitchAxis = null;
    [SerializeField] Text scoreText = null;
    [SerializeField] Text hpText = null;
    [SerializeField] Text bulletsText = null;
    [SerializeField] int hp = 100;
    [SerializeField] int bullets = 20;
    [SerializeField] int bulletsFired = 0;
    [SerializeField] int bulletsHit = 0;
    [SerializeField] int score = 0;
    [SerializeField] Cannon[] canonList = null;
    [SerializeField] int canonIndex = 0;
    public int Score { get { return score; } set { score = value; scoreText.text = "점수: " + score.ToString(); } }
    public int Hp { get { return hp; } set { hp = value; hpText.text = "체력: " + hp.ToString(); } }
    public int Bullets { get { return bullets; } set { bullets = value; bulletsText.text = "총알: " + bullets.ToString(); } }
    public int CanonIndex { get { return canonIndex; } private set { canonIndex = Mathf.Clamp(value, 0, canonList.Length - 1); UpdateCanon(); } }

    private void UpdateCanon() {
        int i = 0;
        foreach (Cannon c in canonList) {
            c.gameObject.SetActive(i == canonIndex);
            i++;
        }
    }

    [SerializeField] Material[] materialList = null;

    Color[] materialOriginalColorList = null;

    private float redFactor = 0;
    
    internal void IncreaseHit() {
        bulletsHit++;
        Score++;

        CanonIndex++;
    }

    private void OnValidate() {
        materialList = GetComponentsInChildren<Renderer>(true).Select(e => e.sharedMaterial).ToArray();
        materialOriginalColorList = materialList.Select(e => e.color).ToArray();
    }

    private void Start() {
        Hp = 100;
        Bullets = 20;
        CanonIndex = 0;
        StartCoroutine(IncreaseBulletCoro());
    }

    IEnumerator IncreaseBulletCoro() {
        while (true) {
            yield return new WaitForSeconds(1.0f);
            Bullets++;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && Bullets > 0) {
            Bullets--;
            bulletsFired++;
            canonList[canonIndex].Fire();
            canonList[canonIndex].RecoilDrum();
        }

        yawAxis.Rotate(Vector3.up, Input.GetAxis("Horizontal"), Space.Self);
        pitchAxis.Rotate(Vector3.right, Input.GetAxis("Vertical"), Space.Self);
        
        for (int i = 0; i < materialList.Length; i++) {
            materialList[i].color = Color.Lerp(materialOriginalColorList[i], Color.red, redFactor);
        }
        redFactor -= 3 * Time.deltaTime;
        if (redFactor < 0) {
            redFactor = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(other.GetComponentInParent<Zombie>().gameObject);
        Hp--;
        redFactor = 1.0f;
    }
}
