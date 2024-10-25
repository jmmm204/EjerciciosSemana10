using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2Semana10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese un número entre 1 y 10:");
            int numero = int.Parse(Console.ReadLine());

            if (numero < 1 || numero > 10)
            {
                Console.WriteLine("Número fuera del rango permitido.");
                return;
            }

            string filePath = $"tabla-{numero}.dat";
            GuardarTablaMultiplicar(filePath, numero);
            MostrarTablaMultiplicar(filePath);
        }

        // Método para guardar la tabla de multiplicar en un archivo binario
        static void GuardarTablaMultiplicar(string filePath, int numero)
        {
            int[] tabla = new int[10];

            for (int i = 1; i <= 10; i++)
            {
                tabla[i - 1] = numero * i;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, tabla);
            }
        }

        // Método para mostrar la tabla de multiplicar
        static void MostrarTablaMultiplicar(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("El fichero no existe.");
                return;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                int[] tabla = (int[])formatter.Deserialize(fs);

                Console.WriteLine($"Tabla de multiplicar del número {Path.GetFileNameWithoutExtension(filePath).Split('-')[1]}:");
                for (int i = 0; i < tabla.Length; i++)
                {
                    Console.WriteLine($"{i + 1} * {tabla.Length} = {tabla[i]}");
                }
            }
        }
    }
}
