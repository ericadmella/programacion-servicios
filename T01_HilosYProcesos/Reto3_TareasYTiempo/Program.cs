// Reto 3. Tareas y tiempo
// Simula tres descargas de 1, 2 y 3 segundos con Task.Delay.
// Se mide con Stopwatch: secuencial, en paralelo con Task.WhenAll
// y, como comparación, lo que pasa si se usa Thread.Sleep en lugar de Task.Delay.

using System.Diagnostics;

// --- 1) Una detrás de otra ---
var sw = Stopwatch.StartNew();
await Descargar("archivo1", 1);
await Descargar("archivo2", 2);
await Descargar("archivo3", 3);
sw.Stop();
Console.WriteLine($"==> Secuencial: {sw.Elapsed.TotalSeconds:F2} s\n");

// --- 2) A la vez con Task.WhenAll ---
sw.Restart();
await Task.WhenAll(
    Descargar("archivo1", 1),
    Descargar("archivo2", 2),
    Descargar("archivo3", 3));
sw.Stop();
Console.WriteLine($"==> En paralelo (WhenAll): {sw.Elapsed.TotalSeconds:F2} s\n");

// --- 3) Mismo WhenAll, pero con Thread.Sleep dentro del método async ---
sw.Restart();
await Task.WhenAll(
    DescargarConSleep("archivo1", 1),
    DescargarConSleep("archivo2", 2),
    DescargarConSleep("archivo3", 3));
sw.Stop();
Console.WriteLine($"==> WhenAll con Thread.Sleep: {sw.Elapsed.TotalSeconds:F2} s\n");

static async Task Descargar(string nombre, int segundos)
{
    Console.WriteLine($"Inicio {nombre} ({segundos} s) en hilo {Environment.CurrentManagedThreadId}");
    await Task.Delay(segundos * 1000); // espera sin bloquear el hilo
    Console.WriteLine($"Fin    {nombre} en hilo {Environment.CurrentManagedThreadId}");
}

#pragma warning disable CS1998 // método async sin await: es justamente lo que queremos demostrar
static async Task DescargarConSleep(string nombre, int segundos)
{
    Console.WriteLine($"Inicio {nombre} ({segundos} s) en hilo {Environment.CurrentManagedThreadId}");
    Thread.Sleep(segundos * 1000); // BLOQUEA el hilo: el método se ejecuta de forma síncrona
    Console.WriteLine($"Fin    {nombre} en hilo {Environment.CurrentManagedThreadId}");
}
#pragma warning restore CS1998
