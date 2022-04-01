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
    GyroScopeReader reader;
    public TextMeshProUGUI xText;
    public TextMeshProUGUI yText;
    public TextMeshProUGUI zText;
    public TextMeshProUGUI wText;
    public TextMeshProUGUI debugText;
    public List<Quaternion> gyroDataList = new List<Quaternion>();
    int filenumber = 1;

    private void Start()
    {
        reader = new GyroScopeReader(0.03f, 0.07f);
        InputSystem.EnableDevice(AttitudeSensor.current);
        csvWriter = new CSVWriter();
    }

    private void Update() {
        
        if (SystemInfo.supportsGyroscope)
        {
            xText.text = "x:" + reader.GyroAxisConvert(AttitudeSensor.current.attitude.ReadValue()).x.ToString(); 
            yText.text = "y:" + reader.GyroAxisConvert(AttitudeSensor.current.attitude.ReadValue()).y.ToString();
            zText.text = "z:" + reader.GyroAxisConvert(AttitudeSensor.current.attitude.ReadValue()).z.ToString();
            wText.text = "w:" + reader.GyroAxisConvert(AttitudeSensor.current.attitude.ReadValue()).w.ToString();
        }

        debugText.text = reader.IsFlat().ToString();
        if (button.isPressed && !reader.IsFlat() && gyroDataList.Count < 700)
        {
            reader.RecordGyroValues(gyroDataList);
            button.SetButtonText("Recording");
        } if (button.isPressed && reader.IsFlat() && gyroDataList.Count > 0)
        {
            button.OnButtonPress();
            csvWriter.WriteCSV("Gyrosensordata" + filenumber, gyroDataList);
            filenumber++;
            gyroDataList.Clear();
        }
        if (!button.isPressed)
        {
            button.SetButtonText("Start");
            gyroDataList.Clear();
        } 
    }
}
