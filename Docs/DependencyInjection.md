# Внедрение зависимостей и архитектура приложения
## VContainer
Ссылка на официальную документацию https://vcontainer.hadashikick.jp/
В качестве DI контейнера в проекте используется VContainer

### Использование интерфейсов VContainer вместо MonoBehavior
Большинство скриптов реализующих логику и не используют функционал MonoBehavior. Поэтому лучше писать скрипты на чистых классах и регистрировать их в контейнере. Это упрощает работу со скриптами, зависимости пробрасываются автоматически в конструктор, все ссылки между скриптами видны в IDE,  не нужно искать компоненты по разным префабам. Также это повышает производительность.

Если в скрипте необходимы доступы к некоторым методам MonoBehavior, например скрипт должен выполняться при каждом Update, нужно использовать интерфейсы предоставленные VContainer`ом

| Интерфейс VContainer	|   Аналог в MonoBehaviour|
| ------ | ------ |
| IStartable.Start()    |	MonoBehaviour.Start()|
| IAsyncStartable.StartAsync()  |	MonoBehaviour.Start()|
| IPostStartable.PostStart()    |	После MonoBehaviour.Start()|
| IFixedTickable.FixedTick()    |	MonoBehaviour.FixedUpdate()|
| IPostFixedTickable.PostFixedTick()    |	После MonoBehaviour.FixedUpdate()|
| ITickable.Tick()  |	MonoBehaviour.Update()|
| IPostTickable.PostTick()  |	После MonoBehaviour.Update()|
| ILateTickable.LateTick()  |	MonoBehaviour.LateUpdate()|
| IPostLateTickable.PostLateTick()  |	После MonoBehaviour.LateUpdate()|
Подробнее https://vcontainer.hadashikick.jp/integrations/entrypoint
Классы которые реализуют эти интерфейсы нужно регистрировать как EntryPoint
```cs
builder.RegisterEntryPoint<FooController>();
```

## Архитектура приложения
В игре используется несколько LifetimeScope выстроенных в иерархии. В каждом регистрируются определенные системы.

### RootLifetimeScope
LifetimeScope игры. Запускается в первую очередь. Нужен для регистрации систем которые нужны в течении всей игры от запуска до закрытия приложения.
> Примеры систем: Менеджер сцен, Сохранение, Глобальная статистика и т.д.

### GameSceneLifetemeScope
LifetimeScope игровой сцены. Запускается когда загружается сцена игры. Зависит от RootLifetimeScope. Нужен для регистрации систем на уровне, которые используются несколькими объектами на сцене.
> Примеры систем: Спаунер врагов, Система шума, Тревога, Коммуникация между врагами и т.д.

### PlayerLifetimeScope
LifetimeScope игрового персонажа. Зависит от GameSceneLifetemeScope. Тут регистрируются системы и компоненты игрока.
> Примеры систем: Здоровье и выносливость, системы передвижения, инвентарь и т.д.