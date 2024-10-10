RabbitMQ Entegrasyonu
Terminalde aşağıdaki komutla RabbitMQ Container çalıştırılır.
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management

Kuyruğun takibi için Worker Service yerine Mass Transit kullanıldı.

In-Memory DB: EF Core InMemory kullanıldı.

