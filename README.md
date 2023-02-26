# Proyecto Lenguaje de programación

## Centro de Veterinaria y Estadias "Care - Stays EGGTP" 🐶😺🏠
_Aplicación para veterinarias y estadía de mascotas donde existe el registro de mascotas, reservación de dos tipos tanto médicas como de alojamiento, consultas de la información registrada dentro de la aplicación, eliminación de citas y mascotas en caso de requerirlo, entre otros._

### Pre-requisitos 📋

_Además tener un dispositivo móvil configurado previamente en el cual se pueda compilar la aplicación, caso contrario se puede instalar un emulador para la ejecución de la aplicación._-

### Frontend 👨‍💻
_Para empezar, instale Visual Studio y seleccione la opción "Desarrollo para dispositivos móviles con .NET" en el programa de instalación._

![requerimiento frontend](https://raw.githubusercontent.com/raochoa019/Pet_Care_EGTTP_APP/main/recursos/requerimiento_frontend.png)

_Proceda a clonar el proyecto, luego de eso podrá observar los siguientes archivos accediendo a la carpeta "Proyecto\_LP/PRY\_LENG\_PROG"_

![requerimiento frontend](https://raw.githubusercontent.com/raochoa019/Pet_Care_EGTTP_APP/main/recursos/solucionVisualStudio.JPG)

_Una vez abierta la solución podrá visualizar el entorno de desarrollo de la siguiente manera_

![requerimiento frontend](https://raw.githubusercontent.com/raochoa019/Pet_Care_EGTTP_APP/main/recursos/VisualStudio.JPG)

_Para la visualización del aplicativo será necesario ciertas configuraciones.\nDeberá tener activado el modo desarrollador, depuración usb, permitir la instalación de aplicaciones de terceros._
_Otra forma de probar el aplicativo es mediante un emulador que puede ser instalado en las mismas herramientas que provee Visual Studio_

_Dado que el proyecto se encuentra trabajando con Apis locales donde se conecta para realizar las debidas peticiones. Es necesario ejecutar el comando desde el simbolo del sistema de Adb de Android_
_Opcion que encontrará cómo lo ve en la imagen_

![requerimiento frontend](https://raw.githubusercontent.com/raochoa019/Pet_Care_EGTTP_APP/main/recursos/adConsole.png)

_En la consola deberá ejecutar el comando para que pueda realizar la conexión local con las peticiones para la visualización de los datos en la aplicación._
```
adb reverse tcp:800 tcp:8000
```

_Una vez aplicado todo lo mencionado anteriormente, en la sección superior podrá visualizar la siguiente opción que le permitirá ejecutar el aplicativo ya sea desde su dispositivo móvil o emulador_

![requerimiento frontend](https://raw.githubusercontent.com/raochoa019/Pet_Care_EGTTP_APP/main/recursos/compilarProyecto.png)

### Base de datos: MySQL Workbench 🗄
_Se está utilizando la herramienta visual de diseño de bases de datos MySQL Workbench para el almaecenamiento y consulta de información_

![requerimiento frontend](https://raw.githubusercontent.com/raochoa019/Pet_Care_EGTTP_APP/main/recursos/database.JPG)

### Backend 🗄️⚙️
_Para empezar debe asegurarse que su máquina local tenga PHP y Composer instalados._
_Una vez instalado los requerimientos anteriores podrá visualizar los archivos de la siguiente manera_

![requerimiento frontend](https://raw.githubusercontent.com/raochoa019/Pet_Care_EGTTP_APP/main/recursos/backend.JPG)

_Existen migraciones que se han realizado, para ejecutarlas y carga la información es necesario la ejecución del siguiente comando_
```
php artisan migrate:fresh --seed
```

_Dicho comando procederá a migrar la información a nuestra base de datos mencionada anteriormente cómo se ve en la imagen_

![requerimiento frontend](https://raw.githubusercontent.com/raochoa019/Pet_Care_EGTTP_APP/main/recursos/migraciones.JPG)

_Finalmente para el levantamiento de los servicios desde el backend es necesario la ejecución del siguiente comando_

```
php artisan serve
```

## Autores ✒️
* **Bryan Alava Calderon**
* **Tommy Beltrán Borbor**
* **Robert Ochoa Ramos**
* **Aaron Villao Mero**