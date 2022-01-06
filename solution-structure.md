# Структура солюшена

Типовая структура солюшена на примере Iwentys:
- *ProjectName*.Common
  - Exceptions
  - Base utils
  - Logging
- *ProjectName*.Domain - описание домена, основная бизнес логика
- *ProjectName*.Endpoints/
  - WebClient
    - Modules - деление логики отдельных модулей
  - Api - ASP проект, который содержит конфигурацию
- *ProjectName*.Infrastructure/
  - Application
    - BackgroundServices - описание логики фоновой активности
    - Middlewares - описание мидлвар, которые используются в проекте
    - Options - различные модели, которые описывают опции и конфиги
  - Database - DbContext, конфигурация DbSet'ов.
  - Database.Seeding - логика сидинга, генерации фейковых данных
- *ProjectName*.Integrations/
  - *IntegrationName* - проект, который содержит логику интеграции с другими продуктами. Например, гугл диск или гитхаб.
- *ProjectName*.Modules/
  - *ModuleName* - Выделенный проект под определённый модуль, чтобы уменьшить связанность между компонентами
    - Dtos/ - модели, которые используются для реквестов и респонсов
    - Queries/ - CQRS квери
    - Commands/ - CQRS команды
- *ProjectName*.Tests/
  - Features/
    - *feature-name*Test