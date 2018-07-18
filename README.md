# Airport Api Async
Airport Rest Api with Async (TAP)

Все возможные запросы к базе данных и контроллеам выполняются асинхронно.

Хелпер метод который иммитирует длительную операцию можно вызвать и получить список по ссылке

[GET] `http://localhost:62444/api/flight/listdelay`

Метод получающий со сторонего API информацию и записывает ее асинхронно в бд и в csv файл по ссылке

[POST] `http://localhost:62444/api/сrew/crewload`
