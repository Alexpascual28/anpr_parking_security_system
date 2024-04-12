<p align="center">
  <img src="https://raw.githubusercontent.com/PKief/vscode-material-icon-theme/ec559a9f6bfd399b82bb44393651661b08aaf7ba/icons/folder-markdown-open.svg" width="100" alt="project-logo">
</p>
<p align="center">
    <h1 align="center">ANPR Parking Security System</h1>
</p>
<p align="center">
    <em><code>Created interface software to communicate with ANPR camera, building a computer vision algorithm to detect permitted cars at the company parking lot, for security purposes.</code></em>
</p>

<br><!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary><br>

- [ Overview](#-overview)
- [ Features](#-features)
- [ Repository Structure](#-repository-structure)
- [ Modules](#-modules)
- [ Getting Started](#-getting-started)
  - [ Installation](#-installation)
  - [ Usage](#-usage)
  - [ Tests](#-tests)
- [ Project Roadmap](#-project-roadmap)
- [ Contributing](#-contributing)
- [ License](#-license)
- [ Acknowledgments](#-acknowledgments)
</details>
<hr>

---

##  Repository Structure

```sh
└── anpr_parking_security_system/
    ├── ANPR
    │   ├── ANPR.csproj
    │   ├── ANPR.csproj.user
    │   ├── ANPR.sln
    │   ├── AlarmSubscription.cs
    │   ├── App.config
    │   ├── CHCNetSDK.cs
    │   ├── ConfigureANPR.cs
    │   ├── HCNetSDK.cs
    │   ├── Program.cs
    │   ├── Properties
    │   ├── ReceiveAlarmArming.cs
    │   ├── ReceiveAlarmListening.cs
    │   ├── References
    │   ├── bin
    │   └── obj
    ├── ANPRTEst
    │   ├── ANPRTEst.csproj
    │   ├── ANPRTEst.sln
    │   ├── App.config
    │   ├── CHCNetSDK.cs
    │   ├── Program.cs
    │   ├── Properties
    │   ├── References
    │   ├── bin
    │   └── obj
    ├── ARAD SDK-ANPR
    │   ├── ARAD SDK-ANPR.sln
    │   ├── ARAD SDK-ANPR.vcxproj
    │   ├── ARAD SDK-ANPR.vcxproj.filters
    │   ├── ARAD SDK-ANPR.vcxproj.user
    │   ├── Source.cpp
    │   └── x64
    ├── ARAD_ANPR
    │   ├── ARAD_ANPR.csproj
    │   ├── ARAD_ANPR.csproj.user
    │   ├── ARAD_ANPR.sln
    │   ├── App.config
    │   ├── App.xaml
    │   ├── App.xaml.cs
    │   ├── CHCNetSDK.cs
    │   ├── Properties
    │   ├── References
    │   ├── UserInterface
    │   ├── arad_anpr
    │   ├── bin
    │   └── obj
    ├── README.md
    └── test1.cpp
```

---

##  Modules

<details closed><summary>.</summary>

| File                                                                                                 | Summary                         |
| ---                                                                                                  | ---                             |
| [test1.cpp](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/test1.cpp) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ANPRTEst</summary>

| File                                                                                                                      | Summary                         |
| ---                                                                                                                       | ---                             |
| [CHCNetSDK.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPRTEst/CHCNetSDK.cs)       | <code>► INSERT-TEXT-HERE</code> |
| [ANPRTEst.sln](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPRTEst/ANPRTEst.sln)       | <code>► INSERT-TEXT-HERE</code> |
| [Program.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPRTEst/Program.cs)           | <code>► INSERT-TEXT-HERE</code> |
| [ANPRTEst.csproj](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPRTEst/ANPRTEst.csproj) | <code>► INSERT-TEXT-HERE</code> |
| [App.config](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPRTEst/App.config)           | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ANPRTEst.Properties</summary>

| File                                                                                                                                 | Summary                         |
| ---                                                                                                                                  | ---                             |
| [AssemblyInfo.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPRTEst/Properties/AssemblyInfo.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ANPRTEst.obj.Debug</summary>

| File                                                                                                                                                                          | Summary                         |
| ---                                                                                                                                                                           | ---                             |
| [ANPRTEst.csproj.FileListAbsolute.txt](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPRTEst/obj/Debug/ANPRTEst.csproj.FileListAbsolute.txt) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ANPRTEst.bin.Debug</summary>

| File                                                                                                                                        | Summary                         |
| ---                                                                                                                                         | ---                             |
| [ANPRTEst.exe.config](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPRTEst/bin/Debug/ANPRTEst.exe.config) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ARAD SDK-ANPR</summary>

| File                                                                                                                                                       | Summary                         |
| ---                                                                                                                                                        | ---                             |
| [ARAD SDK-ANPR.sln](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD SDK-ANPR/ARAD SDK-ANPR.sln)                         | <code>► INSERT-TEXT-HERE</code> |
| [ARAD SDK-ANPR.vcxproj.filters](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD SDK-ANPR/ARAD SDK-ANPR.vcxproj.filters) | <code>► INSERT-TEXT-HERE</code> |
| [ARAD SDK-ANPR.vcxproj](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD SDK-ANPR/ARAD SDK-ANPR.vcxproj)                 | <code>► INSERT-TEXT-HERE</code> |
| [ARAD SDK-ANPR.vcxproj.user](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD SDK-ANPR/ARAD SDK-ANPR.vcxproj.user)       | <code>► INSERT-TEXT-HERE</code> |
| [Source.cpp](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD SDK-ANPR/Source.cpp)                                       | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ARAD SDK-ANPR.x64.Debug.ARAD SDK-ANPR.tlog</summary>

| File                                                                                                                                                                                  | Summary                         |
| ---                                                                                                                                                                                   | ---                             |
| [ARAD SDK-ANPR.lastbuildstate](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD SDK-ANPR/x64/Debug/ARAD SDK-ANPR.tlog/ARAD SDK-ANPR.lastbuildstate) | <code>► INSERT-TEXT-HERE</code> |
| [unsuccessfulbuild](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD SDK-ANPR/x64/Debug/ARAD SDK-ANPR.tlog/unsuccessfulbuild)                       | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ANPR</summary>

| File                                                                                                                                    | Summary                         |
| ---                                                                                                                                     | ---                             |
| [ANPR.sln](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/ANPR.sln)                                 | <code>► INSERT-TEXT-HERE</code> |
| [ConfigureANPR.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/ConfigureANPR.cs)                 | <code>► INSERT-TEXT-HERE</code> |
| [CHCNetSDK.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/CHCNetSDK.cs)                         | <code>► INSERT-TEXT-HERE</code> |
| [ReceiveAlarmListening.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/ReceiveAlarmListening.cs) | <code>► INSERT-TEXT-HERE</code> |
| [ReceiveAlarmArming.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/ReceiveAlarmArming.cs)       | <code>► INSERT-TEXT-HERE</code> |
| [Program.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/Program.cs)                             | <code>► INSERT-TEXT-HERE</code> |
| [App.config](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/App.config)                             | <code>► INSERT-TEXT-HERE</code> |
| [ANPR.csproj.user](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/ANPR.csproj.user)                 | <code>► INSERT-TEXT-HERE</code> |
| [ANPR.csproj](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/ANPR.csproj)                           | <code>► INSERT-TEXT-HERE</code> |
| [AlarmSubscription.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/AlarmSubscription.cs)         | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ANPR.Properties</summary>

| File                                                                                                                             | Summary                         |
| ---                                                                                                                              | ---                             |
| [AssemblyInfo.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/Properties/AssemblyInfo.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ANPR.obj.Debug</summary>

| File                                                                                                                                                                      | Summary                         |
| ---                                                                                                                                                                       | ---                             |
| [ANPR.csproj.FileListAbsolute.txt](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/obj/Debug/ANPR.csproj.FileListAbsolute.txt)         | <code>► INSERT-TEXT-HERE</code> |
| [ANPRTEst.csproj.FileListAbsolute.txt](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/obj/Debug/ANPRTEst.csproj.FileListAbsolute.txt) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ANPR.bin.Debug</summary>

| File                                                                                                                                    | Summary                         |
| ---                                                                                                                                     | ---                             |
| [ANPRTEst.exe.config](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ANPR/bin/Debug/ANPRTEst.exe.config) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ARAD_ANPR</summary>

| File                                                                                                                                   | Summary                         |
| ---                                                                                                                                    | ---                             |
| [App.xaml.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/App.xaml.cs)                     | <code>► INSERT-TEXT-HERE</code> |
| [CHCNetSDK.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/CHCNetSDK.cs)                   | <code>► INSERT-TEXT-HERE</code> |
| [ARAD_ANPR.csproj](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/ARAD_ANPR.csproj)           | <code>► INSERT-TEXT-HERE</code> |
| [App.xaml](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/App.xaml)                           | <code>► INSERT-TEXT-HERE</code> |
| [ARAD_ANPR.sln](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/ARAD_ANPR.sln)                 | <code>► INSERT-TEXT-HERE</code> |
| [ARAD_ANPR.csproj.user](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/ARAD_ANPR.csproj.user) | <code>► INSERT-TEXT-HERE</code> |
| [arad_anpr](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/arad_anpr)                         | <code>► INSERT-TEXT-HERE</code> |
| [App.config](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/App.config)                       | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ARAD_ANPR.Properties</summary>

| File                                                                                                                                              | Summary                         |
| ---                                                                                                                                               | ---                             |
| [Resources.resx](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/Properties/Resources.resx)               | <code>► INSERT-TEXT-HERE</code> |
| [AssemblyInfo.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/Properties/AssemblyInfo.cs)             | <code>► INSERT-TEXT-HERE</code> |
| [Settings.settings](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/Properties/Settings.settings)         | <code>► INSERT-TEXT-HERE</code> |
| [Resources.Designer.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/Properties/Resources.Designer.cs) | <code>► INSERT-TEXT-HERE</code> |
| [Settings.Designer.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/Properties/Settings.Designer.cs)   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ARAD_ANPR.obj.Debug</summary>

| File                                                                                                                                                                             | Summary                         |
| ---                                                                                                                                                                              | ---                             |
| [ARAD_ANPR.csproj.FileListAbsolute.txt](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/obj/Debug/ARAD_ANPR.csproj.FileListAbsolute.txt) | <code>► INSERT-TEXT-HERE</code> |
| [MainWindow.g.i.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/obj/Debug/MainWindow.g.i.cs)                                         | <code>► INSERT-TEXT-HERE</code> |
| [App.g.i.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/obj/Debug/App.g.i.cs)                                                       | <code>► INSERT-TEXT-HERE</code> |
| [ARAD_ANPR_Content.g.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/obj/Debug/ARAD_ANPR_Content.g.cs)                               | <code>► INSERT-TEXT-HERE</code> |
| [ARAD_ANPR_Content.g.i.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/obj/Debug/ARAD_ANPR_Content.g.i.cs)                           | <code>► INSERT-TEXT-HERE</code> |
| [ARAD_ANPR_MarkupCompile.lref](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/obj/Debug/ARAD_ANPR_MarkupCompile.lref)                   | <code>► INSERT-TEXT-HERE</code> |
| [App.g.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/obj/Debug/App.g.cs)                                                           | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ARAD_ANPR.obj.Debug.UserInterface</summary>

| File                                                                                                                                                   | Summary                         |
| ---                                                                                                                                                    | ---                             |
| [MainWindow.g.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/obj/Debug/UserInterface/MainWindow.g.cs)     | <code>► INSERT-TEXT-HERE</code> |
| [MainWindow.g.i.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/obj/Debug/UserInterface/MainWindow.g.i.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ARAD_ANPR.bin.Debug</summary>

| File                                                                                                                                           | Summary                         |
| ---                                                                                                                                            | ---                             |
| [ARAD_ANPR.exe.config](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/bin/Debug/ARAD_ANPR.exe.config) | <code>► INSERT-TEXT-HERE</code> |
| [ANPRTEst.exe.config](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/bin/Debug/ANPRTEst.exe.config)   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ARAD_ANPR.UserInterface</summary>

| File                                                                                                                                           | Summary                         |
| ---                                                                                                                                            | ---                             |
| [MainWindow.xaml](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/UserInterface/MainWindow.xaml)       | <code>► INSERT-TEXT-HERE</code> |
| [MainWindow.xaml.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/UserInterface/MainWindow.xaml.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>ARAD_ANPR.UserInterface.ViewModels</summary>

| File                                                                                                                                                              | Summary                         |
| ---                                                                                                                                                               | ---                             |
| [MainWindowViewModel.cs](https://github.com/Alexpascual28/anpr_parking_security_system.git/blob/master/ARAD_ANPR/UserInterface/ViewModels/MainWindowViewModel.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

---
