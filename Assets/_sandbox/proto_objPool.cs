using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proto_objPool : MonoBehaviour
{
    public static proto_objPool Instance;

    private List<GameObject> PooledObjs = new List<GameObject>();
    private int totalPooled = 10;

    public GameObject prefabObj;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < totalPooled; i++)
        {
            GameObject obj = Instantiate(prefabObj);
            obj.SetActive(false);
            PooledObjs.Add(obj);
        }
    }

    public GameObject GetPooledObj()
    {
        for (int i = 0; i < PooledObjs.Count; i++)
        {
            if (!PooledObjs[i].activeInHierarchy)
                return PooledObjs[i];
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
