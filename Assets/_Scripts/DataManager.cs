using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Save game data in binary file format
/// </summary>
public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    #region  non-Binary data
    public PlayerData mData;//object instance of the data serialized class
    #endregion


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            mData = new PlayerData();
            DontDestroyOnLoad(gameObject);
        }

        LoadLocal();
        DefaultData();
    }

    /// <summary>
    /// Initialize data first time
    /// </summary>
    public void DefaultData()
    {
        if (mData.selectedColor==null)// first time to play
        {
            mData.selectedColor = new SerializableColor();
            mData.selectedColor.SetColor(Color.red);
            mData.shapeIndex = 0;
        }
    }

    /// <summary>
    /// Load local binary data file from the data path specified to the serialized class (Player data)
    /// </summary>
    public void LoadLocal()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.data"))//check if there file
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.OpenRead(Application.persistentDataPath + "/playerData.data");
            mData = (PlayerData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.LogError("No binary data file to read");
        }
    }

    /// <summary>
    /// Save the game data from the serialized class (player data) to the specified path in binary file format
    /// </summary>
    [ContextMenu("Save Local Data")] // To call the function from inspector
    public void SaveLocal()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/playerData.data"))//check if there is file
        {
            file = File.OpenWrite(Application.persistentDataPath + "/playerData.data");
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/playerData.data");// if there is no file create new one
        }

        bf.Serialize(file, mData);
        file.Close();
    }

    /// <summary>
    /// Save file when closing the application
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveLocal();
    }

}
