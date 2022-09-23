
# Naming

1. Основой нейминга является кодстайл майкрософта: <https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines>;
2. Имена должны быть читабельными и понятными, без сокращений и грамматических ошибок. Исключение – общепринятые сокращения, напр. i, j, k в циклах;

```csharp
// ПЛОХО
var a = new List<Student>();
var stds = new List<Student>();
var list = new List<Student>();

// ХОРОШО
var students = new List<Student>();
```
3. Не используйте сокращения;
4. Не используйте отрицание в названиях:

```charp
bool isRed = color == Colors.Red; // Good
bool isNotBlue = color != Colors.Blue; // Bad

if (!isRed // Ok
    && !isNotBlue) // It is not ok
    return ...
```
5. Используйте studentsCount и studenstList вместо numberOfStudents, listOfStudents;
6. Придерживайтесь нейминга в соответствии с A/HC/LC Pattern'ом. Имя состоит с `prefix? + action (A) + high context (HC) + low context? (LC)`.
   1. Action - это действие, которое выполняется (get, set, remove, etc.)
   2. HC и LC - это выделение более значимых элементов. Название метода GetUserAssignments подразумевает, что есть основная сущность (User) и ассоциированные зависимые (Assignments).

## Префиксы

1. Методы, предназначенные для доступа к данным, в результате которых может вернуться null, должны иметь префикс Find.

```csharp
// ХОРОШО
public Student? FindStudent(int id)
{
   /* ищем студента */

   // возвращаем студента вне зависимости от того, null это или нет
   return student;
}
```

2. Методы, предназначенные для доступа к данным всегда возвращающие ненулевое значение, должны иметь префикс Get (и бросать ошибку, если не могут вернуть не null).

   Правильно:
   https://github.com/kysect/CodeStyle/blob/dc89af472d14e0279e58b4a5a8bf829190049899/Samples/Proper/Person.cs#L22-L28

   Неправильно:
   https://github.com/kysect/CodeStyle/blob/dc89af472d14e0279e58b4a5a8bf829190049899/Samples/Invalid/Person.cs#L22-L28

3. Методы, пытающиеся выполнить действие, но не обязательно выполняющие его, должны иметь префикс Try.

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
