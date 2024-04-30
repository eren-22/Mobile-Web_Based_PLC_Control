# Mobile-Web_Based_PLC_Control
Bu proje, Android uygulama, Web API ve Windows servisi kullanarak bir PLC cihazını uzaktan kontrol etmek için geliştirilmiştir.

## Proje Açıklaması
Bu proje, bir mobil uygulama aracılığıyla Web API'ye gönderilen isteklerle bir PLC cihazını kontrol etmeyi amaçlamaktadır. Web API, veritabanı işlemleri için kullanılır ve Windows servisi, belirli aralıklarla veritabanındaki değerleri kontrol ederek PLC cihazını yönetir.

## Proje Yapısı
Proje üç ana bölümden oluşur:

- **Android Uygulaması**: PLC cihazını kontrol etmek için kullanıcı arayüzü sağlar.
- **Web API**: Android uygulamasından gelen istekleri işler ve veritabanı işlemlerini gerçekleştirir.
- **Windows Servisi**: Veritabanındaki değerleri belirli aralıklarla kontrol eder ve PLC cihazını buna göre yönetir.

## Kullanılan Teknolojiler
- **Android Uygulaması**: Kotlin
- **Web API**: C#, ASP.NET Core
- **Windows Servisi**: C#
  
 ## Kurulum
1. Android uygulamasını Android Studio'da açın.
2. Web API ve Windows servisi için Visual Studio'da projeleri açın.
3. Veritabanınızı oluşturun.
4. Web API'yi ve Windows servisini çalıştırın.

## Kullanım
1. Android uygulamasında PLC cihazının kontrolü için gerekli işlemleri gerçekleştirin.

## Proje Arayüzünün Kullanımı
https://github.com/eren-22/Mobile-Web_Based_PLC_Control/assets/75755072/4e81e7ca-098a-4947-b54f-76574740b755

## Log Kayıtları
![Ekran görüntüsü 2024-04-30 163414](https://github.com/eren-22/Mobile-Web_Based_PLC_Control/assets/75755072/905fd7ff-1237-4882-a513-dc5bea1d14c3)

Servis, her 5 saniyede bir PLC cihazına gönderilen "value" değerlerini kontrol eder ve bu değerleri bir metin dosyasına kaydeder.

## Not
Projede bulunan ip adresi, port numarası, connection string vs. gibi yapılar gizlenmiştir.
