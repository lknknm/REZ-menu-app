### How to Build
REZ uses .NET Framework with WinUI as User Interface Layer (Windows only) and Visual Studio as build system. To be able to build this project properly, it is necessary to have [Visual Studio 2022 17.1 and later](https://visualstudio.microsoft.com/pt-br/) installed and running. 

1. After setting up Visual Studio, make sure to check the workloads you have installed under `Visual Studio Installer`. 
For this project it is necessary to have the `.NET Desktop Development` workload installed and `Windows App SDK C# Templates` in the installation details sidebar:

![Alt text](./assets/VisualStudioInstaller.jpg)

#### Other dependencies:
##### Fonts:
- [Segoe UI Variable Font](https://aka.ms/SegoeUIVariable);
- [Segoe Fluent Icons](https://aka.ms/SegoeFluentIcon);
##### Nuget Packages:
- [Microsoft.UI.Xaml](https://github.com/microsoft/microsoft-ui-xaml);
- [Microsoft.Widnows.SDK.BuildTools](https://aka.ms/WinSDKProjectURL);
- [Microsoft.WindowsAppSDK](https://github.com/microsoft/windowsappsdk);
- [Newtonsoft.Json](https://www.newtonsoft.com/json);
- [System.Data.SqlClient](https://github.com/dotnet/corefx);

2. Download the source code of this project by forking (if you want to work on your own features) or by cloning this repository.
```bash
git clone https://github.com/lknknm/REZ-menu-app.git
```

3. In Visual Studio go to `File > Open > Open Project/Solution` and select `./REZ/REZ.sln` to open the project.

4. Now you can Build Solution (Ctrl+B) and execute it normally with Visual Studio debug features.