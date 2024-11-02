using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool { 
    public class ObjectPoolContainer : MonoBehaviour
    {
        public class ObjectPool {
            public readonly string poolName;
            public readonly Stack<GameObject> objectStack = new Stack<GameObject>();
            public readonly GameObject bassPrefab;

            public ObjectPool(string poolName, GameObject bassPrefab, int preloadCount) {
                this.poolName = poolName;
                this.bassPrefab = bassPrefab;

                Preload(preloadCount);
            }

            public bool Preload(int preloadCount) {
                for (int i = 0; i < preloadCount; i++) {
                    GameObject go = CreateImpl();
                    go.SetActive(false);
                    objectStack.Push(go);
                }

                return true;
            }

            public GameObject CreateImpl() {
                if (bassPrefab == null) {
                    Debug.LogError($"ObjectPool - basePrefab is null.");
                    return null;
                }

                GameObject newGo = Instantiate(bassPrefab);
                newGo.name = poolName;

                return newGo;
            }

            public GameObject GetObject() {
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

            public void ReturnObject(GameObject go) {
                if (go == null) {
                    return;
                }

                objectStack.Push(go);
            }
        }

        private static ObjectPoolContainer instance;
        public static ObjectPoolContainer Instance
        {
            get {
                if (instance == null) {
                    GameObject obj = new GameObject("----------ObjectPool-----------");
                    instance = obj.AddComponent<ObjectPoolContainer>();
                }
                return instance;
            }
        }

        public Dictionary<string, ObjectPool> objectPoolDic = new Dictionary<string, ObjectPool>();

        public void CreateObjectPool(string poolName, GameObject prefab, int preloadCount)
        {
            if (objectPoolDic.ContainsKey(poolName)) {
                Debug.LogError($"Object pool {poolName} is already exists.");
                return;
            }

            ObjectPool pool;

            pool = new ObjectPool(poolName, prefab, preloadCount);

            objectPoolDic.Add(poolName, pool);
        }

        public GameObject Pop(string poolName) {
            if (string.IsNullOrEmpty(poolName)) {
                return null;
            }

            if (!objectPoolDic.TryGetValue(poolName, out ObjectPool pool))
            {
                return null;
            }

            return pool.GetObject();
        }

        public void Return(GameObject obj) {
            string poolName = obj.name;

            if (!objectPoolDic.TryGetValue(poolName, out ObjectPool pool)) {
                Debug.LogError($"Cannot found pool Name: {poolName}");
                return;
            }

            obj.gameObject.SetActive(false);
            pool.objectStack.Push(obj);
        }

        public Vector2 ReturnImageSize(string poolName) {
            if (!objectPoolDic.TryGetValue(poolName, out ObjectPool pool)) {
                Debug.LogError($"Cannot found pool Name: {poolName}");
                return Vector2.zero;
            }

            var spriteRenderer = pool.bassPrefab.GetComponent<SpriteRenderer>();
            return spriteRenderer.bounds.size;
        }
    }

    public static class Utility {
        public static void CreateObjectPool(this GameObject obj, string poolName, int preloadCount)
        {
            ObjectPoolContainer.Instance.CreateObjectPool(poolName, obj, preloadCount);
        }

        public static void Return(this GameObject obj) { 
            ObjectPoolContainer.Instance.Return(obj);
        }
    }
}
