// Reto 2. Condición de carrera
// Dos hilos incrementan la misma variable N veces cada uno.
// Se compara: sin protección, con lock y con Interlocked.Increment.
// Uso: dotnet run -c Release -- [iteraciones]   (por defecto 100000)

using System.Diagnostics;

int iteraciones = args.Length > 0 && int.TryParse(args[0], out var n) ? n : 100_000;
const int ejecuciones = 5;

Console.WriteLine($"Iteraciones por hilo: {iteraciones:N0}  ->  resultado esperado: {2L * iteraciones:N0}");
Console.WriteLine();

Probar("Sin protección", iteraciones, ejecuciones, Contador.SinProteccion);
Probar("Con lock", iteraciones, ejecuciones, Contador.ConLock);
Probar("Con Interlocked.Increment", iteraciones, ejecuciones, Contador.ConInterlocked);

static void Probar(string titulo, int iteraciones, int ejecuciones, Action<int> incrementar)
{
    Console.WriteLine($"===== {titulo} =====");
    for (int e = 1; e <= ejecuciones; e++)
    {
        Contador.Valor = 0;
        var sw = Stopwatch.StartNew();

        var h1 = new Thread(() => incrementar(iteraciones));
        var h2 = new Thread(() => incrementar(iteraciones));
        h1.Start(); h2.Start();
        h1.Join(); h2.Join();

        sw.Stop();
        string estado = Contador.Valor == 2L * iteraciones ? "OK" : "INCORRECTO";
        Console.WriteLine($"Ejecución {e}: {Contador.Valor,12:N0}  ({estado}, {sw.ElapsedMilliseconds} ms)");
    }
    Console.WriteLine();
}

static class Contador
{
    public static int Valor;                         // variable compartida
    private static readonly object _cerrojo = new(); // objeto para el lock

    // contador++ son 3 pasos (leer, sumar, escribir): dos hilos pueden pisarse
    public static void SinProteccion(int veces)
    {
        for (int i = 0; i < veces; i++)
            Valor++;
    }

    // Solo un hilo a la vez entra en la sección crítica
    public static void ConLock(int veces)
    {
        for (int i = 0; i < veces; i++)
        {
            lock (_cerrojo)
            {
                Valor++;
            }
        }
    }

    // Incremento atómico a nivel de procesador, sin bloquear
    public static void ConInterlocked(int veces)
    {
        for (int i = 0; i < veces; i++)
            Interlocked.Increment(ref Valor);
    }
}
