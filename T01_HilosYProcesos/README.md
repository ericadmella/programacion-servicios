# T01 · Introducción al desarrollo con hilos y procesos

Ejercicios en C# (.NET 8, aplicaciones de consola) de la práctica T01.

| Carpeta | Reto | Qué demuestra |
|---|---|---|
| `Reto1_PrimerHilo` | Mi primer hilo | `Thread.Start`, `Thread.Join`, `Thread.Sleep` e identificador de hilo |
| `Reto2_CondicionCarrera` | Condición de carrera | Contador compartido sin protección, con `lock` y con `Interlocked.Increment` |
| `Reto3_TareasYTiempo` | Tareas y tiempo | `Task.Delay` secuencial vs `Task.WhenAll`, medido con `Stopwatch` (y el efecto de `Thread.Sleep`) |
| `Reto4_LanzarProceso` | Lanzar un proceso | `Process` + `ProcessStartInfo`, captura de la salida, `WaitForExit` y `ExitCode` |

## Cómo ejecutarlos

Requisitos: [.NET 8 SDK](https://dotnet.microsoft.com/download).

```bash
dotnet run --project Reto1_PrimerHilo
dotnet run --project Reto2_CondicionCarrera -c Release            # 100.000 iteraciones por hilo
dotnet run --project Reto2_CondicionCarrera -c Release -- 1000000 # 1.000.000 iteraciones por hilo
dotnet run --project Reto3_TareasYTiempo
dotnet run --project Reto4_LanzarProceso
```

También se puede abrir `HilosYProcesos.sln` en Visual Studio o Rider y ejecutar cada proyecto.

Las respuestas teóricas y las conclusiones de cada reto están en el documento Word de la entrega.
