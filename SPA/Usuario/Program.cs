using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FuncionesAPI;
using System.Diagnostics;
namespace Usuario
{
    internal class Program
    {
        static int MSG_ENTRA, MSG_COGE_TOALLA, MSG_DEJA_TOALLA, MSG_DUCHA_IN, MSG_DUCHA_OUT;
        static void Main(string[] args)
        {
            /*comprobar que no están limpiando*/
            bool creado;
            Mutex m;
            do
            {
                m = new Mutex(false, "MutexLimpieza", out creado);
                m.Dispose();
            } while (!creado);
            /*fin comprobar*/
            m = new Mutex(false, "MutexUser");
            Mutex exc = Mutex.OpenExisting("ducha");
            Semaphore semaforo = Semaphore.OpenExisting("Toalla");
            MSG_ENTRA = Funciones.RegisterWindowMessage("MSG_ENTRA");
            MSG_DUCHA_IN = Funciones.RegisterWindowMessage("MSG_DUCHA_IN");
            MSG_COGE_TOALLA = Funciones.RegisterWindowMessage("MSG_COGE_TOALLA");
            MSG_DUCHA_OUT = Funciones.RegisterWindowMessage("DUCHA_OUT");
            MSG_DEJA_TOALLA = Funciones.RegisterWindowMessage("MSG_DEJA_TOALLA");
            IntPtr handle = Process.GetProcessesByName("SPA")[0].MainWindowHandle;
            Funciones.PostMessage(handle, MSG_ENTRA, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("ESPERANDO TOALLA...");
            semaforo.WaitOne();
            Funciones.PostMessage(handle, MSG_COGE_TOALLA, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("ESPERANDO DUCHA, CON LA TOALLA...");
            exc.WaitOne();
            Funciones.PostMessage(handle, MSG_DUCHA_IN, IntPtr.Zero, IntPtr.Zero);
            /*CANTAR*/
            Random r = new Random(DateTime.Now.Millisecond);
            int tiempo = r.Next(15, 50);
            DateTime inicio = DateTime.Now;
            while((DateTime.Now-inicio).TotalSeconds<=tiempo)
            {
                Console.Write("lorololo");
            }
            /*F.CANTAR*/
            Funciones.PostMessage(handle, MSG_DUCHA_OUT, IntPtr.Zero, IntPtr.Zero);
            exc.ReleaseMutex();
            Console.WriteLine("SALGO DE LA DUCHA...");
            Funciones.PostMessage(handle, MSG_DEJA_TOALLA, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("DEJO LA TOALLA...");
            semaforo.Release();
            Console.WriteLine("HASTA PRONTO!");
            Console.ReadLine();
            m.Dispose();
        }
    }
}
