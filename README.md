# KebabDelivery
О проекте

KebabDelivery — это RESTful API для управления заказами, меню и доставкой в шаурмичной. Проект разрабатывается в соответствии с принципами Clean Code Architecture и использует ASP.NET Core.

Технологии

Backend: ASP.NET Core, Entity Framework Core

Database: PostgreSQL

Logging: Serilog

Validation: FluentValidation

Testing: xUnit, FluentAssertions, Testcontainers, Moq

Authentication: JWT (планируется)

Storage: Cloudinary (для изображений)

Containerization: Docker

Структура проекта

KebabDelivery.API — слой представления (контроллеры, middleware)

KebabDelivery.Application — бизнес-логика, DTO, сервисы

KebabDelivery.Domain — доменные модели, фабричные методы

KebabDelivery.Infrastructure — репозитории, работа с БД

KebabDelivery.Tests — unit и integration тесты

Как запустить проект

1. Настройка базы данных

Установите PostgreSQL и настройте подключение в appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=KebabDB;Username=postgres;Password=yourpassword"
}

2. Применение миграций

dotnet ef database update

3. Запуск API

dotnet run --project KebabDelivery.API

Запуск в Docker

Соберите Docker-образ:

docker build -t kebabdelivery .

Запустите контейнер:

docker run -p 5000:5000 kebabdelivery

API Документация

После запуска API доступен Swagger по адресу:

http://localhost:5000/swagger/index.html

Контрибьютинг

Если хотите внести вклад, создавайте pull request с описанием изменений.

Лицензия

Этот проект распространяется под MIT лицензией.

