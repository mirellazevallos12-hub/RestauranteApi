# RestauranteApi - API de reservas de un restaurante

API REST en ASP.NET Core (.NET 10) para administrar **clientes**, **mesas** y **reservas** de un restaurante.
**No usa base de datos**: todo se guarda en listas en memoria y se pierde al apagar la API.

## Cómo ejecutar

1. Instalar el SDK de .NET 10.
2. Abrir una terminal dentro de la carpeta `RestauranteApi`.
3. Ejecutar:

```
dotnet run --urls http://localhost:5000
```

## Cómo probar

1. Abrir Postman e importar `RestauranteApi.postmancollection.json`.
2. Ejecutar las peticiones en orden (primero se crea el cliente, luego la reserva).

## Endpoints

### Reservas (CRUD principal)

| Método | Ruta | Qué hace |
|--------|------|----------|
| POST | `/api/reservas` | Registra una reserva |
| GET | `/api/reservas` | Lista las reservas |
| GET | `/api/reservas/{id}` | Consulta una reserva |
| PUT | `/api/reservas/{id}` | Cambia fecha y cantidad de personas |
| PATCH | `/api/reservas/{id}/finalizar` | Marca la reserva como atendida |
| DELETE | `/api/reservas/{id}` | Cancela la reserva (estado "Cancelada") |

### Clientes

| Método | Ruta | Qué hace |
|--------|------|----------|
| POST | `/api/clientes` | Registra un cliente |
| GET | `/api/clientes` | Lista los clientes |
| GET | `/api/clientes/{dni}` | Consulta un cliente |
| PUT | `/api/clientes/{dni}` | Cambia nombre y teléfono |
| DELETE | `/api/clientes/{dni}` | Elimina el cliente (si no tiene reservas) |

### Mesas

| Método | Ruta | Qué hace |
|--------|------|----------|
| GET | `/api/mesas` | Lista las mesas |
| GET | `/api/mesas?fechaHora=2030-12-10T20:00:00` | Solo las mesas libres a esa hora |
| GET | `/api/mesas/{numero}` | Consulta una mesa |

Las mesas ya vienen creadas: mesa 1 (2 personas), 2 (4), 3 (4), 4 (6) y 5 (8).

## Validaciones

- **Data Annotations** en los DTOs: `[Required]`, `[StringLength]`, `[Range]`, `[RegularExpression]` (por ejemplo, el DNI debe tener 8 números).
- **Reglas de negocio** (en `ServicioReservas`):
  1. La fecha debe ser futura.
  2. El cliente debe estar registrado.
  3. La mesa debe existir y tener capacidad suficiente.
  4. Una mesa no puede tener dos reservas con menos de 2 horas de diferencia.

## Manejo de errores

`ManejoErroresMiddleware` captura las excepciones y responde siempre con este formato:

```json
{ "estado": 404, "mensaje": "No se encontró la reserva con id 999." }
```

| Excepción | Código | Cuándo ocurre |
|-----------|--------|---------------|
| `ReglaNegocioException` | 400 | Fecha pasada, mesa pequeña, estado no permitido |
| `RecursoNoEncontradoException` | 404 | Reserva, cliente o mesa que no existe |
| `ConflictoException` | 409 | DNI repetido, mesa ya reservada |
| Cualquier otra | 500 | Error inesperado |

## Estructura

```
RestauranteApi/
  Controllers/   ReservasController, ClientesController, MesasController -> reciben las peticiones HTTP
  Dtos/          Requests con las validaciones (Data Annotations)
  Modelos/       Cliente, Mesa, Reserva -> entidades
  Repositorios/  Interfaces y versiones en memoria -> guardan los datos en listas
  Servicios/     Interfaces y clases con las reglas de negocio
  Excepciones/   ReglaNegocioException, RecursoNoEncontradoException, ConflictoException
  Middlewares/   ManejoErroresMiddleware
  Program.cs     Configuración de la API
```

Flujo de una petición: **Controller → Servicio → Repositorio**.
