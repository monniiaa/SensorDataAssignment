using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter 
{
    string filename;

    public CSVWriter()
    {
       
    }

    /// <summary>
    /// Writting the values from the gyrolist in a CSV file
    /// </summary>
    /// <param name="name">The name of the document</param>
    public void WriteCSV(string name, List<Quaternion> list)
    {
        filename = Application.persistentDataPath + "/" + name +".CSV";
        if (list.Count > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("x; y; z");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for (int i = 0; i<list.Count; i++)
            {
                tw.WriteLine(list[i].x + ";" + list[i].y + ";" +
                                list[i].z);
            }
            tw.Close();
        }
    }
}
