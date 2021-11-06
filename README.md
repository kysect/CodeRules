# Процедура добавления нового правила стиля / изменения существующего
1. Создать новый Discussion
2. Описать ситуацию применения правила стиля
   + Если стиль уже существует прикрепить [пермалинк](https://docs.github.com/en/github/writing-on-github/working-with-advanced-formatting/creating-a-permanent-link-to-a-code-snippet) существующих правил и сэмплов кода к дисскуссии
3. Прикрепить сниппет с пирмером предложенного стиля (альтернативные варианты - опционально)
4. По завешению дискуссии создать ишую со следующим содержанием
   + Описание ситуации применения стиля
   + Сниппет с примером кодстайла полученного в ходее дискуссии
   + Правила `.editorconfig` для настройки этих стилей
5. Создать пр со следующим содержанием
   + Добавление правил настройки стилей в `.editorconfig` с документацией ситуации применения стиля (кратко, 1-2 строчки комментария)
   + Добавление сэмпла кода, если нововведённые правила не относятся к уже существующим сэмплам


## Важно
Если ваш пр имеет в себе изменения не свзяанные с описанной ситуацией применения стилей, то вы где-то напартачили, такой пр не будет вмержен. \
Если вы оказались в ситуации, когда в уже сделанном коммите есть посторонние изменения, воспользуйтесь функцией `Ammend`. \
У пр по принятию стиля не должно быть диффа не отноящегося к ситуациеи его применения.