// Reto 4. Lanzar un proceso
// Ejecuta un programa externo con la clase Process, captura su salida
// y la muestra en nuestra consola junto con el ExitCode.

using System.ComponentModel;
using System.Diagnostics;

Lanzar("dotnet", "--version");              // todo bien -> ExitCode 0
Lanzar("dotnet", "comando-que-no-existe");  // el programa existe pero falla -> ExitCode distinto de 0
Lanzar("programa_que_no_existe", "");       // el ejecutable no existe -> excepción, no hay ExitCode

static void Lanzar(string programa, string argumentos)
{
    Console.WriteLine($"===== {programa} {argumentos} =====");

    var info = new ProcessStartInfo
    {
        FileName = programa,
        Arguments = argumentos,
        RedirectStandardOutput = true, // capturamos lo que escribe en pantalla
        RedirectStandardError = true,
        UseShellExecute = false,       // necesario para redirigir la salida
        CreateNoWindow = true
    };

    try
    {
        using var proceso = Process.Start(info)!;

        // Leemos toda la salida y esperamos a que el proceso termine
        string salida = proceso.StandardOutput.ReadToEnd();
        string errores = proceso.StandardError.ReadToEnd();
        proceso.WaitForExit();

        Console.WriteLine("Salida capturada:");
        Console.WriteLine(string.IsNullOrWhiteSpace(salida) ? "  (vacía)" : "  " + salida.Trim().Replace("\n", "\n  "));
        if (!string.IsNullOrWhiteSpace(errores))
        {
            var primeraLinea = errores.Trim().Split('\n')[0];
            Console.WriteLine($"Error capturado (1ª línea): {primeraLinea.Trim()}");
        }
        Console.WriteLine($"ExitCode: {proceso.ExitCode}");
    }
    catch (Win32Exception ex)
    {
        // Si el ejecutable no existe, Process.Start lanza Win32Exception
        Console.WriteLine($"No se pudo iniciar el proceso: {ex.Message}");
        Console.WriteLine($"(Win32Exception, NativeErrorCode = {ex.NativeErrorCode}; no llega a haber ExitCode)");
    }
    Console.WriteLine();
}
