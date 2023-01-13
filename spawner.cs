using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject cube;
    public float spawnDistance = 10f;
    public int maxObjectsOnScreen = 10;
    public float spawnDelay = 1f;
    public float objectLifeTime = 5f;
    public float leftLimit = -5f;
    public float rightLimit = 5f;
    public float destroyDelay = 2f;

    private int currentObjectCount = 0;

    void Start()
    {
        
        InvokeRepeating("SpawnObject", spawnDelay, spawnDelay);
    }

    void SpawnObject()
    {
        if (currentObjectCount >= maxObjectsOnScreen)
        {
            return;
        }

        float randomX = Random.Range(leftLimit, rightLimit);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        GameObject newObject = Instantiate(cube, spawnPosition, Quaternion.identity);
        newObject.tag = "cube";
        currentObjectCount++;

        StartCoroutine(RemoveColliderAfterDelay(newObject, destroyDelay));
    }

    IEnumerator RemoveColliderAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        StartCoroutine(DestroyAfterDelay(obj, objectLifeTime - destroyDelay));
    }

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
        currentObjectCount--;
    }

}
