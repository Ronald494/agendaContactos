
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace agendaContactos
{
    class Contacto
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
    class Program
    {
        static List<Contacto> contactos = new List<Contacto>();
        static string archivo = "contactos.txt";
        static void Main(string[] args)
        {
            CargarContactos();

            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===Agenda de Contactos===");
                Console.WriteLine("1. Agregar contactos");
                Console.WriteLine("2. Listar contactos");
                Console.WriteLine("3. Buscar contacto");
                Console.WriteLine("4. Editar contacto");
                Console.WriteLine("5. Eliminar contacto");
                Console.WriteLine("6. Salir");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opcion invalida. presiona una tecla...");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1: AgregarContacto(); break;
                    case 2: ListarContactos(); break;
                    case 3: BuscarContacto(); break;
                    case 4: EditarContacto(); break;
                    case 5: EliminarContacto(); break;
                    case 6: GuardarContactos(); break;
                    default:
                        Console.WriteLine("Opcion no valida");
                        break;

                }

            } while (opcion != 6);
        }
        static void AgregarContacto()
        {
            Console.Clear();
            Contacto nuevo = new Contacto();

            Console.Write("Nombre: ");
            nuevo.Nombre = Console.ReadLine();

            Console.Write("Telefono: ");
            nuevo.Telefono = Console.ReadLine();

            Console.Write("Email: ");
            nuevo.Email = Console.ReadLine();

            contactos.Add(nuevo);
            Console.WriteLine("Contacto agregado. Presiona una tecla...");
            Console.ReadKey();

        }
        static void ListarContactos()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE CONTACTOS ===");
            foreach (var c in contactos)
            {
                Console.WriteLine($"Nombre: {c.Nombre}, Tel: {c.Telefono}, Email: {c.Email}");
            }
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }
        static void BuscarContacto()
        {
            Console.Clear();
            Console.Write("Ingresa nombre a buscar: ");
            string nombre = Console.ReadLine().ToLower();

            var resultado = contactos.FindAll(c => c.Nombre.ToLower().Contains(nombre));

            if (resultado.Count > 0)
            {
                Console.WriteLine("Contactos encontrados:");
                foreach (var c in resultado)
                    Console.WriteLine($"Nombre: {c.Nombre}, Tel: {c.Telefono}, Email: {c.Email}");
            }
            else
            {
              Console.WriteLine("No se encontraron contactos");
            }
                Console.WriteLine("Presiona una tecla para continuar");
            Console.ReadKey();

        }
        static void EditarContacto()
        {
            Console.Clear();
            Console.Write("Ingresa el nombre del contacto a editar: ");
            string nombre = Console.ReadLine().ToLower();

            var contacto = contactos.Find(c => c.Nombre.ToLower() == nombre);

            if (contacto != null)
            {
                Console.Write("Nuevo telefono: ");
                contacto.Telefono = Console.ReadLine();

                Console.Write("Nuevo email: ");
                contacto.Email = Console.ReadLine();

                Console.WriteLine("Contacto actualizado");
            }
            else
            {
                Console.WriteLine("No encontrado");
            }
            Console.WriteLine("Presiona una tecla para continuar");
            Console.ReadKey();

        }
        static void EliminarContacto()
        {
            Console.Clear();
            Console.Write("Ingresa el nombre del contacto a eliminar: ");
            string nombre = Console.ReadLine().ToLower();

            var contacto = contactos.Find(c => c.Nombre.ToLower() == nombre);

            if (contacto != null)
            {
                contactos.Remove(contacto);
                Console.WriteLine("Contacto eliminado");
            }
            else
            {
                Console.WriteLine("No encontrado");
            }
            Console.WriteLine("Presiona una tecla para continuar");
            Console.ReadKey();
        }
        static void GuardarContactos()
        {
           using (StreamWriter sw = new StreamWriter(archivo))
            {
                foreach (var c in contactos)
                {
                    sw.WriteLine($"{c.Nombre}|{c.Telefono}|{c.Email}");
                }
            }
        }
        static void CargarContactos()
        {
            if (File.Exists(archivo))
            {
                using (StreamReader sr = new StreamReader(archivo))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        var partes = linea.Split('|');
                        if (partes.Length == 3)
                        {
                            contactos.Add(new Contacto
                            {
                                Nombre = partes[0],
                                Telefono = partes[1],
                                Email = partes[2]
                            });
                        }
                    }
                }
            }
        }

    }
}