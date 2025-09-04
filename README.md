# AdPlatforms API

Простой веб‑сервис на **ASP.NET Core**, который хранит рекламные площадки в памяти и позволяет искать их по локациям.

## Запуск

1. Сначала открываем терминал (PowerShell, CMD или любой другой), клонируем проект и заходим нужную папку для запуска:
   ```bash
   git clone https://github.com/AcciGen/AdPlatforms.git
   cd AdPlatforms\src\AdPlatforms.API
   ```

2. Запускаем проект:
   ```bash
   dotnet run
   ```

3. Открываем в браузере Swagger UI для тестирования:
   ```
   http://localhost:5133/swagger
   ```

## 📌 Методы API

### 1. Загрузка площадок из файла
`POST /api/AdPlatforms/upload`

### 2. Поиск площадок по локации
`GET /api/AdPlatforms/search?location=%2Fru`

Поиск:
`/ru`

Ответ:
```json
[ "Яндекс.Директ" ]
```

## Формат входного файла
```
Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
Крутая реклама:/ru/svrd
```

## Тестирование

1. Если проекта нету то сначала открываем терминал (PowerShell, CMD или любой другой) и клонируем проект:
   ```bash
   git clone https://github.com/AcciGen/AdPlatforms.git
   ```

2. Открываем нужную папку для тестов:
   ```bash
   cd AdPlatforms\tests\AdPlatforms.Tests
   ```

3. Запускаем тесты:
   ```bash
   dotnet test --logger "console;verbosity=detailed"
   ```
