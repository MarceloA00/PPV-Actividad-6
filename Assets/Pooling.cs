using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polling : MonoBehaviour {
    Queue<GameObject> pool = new();
    public GameObject prefab; 
    public int limit;
    public int timeBetweenSpawns;
    public bool spawn = true; 

    private void Update() {
        if(spawn) {
            spawn = false;
            StartCoroutine(spawnGrain());
        }
    }

    private IEnumerator spawnGrain() {
        InstantiateNewGameObject(transform.position);
        yield return new WaitForSeconds(timeBetweenSpawns);
        spawn = true;
    }

    public void InstantiateNewGameObject(Vector2 position) {
        position = new Vector2(Random.Range(-2.5f, 2.5f), position.y);
        if (HasFreeSpace()) {
            pool.Enqueue(Instantiate(prefab, position, Quaternion.identity));
        } else {
            GameObject go = pool.Dequeue();
            go.transform.position = position;
            pool.Enqueue(go);
        }
    }

    private bool HasFreeSpace() { return pool.Count >= limit ? false : true;}
}
