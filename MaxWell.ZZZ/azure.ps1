az group create --name maxwellrg --location "West Europe"
az sql server create --name maxwellsql --resource-group maxwellrg --location "West Europe" --admin-user maxwellsqladmin --admin-password maxwellsqlPass1!
az sql server firewall-rule create --resource-group maxwellrg --server maxwellsql --name AllowYourIp --start-ip-address 95.24.33.17 --end-ip-address 95.24.33.17
az sql server firewall-rule create --resource-group maxwellrg --server maxwellsql --name AllowYourIp2 --start-ip-address 52.178.75.200 --end-ip-address 52.178.75.200
az sql db create --resource-group maxwellrg --server maxwellsql --name coreDB --service-objective S0
az webapp deployment user set --user-name maxwelladmin --password MaxWellPass1!
az appservice plan create --name maxwellappsp --resource-group maxwellrg --sku FREE
az webapp create --resource-group maxwellrg --plan maxwellappsp --name maxwellwebapp --deployment-local-git
az webapp config connection-string set --resource-group maxwellrg --name maxwellwebapp --settings MyDbConnection='Server=tcp:maxwellsql.database.windows.net,1433;Database=coreDB;User ID=maxwellsqladmin;Password=maxwellsqlPass1!;Encrypt=true;Connection Timeout=30;' --connection-string-type SQLServer
az webapp config appsettings set --name maxwellwebapp --resource-group maxwellrg --settings ASPNETCORE_ENVIRONMENT="Production"
                                         52.178.75.200,52.178.94.222,52.178.102.126,52.178.88.128,52.178.91.167


Доступ в панель управления хостингом
Логин: u0620868

Пароль: N_9ChNNy

Панель управления: Parallels Plesk

Адрес панели управления хостингом: https://wpl24.hosting.reg.ru:8443

Доступ к FTP
Логин: u0620868

Пароль: N_9ChNNy

IP-адрес сервера: 31.31.196.199

Доступ к MySQL


Server=tcp:localhost,1433;Database=u0620868_maxwellsql;User ID=u0620868_maxwellsqladmin;Password=maxwellsqlPass1!;Encrypt=true;Connection Timeout=30;
u0620868_maxwellsql

u0620868_maxwellsql
u0620868_maxwellsqladmin
data source=.\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true