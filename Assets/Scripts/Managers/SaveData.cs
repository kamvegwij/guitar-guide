using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveData
{
    public static void SaveProgressData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/progress.txt";
        FileStream stream = new FileStream(savePath, FileMode.Create);

        PlayerProgress playerData = new PlayerProgress();//load values from gamemanager.

        formatter.Serialize(stream, playerData);
        Debug.Log("Saved Successfully");
        stream.Close();//close file
    }

    public static PlayerProgress LoadProgress()
    {
        string savePath = Application.persistentDataPath + "/progress.txt";
        if (File.Exists(savePath)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);
            PlayerProgress playerData = formatter.Deserialize(stream) as PlayerProgress;
            stream.Close();
            return playerData;
        }
        else
        {
            Debug.Log("File not found");
            return null;
        }
    }
}
