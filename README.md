ƒл€ запуска этого приложени€ необходимо создать рабочию директорию. ¬ ней из командной строки прописать git clone https://github.com/Onederdeken/Teledock
ƒальше требуетс€ скачать dotnet, dotnet ef, docker, docker-compose, дальше необходимо подн€ть контейнер дл€ этого отркройте в терминале папку ../рабоча€_директори€/Teledock и в ней пропишите команду docker-compose up -d
требуетс€ применить миграции создать две базы данных: из директории Teledock/WebApi в командной строке вызываемм следующие команды

                dotnet ef database update --context DbCommand --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
                dotnet ef database update --context DbQuery --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
“еперь можно запускать.

навс€кий случай 
из директории Teledock/WebApi

            dotnet ef migrations add InitCommandLast --context DbCommand  --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
            dotnet ef database update --context DbCommand --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj # делаем миграции дл€ базы данных dbCommand
            dotnet ef migrations add initQueryLast --context DbQuery --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
            dotnet ef database update --context DbQuery --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj # делаем миграции дл€ базы данных dbQuery
            
ѕеред этим необходимо подн€ть контейнер.