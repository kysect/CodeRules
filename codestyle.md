# Codestyle

## General

1. Код должен быть простым, ясным и читаемым, в той мере, в которой это возможно для реализации требуемой функциональности.
2. Код должен быть самодокументируемым. Комментируйте только неочевидные вещи – костыли, воркэраунды, почему именно так сделано.
3. Нужно стремиться к минимизации статических методов и классов.

## Naming

1. Основой нейминга является кодстайл майкрософта: <https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines>.
2. Имена должны быть читабельными и понятными, без сокращений и грамматических ошибок. Исключение – общепринятые сокращения, напр. i, j, k в циклах.

```csharp
// ПЛОХО
var a = new List<Student>();
var stds = new List<Student>();
var list = new List<Student>();

// ХОРОШО
var students = new List<Student>();
```

3. Методы, предназначенные для доступа к данным, в результате которых может вернуться null, должны иметь префикс Find.

```csharp
// ХОРОШО
public Student? FindStudent(int id)
{
   /* ищем студента */

   // возвращаем студента вне зависимости от того, null это или нет
   return student;
}
```

4. Методы, предназначенные для доступа к данным всегда возвращающие ненулевое значение, должны иметь префикс Get (и бросать ошибку, если не могут вернуть не null).\
\
   Правильно:
   https://github.com/kysect/CodeStyle/blob/dc89af472d14e0279e58b4a5a8bf829190049899/Samples/Proper/Person.cs#L22-L28

   Неправильно:
   https://github.com/kysect/CodeStyle/blob/dc89af472d14e0279e58b4a5a8bf829190049899/Samples/Invalid/Person.cs#L22-L28

5. Методы, пытающиеся выполнить действие, но не обязательно выполняющие его, должны иметь префикс Try.

```csharp
// ПЛОХО
public void TryWithdrawMoney(CreditCard creditCard, int password, double moneyToWithdraw)
{
   // если пароль совпадает, снимаем деньги
   if (creditCard.IsPasswordCorrect(password))
      creditCard.Withdraw(moneyToWithdraw);
}

// ХОРОШО
public bool TryWithdrawMoney(CreditCard creditCard, int password, double moneyToWithdraw)
{
   // если пароль не верный, возвращаем false
   if (!creditCard.IsPasswordCorrect(password))
      return false;

   // если пароль верный, то снимаем деньги
   creditCard.Withdraw(moneyToWithdraw);

   return true;
}
```

6. Используйте studentsCount и studenstList вместо numberOfStudents, listOfStudents.

## Variable and operator declaration

1. Для неочевидных числовых значений необходимо создавать именованные константы. Не используйте в коде магические числа.

```csharp
// ПЛОХО
public Student AddStudent(string name, string surname)
{
   var student = new Student(name, surname);

   // сравниваем с магическим числом
   if (_students.Count >= 20)
      throw new Exception("Students limit exceeded");

   _students.Add(stident);

   return student;
}

// ХОРОШО
public Student AddStudent(string name, string surname)
{
   var student = new Student(name, surname);

   // сравниваем с читаемой константов
   if (_students.Count >= MaxStudentsAmount)
      throw new Exception("Students limit exceeded");

   _students.Add(stident);

   return student;
}
```

2. Используйте var, только если тип переменной понятен из контекста.

```csharp
// ПЛОХО
var percents = bankAccount.CalculatePercents();

// ХОРОШО
double percents = bankAccount.CalculatePercents();
decimal percents = bankAccount.CalculatePercents();
Money percents = bankAccount.CalculatePercents();
```

3. Задавайте default в операторе switch. Если поведение не определено - кидайте исключение.

```csharp
// ПЛОХО
switch (deposit)
{
   case < 30:
      Console.WriteLine("Your percent is 3");
      break;
   case < 50:
      Console.WriteLine("Your percent is 5");
      break;
}

// ХОРОШО
switch (deposit)
{
   case < 30:
      Console.WriteLine("Your percent is 3");
      break;
   case < 50:
      Console.WriteLine("Your percent is 5");
      break;
   default:
      throw new Exception("Unexpected case");
}
```

4. При сравнении переменной с константой сначала указывается переменная, потом константа.

