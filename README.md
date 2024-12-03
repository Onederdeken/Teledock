Для запуска приложение необходимо скачать git, docker,docker-compose, dotnet
Теперь создайте рабочию директорию например example зайдите в нее через терминал и скачайте приложение git clone https://github.com/Onederdeken/Teledock
в direct должна появиться папка Teledock проваливемся туда в терминале cd Teledock из каталога example

дальше необходимо собрать приложение и восстановить зависимости

      cd Teledock.Domain
      dotnet restore
      dotnet build
      cd ../Teledock.Infrastructure
      dotnet restore
      dotnet build
      cd ../Teledock.Application
      dotnet restore
      dotnet build
      cd ..
      dotnet restore
      dotnet build
Если не выдало ошибок то отлично идем дальше

Перед запуском необходимо поднять контейнер и приминить миграции для созданией баз данных
  из директории Teledock

Установим dotnet ef глобально 

    dotnet tool install --global dotnet-ef
  
Так как миграции уже созданы пересоздавать их нет смысла но навяский случай или если сломалось применение миграций

      cd WebApi
      dotnet ef migrations add InitCommandLast --context DbCommand  --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
      dotnet ef migrations add initQueryLast --context DbQuery --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
      cd ..
      // эти две команды должы создать папку (если ее нет) Teledock.Infrastructure/Migrations с файлами

Перед примененим миграций поднимем контейнер с сервером mariadb

    docker-compose up -d
Применим миграции

      cd WebApi
      dotnet ef database update --context DbCommand --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj 
      dotnet ef database update --context DbQuery --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj // создатутся база данных с теми же схемами что и в миграциях
      cd ..
Если еще ничего не взорвалось можно запустить приложение

      dotnet run 
      
открываем браузер и переходим по Url :https://localhost:7105/swagger/index.html

  
      

    
