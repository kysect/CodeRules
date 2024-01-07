# Codestyle

- [Naming](Naming.md)

## Formatting

1. Добавляйте переносы строк до и после многострочных элементов кода (кроме случаев когда элемент находится в конце блока). 
Однострочные элементы кода можно группировать без пропуска строк.

### Хорошо
```csharp
var location = "Yerevan";
var age = 69;

var relevantStudents = _students
   .Where(x => x.Location.Equals(location))
   .Where(x => x.Age.Equals(age));
   
foreach (var student in relevantStudents)
{
   Console.WriteLine($"Найден дед - {student.Name}");
}
```

### Плохо
```cs
var location = "Yerevan";
var age = 69;
var relevantStudents = _students
   .Where(x => x.Location.Equals(location))
   .Where(x => x.Age.Equals(age));
foreach (var student in relevantStudents)
{
   Console.WriteLine($"Найден дед - {student.Name}");
}
```

2. Лоически групиируйте члены типа, отделяя группы переносами строк. (При больших размерах групп, можно выделять подгруппы, по смысловому признаку, отделяя подгруппы пропусками строк)

### Хорошо

```cs
public class Student 
{
   public int Id { get; }
   public string FirstName { get; }
   public string LastName { get; }
   
   public string FullName => $"{FirstName} {LastName}";
   
   public override string ToString()
      => $"[{Id}] {FullName}";
}
```

### Плохо

```cs
public class Student 
{
   public int Id { get; }
   public string FirstName { get; }
   public string LastName { get; }
   public string FullName => $"{FirstName} {LastName}";
   public override string ToString()
      => $"[{Id}] {FullName}";
}
```

```cs
public class Student 
{
   public int Id { get; }

   public string FirstName { get; }

   public string LastName { get; }

   public string FullName => $"{FirstName} {LastName}";

   public override string ToString()
      => $"[{Id}] {FullName}";
}
```

3. Добавляйте перенос строк перед оператором `return`.

### Хорошо
```cs
public int Calculate()
{
   var x = 420 * 228;
   var y = x / 1337d;
   
   return x + y;
}
```

### Плохо
```cs
public int Calculate()
{
   var x = 420 * 228;
   var y = x / 1337d;
   return x + y;
}
```

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

## Property declaration

1. При объявлении автосвойств, помещайте аксессоры на одной строке с названием и типом
```cs
// ХОРОШО
public int Value { get; set; }

// ПЛОХО
public int Value
{ get; set; }
```
2. При объявлении get-only свойств, используйте bodied expressions вместо явного `get` аксессора
```cs
// ХОРОШО
public IReadOnlyCollection<string> Values => _values;

// ПЛОХО
public IReadOnlyCollection<string> Values
{
   get 
   {
      return _values;
   }
}

public IReadOnlyCollection<string> Values
{
   get => _values;
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

1. Для ошибок бизнес логики стоит бросать кастомный эксепшен. Для стандартных ошибок, например, невалидных аргументов, стоит использовать стандартные типы.
2. Нужно обрабатывать, где это оправдано, ошибки NRE, OutOfRange etc и вместо них бросать более понятные ошибки, которые описывают проблемную ситуацию.
3. Если ошибка не может быть обработана, то её необходимо прокидывать дальше, а не игнорировать.

# Common

1. Методы расширения должны выделяться в специальные классы. Они должны иметь соответствующий постфикс Extensions.
2. Весь исходный код должен быть написан на английском. Это касается нейминга, комментариев и ошибок. Если есть необходимость использовать другой язык, то нужно применить инструменты локализации.
3. Для обозначения отсутствия значения стоит использовать null, а не default. Для значимых типов стоит возвращать Nullable<T>.
4. Избегайте кастов там, где можно их не использовать. Программа должна стремиться к повышению типизации и увеличению количества мест, где происходят проверки во время компиляции.
5. Минимизируйте количество ап кастов. Старайтесь не использовать более общие типы в сигнатурах, если они не поддерживаются.
6. При написании цепочки вызовов методов, переносите каждый вызов на отдельную строку.
7. Используйте Type.Parse вместо Convert.ToType (например, int.Parse вместо Convert.ToInt32).

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

# Suggestions

1. При реализации методов создания или поиска, стоит возвращать сущности, а не какие-то их части - названия или идентификаторы. Если написано, что нужно найти магазин, то ожидается, что вернется не строка, а магазин.
2. Используйте специализированные проверки вида CollectionAssert.Contains(...) вместо Assert.IsTrue(collection.Contains(...)).
3. Не пишите сложную логику поверх примитивных типов, вводите информативные обёртки, в которых можно инкапсулировать валидацию и логику.
4. Стоит отделять логику ввода, вывода и бизнес-логику.


## Dto

1. Для простых DTO, где это возможно, стоит использовать `record`.
2. Если для DTO используется класс, то сеттеры должны быть приватными.
3. Если для DTO используется класс, то должен быть публичный пустой конструктор.
