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
        public string operando_actual = "";
        public float operando_anterior = 0;
        public string operador = null;

        public MainPage()
        {
            SizeChanged += (object sender, EventArgs args) =>
            {
                if (this.Height > 0)
                    OperandoLabel.FontSize = this.Height / 7;
            };

            InitializeComponent();
        }

        void Reset(System.Object sender, System.EventArgs e)
        {
            OperandoLabel.Text = "0";
            operando_actual = "";
            operando_anterior = 0;
        }

        void Concatenar(System.Object sender, System.EventArgs e)
        {
            string valor_actual = ((Button)sender).Text;
            if (operando_actual == "0" || valor_actual == "-") operando_actual = "";
            OperandoLabel.Text = operando_actual;
            if (valor_actual == "0" && operando_actual == "0") return;
            operando_actual += valor_actual;
            OperandoLabel.Text = operando_actual;
        }

        void AgregarPunto(System.Object sender, System.EventArgs e)
        {
            

            if(operando_actual.Length > 0)
            {
                Match m = Regex.Match(operando_actual, @"[.]+");

                if (!m.Success)
                {
                    operando_actual += ".";
                }
            } else
            {
                operando_actual = "0.";
            }

            OperandoLabel.Text = operando_actual;
        }

        void InvertirPrefijo(System.Object sender, System.EventArgs e)
        {
            if(!Double.IsNaN(Convert.ToDouble(operando_actual)))
            {
                float valorInvertido = float.Parse(operando_actual) * -1;
                operando_actual = valorInvertido.ToString();
                OperandoLabel.Text = operando_actual;
            }
            
        }

        void Retroceder(System.Object sender, System.EventArgs e)
        {
            operando_actual = (operando_actual.Length <= 1) ? "0" : operando_actual.Substring(0, operando_actual.Length - 1);
            OperandoLabel.Text = operando_actual;
        }

        void ObtenerResultado(System.Object sender, System.EventArgs e)
        {
            if(operador != null)
            {
                float resultado = Calcular(operando_anterior, operando_actual.Length > 0 ? float.Parse(operando_actual) : 0, operador);

                if(Double.IsInfinity((double)resultado))
                {
                    resultado = 0;
                }
                operando_actual = resultado.ToString();
                OperandoLabel.Text = operando_actual;
            }
            else
            {
                OperandoLabel.Text = operando_actual.Length > 0 ? operando_actual : "0";
            }
            
            operador = null;
        }

        void Operar(System.Object sender, System.EventArgs e)
        {
            if (operador == null)
            {
                
                operando_anterior = operando_actual.Length > 0 ? float.Parse(operando_actual) : 0;
            }
            else
            {
                operando_anterior = Calcular(operando_anterior, operando_actual.Length > 0 ? float.Parse(operando_actual) : 0, operador);
            }

            operando_actual = "";
            operador = ((Button)sender).Text;
            OperandoLabel.Text = operando_anterior.ToString();
        }

        float Calcular(float a, float b, string operador)
        {
            float resultado = 0;

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
