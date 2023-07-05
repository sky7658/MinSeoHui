using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Utility
{
    [System.Serializable]
    public class ObjectInfo
    {
        public string name;
        public Queue<GameObject> objectQueue = new Queue<GameObject>();
        public Transform ableParent;
        public Transform disableParent;
        public int count;
    }

    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        [SerializeField] private List<ObjectInfo> objects = new List<ObjectInfo>();

        private void Start()
        {
            Initialized();
        }

        private void Initialized()
        {
            foreach(var obj in objects)
            {
                InsertQueue(obj);
            }
        }

        private void InsertQueue(ObjectInfo info)
        {
            for(int i = 0; i < info.count; i++)
            {
                info.objectQueue.Enqueue(CreateObject(info));
            }
        }

        private GameObject CreateObject(ObjectInfo info)
        {
            var newObj = Instantiate(Manager.GameManager.Instance.ResourceLoadObj(info.name));
            newObj.SetActive(false);

            if (info.disableParent == null) newObj.transform.SetParent(transform);
            else newObj.transform.SetParent(info.disableParent.transform);

            return newObj;
        }

        public T GetObject<T>(string name)
        {
            foreach (var obj in objects)
            {
                if(obj.name == name)
                {
                    if(obj.objectQueue.Count > 0)
                    {
                        var _obj = obj.objectQueue.Dequeue();
                        _obj.SetActive(true);

                        if(obj.ableParent == null) _obj.transform.SetParent(null);
                        else _obj.transform.SetParent(obj.ableParent);

                        return _obj.GetComponent<T>();
                    }
                    else
                    {
                        var _newObj = CreateObject(obj);
                        _newObj.SetActive(true);

                        if (obj.ableParent == null) _newObj.transform.SetParent(null);
                        else _newObj.transform.SetParent(obj.ableParent);


                        return _newObj.GetComponent<T>();
                    }
                }
            }

            Debug.Log("오브젝트를 받아올 수 없습니다.");
            return default(T);
        }

        public void ReturnObject(GameObject returnObj, string name)
        {
            foreach(var obj in objects)
            {
                if(obj.name == name)
                {
                    if (obj.disableParent == null) returnObj.transform.SetParent(transform);
                    else returnObj.transform.SetParent(obj.disableParent);

                    returnObj.SetActive(false);
                    obj.objectQueue.Enqueue(returnObj);
                    return;
                }
            }

            Debug.Log("오브젝트를 리턴할 수 없습니다.");
        }
    }

}
