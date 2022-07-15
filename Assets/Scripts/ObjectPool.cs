using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectPool : NetworkBehaviour, INetworkPrefabInstanceHandler
{
    [SerializeField] private GameObject poolItem;

    private Queue<GameObject> _pool;

    [HideInInspector] public static ObjectPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _pool = new Queue<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(poolItem);
            go.SetActive(false);
            _pool.Enqueue(go);
        }
    }

    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        return FetchObject(position, rotation);
    }

    public void Destroy(NetworkObject networkObject)
    {
        PoolObject(networkObject.gameObject);
    }

    private NetworkObject FetchObject(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Fetching from pool");
        GameObject go;

        if (_pool.Count > 0)
        {
            go = _pool.Dequeue();
            go.SetActive(true);
        }
        else
        {
            go = Instantiate(poolItem);
        }

        go.transform.position = position;
        go.transform.rotation = rotation;

        return go.GetComponent<NetworkObject>();
    }
    
    private NetworkObject FetchObject()
    {
        Debug.Log("Fetching from pool");
        GameObject go;

        if (_pool.Count > 0)
        {
            go = _pool.Dequeue();
            go.SetActive(true);
        }
        else
        {
            go = Instantiate(poolItem);
        }

        return go.GetComponent<NetworkObject>();
    }

    private void PoolObject(GameObject go)
    {
        Debug.Log("Puuting back in pool");
        go.SetActive(false);
        _pool.Enqueue(go);
    }

    public GameObject FetchFromPool()
    {
        NetworkObject no = FetchObject();
        no.Spawn();

        return no.gameObject;
    }

    public void ReturnToPool(GameObject go)
    {
        PoolObject(go);
        go.GetComponent<NetworkObject>().Despawn();
    }



}