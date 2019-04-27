using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {
    [SerializeField] GameObject zombiePrefab = null;
    [SerializeField] float spawnInterval = 2.0f;

    IEnumerator Start() {
        while (true) {
            yield return new WaitForSeconds(spawnInterval);
            var angle = Random.Range(45.0f, 135.0f);
            var pos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * 100;
            var zombie = Instantiate(zombiePrefab, pos, Quaternion.identity).GetComponent<Zombie>();
            if (Time.time > 12.0f && Random.Range(0, 10) == 0) {
                zombie.SetMoveSpeed(9.0f, 1);
            }
            if (Time.time > 12.0f && Random.Range(0, 20) == 0) {
                zombie.SetMoveSpeed(18.0f, 2);
            }
        }
    }
}
