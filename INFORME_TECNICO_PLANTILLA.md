# INFORME TÉCNICO: RESTAURANTEAPI
## Evaluación Parcial Preliminar – Desarrollo de Servicios Web

**Instituto:** IDAT  
**Proyecto:** RestauranteApi – API REST de reservas para restaurante  
**Curso:** Desarrollo de Servicios Web  
**Docente:** [NOMBRE DEL DOCENTE]  
**Integrantes (3):** [NOMBRE 1], [NOMBRE 2], [NOMBRE 3]  
**Repositorio GitHub:** [PEGAR ENLACE REAL]

## 1. Introducción
El proyecto RestauranteApi implementa una API RESTful con C# y ASP.NET Core para organizar clientes, mesas y reservas de un restaurante, utilizando listas en memoria como persistencia temporal.

## 2. Problema identificado
El registro manual de reservas puede ocasionar cruces de horarios, dificultades para consultar disponibilidad y pérdida de información. La API proporciona una interfaz HTTP centralizada para registrar y gestionar reservas.

## 3. Objetivo
Desarrollar una API REST que permita consultar, registrar, modificar y cancelar reservas, aplicando programación orientada a objetos, interfaces, arquitectura por capas, validaciones y manejo centralizado de errores.

## 4. Tecnologías
C#, .NET 10, ASP.NET Core Web API, estructuras List<T>, Visual Studio Code, Postman y GitHub (pendiente de enlace).

## 5. Estructura del código
- `Models`: Cliente, Mesa y Reserva.
- `DTOs`: solicitudes y respuestas con DataAnnotations.
- `Repositories`: interfaces y listas en memoria.
- `Services`: reglas de negocio de aforo, horarios y disponibilidad.
- `Controllers`: endpoints HTTP.
- `Exceptions` y `Middlewares`: gestión centralizada de errores.
- `Program.cs`: registro de dependencias y configuración.

## 6. Endpoints principales
| Verbo | Ruta | Función |
|---|---|---|
| GET | /api/mesas | Lista las mesas |
| GET | /api/clientes | Lista los clientes |
| POST | /api/clientes | Registra clientes |
| GET | /api/reservas | Lista las reservas |
| GET | /api/reservas/{id} | Consulta reserva |
| POST | /api/reservas | Crea reserva |
| PUT | /api/reservas/{id} | Actualiza reserva |
| DELETE | /api/reservas/{id} | Cancela reserva lógica |
| PATCH | /api/reservas/{id}/finalizar | Finaliza reserva |

## 7. Pruebas funcionales con Postman
**PENDIENTE: añadir capturas reales, con URL, verbo, Body (si procede), JSON y estado HTTP.**
1. GET mesas: capturar 200 OK.
2. GET reservas: capturar 200 OK.
3. POST cliente correcto: capturar 201 Created.
4. POST reserva correcta: capturar 201 Created.
5. PUT reserva: capturar 200 OK.
6. DELETE reserva: capturar 204 No Content.
7. POST datos inválidos: capturar 400 Bad Request.
8. GET identificador inexistente: capturar 404 Not Found.
9. POST reserva duplicada en mesa y hora: capturar 409 Conflict.

## 8. Responsabilidades del equipo
- Integrante 1: [NOMBRE Y TRABAJO REAL EFECTUADO]
- Integrante 2: [NOMBRE Y TRABAJO REAL EFECTUADO]
- Integrante 3: [NOMBRE Y TRABAJO REAL EFECTUADO]

## 9. Conclusiones
La arquitectura por capas facilita la separación de responsabilidades. Los repositorios en memoria permiten estudiar operaciones CRUD sin base de datos y las validaciones reducen registros inválidos. Al reiniciar la API, los cambios temporales se pierden, conforme al alcance educativo del parcial.

## 10. Enlace del repositorio
[PENDIENTE: PEGAR ENLACE GITHUB CON ACCESO PARA EL DOCENTE]
