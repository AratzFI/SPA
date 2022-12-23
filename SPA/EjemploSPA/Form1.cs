using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using FuncionesAPI;
namespace EjemploSPA
{
    public partial class Form1 : Form
    {
        Semaphore semaforo;
        Mutex exc;
        int MSG_ENTRA, MSG_COGE_TOALLA, MSG_DEJA_TOALLA, MSG_DUCHA_IN, MSG_DUCHA_OUT,MSG_ENTRA_LIMPIEZA,MSG_SALE_LIMPIEZA;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            semaforo = new Semaphore(3, 3, "Toalla");
            exc  = new Mutex(false, "ducha");
            MSG_ENTRA_LIMPIEZA = Funciones.RegisterWindowMessage("MSG_ENTRA_LIMPIEZA");
            MSG_SALE_LIMPIEZA = Funciones.RegisterWindowMessage("MSG_SALE_LIMPIEZA");
            MSG_ENTRA =Funciones.RegisterWindowMessage("MSG_ENTRA");
            MSG_DUCHA_IN = Funciones.RegisterWindowMessage("MSG_DUCHA_IN");
            MSG_COGE_TOALLA = Funciones.RegisterWindowMessage("MSG_COGE_TOALLA");
            MSG_DUCHA_OUT= Funciones.RegisterWindowMessage("DUCHA_OUT");
            MSG_DEJA_TOALLA = Funciones.RegisterWindowMessage("MSG_DEJA_TOALLA");
        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == MSG_ENTRA)
            {
                txtUsuarios.Text=(int.Parse(txtUsuarios.Text)+1).ToString();    
            }
            else if (m.Msg == MSG_DUCHA_IN)
            { 
                checkBox1.Checked= true;
            }
            else if (m.Msg == MSG_COGE_TOALLA)
            {
                listBox1.Items.RemoveAt(0);
            }
            else if (m.Msg == MSG_DUCHA_OUT)
            {
                checkBox1.Checked = false;
            }
            else if (m.Msg == MSG_DEJA_TOALLA)
            {
                listBox1.Items.Add("Toalla");
                txtUsuarios.Text = (int.Parse(txtUsuarios.Text) -1).ToString();
            }
            else if(m.Msg==MSG_ENTRA_LIMPIEZA)
            {
                pictureBox1.Image = Image.FromFile("imagenes\\rojo.png");
            }
            else if(m.Msg== MSG_SALE_LIMPIEZA)
            {
                pictureBox1.Image = Image.FromFile("imagenes\\verde.png");
            }
            else{
                base.WndProc(ref m);
            }
        }
    }
}
