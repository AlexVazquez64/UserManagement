# UserManagement: Aplicación CRUD de Gestión de Usuarios

Este proyecto es una aplicación CRUD (Crear, Leer, Actualizar, Eliminar) para gestionar usuarios, desarrollada con Angular para el frontend, C# (.NET Core) para el backend y SQL Server como base de datos. Incluye autenticación JWT y buenas prácticas de seguridad.

## Requisitos Previos

* **Docker Toolbox:** Asegúrate de tener Docker Toolbox instalado y configurado en tu sistema. Puedes descargarlo e instalarlo desde [https://docs.docker.com/toolbox/toolbox_install_mac/](https://docs.docker.com/toolbox/toolbox_install_mac/).

* **Azure Data Studio (u otro GUI de SQL Server):** Necesitarás un cliente de interfaz gráfica para interactuar con la base de datos SQL Server. Azure Data Studio es una buena opción gratuita.

## Configuración y Ejecución

### 1. Iniciar Docker Quickstart Terminal

Abre la aplicación Docker Quickstart Terminal. Esta terminal te proporcionará un entorno de línea de comandos adecuado para trabajar con Docker Toolbox.

### 2. Iniciar la Máquina Virtual de Docker

Si la máquina virtual `default` de Docker Toolbox no está en ejecución, iníciala con el siguiente comando:

```bash
docker-machine start default
```

### 3. Iniciar SQL Server en Docker

Ejecuta el siguiente comando para iniciar el contenedor de SQL Server:

```bash
docker run -d --name sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=TuContraseñaSegura123' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest
```

**Importante:** Reemplaza `TuContraseñaSegura123` con una contraseña fuerte para el usuario `SA` de SQL Server.

### 4. Obtener la Dirección IP de la Máquina Virtual

Ejecuta el siguiente comando para obtener la dirección IP de la máquina virtual `default`:

```bash
docker-machine ip default
```

La salida de este comando te mostrará la dirección IP, por ejemplo: `192.168.99.100`. Anota esta dirección IP, ya que la necesitarás para la conexión a la base de datos.

### 5. Configurar la Conexión a la Base de Datos

1. Abre el archivo `appsettings.json` en la carpeta `UserManagementAPI`.
2. Modifica la cadena de conexión `DefaultConnection` de la siguiente manera:

   ```json
   "DefaultConnection": "Server=TU_DIRECCION_IP,1433;Database=UserManagement;User Id=SA;Password=TuContraseñaSegura123;"
   ```

Reemplaza `TU_DIRECCION_IP` con la dirección IP que obtuviste en el paso anterior y `TuContraseñaSegura123` con la contraseña que estableciste para SQL Server.

### 6. Iniciar el Backend (C# .NET Core)

Abre una nueva terminal (no la de Docker Quickstart) y navega a la carpeta `UserManagementAPI`. Luego, ejecuta el siguiente comando para iniciar el backend:

```bash
dotnet run
```

El backend se iniciará y estará escuchando en `http://localhost:5000` (o en el puerto que hayas configurado).

### 7. Iniciar el Frontend (Angular)

**Próximamente:** Las instrucciones para iniciar el frontend se agregarán aquí una vez que hayamos desarrollado esa parte del proyecto.

## Contribuciones

¡Las contribuciones son bienvenidas! Si encuentras algún error o tienes alguna sugerencia de mejora, no dudes en abrir un issue o enviar un pull request.

## Versiones

* **.NET Core:** 6.0
* **Angular:** (Se especificará una vez que se desarrolle el frontend)
* **Entity Framework Core:** 6.0.21
* **SQL Server:** 2019 (utilizando Docker)
