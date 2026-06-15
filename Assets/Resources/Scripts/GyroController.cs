using UnityEngine;

public class GyroController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    void Start()
    {
        // Cihazýn jiroskopu var mý kontrol et
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroEnabled = true;

            // Kameranýn baþlangýç eksenlerini mobil cihazlara göre düzeltmek için bir kapsül (parent) obje oluþturuyoruz
            GameObject cameraParent = new GameObject("CameraParent");
            cameraParent.transform.position = transform.position;

            // Kamerayý bu kapsülün içine al
            transform.SetParent(cameraParent.transform);

            // Android ve iOS için varsayýlan yönü ayarla
            cameraParent.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        }
        else
        {
            Debug.LogWarning("Bu cihazda Jiroskop bulunmuyor!");
        }
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // Telefonun jiroskop verisini (Attitude) Unity'nin Quaternion eksenlerine çevir
            Quaternion gyroAttitude = gyro.attitude;
            transform.localRotation = new Quaternion(gyroAttitude.x, gyroAttitude.y, -gyroAttitude.z, -gyroAttitude.w);
        }
    }
}