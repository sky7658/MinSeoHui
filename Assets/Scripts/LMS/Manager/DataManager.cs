using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace LMS.Manager
{
    public class DataManager : MonoBehaviour
    {
        private static DataManager instance = null;
        public Transform test;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    return null;
                }
                return instance;
            }
        }

        /// <summary>
        /// Json 형식 파일의 데이터를 불러옵니다
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T LoadData<T>(string fileName)
        {
            var _path = Application.dataPath + "/Json/" + fileName + ".Json";
            var _json = File.ReadAllText(_path);
            var data = JsonConvert.DeserializeObject<T>(_json);
            
            return data;
        }

        /// <summary>
        /// 데이터를 Json 형식 파일에 저장합니다
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        public void SaveData<T>(T data, string fileName)
        {
            var _path = Application.dataPath + "/Json/" + fileName + ".Json";
            var _data = JsonConvert.SerializeObject(data);
            Debug.Log(_data);
            File.WriteAllText(_path, _data);
        }

        
    }
}


