# AMERİKAN HASTANESİ ASSESSMENT PROJECT – NET BACKEND DEVELOPER

Bu proje, Amerikan Hastanesi'nin blog uygulaması için bir backend servisidir. Proje, .NET 8 ve PostgreSQL kullanılarak geliştirilmiştir.

## Mimari Seçimler

- **.NET 8:** .NET 8, platformlar arası çalışabilen ve yüksek performanslı uygulamalar geliştirmek için kullanılan bir çerçevedir. Bu projede, .NET 5'in sunduğu özelliklerden ve ekosistemden yararlanılmıştır.

 **N-Layer (Katmanlı) Mimari:** Bu proje, aşağıdaki katmanlardan oluşan bir N-Layer mimari yapısına sahiptir. Uygulamanın farklı sorumlulukları ve işlevleri arasında ayrım yaparak, kodun daha okunabilir, bakım yapılabilir ve test edilebilir olması amaçlandı.
  - **Web API Katmanı:** Bu katman, HTTP isteklerini alır ve yanıtlar. Bu katman ayrıca, gelen isteklerin doğrulamasını ve yetkilendirmesini de yapar.
  - **Test Katmanı:** Bu katman, API katmanındaki controller'lar için test koşulmasını sağlar.
  - **Service Katmanı:** Bu katman, iş mantığını içerir. Veritabanı işlemleri ve diğer backend işlemleri bu katmanda gerçekleştirilir.
  - **DataAccess Katmanı:** Bu katman, context'i, Migration'ları ve entity repository'lerini içerir.. Bu katman, Entity Framework Core kullanılarak oluşturulmuştur.
  - **Entity Katmanı:** Uygulamadaki tabloların tanımlandığı katmandır.
  - **Core Katmanı:** Uygulamadaki temel yapının oluşturulduğu katmandır. Abstract sınıfları ve temel Generic Repository'yi barındırır.
  - **Common Katmanı:** Dto'ların, helper'ların ve attribute'lerin oluşturulduğu katmandır.

- **Entity Framework Core:** Veritabanı işlemleri için Entity Framework Core kullanılmıştır. Bu, veritabanı işlemlerini kolaylaştırır ve veritabanı ile uygulama arasındaki etkileşimi yönetir.

- **Npgsql:** PostgreSQL veritabanı ile etkileşim için Npgsql kullanılmıştır. Npgsql, .NET için PostgreSQL veritabanı ile etkileşim sağlayan bir kütüphanedir.

- **Xunit:** Unit testler için Xunit test çerçevesi kullanılmıştır.

## Uygulamanın Çalıştırılması

1. Projeyi klonlayın veya indirin.
2. Terminalde projenin ana dizinine gidin.
3. `dotnet restore` komutunu çalıştırın.
4. `dotnet run --project Blog.WebApi/Blog.WebApi.csproj` komutunu çalıştırın.
5. Tarayıcınızda `localhost:5222/swagger` adresine gidin.

## Testlerin Çalıştırılması

1. Terminalde test projesinin dizinine gidin (`Blog.Test`).
2. `dotnet test` komutunu çalıştırın.

## API Dokümantasyonu

API dokümantasyonu için Swagger kullanılmıştır. Uygulama çalışırken tarayıcınızda `http://localhost:5000/swagger` adresine giderek API dokümantasyonunu görüntüleyebilirsiniz.