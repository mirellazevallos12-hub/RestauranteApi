# Ejecutar en OTRA terminal PowerShell, con dotnet run activo en la primera.
# Ejemplo: powershell -ExecutionPolicy Bypass -File .\pruebas\ProbarApi.ps1
$ErrorActionPreference = 'Stop'
$base = 'http://localhost:5000'
function Probar($nombre, $metodo, $ruta, $cuerpo, $esperado) {
    try {
        $opciones = @{ Uri = "$base$ruta"; Method = $metodo; ErrorAction = 'Stop' }
        if ($null -ne $cuerpo) {
            $opciones.ContentType = 'application/json'
            $opciones.Body = ($cuerpo | ConvertTo-Json -Depth 6)
        }
        $respuesta = Invoke-WebRequest @opciones
        $codigo = [int]$respuesta.StatusCode
        if ($codigo -ne $esperado) { throw "Se esperaba $esperado y llego $codigo" }
        Write-Host "OK $codigo - $nombre" -ForegroundColor Green
        if ($respuesta.Content) { return ($respuesta.Content | ConvertFrom-Json) }
    } catch {
        $codigo = 0
        if ($_.Exception.Response) { $codigo = [int]$_.Exception.Response.StatusCode }
        if ($codigo -eq $esperado) { Write-Host "OK $codigo - $nombre" -ForegroundColor Green; return }
        Write-Host "FALLO - $nombre - $($_.Exception.Message)" -ForegroundColor Red
        throw
    }
}
$fecha = (Get-Date).AddDays(14).Date.AddHours(18).ToString('yyyy-MM-ddTHH:mm:ss')
$actualizacion = (Get-Date).AddDays(15).Date.AddHours(20).ToString('yyyy-MM-ddTHH:mm:ss')
$dni = (Get-Random -Minimum 20000000 -Maximum 79999999).ToString()
Probar 'Inicio' 'GET' '/' $null 200 | Out-Null
Probar 'Listar mesas' 'GET' '/api/mesas' $null 200 | Out-Null
Probar 'Listar clientes' 'GET' '/api/clientes' $null 200 | Out-Null
Probar 'Listar reservas' 'GET' '/api/reservas' $null 200 | Out-Null
Probar 'DNI invalido' 'POST' '/api/clientes' @{dni='abc';nombreCompleto='Cliente Prueba';telefono='987654321'} 400 | Out-Null
$cliente = Probar 'Crear cliente' 'POST' '/api/clientes' @{dni=$dni;nombreCompleto='Cliente de Prueba';telefono='987654321'} 201
Probar 'Consultar cliente' 'GET' "/api/clientes/$dni" $null 200 | Out-Null
Probar 'DNI duplicado' 'POST' '/api/clientes' @{dni=$dni;nombreCompleto='Cliente Prueba';telefono='987654321'} 409 | Out-Null
$reserva = Probar 'Crear reserva' 'POST' '/api/reservas' @{clienteDni=$dni;numeroMesa=1;fechaHora=$fecha;cantidadPersonas=2} 201
Probar 'Cruce de horarios' 'POST' '/api/reservas' @{clienteDni=$dni;numeroMesa=1;fechaHora=$fecha;cantidadPersonas=2} 409 | Out-Null
Probar 'Consultar reserva' 'GET' "/api/reservas/$($reserva.id)" $null 200 | Out-Null
Probar 'Actualizar reserva' 'PUT' "/api/reservas/$($reserva.id)" @{fechaHora=$actualizacion;cantidadPersonas=2} 200 | Out-Null
Probar 'Cancelar reserva' 'DELETE' "/api/reservas/$($reserva.id)" $null 204 | Out-Null
Probar 'Reserva inexistente' 'GET' '/api/reservas/9999999' $null 404 | Out-Null
Write-Host 'Pruebas completadas. Para evidencias oficiales, repetirlas en Postman.' -ForegroundColor Cyan
