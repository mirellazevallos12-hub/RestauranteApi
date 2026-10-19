# RestauranteApi – Sistema de reservas de restaurante

API REST desarrollada con **C# y ASP.NET Core (.NET 10)** para la evaluación parcial de Desarrollo de Servicios Web.
El objetivo es organizar reservas, clientes y consultas de mesas sin conectar una base de datos.

## Requisitos
- .NET SDK 10 (`dotnet --version`)
- Visual Studio Code + extensión C# (opcional)
- Postman (para pruebas)

## Ejecutar en Windows / VS Code
1. Descomprime el ZIP y abre la carpeta `RestauranteApi` en VS Code.
2. Abre la terminal en esa carpeta.
3. Ejecuta `dotnet restore`.
4. Ejecuta `dotnet run --urls http://localhost:5000`.
5. En Postman usa `http://localhost:5000` como URL base.

**No utiliza MySQL, SQL Server ni Entity Framework.** Al detener la aplicación, los cambios desaparecen.

## Organización del proyecto
- `Models/`: entidades Cliente, Mesa y Reserva.
- `DTOs/`: datos de entrada y salida y validaciones por anotaciones.
- `Controllers/`: recibe y responde solicitudes HTTP.
- `Services/`: reglas de negocio (fechas, aforo, conflictos de horario).
- `Repositories/`: almacena objetos temporalmente mediante `List<T>`.
- `Exceptions/`: errores personalizados.
- `Middlewares/`: captura excepciones y convierte errores a JSON.
- `Program.cs`: configura las dependencias y las rutas.

## Inicio y uso desde terminal
- `GET /` devuelve información de la API (evita el 404 al abrir la dirección principal).
- **Terminal 1:** `dotnet run --urls http://localhost:5000` y dejar abierta.
- **Terminal 2:** `Invoke-RestMethod http://localhost:5000/api/mesas`.
- También puedes ejecutar `powershell -ExecutionPolicy Bypass -File .\pruebas\ProbarApi.ps1` en la segunda terminal para probar automáticamente códigos HTTP comunes.
- La API no tiene panel visual; es un proyecto backend.

## Endpoints
| Método | Ruta | Acción |
| --- | --- | --- |
| GET | `/api/clientes` | Listar clientes |
| GET | `/api/clientes/{dni}` | Consultar cliente |
| POST | `/api/clientes` | Registrar cliente |
| PUT | `/api/clientes/{dni}` | Modificar cliente |
| DELETE | `/api/clientes/{dni}` | Eliminar cliente sin historial de reservas |
| GET | `/api/mesas` | Listar mesas y su capacidad |
| GET | `/api/mesas?fechaHora=2027-12-10T20:00:00&personas=4` | Consultar disponibilidad para una fecha y personas |
| GET | `/api/mesas/{numero}` | Consultar mesa; el campo Disponible se calcula si se indica `?fechaHora=...`; sin fecha solo indica que no se está consultando ocupación en un horario |
| GET | `/api/reservas` | Listar reservas |
| GET | `/api/reservas/{id}` | Consultar reserva |
| POST | `/api/reservas` | Registrar reserva |
| PUT | `/api/reservas/{id}` | Actualizar reserva |
| PATCH | `/api/reservas/{id}/finalizar` | Finalizar atención |
| DELETE | `/api/reservas/{id}` | Cancelar reserva (no elimina el historial) |

## Ejemplos de JSON en Postman
POST `/api/clientes`:
```json
{"dni":"11112222","nombreCompleto":"María López","telefono":"987123456"}
```
POST `/api/reservas` (usa una fecha **futura** y una mesa libre):
```json
{"clienteDni":"11112222","numeroMesa":3,"fechaHora":"2027-12-10T19:00:00","cantidadPersonas":4}
```
PUT `/api/reservas/{id}`:
```json
{"fechaHora":"2027-12-10T21:30:00","cantidadPersonas":3}
```

## Reglas
- El cliente debe estar registrado antes de reservar.
- DNI de 8 dígitos; el número de personas no debe superar la capacidad de la mesa.
- La reserva debe hacerse para una fecha posterior al momento actual del servidor.
- Dos reservas confirmadas de una misma mesa deben estar separadas por **al menos 120 minutos**.
- Solo se pueden modificar o cancelar reservas confirmadas.
- `DELETE` cancela lógicamente la reserva y la conserva en el historial.
- Los datos residen solo en RAM (repositorios Singleton).
- La disponibilidad se calcula **para la fecha y hora indicadas**, no representa la ocupación física en tiempo real.
- La fecha se interpreta según la zona horaria local del servidor; en Postman usa fecha local sin sufijo Z.

## Códigos de respuesta
- `200` consulta/actualización; `201` creación; `204` cancelación/eliminación.
- `400` validación o fecha inválida; `404` recurso no encontrado; `409` conflictos; `500` error inesperado.

## Datos iniciales
- Mesas: 1 (2 personas), 2 (4), 3 (4), 4 (6), 5 (8).
- Clientes de práctica: DNI `12345678`, `87654321`.
- Dos reservas de ejemplo para mañana a las 20:00 y 21:00 (mesas 2 y 4).

## Postman
Importar `postman/RestauranteApi.postman_collection.json`. Ejecutar cada petición y guardar capturas del método, URL, cuerpo y código de respuesta. Cambiar las fechas de ejemplo si ya pasaron. Las capturas **no están incluidas**, deben realizarse con la API en ejecución.

## Equipo / entrega
Completar con los nombres de los **3 integrantes**, sus roles reales, capturas, enlace de GitHub y el informe PDF. Este proyecto es académico, no un sistema empresarial desplegado.

## Evidencias y limitaciones de entrega
- Se adjunta una colección Postman y un script PowerShell. **No sustituyen las capturas de Postman solicitadas por el docente.**
- Completa el informe técnico PDF con capturas auténticas, integrantes y enlace real de GitHub.
- El proyecto compila y funciona en Windows del estudiante según las salidas compartidas en conversación; **la presente revisión del paquete no incluye ejecución de pruebas en .NET**.
- Sin persistencia permanente: cerrar el servidor reinicia los registros a datos de ejemplo.

## Subir a GitHub (desde la carpeta RestauranteApi)
```powershell
git init
git add .
git commit -m "Entrega parcial RestauranteApi"
git branch -M main
git remote add origin https://github.com/TU_USUARIO/RestauranteApi.git
git push -u origin main
```
Crea primero un repositorio vacío con nombre RestauranteApi en GitHub, sustituye `TU_USUARIO` y configura tu acceso antes de ejecutar `git push`. No subas carpetas `bin`, `obj` ni secretos.
