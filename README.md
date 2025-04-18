# KebabDelivery

## 📌 О проекте
**KebabDelivery** — это **RESTful API** для управления заказами, меню и доставкой в шаурмичной.  
Проект разрабатывается в соответствии с принципами **Clean Code Architecture** и использует **ASP.NET Core**.

## 🚀 Технологии
- **Backend**: ASP.NET Core, Entity Framework Core  
- **Database**: PostgreSQL  
- **Logging**: Serilog  
- **Validation**: FluentValidation  
- **Testing**: xUnit, FluentAssertions, Moq, Testcontainers  
- **Authentication**: JWT (планируется)  
- **Storage**: Cloudinary (для изображений)  
- **Containerization**: Docker  

## 📂 Структура проекта
```plaintext
KebabDelivery/
│── KebabDelivery.sln
│── .gitignore
│── README.md  # Этот файл
│── KebabDelivery.API/           # Контроллеры, middleware
│── KebabDelivery.Application/   # Бизнес-логика, DTO, сервисы
│── KebabDelivery.Domain/        # Доменные модели, фабричные методы
│── KebabDelivery.Infrastructure/ # Репозитории, работа с БД
│── KebabDelivery.Tests/         # Unit и Integration тесты
```

## ⚙ Как запустить проект
### 1️⃣ Настройка базы данных
Установите PostgreSQL и настройте подключение в `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=KebabDB;Username=postgres;Password=yourpassword"
}
```

### 2️⃣ Применение миграций
```sh
dotnet ef database update
```

### 3️⃣ Запуск API
```sh
dotnet run --project KebabDelivery.API
```

## 🐳 Запуск в Docker
### 1. Соберите Docker-образ:
```sh
docker build -t kebabdelivery .
```

### 2. Запустите контейнер:
```sh
docker run -p 5000:5000 kebabdelivery
```

## 📜 API Документация
После запуска API доступен **Swagger** по адресу:
```sh
http://localhost:5000/swagger/index.html
```

## 🤝 Контрибьютинг
Если хотите внести вклад, следуйте этим шагам:

1. **Форкните** репозиторий.
2. **Создайте новую ветку** для ваших изменений:
   ```sh
   git checkout -b feature-branch
   ```
3. **Закоммитьте** изменения:
   ```sh
   git commit -m "Добавлена новая функция"
   ```
4. **Запушьте** в удалённый репозиторий:
   ```sh
   git push origin feature-branch
   ```
5. **Создайте Pull Request** на GitHub.

## 📜 Лицензия
Этот проект распространяется под **MIT лицензией**.

