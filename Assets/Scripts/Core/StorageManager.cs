using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Xml;

public class StorageManager : BaseManagerMono<StorageManager>
{
    // 保存

    // 加载

    // 存档路径

    // 存档文件名格式

    // 存档信息文件名


    public class PrefsStorage
    {
        public void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void ClearKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public void ClearAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }
    }

    public void CheckDirectory(string fullpath)
    {
        if (!Directory.Exists(Path.GetDirectoryName(fullpath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
        }
    }

    public bool CheckFile(string fullpath)
    {
        return File.Exists(fullpath);
    }

    public void SaveBinaryFile<T>(T data, string filename)
    {
        string fullpath = Path.Combine(Application.persistentDataPath, filename);
        fullpath = Path.GetFullPath(fullpath);
        CheckDirectory(fullpath);

        FileStream fs = File.Create(fullpath);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, data);
        fs.Close();

        Debug.LogFormat("Save binary data to {0}", fullpath);
    }

    public T LoadBinaryFile<T>(string filename) where T : class
    {
        string fullpath = Path.Combine(Application.persistentDataPath, filename);
        fullpath = Path.GetFullPath(fullpath);
        if (CheckFile(fullpath))
        {
            FileStream fs = File.Open(fullpath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            T data = bf.Deserialize(fs) as T;
            fs.Close();
            Debug.LogFormat("Load binary data from {0}", fullpath);
            return data;
        }
        else
        {
            Debug.LogFormat("Not exist file {0}", fullpath);
            return default(T);
        }
    }

    public void SaveJsonFile<T>(T data, string filename)
    {
        string fullpath = Path.Combine(Application.dataPath, filename);
        fullpath = Path.GetFullPath(fullpath);
        CheckDirectory(fullpath);

        string jsonStr = JsonUtility.ToJson(data);
        StreamWriter sw = new StreamWriter(fullpath);
        sw.Write(jsonStr);
        sw.Close();

        Debug.LogFormat("Save binary data to {0}", fullpath);
    }

    public T LoadJsonFile<T>(string filename)
    {
        string fullpath = Path.Combine(Application.dataPath, filename);
        fullpath = Path.GetFullPath(fullpath);
        if (CheckFile(fullpath))
        {
            StreamReader sr = new StreamReader(fullpath);
            string jsonStr = sr.ReadToEnd(); // todo : 尝试 ReadToEndAsync
            sr.Close();
            try
            {
                T data = JsonUtility.FromJson<T>(jsonStr);
                Debug.LogFormat("Load json data from {0}", fullpath);
                return data;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                Debug.LogErrorFormat("Load json data failed from {0}", fullpath);
                return default(T);
            }
        }
        else
        {
            Debug.LogFormat("Not exist file {0}", fullpath);
            return default(T);
        }
    }

    //public void SaveXmlFile<T>(T data, string filename)
    //public T LoadXmlFile<T>(string filename)

    #region Test Storage Manager
#if true // Test Storage Manager

    [System.Serializable]
    public class TempData
    {
        public int a1;
        public bool b1;
        public float c1;
        public string d1;
        public List<int> a2;
        public List<bool> b2;
        public List<float> c2;
        public List<string> d2;
        public Dictionary<int, bool> ab3;
        public Dictionary<string, float> dc3;
    }

    [ContextMenuItem("save binary", "TestSaveBinaryData1")]
    [ContextMenuItem("load binary", "TestLoadBinaryData1")]
    [ContextMenuItem("save json", "TestSaveJsonData1")]
    [ContextMenuItem("load json", "TestLoadJsonData1")]
    [ContextMenuItem("clear it", "TestClearData1")]
    public TempData data1;

    public void TestSaveBinaryData1()
    {
        SaveBinaryFile(data1, "testSave/data1_1");
    }
    public void TestLoadBinaryData1()
    {
        data1 = LoadBinaryFile<TempData>("testSave/data1_1");
    }
    public void TestSaveJsonData1()
    {
        SaveJsonFile(data1, "testSave/data1_2");
    }
    public void TestLoadJsonData1()
    {
        data1 = LoadJsonFile<TempData>("testSave/data1_2");
    }
    public void TestClearData1()
    {
        data1 = new TempData();
    }

    [ContextMenuItem("save binary", "TestSaveBinaryData2")]
    [ContextMenuItem("load binary", "TestLoadBinaryData2")]
    [ContextMenuItem("save json", "TestSaveJsonData2")]
    [ContextMenuItem("load json", "TestLoadJsonData2")]
    [ContextMenuItem("clear it", "TestClearData2")]
    public TempData data2;

    public void TestSaveBinaryData2()
    {
        SaveBinaryFile(data2, "testSave/data2_1");
    }
    public void TestLoadBinaryData2()
    {
        data2 = LoadBinaryFile<TempData>("testSave/data2_1");
    }
    public void TestSaveJsonData2()
    {
        SaveJsonFile(data1, "testSave/data2_2");
    }
    public void TestLoadJsonData2()
    {
        data1 = LoadJsonFile<TempData>("testSave/data2_2");
    }
    public void TestClearData2()
    {
        data2 = new TempData();
    }

#endif
    #endregion


}
