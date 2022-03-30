using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.InputSystem;


public class Attitude : MonoBehaviour
{
    [SerializeField]
    ButtonBehavior button;
    CSVWriter csvWriter;
    public TextMeshProUGUI xText;
    public TextMeshProUGUI yText;
    public TextMeshProUGUI zText;
    public TextMeshProUGUI wText;
    public TextMeshProUGUI debugText;
    public List<Quaternion> gyroDataList = new List<Quaternion>();
    int filenumber = 1;

    private void Start()
    {
        GyroScopeReader.xTreshHold = 0.03f;
        GyroScopeReader.zTreshHold = 0.07f;
        InputSystem.EnableDevice(AttitudeSensor.current);
        csvWriter = GetComponent<CSVWriter>();
    }

    private void Update() {
        
        if (SystemInfo.supportsGyroscope)
        {
            xText.text = "x:" + GyroScopeReader.GyroAxisConvert(AttitudeSensor.current.attitude.ReadValue()).x.ToString(); 
            yText.text = "y:" + GyroScopeReader.GyroAxisConvert(AttitudeSensor.current.attitude.ReadValue()).y.ToString();
            zText.text = "z:" + GyroScopeReader.GyroAxisConvert(AttitudeSensor.current.attitude.ReadValue()).z.ToString();
            wText.text = "w:" + GyroScopeReader.GyroAxisConvert(AttitudeSensor.current.attitude.ReadValue()).w.ToString();
        }

        debugText.text = GyroScopeReader.IsFlat().ToString();
        if (button.isPressed && !GyroScopeReader.IsFlat() && gyroDataList.Count < 700)
        {
            GyroScopeReader.RecordGyroValues(gyroDataList);
            button.SetButtonText("Recording");
        } if (button.isPressed && GyroScopeReader.IsFlat() && gyroDataList.Count > 0)
        {
            button.OnButtonPress();
            UploadGyroValue(gyroDataList, "Gyrodata" + filenumber);
            filenumber++;
            gyroDataList.Clear();
        }
        if (!button.isPressed)
        {
            button.SetButtonText("Start");
            gyroDataList.Clear();
        } 
    }

    public void UploadGyroValue(List<Quaternion> list, string name)
    {
        csvWriter.ConvertData(list);
        csvWriter.WriteCSV(name);
    }

}
