

https://github.com/user-attachments/assets/6d63d93b-d3d6-4895-877c-a357c407b9fd

# Harput MR Deneyimi - Mobil Uygulama Kullanım ve Sunum Kılavuzu

Bu proje; Harput'un tarihi ve kültürel mirasını Mobil VR (Sanal Tur) ve Artırılmış Gerçeklik (AR) teknolojileriyle birleştiren Unity tabanlı bir mobil uygulamadır. 

Aşağıda, APK telefona yüklendikten sonra uygulamanın nasıl test edileceği, modüllerin işleyişi ve hangi modülde hangi görsellerin okutulacağı adım adım açıklanmıştır.

---

## 📱 APK Açıldığında Modüllerin Çalıştırılması

### 1. Modül: Harput 360° Deneyimi (Mobil VR / Sanal Tur)
* **Nasıl Çalıştırılır?:** Uygulama ilk açıldığında (veya ana menüden bu modül seçildiğinde) direkt olarak aktif olur.
* **Okutulacak Fotoğraf:** **Herhangi bir fotoğraf okutulmasına gerek yoktur.**
* **Telefonda Ne Yapılacak?:** Bu modül telefonun dahili **Jiroskop (Gyroscope)** sensörünü kullanır. Telefonu elinizde yukarı, aşağı, sağa ve sola çevirerek yapay zeka (Leonardo.ai) ile üretilmiş olan 360 derecelik tarihi Harput manzarasının içinde gezinebilirsiniz. Ekrandaki geçiş butonlarını kullanarak AR sahnelerine geçiş yapabilirsiniz.

### 2. Modül: Hazine Avı Modülü (Treasure Hunt AR)
* **Nasıl Çalıştırılır?:** Uygulama içinden Hazine Avı sahnesine geçiş yapılır ve telefon kamerası hedef görsele doğrultulur.
* **Okutulacak Fotoğraf:** Unity içinde `TreasureImageLibrary` kütüphanesine tanımlanmış olan tarihi 3 tane görseli ayrı ayrı okutulması gerekir yani her görsel ayrı hazine gösterir gokuttuktan sonra uygulamayı tekrar kapatıp başka görseli okutunuz..
<img width="1600" height="1200" alt="harputkabartma" src="https://github.com/user-attachments/assets/fd934330-fe50-4948-a43c-f8ca5f56f051" />
<img width="500" height="327" alt="HarputEski" src="https://github.com/user-attachments/assets/d7016401-da41-4c28-9d42-46bc484fa927" />
<img width="900" height="450" alt="ArtukluSikke" src="https://github.com/user-attachments/assets/814a8670-db4a-442a-ab1c-1c2458782a5a" />
* **Telefonda Ne Yapılacak?:** Kamera hedef fotoğrafı algıladığı anda:
  1. Fotoğrafın tam üzerinde 3 boyutlu, ahşap bir **Hazine Sandığı** modeli belirir.
  2. Aynı anda ekranda *"Kadim Artuklu hazinesine ulaştın! Mir-i Alem Kahvecisi'nde Dibek Kahvesi Kazandın!"* yazılı ödül kuponu paneli açılır.
  3. "Tamam" butonuna basılarak panel kapatılır ve deneyim sonlandırılır.

### 3. Modül: Bilgi Yarışması Modülü (AR Quiz System)
* **Nasıl Çalıştırılır?:** Uygulamada Bilgi Yarışması sahnesine geçilir. Ekran ilk başta tamamen temiz ve boş gelir (Arayüz kodla gizlenmiştir).
* **Okutulacak Fotoğraf:** Unity içinde `HarputLibrary` kütüphanesine eklenen ve ismi harfi harfine **`QuizTrigger`** yapılan hedef fotoğrafın okutulması gerekir.
* <img width="2816" height="1536" alt="Gemini_Generated_Image_pg7tmhpg7tmhpg7t" src="https://github.com/user-attachments/assets/abc7db2b-31f0-42f5-855e-db059d824709" />
* **Telefonda Ne Yapılacak?:** Kamera `QuizTrigger` fotoğrafını gördüğü anda:
  1. Havada (World Space) Bilgi Yarışması arayüzü (`Canvas`) dinamik olarak açılır ve ilk soru yüklenir.
  2. Ekrana gelen Harput tarihiyle ilgili sorular okunarak **A, B, C, D** butonlarından birine dokunulur.
  3. Doğru şık seçilirse buton **Yeşil** yanar ve skor tabelasına **+10 Puan** eklenir. Yanlış şık seçilirse buton **Kırmızı** yanar ve puan verilmez.
  4. Sistem 1.2 saniye bekledikten sonra otomatik olarak bir sonraki rastgele soruya geçer.
  5. Tüm sorular bittiğinde butonlar gizlenir ve ekranda nihai skorun yazılı olduğu *"Tebrikler! Yarışmayı Tamamladın."* bitiş ekranı belirir.

---
<img width="2816" height="1536" alt="Gemini_Generated_Image_pg7tmhpg7tmhpg7t" src="https://github.com/user-attachments/assets/abc7db2b-31f0-42f5-855e-db059d824709" />
<img width="1600" height="1200" alt="harputkabartma" src="https://github.com/user-attachments/assets/fd934330-fe50-4948-a43c-f8ca5f56f051" />
<img width="500" height="327" alt="HarputEski" src="https://github.com/user-attachments/assets/d7016401-da41-4c28-9d42-46bc484fa927" />
<img width="900" height="450" alt="ArtukluSikke" src="https://github.com/user-attachments/assets/814a8670-db4a-442a-ab1c-1c2458782a5a" />
