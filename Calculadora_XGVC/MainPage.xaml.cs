using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculadora_XGVC
{
    public partial class MainPage : ContentPage
    {
        
        private string primerNumero = "";  
        private string segundoNumero = ""; 
        private string operador = "";      
        private bool numeroNuevo = true;   
        public MainPage()
        {
            InitializeComponent();
        }
        private void Numero(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string texto = button.Text;

           
            if (double.TryParse(texto, out _) || texto == ".")
            {
                // Si es un nuevo número, limpiar el texto
                if (numeroNuevo)
                {
                    inter.Text = string.Empty;
                    numeroNuevo = false;
                }

                
                if (texto == ".")
                {
                    if (inter.Text.Contains(".")) return;  
                    if (string.IsNullOrEmpty(inter.Text)) inter.Text = "0";  
                }

               
                inter.Text += texto;
            }
            else
            {
                
                switch (texto)
                {
                    case "C":
                        LimpiarTodo();
                        break;

                    case "←":
                        if (inter.Text.Length > 0)
                        {
                            inter.Text = inter.Text.Remove(inter.Text.Length - 1);
                        }
                        break;

                    case "=":
                        if (!string.IsNullOrEmpty(operador))
                        {
                            segundoNumero = inter.Text;
                            double resultado = RealizarOperacion(primerNumero, segundoNumero, operador);
                            Resultado.Text = resultado.ToString();
                            inter.Text = resultado.ToString();
                            numeroNuevo = true;
                        }
                        break;

                    default:
                        if (texto == "+" || texto == "-" || texto == "x" || texto == "÷")
                        {
                            if (!string.IsNullOrEmpty(operador))
                            {
                                // Si ya hay un operador, realizar la operación antes de cambiar el operador
                                segundoNumero = inter.Text;
                                double resultado = RealizarOperacion(primerNumero, segundoNumero, operador);
                                inter.Text = resultado.ToString();
                                primerNumero = resultado.ToString();  // Guardar el resultado como primer número para la próxima operación
                            }
                            else
                            {
                                primerNumero = inter.Text;  // Guardar el primer número si no hay operador
                            }
                            operador = texto;  // Guardar el operador
                            numeroNuevo = true;  // Indicar que se empieza un nuevo número
                        }
                        break;
                }
            }
        }

        // Método para realizar la operación
        private double RealizarOperacion(string num1, string num2, string oper)
        {
            double numero1 = Convert.ToDouble(num1);
            double numero2 = Convert.ToDouble(num2);

            switch (oper)
            {
                case "+":
                    return numero1 + numero2;
                case "-":
                    return numero1 - numero2;
                case "x":
                    return numero1 * numero2;
                case "÷":
                    if (numero2 == 0)
                    {
                        DisplayAlert("Error", "No se puede dividir entre cero", "Ta bieen:)");
                        return 0;  // Evitar la división por cero
                    }
                    return numero1 / numero2;
                default:
                    return 0;
            }
        }

        // Método para limpiar todo
        private void LimpiarTodo()
        {
            inter.Text = string.Empty;
            Resultado.Text = string.Empty;
            primerNumero = "";
            segundoNumero = "";
            operador = string.Empty;
            numeroNuevo = true;
        }
        //private void Numero(object sender, EventArgs e)
        //{
        //    Button button = sender as Button;
        //    string texto = button.Text;


        //    if (double.TryParse(texto, out _) || texto == ".")
        //    {
        //        if (numeroNuevo)
        //        {
        //            inter.Text = string.Empty;
        //            numeroNuevo = false;
        //        }
        //        if (texto == "." && inter.Text.Contains("."))
        //        {

        //            return;
        //        }
        //        inter.Text += texto;
        //    }
        //    else
        //    {
        //        switch (texto)
        //        {
        //            case "C":
        //                inter.Text = string.Empty;
        //                Resultado.Text = string.Empty;
        //                primerNumero = 0;
        //                segundoNumero = 0;
        //                operador = string.Empty;
        //                break;

        //            case "←":
        //                if (inter.Text.Length > 0)
        //                {
        //                    inter.Text = inter.Text.Remove(inter.Text.Length - 1);
        //                }
        //                break;

        //            case "=":
        //                if (!string.IsNullOrEmpty(operador))
        //                {
        //                    segundoNumero = Convert.ToDouble(inter.Text);
        //                    double resultado = RealizarOperacion(primerNumero, segundoNumero, operador);
        //                    Resultado.Text = resultado.ToString();
        //                    inter.Text = resultado.ToString();
        //                    numeroNuevo = true;

        //                }
        //                break;

        //                default:
        //                if (texto == "+" || texto == "-" || texto == "x" || texto == "÷")
        //                {
        //                    if (!string.IsNullOrEmpty(operador))
        //                    {
        //                        segundoNumero = Convert.ToDouble(inter.Text);
        //                        primerNumero = RealizarOperacion(primerNumero, segundoNumero, operador);
        //                        inter.Text = primerNumero.ToString();
        //                    }
        //                    else
        //                    {
        //                        primerNumero = Convert.ToDouble(inter.Text);
        //                    }
        //                    operador = texto;
        //                    numeroNuevo = true;
        //                }
        //                break;
        //        }
        //    }
        //}
        //private double RealizarOperacion(double num1, double num2, string oper)
        //{
        //    switch (oper)
        //    {
        //        case "+":
        //            return num1 + num2;
        //        case "-":
        //            return num1 - num2;
        //        case "x":
        //            return num1 * num2;
        //        case "÷":
        //            if (num2 == 0)
        //            {
        //                DisplayAlert("Error", "No se puede dividir entre cero", "Okeey");
        //            }
        //        return num1 / num2;
        //        default:
        //            return 0;
        //    }
        //}

    }

}
