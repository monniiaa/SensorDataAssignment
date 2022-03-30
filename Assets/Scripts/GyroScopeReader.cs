using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public static class GyroScopeReader
{
    public static float xTreshHold;
    public static float zTreshHold;
    /// <summary>
    /// Turns the axis around
    /// </summary>
    /// <param name="q">The quaternion to be converted</param>
    /// <returns></returns>
    public static Quaternion GyroAxisConvert(Quaternion q)
    {
        return new Quaternion(q.x, q.z, q.y, -q.w);
    }

    /// <summary>
    /// Returns true if the phone is lying flat and false otherwise
    /// </summary>
    /// <returns></returns>
    public static bool IsFlat()
    {
        if (AttitudeSensor.current.attitude.ReadValue().x <= xTreshHold && AttitudeSensor.current.attitude.ReadValue().x >= -xTreshHold &&
             AttitudeSensor.current.attitude.ReadValue().z >= -zTreshHold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Puts gyroscope values into list
    /// </summary>
    public static void RecordGyroValues(List<Quaternion> list)
    {

        Quaternion quat = GyroAxisConvert(AttitudeSensor.current.attitude.ReadValue());
        list.Add(quat);
    }
}
