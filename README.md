_RU_
_Симулятор 2d-сражений между командами ботов._

## Меню:
Уровни подгружаются из всех текстовых файлов в папке Files/Levels. Правила составления уровней см. ниже.
Для выбора уровня на экране меню используйте стрелочки, для подтверждения выбора используйте Enter.

## Основная игра:
После загрузки уровня вы увидите карту, представляющую из себя матрицу, на которой расположены элементы, подсвеченные разными цветами:
-Красным, синим, зеленым и розовым подсвечиваются бойцы соответственных команд.
-Желтым подсвечиваются стены.
-Серым подсвечивается земля.
Чтобы перевести сражение на следующий ход нажмите Enter.
По окончании сражения выведется специальное окно.
Для выхода из режима боя нажмите Esc.

## Типы юнитов:
-A - Archer - Слабый дальнобойный юнит
-M - Mage - Сильный, но хрупкой дальнобойный юнит
-W - Warrior - Обычный воин
-S - Spearman - Мощный ближник с расширенным радиусом атаки 
-P - Paladin - Очень толстый, но слабый ближник.
-R - Rogue - Быстрый, но хрупкий ближник.
-G - God - Литералли бог. Обычно не появялется в игре.

## Составление уровней:
Файл уровня представляет из себя текстовую матрицу разбитую на строки и разделенную символом пробела. В каждой ячейке матрицы находится один из символов:
-R - создание на данном месте случайного по типу бойца красной команды.
-B - создание на данном месте случайного по типу бойца синей команды.
-G - создание на данном месте случайного по типу бойца зеленой команды.
-M - создание на данном месте случайного по типу бойца розовой команды.
-O - создание на данном месте стены.
-Любое другое значение строки в ячейке создаст пустой элемент на данном месте (рекомендуется использование символа ".").
