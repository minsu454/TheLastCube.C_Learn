using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool { 
    public class ObjectPoolContainer : MonoBehaviour
    {
        private static ObjectPoolContainer instance;
        public static ObjectPoolContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("----------ObjectPool----------");
                    instance = obj.AddComponent<ObjectPoolContainer>();
                }
                return instance;
            }
        }

        public Dictionary<string, ObjectPool> objectPoolDic = new Dictionary<string, ObjectPool>();

        public void CreateObjectPool(string poolName, GameObject prefab, int preloadCount, Transform poolTr = null)
        {
            if (objectPoolDic.ContainsKey(poolName))
            {
                Debug.LogError($"Object pool {poolName} is already exists.");
                return;
            }

            ObjectPool pool;
            pool = new ObjectPool(poolName, prefab, poolTr, preloadCount);

            objectPoolDic.Add(poolName, pool);
        }

        public GameObject Pop(string poolName)
        {
            if (string.IsNullOrEmpty(poolName))
            {
                return null;
            }

            if (!objectPoolDic.TryGetValue(poolName, out ObjectPool pool))
            {
                return null;
            }

            return pool.GetObject();
        }

        public void Return(GameObject obj)
        {
            string poolName = obj.name;

            if (!objectPoolDic.TryGetValue(poolName, out ObjectPool pool))
            {
                Debug.LogError($"Cannot found pool Name: {poolName}");
                return;
            }

            obj.gameObject.SetActive(false);
            pool.objectStack.Push(obj);
        }

        public Vector2 ReturnImageSize(string poolName)
        {
            if (!objectPoolDic.TryGetValue(poolName, out ObjectPool pool))
            {
                Debug.LogError($"Cannot found pool Name: {poolName}");
                return Vector2.zero;
            }

            var spriteRenderer = pool.bassPrefab.GetComponent<SpriteRenderer>();
            return spriteRenderer.bounds.size;
        }
    }

    public static class Utility {
        public static void CreateObjectPool(this GameObject obj, int preloadCount, Transform poolTr = null)
        {
            ObjectPoolContainer.Instance.CreateObjectPool(obj.name, obj, preloadCount, poolTr);
        }

        public static void Return(this GameObject obj) { 
            ObjectPoolContainer.Instance.Return(obj);
        }
    }
}
