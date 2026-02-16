# Мини веб служба и конвейер обработки запросов
## Тема: реестр парфюмерии
Практическое задание 1, Технологии разработки приложений на базе фреймворков

## Было реализовано:
1. Точка доступа для создания объекта парфюмерии
   <img src="./ReadmeAssets/CreateItem.png" alt="testCreateItem"/>
2. Точка доступа для получения парфюмерии по идентификатору
   <img src="./ReadmeAssets/GetItemById.png" alt="testGetItemById"/>
3. Точка доступа для получения списка объектов парфюмерии
   <img src="./ReadmeAssets/GetItems.png" alt="testGetItems"/>
   <img src="./ReadmeAssets/GetItemWithFilters.png" alt="testGetItemWithFilters"/>
4. Единый обработчик ошибок
   <img src="./ReadmeAssets/CreateItemError.png" alt="testCreateItemError"/>
   <img src="./ReadmeAssets/GetItemByIdError.png" alt="testGetItemByIdError"/>
5. Обработчик для логирования запроса, времени выполнения запроса и идентификатора запроса
   <img src="./ReadmeAssets/Logs.png" alt="Logs"/>
   В квадратных скобках пишется уникальный ID запроса. 