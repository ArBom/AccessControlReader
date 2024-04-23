
# AccessControlReader
Project of RFID access control reader based on Raspberry Pi with use C#. It uses SQL to check the entrance eligibility and write the time of get in.

## About the App
One used finite state machines to design this software. You can see machine state diagram below.
```mermaid
stateDiagram-v2
    [*] --> Init
    Init --> Unconfig :SQL
    Unconfig --> Blocked :SQL
    Init --> Blocked :SQL
    Init --> Reading :SQL
    Unconfig --> Reading :SQL
    Blocked --> Reading :SQL
    Reading --> Blocked :SQL
    Unconfig --> Unconfig :SQL

    state Blocked {
        [*] --> _Blocked
        _Blocked --> BUnaOpen :DoorOpen
        BUnaOpen --> _Blocked :DoorClose
        _Blocked --> _Blocked :SQL
        _Blocked --> [*]
    }

    state Reading {
        [*] --> _Reading
        _Reading --> OpenRFID :ValidCard
        OpenRFID --> _Reading :t
        OpenRFID --> StillOpen :DoorOpen
        StillOpen --> _Reading :DoorClose
        StillOpen --> TooLondOpen :t
        TooLondOpen --> _Reading :DoorClose
        _Reading --> OpenButton :ButtonPres
        OpenButton --> _Reading :t
        OpenButton --> StillOpen :DoorOpen
        _Reading --> WrongCard :WrongCard
        WrongCard --> _Reading :t
        WrongCard --> RUnaOpen :DoorOpen
        _Reading --> RUnaOpen :DoorOpen
        RUnaOpen --> _Reading :DoorClose
            _Reading --> _Reading :SQL
        _Reading --> [*]
    }
```
The app starts the cyclic check of reader status, after boot itself. Reader can be switched off / blocked with SQL.

In time of use RFID card it check the entrance eligibility
```mermaid
sequenceDiagram
    actor U as User
    participant R as Reader
    participant D as Database
    U->>R: Getting close card to reader
    R->>D: Record existence check of card
    D->>R: Answer
    alt record exist
        R->>D: Take tier, user of card
        D->>R: Answer
        alt valid tier
            R->>D: Take user's data
            R->>D: Save record
            R->>U: entrance
        else invalid tier
            R->>D: Save record
            R->>U: no entry
        end
    else no exist 
        R->>D: Save data
        R->>U: no entrance
    end
```
## How to run
You need to install .NET on your Raspbian. To do it You should follow the instruction at [learn.microsoft.com](https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual)
To compile You need to install NuGet packages:
```
PM> NuGet\Install-Package Iot.Device.Bindings -Version 2.2.0
PM> NuGet\Install-Package Microsoft.EntityFrameworkCore -Version 7.0.5
PM> NuGet\Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 7.0.5
PM> NuGet\Install-Package Microsoft.EntityFrameworkCore.Tools -Version 7.0.5
PM> NuGet\Install-Package NetCoreAudio -Version 1.7.0
PM> NuGet\Install-Package System.Device.Gpio -Version 2.2.0
```

In time of first run App it create .xml file on your desktop. You should change connection string inside it, and if it is necessary, devices properties.

