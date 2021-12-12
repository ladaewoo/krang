using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace krang
{
    public partial class MainPage : ContentPage
    {
        string operador = null;
        string operando1 = null;
        string ultimo_estado = null;

        public MainPage()
        {
            SizeChanged += (object sender, EventArgs args) =>
            {
                if (this.Height > 0)
                    OperandoLabel.FontSize = this.Height / 9;
            };

            InitializeComponent();
        }

        void Reset(System.Object sender, System.EventArgs e)
        {
            OperandoLabel.Text = "0";
        }

        void Concatenar(System.Object sender, System.EventArgs e)
        {
            if (operador == null)
            {
                OperandoLabel.Text += ((Button)sender).Text;
                OperandoLabel.Text = (Convert.ToDecimal(OperandoLabel.Text)).ToString();
            }
            else
            {
                operando1 = OperandoLabel.Text;
                OperandoLabel.Text = ((Button)sender).Text;
            }

            ultimo_estado = "C";
        }

        void AgregarPunto(System.Object sender, System.EventArgs e)
        {
            Match m = Regex.Match(OperandoLabel.Text, @"[.]+");

            if (!m.Success)
            {
                OperandoLabel.Text += ".";
            }
        }

        void InvertirPrefijo(System.Object sender, System.EventArgs e)
        {
            OperandoLabel.Text = (Convert.ToDecimal(OperandoLabel.Text) * -1).ToString();
        }

        void Retroceder(System.Object sender, System.EventArgs e)
        {
            string label = OperandoLabel.Text;
            OperandoLabel.Text = label.Length <= 1 ? "0" : label.Substring(0, label.Length - 1); 
        }

        void Operacion()
        {
            decimal operando2 = Convert.ToDecimal(OperandoLabel.Text);
            decimal resultado = Calcular(Convert.ToDecimal(operando1), operador, operando2);
            OperandoLabel.Text = resultado.ToString();
            operando1 = null;
            // operador = null;
        }

        void ObtenerResultado(System.Object sender, System.EventArgs e)
        {
            if(operador != null) Operacion();
        }

        void Operar(System.Object sender, System.EventArgs e)
        {
            operador = ((Button)sender).Text;

            if (ultimo_estado != "O")
            {
                ultimo_estado = "O";

                if (OperandoLabel.Text != "0")
                {
                    if (operando1 != null)
                    {
                        Operacion();
                    }
                    else
                    {
                        operando1 = OperandoLabel.Text;
                    }
                }
            }
        }

        decimal Calcular(decimal a, string operador, decimal b)
        {
            decimal resultado = 0;

            switch (operador)
            {
                case "+":
                    resultado = a + b;
                    break;
                case "-":
                    resultado = a - b;
                    break;
                case "*":
                    resultado = a * b;
                    break;
                case "÷":
                    resultado = a / b;
                    break;
            }
            
            return resultado;
        }
    }
}
