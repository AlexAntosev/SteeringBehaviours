using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pulls
{
    public class GenericPull
    {
        public List<GameObject> prefabs;
        
        private List<GameObject> _items;

        public GenericPull()
        {
            _items = new List<GameObject>();
            prefabs = new List<GameObject>();
        }

        public T Get<T>() where  T : Component
        {
            var poolItem = _items.FirstOrDefault(i => i.GetComponent<T>() != null);
            if (poolItem != null)
            {
                _items.Remove(poolItem);
                return poolItem.GetComponent<T>();
            }

            var prefabItem = prefabs.FirstOrDefault(i => i.GetComponent<T>() != null)?.GetComponent<T>();
            if (prefabItem != null)
            {
                return Object.Instantiate(prefabItem);
            }

            throw new Exception("No such prefab");
        }

        public void Return<T>(T item) where  T : Component
        {
            item.transform.position = Vector3.zero;
            item.transform.rotation = Quaternion.identity;
            
            _items.Add(item.gameObject);
        }
    }
}