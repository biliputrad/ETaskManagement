# ETaskManagement

How to Install This API

- Install Dot Net Core 6
- Install PostgreSQL
- If you are have postgreSQL, just update appsettings.json and appsettings.Development.Json on ConnectionStrings change Default username, password, and Host base on your Database Settings.
- If you not have postgreSQL just create new host on and with username "postgres" and pass "123456"
- go to folder E-TaskManagement with terminal and copy this command "dotnet ef database update --project ./ETaskManagement.Infrastructure --startup-project ./ETaskManagement.Api" and press enter
- if database successful create just run this program and on your browser write "http://localhost:5002/index.html" and press enter
