using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FuncionesAPI;
using System.Diagnostics;

namespace Limpieza
{
    internal class Program
    {
        static int MSG_ENTRA_LIMPIEZA;
        static int MSG_SALE_LIMPIEZA;
        static void Main(string[] args)
        {
            bool creado;
            Mutex m;
            MSG_ENTRA_LIMPIEZA = Funciones.RegisterWindowMessage("MSG_ENTRA_LIMPIEZA");
            MSG_SALE_LIMPIEZA = Funciones.RegisterWindowMessage("MSG_SALE_LIMPIEZA");
            IntPtr handle = Process.GetProcessesByName("SPA")[0].MainWindowHandle;
            Funciones.PostMessage(handle, MSG_ENTRA_LIMPIEZA, IntPtr.Zero, IntPtr.Zero);
            do
            {
               
                m= new Mutex(false, "MutexUser", out creado);
                m.Dispose();
            } while (!creado);

            m = new Mutex(false, "MutexLimpieza");
            m.WaitOne();
            /*CANTAR*/
            Random r = new Random(DateTime.Now.Millisecond);
            int tiempo = r.Next(15, 50);
            DateTime inicio = DateTime.Now;
            while ((DateTime.Now - inicio).TotalSeconds <= tiempo)
            {
                Console.Write("laralala");
            }
            /*F.CANTAR*/
            m.ReleaseMutex();
            Funciones.PostMessage(handle, MSG_SALE_LIMPIEZA, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
