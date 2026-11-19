# CRUD_D_E – Departamentos y Empleados

API REST para gestionar **departamentos** y **empleados**, construida con **ASP.NET Core Web API** y **Entity Framework Core (Code First)** sobre **SQL Server**, con documentación interactiva mediante **Swagger**.

## Características

- CRUD completo de departamentos y empleados.
- Relación uno a muchos: un departamento tiene muchos empleados.
- Si se elimina un departamento, sus empleados no se borran: quedan sin departamento (`IdDepartamento = null`).
- Consulta de los empleados de un departamento y de los empleados inactivos.
- Documentación Swagger en la ruta raíz (en entorno de desarrollo).
- CORS habilitado para cualquier origen.

## Tecnologías

- .NET 11 (`net11.0`)
- ASP.NET Core Web API
- Entity Framework Core 11 + SQL Server
- Swashbuckle (Swagger)

## Requisitos

- [.NET SDK 11](https://dotnet.microsoft.com/download)
- SQL Server (o SQL Server Express / LocalDB)
- Herramienta de EF Core: `dotnet tool install --global dotnet-ef`

## Configuración

1. Clona el repositorio:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd CRUD_D_E
   ```
2. Edita la cadena de conexión en `CRUD_D_E/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "Bd": "Data Source=TU_SERVIDOR;Initial Catalog=consultaEmpleado;Integrated Security=True;Encrypt=False;TrustServerCertificate=True"
   }
   ```
3. Aplica las migraciones para crear la base de datos:
   ```bash
   cd CRUD_D_E
   dotnet ef database update
   ```
4. Ejecuta el proyecto:
   ```bash
   dotnet run
   ```
5. Abre **http://localhost:5218**: te redirige automáticamente a Swagger (`/swagger/index.html`).

## Modelo de datos

**Departamento**

| Campo | Tipo | Descripción |
|---|---|---|
| Id | int | Identificador (PK) |
| Nombre | string? | Nombre del departamento |

**Empleado**

| Campo | Tipo | Descripción |
|---|---|---|
| Id | int | Identificador (PK) |
| Nombre | string? | Nombre del empleado |
| IdDepartamento | int? | FK a Departamento (opcional) |
| Salario | decimal(18,2) | Salario |
| Estatus | enum | `1` = Activo, `0` = Inactivo |

## Endpoints

### Departamento

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/Departamento` | Lista todos los departamentos |
| GET | `/api/Departamento/{id}` | Obtiene un departamento |
| GET | `/api/Departamento/{id}/Empleados` | Empleados de un departamento |
| POST | `/api/Departamento` | Crea un departamento |
| PUT | `/api/Departamento/{id}` | Actualiza un departamento |
| DELETE | `/api/Departamento/{id}` | Elimina un departamento |

### Empleado

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/Empleado` | Lista todos los empleados |
| GET | `/api/Empleado/{id}` | Obtiene un empleado |
| GET | `/api/Empleado/Inactivo` | Lista los empleados inactivos |
| POST | `/api/Empleado` | Crea un empleado |
| PUT | `/api/Empleado/{id}` | Actualiza un empleado (el `id` de la ruta debe coincidir con el del cuerpo) |
| DELETE | `/api/Empleado/{id}` | Elimina un empleado |

### Ejemplos

Crear un departamento:
```http
POST http://localhost:5218/api/Departamento
Content-Type: application/json

{ "nombre": "Recursos Humanos" }
```

Crear un empleado:
```http
POST http://localhost:5218/api/Empleado
Content-Type: application/json

{ "nombre": "Juan Pérez", "idDepartamento": 1, "salario": 50000, "estatus": 1 }
```

Más peticiones de prueba en [`CRUD_D_E/CRUD_D_E.http`](CRUD_D_E/CRUD_D_E.http).

## Estructura del proyecto

```
CRUD_D_E/
├── CRUD_D_E.slnx
├── Models/                        # Biblioteca de clases con las entidades
│   ├── Departamento.cs
│   └── Empleado.cs
└── CRUD_D_E/                      # Proyecto Web API
    ├── Controllers/
    │   ├── DepartamentoController.cs
    │   └── EmpleadoController.cs
    ├── Data/Bd.cs                 # DbContext
    ├── Migrations/                # Migraciones de EF Core
    ├── Properties/launchSettings.json
    ├── Program.cs
    └── appsettings.json
```