```csharp
// ПЛОХО
if (MaxStudentsAmount <= _students.Count)
   throw new Exception("Students limit exceeded");

// ХОРОШО
if (_students.Count >= MaxStudentsAmount)
   throw new Exception("Students limit exceeded");
```

5. Локальные переменные должны располагаться как можно ближе к месту использования.

```csharp
// ПЛОХО
public void SomeCalculations(List<int> nubmers)
{
   var oddOnly = new List<int>();
   var oddOnlyUnique = new List<int>();
   var oddOnlyUniqueLimited = new List<int>();
   int numbersCount;

   oddOnly.AddRange(numbers.Where(...));

   // какие-то вычисления 

   oddOnlyUnique.AddRange(oddOnly.Where(...));

   // какие-то вычисления 

   oddOnlyUniqueLimited.AddRange(oddOnlyUnique.Take(...));
}

// УЖЕ ЛУЧШЕ
public void SomeCalculations(List<int> nubmers)
{
   var oddOnly = numbers.Where(...);

   // какие-то вычисления 

   var oddOnlyUnique = oddOnly.Where(...));

   // какие-то вычисления 

   var oddOnlyUniqueLimited = oddOnlyUnique.Take(...);
   var numbersCount = oddOnlyUniqueLimited.Count();
}
```

6. Минимизируйте уровень вложенности, где это возможно без потери читаемости. Этого можно добиться инвертированием условного оператора if, декомпозицией логики. <https://refactoring.com/catalog/replaceNestedConditionalWithGuardClauses.html>.

```csharp
// не очень ХОРОШО
if (_students.Count >= MaxStudentsAmount)
{
   throw new Exception("Students limit exceeded");
}
else 
{
   _students.Add(stident);

   return student;
}

// ХОРОШО
if (_students.Count >= MaxStudentsAmount)
   throw new Exception("Students limit exceeded");

_students.Add(stident);

return student;
```

7. Не используйте boolean флаги для того, чтобы управлять условиями выхода из цикла.

```csharp
// ПЛОХО (не нада так пажалуста)
while (true)
{
   // что угодно тут будет плохо :)
   // особенно, если не будет brake;
}

// ЛУЧШЕ
for (int i = 0; i < studentsConut; ++i)
{
   // почти всё что угодно тут будет лучше чем предыдущий вариант
   // особенно, если тут не будет while (true)
}
```

## Method declaration

1. Метод, возвращающий коллекцию, в случае отсутствия элементов для возврата, должен возвращать пустую коллекцию, а не null.

```csharp
// ПЛОХО
public List<Student> FindStudents(int course)
{
   // объявляем лист (но не инициализируем)
   List<Student> students;

   /* ищем студентов любым возможным методом */

   // если студенты не нашлись, возвращаем null
   if (students.Count is null)
      return null;

   // возвращаем студентов, если хоть кто-то нашёлся
   return students;
}

// ХОРОШО
public List<Student> FindStudents(int course)
{
   // создаём пустой лист
   var students = new List<Student>();

   /* ищем студентов любым возможным методом */

   // возвращаем студентов даже, если это пустой лист (не null)
   return students;
}
```

2. Метод, который работает с пользовательскими аргументами, должен валидировать их.

```csharp
// ПЛОХО
public void FindStudentByFullName(string name, string surname)
{
   /* поиск студента без проверки входных данных */
}

// ХОРОШО
public void FindStudentByFullName(string name, string surname)
{
   if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Name to find student is empty");

   if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Surname to find student is empty");

   /* поиск студента после проверки входных данных */
}
```

3. В конструкторе должен соблюдаться порядок инициализации:
   1. Валидация аргументов
   2. Инициализация, которая не зависит от аргументов
   3. Инициализация полей аргументами
   4. Инициализация, которая требует какой-то логики, вызовов методов

```csharp
// ХОРОШО
public MegaFaculty(string facultyName)
{
   // валидация
   if (string.IsNullOrWhiteSpace(facultyName))
      throw new ArgumentException("Mega faculty name is empty");

   // инициализация, не зависящая от аргументов
   _courses = new List<OgnpCourse>();

   // инициализация полей аргументами
   Name = facultyName;

   /* сложная инициализация с вызовом различных методов */
   NotifyISU(this);
}
```

