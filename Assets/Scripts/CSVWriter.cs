using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string filename;
    bool fileIsmade = false;

    Attitude attitude;
    [System.Serializable]
    public class GyroScopeData
    {
        public float x;
        public float y;
        public float z;
    }

    [System.Serializable]
    public class GyroScopeDataList
    {
        public List<GyroScopeData> datapoint = new List<GyroScopeData>();
    }

    public GyroScopeDataList gyroList = new GyroScopeDataList();

    /// <summary>
    /// Converts a list of Quaternions to 
    /// </summary>
    /// <param name="list"> The list up for convertion</param>
    public void ConvertData(List<Quaternion> list)
    {
        gyroList.datapoint.Clear();
        for (int i = 0; i< list.Count; i++)
        {
            GyroScopeData data = new GyroScopeData();
            data.x = list[i].x;
            data.y = list[i].y;
            data.z = list[i].z;

            gyroList.datapoint.Add(data);
        }
    }

    /// <summary>
    /// Writting the values from the gyrolist in a CSV file
    /// </summary>
    /// <param name="name">The name of the document</param>
    public void WriteCSV(string name)
    {
        filename = Application.persistentDataPath + "/" + name +".CSV";
        if (gyroList.datapoint.Count > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("x; y; z");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for (int i = 0; i<gyroList.datapoint.Count; i++)
            {
                tw.WriteLine(gyroList.datapoint[i].x + ";" + gyroList.datapoint[i].y + ";" +
                                gyroList.datapoint[i].z);
            }
            tw.Close();
        }
    }
}
