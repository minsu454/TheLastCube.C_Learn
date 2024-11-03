using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPool
    {
        public readonly string poolName;
        public readonly Stack<GameObject> objectStack = new Stack<GameObject>();
        public readonly GameObject bassPrefab;
        public readonly Transform poolTr;

        public ObjectPool(string poolName, GameObject bassPrefab, Transform poolTr, int preloadCount)
        {
            this.poolName = poolName;
            this.bassPrefab = bassPrefab;
            this.poolTr = poolTr;

            Preload(preloadCount);
        }

        public bool Preload(int preloadCount)
        {
            for (int i = 0; i < preloadCount; i++)
            {
                GameObject go = CreateImpl();
                go.SetActive(false);
                objectStack.Push(go);
            }

            return true;
        }

        public GameObject CreateImpl()
        {
            if (bassPrefab == null)
            {
                Debug.LogError($"ObjectPool - basePrefab is null.");
                return null;
            }

            GameObject newGo = Object.Instantiate(bassPrefab, poolTr);
            newGo.name = poolName;

            return newGo;
        }

        public GameObject GetObject()
        {
            GameObject go;

            if (objectStack.Count == 0)
            {
                go = CreateImpl();
            }
            else
            {
                go = objectStack.Pop();
            }

            return go;
        }

        public void ReturnObject(GameObject go)
        {
            if (go == null)
            {
                return;
            }

            go.transform.parent = poolTr;
            objectStack.Push(go);
        }
    }
}
