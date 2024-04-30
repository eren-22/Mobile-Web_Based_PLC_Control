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
