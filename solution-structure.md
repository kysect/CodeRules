# Структура солюшена

Типовая структура солюшена на примере Iwentys:
- *ProjectName*.Common
  - Exceptions
  - Base utils
  - Logging
- *ProjectName*.Domain - описание домена, основная бизнес логика
- Web/
  - *ProjectName*.WebClient
    - Modules - деление логики отдельных модулей
  - *ProjectName*.WebApi - ASP проект, который содержит конфигурацию
- Infrastructure/
  - *ProjectName*.Application
    - BackgroundServices - описание логики фоновой активности
    - Middlewares - описание мидлвар, которые используются в проекте
    - Options - различные модели, которые описывают опции и конфиги
  - *ProjectName*.Database - DbContext, конфигурация DbSet'ов.
  - *ProjectName*.Database.Seeding - логика сидинга, генерации фейковых данных
- Integrations/
  - *ProjectName*.*IntegrationName* - проект, который содержит логику интеграции с другими продуктами. Например, гугл диск или гитхаб.
- Modules/
  - *ProjectName*.Modules.*ModuleName* - Выделенный проект под определённый модуль, чтобы уменьшить связанность между компонентами
    - Dtos/ - модели, которые используются для реквестов и респонсов
    - Queries/ - CQRS квери
    - Commands/ - CQRS команды
- Tests/
  - Features/
    - *feature-name*Test
