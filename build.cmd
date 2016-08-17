cd ARDA

cd Arda.Common
dotnet restore
cd ..

cd Arda.Main
dotnet restore
cd ..

cd Arda.Kanban
dotnet restore
cd ..

cd Arda.Permissions
dotnet restore
cd ..

cd Arda.Reports
dotnet restore
cd ..

REM publish

cd Arda.Main
dotnet publish -o bin/publish
cd ..

cd Arda.Kanban
dotnet publish -o bin/publish
cd ..

cd Arda.Permissions
dotnet publish -o bin/publish
cd ..

cd Arda.Reports
dotnet publish -o bin/publish
cd ..