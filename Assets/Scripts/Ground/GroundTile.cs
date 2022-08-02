using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GroundTile : MonoBehaviour
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


        Vector3 point = new Vector3(Random.Range(-11, 11), 1f, Random.Range(-11, 11));
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }
        point.y = 1f;
        return point;
    }
  
}
