# EdynamicsLog TEST
## Crear un proyecto en .NET Core con Clean Architecture y soporte multitenant basado en el path de URL,

## Instalacion

clonamos el repositorio

```sh
git clone https://github.com/carlosconto/EdynamicsLog-test.git

//configuramos la cadena de conexion y agregamos los datos para jwt

/*
"ConnectionStrings": {
  "master": "Server=GENTLEMAN\\GENTLEMAN;Initial Catalog=EdynamicsLog; User ID=sa;Password=123; TrustServerCertificate=True",
  "product": "Server=GENTLEMAN\\GENTLEMAN;Initial Catalog=EdynamicsLog_Products; User ID=sa;Password=123; TrustServerCertificate=True"
},
"Jwt": {
  "Key": "V6doLUl2mbXei/RxH0u5xy0bkrOiybCAwRpxbnFpbXY=",
  "Issuer": "https://edynamicslog.com/",
  "Audience": "https://edynamicslog.com/"
},

*/

//ejecutando las migraciones en la consola del administrador de paquetes de visual studio
// se inicializa la tabla de Organizations con data dummy para probar los tenant
Add-Migration initialDb -Context EdynamicsLogContext -Project Infrastructure

Add-Migration initialDb -Context EdynamicsLogProductContext -Project Infrastructure

Update-Database -Context EdynamicsLogContext -Project Infrastructure

Update-Database -Context EdynamicsLogProductContext -Project Infrastructure
```

en el proyecto hay dos archivos .http con los endpoints para sus respectivas pruebas