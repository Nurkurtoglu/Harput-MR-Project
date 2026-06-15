using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TreasureManager : MonoBehaviour
{
    [Header("Arayüz Baðlantýlarý")]
    public GameObject couponPanel;
    public TextMeshProUGUI couponText;

    [Header("AR Yöneticisi")]
    public ARTrackedImageManager imageManager; // Hangi resmin algýlandýðýný buradan dinleyeceðiz

    void Start()
    {
        // Baþlangýçta paneli gizle
        if (couponPanel != null) couponPanel.SetActive(false);
    }

    void OnEnable()
    {
        // Kod aktif olduðunda AR motorunun "Resim Algýlandý" olayýna abone ol
        if (imageManager != null)
            imageManager.trackedImagesChanged += OnImageChanged;
    }

    void OnDisable()
    {
        // Kod kapanýrken aboneliði iptal et (Hafýza sýzýntýsýný önlemek için)
        if (imageManager != null)
            imageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        // Yeni bir resim kameraya girdiðinde veya takip edilmeye baþlandýðýnda
        foreach (var trackedImage in args.added)
        {
            ShowCoupon(trackedImage);
        }

        foreach (var trackedImage in args.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                ShowCoupon(trackedImage);
            }
        }
    }

    void ShowCoupon(ARTrackedImage trackedImage)
    {
        // GÜVENLÝK KONTROLÜ: Resim verisi boþ geldiyse (örneðin bilgisayarda Play'e basýldýysa) çökmesini engelle ve iþlemi iptal et.
        if (trackedImage.referenceImage == null) return;

        // Kütüphanedeki resmin orijinal adýný al
        string resimAdi = trackedImage.referenceImage.name;

        // Ýsme göre yazýyý ayarla
        if (resimAdi.Contains("Rolyef"))
        {
            couponText.text = "Harput'un taþ iþçiliðini keþfettin!\nHüseyin Amca'dan Magnetlerde %20 Ýndirim Kazandýn!";
        }
        else if (resimAdi.Contains("Sikke"))
        {
            couponText.text = "Kadim Artuklu hazinesine ulaþtýn!\nMir-i Alem Kahvecisi'nde Dibek Kahvesi Kazandýn!";
        }
        else if (resimAdi.Contains("Gravur"))
        {
            couponText.text = "Eski Harput'un ruhunu gördün!\nHarput Sofrasý'nda Yemek Yanýnda Ýkram Kazandýn!";
        }

        // Panel kapalýysa aç
        if (!couponPanel.activeSelf)
        {
            couponPanel.SetActive(true);
        }
    }

    public void CloseAppAndSaveBattery()
    {
        Application.Quit();
    }
}