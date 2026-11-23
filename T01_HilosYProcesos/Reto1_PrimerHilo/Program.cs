// Reto 1. Mi primer hilo
// Un hilo secundario muestra los números del 1 al 5 con medio segundo de pausa
// mientras el hilo principal escribe sus propios mensajes.
// Se prueban tres escenarios: sin Join, Join justo después de Start y Join al final.

Console.WriteLine($"[Principal] Id del hilo principal: {Environment.CurrentManagedThreadId}");
Console.WriteLine();

Escenario("1) Sin Join", modo: 0);
Escenario("2) Join justo después de Start", modo: 1);
Escenario("3) Join al final del programa", modo: 2);

static void Contar()
{
    for (int i = 1; i <= 5; i++)
    {
        Console.WriteLine($"   [Secundario | hilo {Environment.CurrentManagedThreadId}] número {i}");
        Thread.Sleep(500);
    }
    Console.WriteLine($"   [Secundario | hilo {Environment.CurrentManagedThreadId}] he terminado");
}

static void Escenario(string titulo, int modo)
{
    Console.WriteLine($"===== {titulo} =====");
    var hilo = new Thread(Contar);
    hilo.Start();

    if (modo == 1)
        hilo.Join(); // el principal se queda esperando aquí hasta que el secundario acaba

    for (int i = 1; i <= 3; i++)
    {
        Console.WriteLine($"[Principal | hilo {Environment.CurrentManagedThreadId}] mensaje propio {i}");
        Thread.Sleep(300);
    }
    Console.WriteLine($"[Principal | hilo {Environment.CurrentManagedThreadId}] fin de mi trabajo");

    if (modo == 2)
        hilo.Join(); // el principal espera al secundario solo al final

    if (modo == 0)
        hilo.Join(); // solo para que la salida no se mezcle con el siguiente escenario
                     // (sin esta línea el programa también esperaría: los Thread son de primer plano)
    Console.WriteLine();
}
