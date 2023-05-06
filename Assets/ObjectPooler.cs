using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;

    private List<GameObject> pooledObjects = new List<GameObject>();

    private int amountToPool = 700;

    [SerializeField] private GameObject npcPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        for(int i = 0; i< amountToPool; i++) {
            GameObject obj = Instantiate(npcPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
       
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];    
            }
        }
        return null;
    }

}
