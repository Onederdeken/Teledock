��� ������� ����� ���������� ���������� ������� ������� ����������. � ��� �� ��������� ������ ��������� git clone https://github.com/Onederdeken/Teledock
������ ��������� ������� dotnet, dotnet ef, docker, docker-compose, ������ ���������� ������� ��������� ��� ����� ��������� � ��������� ����� ../�������_����������/Teledock � � ��� ��������� ������� docker-compose up -d
��������� ��������� �������� ������� ��� ���� ������: �� ���������� Teledock/WebApi � ��������� ������ ��������� ��������� �������

                dotnet ef database update --context DbCommand --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
                dotnet ef database update --context DbQuery --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
������ ����� ���������.

�������� ������ 
�� ���������� Teledock/WebApi

            dotnet ef migrations add InitCommandLast --context DbCommand  --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
            dotnet ef database update --context DbCommand --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj # ������ �������� ��� ���� ������ dbCommand
            dotnet ef migrations add initQueryLast --context DbQuery --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj
            dotnet ef database update --context DbQuery --project ../Teledock.Infrastructure/Teledock.Infrastructure.csproj --startup-project Teledock.csproj # ������ �������� ��� ���� ������ dbQuery
            
����� ���� ���������� ������� ���������.