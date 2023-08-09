using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Utility
{
    [System.Serializable]
    public class ObjectInfo
    {
        public string name;
        public Transform ableParent;
        public Transform disableParent;
        public int count;
    }

    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        [SerializeField] public List<ObjectInfo> objectInfos = new List<ObjectInfo>();

        private Dictionary<string, object> pools = new Dictionary<string, object>();

        private void Start()
        {
            pools.Add(objectInfos[0].name, new GenericObjectPool<Cards.Card>(objectInfos[0]));
            pools.Add(objectInfos[1].name, new GenericObjectPool<Cards.LionRoar>(objectInfos[1]));
            pools.Add(objectInfos[2].name, new GenericObjectPool<Cards.Meteors>(objectInfos[2]));
            pools.Add(objectInfos[3].name, new GenericObjectPool<ParticleSystem>(objectInfos[3]));
            pools.Add(objectInfos[4].name, new GenericObjectPool<Cards.NormalProjectile>(objectInfos[4]));
        }

        /// <summary>
        /// �ش� Ÿ���� ������Ʈ�� �޾ƿɴϴ�.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T GetObject<T>(string name)
        {
            if (pools.TryGetValue(name, out var objectPool) && objectPool is GenericObjectPool<T> typedObjectPool)
            {
                return typedObjectPool.GetObject();
            }
            Debug.Log("������Ʈ�� ã�� �� �����ϴ�.");
            return default(T);
        }

        /// <summary>
        /// �ش� Ÿ���� ������Ʈ�� ���� �մϴ�.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnObj"></param>
        /// <param name="name"></param>
        public void ReturnObject<T>(T returnObj, string name)
        {
            if(pools.TryGetValue(name, out var objectPool) && objectPool is GenericObjectPool<T> typedObjectPool)
            {
                typedObjectPool.ReturnObject(returnObj);
                return;
            }
            Debug.Log("������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    public class GenericObjectPool<T>
    {
        private ObjectInfo info;
        private Queue<T> objectQueue = new Queue<T>();

        public GenericObjectPool(ObjectInfo info)
        {
            this.info = info;

            for(int i = 0; i < info.count; i++)
            {
                objectQueue.Enqueue(CreateObject<T>());
            }
        }

        private T CreateObject<T>()
        {
            var newObj = GameObject.Instantiate(Manager.GameManager.Instance.ResourceLoadObj(info.name));

            UtilFunction.TurnOnOff(info, newObj);

            //newObj.SetActive(false);
            //if(info.disableParent != null) newObj.transform.SetParent(info.disableParent);
            //else newObj.transform.SetParent(null);

            return newObj.GetComponent<T>();
        }

        /// <summary>
        /// ������Ʈ�� �޾ƿɴϴ�.
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            if(objectQueue.Count > 0)
                return objectQueue.Dequeue();
            else
                return CreateObject<T>();
        }

        /// <summary>
        /// ������Ʈ�� �����մϴ�.
        /// </summary>
        /// <param name="returnObj"></param>
        public void ReturnObject(T returnObj) => objectQueue.Enqueue(returnObj);
    }

    public class UtilFunction
    {
        public static void TurnOnOff(ObjectInfo info, GameObject obj, bool set = false)
        {
            obj.SetActive(set);
            if (set)
            {
                if (info.ableParent != null) obj.transform.SetParent(info.ableParent);
                else obj.transform.SetParent(null);
            }
            else
            {
                if (info.disableParent != null) obj.transform.SetParent(info.disableParent);
                else obj.transform.SetParent(null);
            }
        }
    }
}
