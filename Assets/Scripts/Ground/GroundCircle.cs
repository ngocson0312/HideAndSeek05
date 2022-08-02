﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCircle : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] int coinsToSpawnCount;
    private void Start()
    {
        SpawnCoins();

    }

    //duyệt để spawn ra coin
    public void SpawnCoins()
    {
        for (int i = 0; i < coinsToSpawnCount; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }

    }


    // tính toán điểm sẽ spawn ra coin
    Vector3 GetRandomPointInCollider(Collider collider)
    {
        //Vector3 point = new Vector3(
        //    Random.Range(collider.bounds.min.x + 3, collider.bounds.max.x - 3),
        //    Random.Range(collider.bounds.min.y, collider.bounds.max.y),
        //    Random.Range(collider.bounds.min.z + 3, collider.bounds.max.z - 3)
        //    );

        Vector3 point = Random.insideUnitSphere * 13f;
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }
        point.y = 1.5f;
        return point;
    }
}
