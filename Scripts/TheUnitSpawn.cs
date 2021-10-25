using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TheUnitSpawn : MonoBehaviourPun
{
    public string UnitPrefabPath;
    public float maxEnemies;
    public float spawnRadius;
    public float spawnCheckTime;

    private float lastSpawnCheckTime;
    private List<GameObject> curUnit = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (Time.time - lastSpawnCheckTime > spawnCheckTime)
        {
            lastSpawnCheckTime = Time.time;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        // remove any dead enemies from the curEnemies list
        for (int x = 0; x < curUnit.Count; ++x)
        {
            if (!curUnit[x])
                curUnit.RemoveAt(x);
        }

        // if we have maxed out our enemies, return
        if (curUnit.Count >= maxEnemies)
            return;

        // otherwise, spawn an enemy
        Vector3 randomInCircle = Random.insideUnitCircle * spawnRadius;
        GameObject enemy = PhotonNetwork.Instantiate(UnitPrefabPath, transform.position + randomInCircle, Quaternion.identity);
        curUnit.Add(enemy);
    }
}
