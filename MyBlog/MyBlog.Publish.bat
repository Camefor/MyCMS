color B

del ..\MyBlog\bin\Release\publish\UseBatTool\*.*   /s /q

dotnet restore

dotnet build

cd Blog.Core

dotnet publish -o ..\MyBlog\bin\Debug\netcoreapp3.1\

md ..\MyBlog\bin\Release\publish\UseBatTool

xcopy ..\MyBlog\bin\Debug\netcoreapp3.1\*.* ..\MyBlog\bin\Release\publish\UseBatTool\ /s /e 

echo "Successfully! please see the file MyBlog\bin\Release\netcoreapp3.1\publish\UseBatTool"

cmd