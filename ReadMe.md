# Basket Management ![.github/workflows/github.yml](https://github.com/AdemCatamak/BasketManagement/workflows/.github/workflows/github.yml/badge.svg?branch=master)

## __Çalıştırma__

Ana dizinde yer alan _docker-compose_ dosyası kullanılarak uygulama çalıştırılabilir ve localhost:5000 üzerinden hizmet
alınabilir.

Not: _docker-compose_ aracılığı ile uygulama çalıştırılmak ve test edilmek istenirse 5000 portunun başka bir uygulama
tarafından kullanılmaması gerekmektedir.

Bir IDE aracılığı ile debug yapmak isterseniz de ```docker-compose -f docker-compose-test.yml up``` komutu ile
uygulamanın ihtiyaç duyacağı SqlServer ve RabbitMQ bileşenlerini bir konteyner içerisinde oluşturabilirsiniz. Ayrıca
debug modda uygulamayı çalıştırabilmeniz için bilgisayarınızda
[DotNet 5](https://dotnet.microsoft.com/download/dotnet/5.0)  yüklü olmalıdır.

Not: ```docker-compose-test.yml``` ile gereken bağımlılıkların çalıştırılabilmesi için `31433, 45672, 55672` portlarının
kullanılmıyor olması gerekmektedir.

## __Geliştirme Sırasında Kullanılan Teknikler__

Bu
proje [Olaya Dayalı Programlama](https://ademcatamak.medium.com/olaya-dayal%C4%B1-programlama-event-driven-programming-d6b7e2c0d948)
tekniğiyle oluşturulmuştur. Yazımda bahsetmiş olduğum gibi bu şekilde kod olabildiğince düşük bağımlılıkla
tasarlanabilmekte ve bu şekilde geliştirmeye açık kalmaktadır.

Projede yer alan katmanlar DDD konseptine göre
oluşturulmuştur. [DDD Projelerindeki Katmanlar](https://ademcatamak.medium.com/layers-in-ddd-projects-bd492aa2b8aa)
yazım aracılığı ile hangi sorumluluğa sahip sınıfları hangi katmanlara koyduğuma dair gerekçelere erişeiblirsiniz.

Kullanıcılar uygulamamız ile etkileşime geçtiği BasketManagement.WebApi kısmında REST mimarisine bağlı kalmaya özen
gösterdim. REST mimarisine göre bir web servis tasarlarken dikkat
ettiğim [REST Mimarisinin 5 Kuralına](https://ademcatamak.medium.com/5-rules-of-rest-architecture-434abaf5db44)
bu link üzerinden görebilirsiniz.

Veri saklama ortamından verileri talep ederken specification tasarım deseninden yararlandım. Bu şekilde kritelerin
tekrar tekrar kullanılması ve birleştirilmesi daha kolay
olmaktadır. [Specification Tasarım Deseninin](https://ademcatamak.medium.com/specification-design-pattern-c814649be0ef)
avantajlarına ve C# dilinde nasıl implemente ettiğime dair bilgilere link aracılığı ile erişebilirsiniz.

Farklı okuma ve yazma modellerine ihtiyaç duyulması ve yazma üzerinde kilit işleminin okuma işlemini etkilememesi için
[CQRS Deseninden](https://ademcatamak.medium.com/cqrs-command-query-responsibility-segregation-476d2d81225a)
yararlandım. Bu desenin örnek uygulaması ve açıklamaları için ise
[CQRS ile Stock Yönetimi](https://ademcatamak.medium.com/stok-y%C3%B6netimi-cqrs-%C3%B6rne%C4%9Fi-c8243b82c7b2) yazıma
göz atabilirsiniz.

RabbitMQ mesaj brokerına mesajlar
gönderilirken [Outbox Tasarım Deseni](https://ademcatamak.medium.com/outbox-design-pattern-57e1519ed5e4) kullanılmıştır.
Böylece sistemde oluşan değişimlere ait mesajların RabbitMQ üzerine gönderilmesi eylemi aynı Transaction Scope içerisine
alınmıştır. Bir diğer deyişle sistemde oluşan değişiklikler daha sonra RabbitMQ sistemine iletilmek üzere kaydedilmiş ve
mesaj kaybının yaşanmayacağı garanti altına alınmıştır.

### __Stock__

Bu modul ürünlere ait stok verisini yönetmektedir. StockAction adındaki veri tabanı tablosunda ürün stoğu üzerinde
yapılan tüm değişiklikler saklanmaktadır. Bu eylemler aynı Transaction Scope içerisinde StockSnapshot adındaki tabloya
yansıtılmaktadır. StockSnapshot bir ürüne ait kullanılabilir değeri saklamaktadır. Bu değerin negatif değere dönmesi
durumunda yeterli stok olmadığına dair hata oluşturulacaktır. Bu durumda da veri tabanı işlemleri Rollback edilecektir.
Rollback sayesinde de StockSnapshot tablosuna yazılan kayıtlar kayıt edilmemiş olacaktır.

StockSnapshot üzerinde oluşan değişikler Stock adındaki tabloya asenkron olarak yansıtılmaktadır. Bu şekilde okuma ve
yazma modelleri ayrıştırılmıştır. StockAction modeli ile stok üzerinde yapılmak istenen değişiklikler için emirler
sisteme aktarılırken Stock modeli ile okuma ihtiyaçları karşılanmaktadır.

BasketManagement projesinde konudan çok uzaklaşmamak için WebApi üzerinden stok üzerinde aksiyon alınabilmesini
sağlayacak endpointlere yer vermedim. Bu endpointlere ihtiyaç duyacağım durumda daha önce PoC olarak hazırlamış olduğum
[CQRS ile Stock Yönetimi](https://ademcatamak.medium.com/stok-y%C3%B6netimi-cqrs-%C3%B6rne%C4%9Fi-c8243b82c7b2) yazıma
ve örnek projeye erişebilirsiniz.

### __Basket__

Uygulamanın kolayca kullanılıp test edilebilmesi için authentication işlemleri implemente edilmemiştir. Bu sebeple test
yapan kişi istediği `AccountId` değerini kullanabilmektedir. Normal şartlar altında bu değer kontrol edilmeli ve talebi
gönderen kişinin istekte bahsi geçen `AccountId` için işlemleri yürütüp yürütemeyeceği kontrol edilmelidir.

#### __POST accounts/{accountId/}baskets__

Kullanıcıyı işaret edecek bir değer talep etmektedir. Her bir talepte kullanıcı için yeni bir sepet oluşturulmaktadır.

#### __GET accounts/{accountId/}baskets__

Kullanıcıyı işaret edecek bir değer talep etmektedir. Bu kullanıcıya ait sepetleri ve sepetin içeriklerini cevap olarak
dönmektedir. Opsiyonel olarak `BasketId` değeri verilerek kullanıya ait özel bir sepet bilgisi de elde edilebilmektedir.
Kriterlere uygun bir sepet bulunamaması durumunda kullanıcı talebine 4XX seviyesinden yanıt dönülecektir.

#### __PUT accounts/{accountId/}baskets/{basketId}__

İstek gövdesinden sepette yer alınması istenen ürün ve adet değeri girilmektedir. Ürün sepette yoksa eklenecektir. Eğer
ürün sepette varsa adet değeri güncellenecektir. Sepetin bulunamaması durumda kullanıcıya isteğin hatalı olduğuna dair
bilgi verilecektir.

Not: Adet değerinin 0 girilmesi ürünün sepetten çıkarılmasına eş değerdir.

#### __DELETE accounts/{accountId/}baskets/{basketId}/products/{productId}__

Kullanıcı için sepete bilgisi aranacaktır. Eğer sepet bilgisi bulunursa bahsi geçen ürünün sepette olup olmadığı kontrol
edilecektir. Sepette ürünün bulunması halinde ürün sepetten çıkarılacaktır. Sepetin veya ürünün bulunamaması durumda
hata kullanıcıya yansıtılacaktır.

#### __DELETE accounts/{accountId/}baskets/{basketId}__

Kullanıcı için sepete bilgisi aranacaktır. Eğer sepet bilgisi bulunursa bahsi geçen sepetteki tüm ürünler sepetten
çıkarılacak ardından sepet silinecektir. Sepetin bulunamaması durumda hata kullanıcıya yansıtılacaktır.

## Next Step

[Integration Test with Docker](https://ademcatamak.medium.com/integration-test-with-net-core-and-docker-21b241f7372)
will be implemented. After that, tests will be executed in pipeline
via [Cake Build](https://ademcatamak.medium.com/cake-build-nedir-684eb1885b06) files.
