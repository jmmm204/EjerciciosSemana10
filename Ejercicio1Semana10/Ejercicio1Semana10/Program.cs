using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1Semana10
{
    // Definición de la estructura Producto
    [Serializable]
    public struct Producto
    {
        public int ID;
        public string Nombre;
        public int Cantidad;
        public decimal Precio;

        public Producto(int id, string nombre, int cantidad, decimal precio)
        {
            ID = id;
            Nombre = nombre;
            Cantidad = cantidad;
            Precio = precio;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "productos.dat";
            List<Producto> productos = new List<Producto>
        {
            new Producto(1, "Cereal", 50, 0.30m),
            new Producto(2, "Jabon", 30, 0.20m),
            new Producto(3, "Pasta dental", 20, 0.25m),
            new Producto(4, "Bolsa de basura", 35, 0.15m),
            new Producto(5, "Aceite de oliva", 30, 0.30m)
        };

            // Guardar productos en un archivo binario
            GuardarProductos(filePath, productos);

            // Buscar un producto por ID
            Console.WriteLine("Ingrese el ID del producto a buscar:");
            int idBuscado = int.Parse(Console.ReadLine());
            Producto? productoEncontrado = BuscarProductoPorID(filePath, idBuscado);

            if (productoEncontrado.HasValue)
            {
                Console.WriteLine($"Producto encontrado: ID: {productoEncontrado.Value.ID}, Nombre: {productoEncontrado.Value.Nombre}, Cantidad: {productoEncontrado.Value.Cantidad}, Precio: {productoEncontrado.Value.Precio}");
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        // Método para guardar productos en un archivo binario
        static void GuardarProductos(string filePath, List<Producto> productos)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, productos.OrderBy(p => p.ID).ToList());
            }
        }

        // Método para buscar un producto por su ID utilizando búsqueda binaria
        static Producto? BuscarProductoPorID(string filePath, int id)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("El archivo no existe.");
                return null;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                List<Producto> productos = (List<Producto>)formatter.Deserialize(fs);

                // Implementación de búsqueda binaria
                int izquierda = 0;
                int derecha = productos.Count - 1;

                while (izquierda <= derecha)
                {
                    int medio = (izquierda + derecha) / 2;
                    if (productos[medio].ID == id)
                    {
                        return productos[medio];
                    }
                    else if (productos[medio].ID < id)
                    {
                        izquierda = medio + 1;
                    }
                    else
                    {
                        derecha = medio - 1;
                    }
                }
            }

            return null; // No encontrado
        }
    }
}