![connection string placement](https://onedrive.live.com/embed?resid=5B6E90429D9C8454%21343840&authkey=%21AP_X876Eg5EZ8uU&width=660&height=999999)


## Hardware
Wire and devices connection schema consistent with default .xml file and most popular components' variant on market is featured bellow.
Project foretakes a use of magnetic sensor to detect a door open, and monostable switch to open the door from inside.
![wiring schema](https://onedrive.live.com/embed?resid=5B6E90429D9C8454%21343799&authkey=%21ALK-FBKf9opecAE&width=2082&height=2391)
Individual components may use different supply voltages. Make you sure they are connected correctly. Particular attention should be sensitive to the electric-bolt, and secure the rest of components. On schamat at above employed 24V-electic-bolt to open a door lock.
In order to improve user-friendly communication and general make better design, several simple animations have been implemented, which are pesented in the table below. These animations are associated with states of device.

| Info | RFID | Blockade | No entry | Entrance | Alert | Exclamation mark |
|:---: |:---:| :---:| :---: |:---:| :---:| :---:|
| ![info_sign](https://github.com/ArBom/AccessControlReader/assets/59375967/01e67110-d704-4349-9ae1-40ff68d983eb) | ![rfid](https://github.com/ArBom/AccessControlReader/assets/59375967/a102a3ea-a4be-4c48-adb6-fddc29bf3ae5) | ![blocked](https://github.com/ArBom/AccessControlReader/assets/59375967/dae66bca-160e-4bea-aee3-ffa6d0a0d694) | ![stop_hand](https://github.com/ArBom/AccessControlReader/assets/59375967/b7eda6f6-b0e3-4d25-b880-02c9e1ce70aa) | ![rgbkit (4)-min](https://github.com/ArBom/AccessControlReader/assets/59375967/71aa65e1-d60f-47bc-bc3c-d7c7b1cad72e) | ![door_alert](https://github.com/ArBom/AccessControlReader/assets/59375967/ae5fbd22-4c08-477b-b092-b38b84289def) | ![exclamation_mark](https://github.com/ArBom/AccessControlReader/assets/59375967/f9d99e24-942e-4a36-b717-999dea16fc75) |

## Exceptions handling 
<details> 
  <summary>Click me to show at possible errors table</summary>
| №    | Type  | Module       | Descripion                                           | Icons  |
| :--- | :---: | :---         | :---                                                 |---:    |
| 1    |       | DataBase     |Unconfig; reader just added itself to SQL             |        |
| 2    |  ⚠️  | DataBase     |Lack of data about this reader at SQL                 |📄🛢    | 
| 7    |  💥   | DataBase     |ConnectionString is null or empty                     |📄🛢    | 
| 8    |  💥   | DataBase     |DonnectionString is: "❗❗❗ INSERT IT HERE ❗❗❗"        |📄🛢    | 
| 9    |  ⚠️  | DataBase     |Cannot to connect with DataBase                       |📄🛢🖧  |  
| 10   |  ️💥   | Configurator |Platform not supported (its not Linux)                |:(      |
| 11   |  💥   | Configurator |Cannot get the Desktop folder                         |:(      |
| 12   |  ️💥   | Configurator |Problem with default XML file (at app folder)         |:( 📄   |  
| 13   |  ⚠️  | Configurator |Cannot save default XML file at Desktop               |:( 📄   |  
| 14   |  💥   | Configurator |XML file exist on Desktop, but it cannot be read      |:( 📄   |  
| 15   |  ⚠️  | Configurator |Problem inside XML file                               |:( 📄   |  
| 16   |  ⚠️  | Configurator | MXL file exception                                   |📄      |  
| 17   |  ⚠️  | Configurator |Cannot to read XML value (ACConfig/Devices)           |📄      |  
| 18   |  ⚠️  | Configurator |Lack of XML Key                                       |📄      |  
| 20   |  ️💥   | DataBase     |Config XML element is null                            |📄🛢    | 
| 22   |  ️💥   | DataBase     |No reader ID                                          |:(      |
| 30   |  ️💥   | GPIO         |XML config GPIO el. is empty                          |📄📟    |  
| 31   |  ️⚠️  | GPIO         |Reading of hardware property (GPIO) from XML problem  |📄📟    | 
| 32   |  ️💥   | GPIO         |Bolt pin is closed                                    |📟      | 
| 33   |  ️⚠️  | GPIO         |Exit Button pin is closed                             |📟      | 
| 34   |  ️💥   | GPIO         |Cannot to set Bolt pin value                          |📟      | 
| 35   |  ️⚠️  | GPIO         |Cannot to open at least one pin                       |📟      | 
| 36   |  ️💥   | GPIO         |Bolt pin is closed                                    |📟      | 
| 37   |  ️⚠️  | GPIO         |Door Sensor pin is closed                             |📟      | 
| 40   |  ️💥   |RC522         |XML config RC522 el. is empty                         |📄📟 📡 |
| 41   |  ️️💥   |RC522         |Reading of hardware property (RC522) from XML proble  | 📄📟 📡|
| 42   |  ️️🛈   |RC522         |Unknown RC522 hardware version                        |📟📡    | 
| 43   |  ️💥   |RC522         |mfRc522 is not an object at software                  |📟📡    | 
| 50   |  ️⚠️  |Screen        |XML config screen el. is empty                        |📄📟    | 
| 51   |  ️⚠️  |Screen        |Reading of hardware property (Sreen) from XML problem |📄📟    | 
| 52   |  ️⚠️  |Screen        |Communication with screen problem                     |📟      | 
| 60   |  ️⚠️  |StateMachine  |XML config State Machine el. is empty                 |📄      |  
| 61   |  ️⚠️  |StateMachine  |Reading of state text communicat from XML problem     |📄      |  
| 74   |  ️🛈  | GPIO          |Green diod pin closed                                 |📟      | 
| 75   |  ️🛈  | GPIO          |Diod pin(s) closed                                    |📟      | 
| 77   |  ️🛈  | GPIO          |Red diod pin closed                                   |📟      | 
| 78   |  ️️🛈  | GPIO          |Time of bolt open cannot be shorter than 1s           |📄📟    | 
| 79   |  ️️🛈  | GPIO          |Time alert cannot be shorter than .5s                 |📄📟    | 
| 80   |  ️🛈️️  |Noises         |XML config Noises el. is empty                        |📄  🔇  | 
| 83   |  ️🛈️️  |Noises         |Cant open noise file                                  |🔇      |
| 84   |  ️🛈️️  |Noises         |Wrong format of noise file                            |🔇      |

Error type:
🛈- info
⚠️- warning
💥 -critical error

Icons:
🔇-noises
📡-RC522
📟-hardware
🖧-LAN / connection
🛢-SQL
📄-XML file
:( - internal
</details>

---
Source of noises used at project: https://mixkit.co <br>
License: [Creative Commons Attribution-NonCommercial-ShareAlike 4.0](https://creativecommons.org/licenses/by-nc-sa/4.0/legalcode)
