using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour {
    [SerializeField] Transform[] drumTipList = null;
    [SerializeField] Transform[] drumList = null;
    List<Vector3> drumInitialLocalPosition = new List<Vector3>();
    [SerializeField] Headquarter headquarter = null;
    [SerializeField] GameObject bulletPrefab = null;

    private void OnValidate() {
        headquarter = GetComponentInParent<Headquarter>();
    }

    private void Start() {
        foreach (var t in drumList) {
            foreach (var tt in t) {
                drumInitialLocalPosition.Add(t.transform.localPosition);
            }
        }
    }

    private void Update() {
        for (int i = 0; i < drumList.Length; i++) {
            Vector3 vel = Vector3.zero;
            drumList[i].localPosition = Vector3.SmoothDamp(drumList[i].localPosition, drumInitialLocalPosition[i], ref vel, 0.05f);
        }
    }

    public void Fire() {
        foreach (var drumTip in drumTipList) {
            var bullet = Instantiate(bulletPrefab, drumTip.position, drumTip.rotation).GetComponent<Bullet>();
            bullet.SetHeadquarter(headquarter);
        }
    }

    public void RecoilDrum() {
        for (int i = 0; i < drumList.Length; i++) {
            drumList[i].localPosition = new Vector3(drumList[i].localPosition.x, drumInitialLocalPosition[i].y / 1.5f, drumList[i].localPosition.z);
        }
    }
}