## Type declaration

1. Конструктор по умолчанию объявляйте явно.
2. Конструкторы должны полностью инициализировать объект. Валидация аргументов должна происходить в конструкторах.
3. Минимизируйте область доступа к данным. Предпочтительней хранить информацию в приватных полях нежели в публичных свойствах. Методы, которые не нужны внешнему коду, нужно делать приватными.
4. Не оставляйте мутабельные поля для отложенной инициализации. Инициализируйте поля в конструкторах и делайте иммутабельные поля и свойства, где это уместно.
5. Не использовать поля для передачи данных внутри метода или между методами класса.
6. Поддерживайте инвариант типа. Если у типа есть несколько полей, которые между собой связаны, то не должно быть способа изменить одно из полей и нарушить связь между ними.
7. Члены класса должны располагаться в следующем порядке:
   1. Константы
   2. Поля
   3. Свойства
   4. Конструкторы и Create-методы
   5. Публичные методы
   6. Приватные методы
8. Нумерация значений енама должна начинаться с 1. 0 может быть использован для Undefined значений.
9. Не используйте приватные свойства.
10. Не используйте оператор `==` для сравнения не числовых типов. Не переопределяйте оператор `==` для не числовых типов.
11. Не используйте наследование для переиспользования логики. Если объект наследуется, то справедливым должно быть высказывание, что производный объект является базовым (см. LSP).

## Exceptions

1. Все производные от Exception классы должны иметь постфикс "Exception".
2. Для ошибок бизнес логики стоит бросать кастомный эксепшен. Для стандартных ошибок, например, невалидных аргументов, стоит использовать стандартные типы.
3. Нужно обрабатывать, где это оправдано, ошибки NRE, OutOfRange etc и вместо них бросать более понятные ошибки, которые описывают проблемную ситуацию.
4. Если ошибка не может быть обработана, то её необходимо прокидывать дальше, а не игнорировать.
5. Если возникает ошибка при валидации аргументов, то нужно указывать, какой именно аргумент приводит к ошибке в тексте ошибки.

# Common

1. Методы расширения должны выделяться в специальные классы. Они должны иметь соответствующий постфикс Extensions.
2. Весь исходный код должен быть написан на английском. Это касается нейминга, комментариев и ошибок. Если есть необходимость использовать другой язык, то нужно применить инструменты локализации.
3. Для обозначения отсутствия значения стоит использовать null, а не default. Для значимых типов стоит возвращать Nullable<T>.
4. Избегайте кастов там, где можно их не использовать. Программа должна стремиться к повышению типизации и увеличению количества мест, где происходят проверки во время компиляции.
5. Минимизируйте количество ап кастов. Старайтесь не использовать более общие типы в сигнатурах, если они не поддерживаются.
6. При написании цепочки вызовов методов, переносите каждый вызов на отдельную строку.
7. Для проверки на null использовать конструкции `is null` и `is not null`.
8. Используйте Type.Parse вместо Convert.ToType (например, int.Parse вместо Convert.ToInt32).
9. Названия namespaces должны содержать только название проекта, не включать в себя папки. Чтобы не было конфликтов с анализаторами и Resharper, в `ProjectName.csproj.DotSettings` можно прописать опции NamespaceProvider = false. Сделать это можно в VS в свойствах папки.

# Restrict

1. Не используйте dynamic.
2. Не используйте goto.
3. Не пишите касты для своих типов - implicit или explicit.
4. Не используйте публичные вложенные типы.
5. Не используйте модификатор доступа internal. Исключение - если нет возможности использовать другой.
6. Не используйте reflection для доступа к полям.
7. Не используйте query-like LINQ.
8. Не используйте Tuple, ValueTuple и KeyValueTuple в сигнатурах своих методов. Вводите специальные типы, которые лучше описывают данную структуру.
9. Не пишите в лямбдах больше одной операции, выносите сложную логику в методы.
10. Не используйте char, short для экономии памяти. Большинство интерфейсов работают с int, а значит в коде появится много кастов, которые усложняют код.
11. Не используйте unsigned для гарантии, что значение будет больше нуля. Большинство интерфейсов работают с int, а значит в коде появится много кастов, которые усложняют код.
