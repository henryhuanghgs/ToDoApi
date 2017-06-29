# ToDoApi Tutorial Project

## Setup Environment

1. Install dotnet runtime in Mac
  https://www.microsoft.com/net/core#macos

2. Install Visual Studio 2017 for Mac, if you want to develop or debug in Visual Studio
  https://www.visualstudio.com/vs/visual-studio-mac/

3. Download the source code of this project (e.g. ~/workdir/ToDoApi)
```
  cd ~/workdir
  git clone https://github.com/henryhuanghgs/ToDoApi.git
```
## How to run
1. change current directory
```
  cd ~/workdir/ToDoApi
```
2. build
```
  dotnet restore
  dotnet build
```
3. unit test
```
  dotnet test ToDoApiTests/ToDoApiTests.csproj
```
4. create/update database (The database is sqllite by default. Its file is /tmp/todo.db)
```
  cd ToDoApi
  dotnet ef database update
```
5. run
```
  dotnet run
```
6. browse
  http://localhost:5000/api/todo
  http://localhost:5000/api/todo/1


## Note:
1. The project is ported from the dotnet tutorial 
  * Tutorial: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api-mac

2. Unit tests project (ToDoApiTests) is added

3. logging is added (Serilog.Extensions.Logging.File)




