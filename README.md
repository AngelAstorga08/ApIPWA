# API de productos

API ASP.NET Core 10 con Entity Framework Core y PostgreSQL. No requiere autenticación.

## Desarrollo local

1. Iniciar Docker Desktop.
2. Ejecutar `docker compose up -d` en esta carpeta para iniciar PostgreSQL con un volumen persistente.
3. Ejecutar `dotnet run` para iniciar la API en `http://localhost:5028`.

Al iniciar por primera vez, EF crea la tabla `Products` y agrega tres productos de ejemplo. Los datos posteriores permanecen en PostgreSQL. La cadena local se encuentra en `appsettings.Development.json`.

## Endpoints

| Método | Ruta | Acción |
| --- | --- | --- |
| GET | `/api/products` | Catálogo completo |
| GET | `/api/products/{id}` | Consultar producto |
| POST | `/api/products` | Alta de producto |
| PUT | `/api/products/{id}` | Actualizar producto |
| DELETE | `/api/products/{id}` | Eliminar producto |

Para POST y PUT, enviar JSON con `name`, `description`, `price` y `stock`. Hay ejemplos listos en `ApiParaPWA.http`.

## Render

Publicar esta carpeta como raíz de un repositorio y crear un Blueprint en Render con `render.yaml`. El Blueprint construye la imagen con `Dockerfile` y solicita el valor secreto `DATABASE_URL`. Pegar ahí la cadena de conexión de Neon. La base de datos se aloja en Neon; Render solo aloja la API. No guardar la cadena de conexión en el repositorio.
