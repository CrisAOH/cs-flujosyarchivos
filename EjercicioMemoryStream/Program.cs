using System;
using System.IO;
using System.Text;

namespace EjercicioMemoryStream
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion;
            bool repetir = true;
            string mensajeCifrar, contraseniaMensaje, mensajeCifrado;
            MemoryStream memoryStream = new MemoryStream();

            Console.Write("Ingresa el mensaje que quieres cifrar: ");
            mensajeCifrar = Console.ReadLine();

            Console.Write("Ingresa una contraseña para proteger el mensaje: ");
            contraseniaMensaje = Console.ReadLine();

            mensajeCifrado = CifrarMensaje(mensajeCifrar);

            byte[] matrizCadenaByte = Encoding.UTF8.GetBytes(mensajeCifrado);

            memoryStream.Write(matrizCadenaByte, 0, matrizCadenaByte.Length);

            Console.WriteLine("El mensaje está protegido, presiona cualquier tecla para continuar...");
            Console.ReadKey();

            byte[] bufferBytesLeidos = new byte[100];
            
            memoryStream.Seek(0, SeekOrigin.Begin);

            memoryStream.Read(bufferBytesLeidos, 0, (int)memoryStream.Length);

            string cadenaDescodificadaCifrada = Encoding.UTF8.GetString(bufferBytesLeidos);

            do
            {
                Console.Clear();

                Console.WriteLine("1. Mostrar mensaje");
                Console.WriteLine("2. Descifrar mensaje");
                Console.WriteLine("3. Me rindo");

                Console.Write("Escoje una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine($"Mensaje: \"{cadenaDescodificadaCifrada}\"");

                        Console.WriteLine("Presiona cualquir tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Ingresa la contraseña para descifrar el mensaje: ");
                        string posibleContrasenia = Console.ReadLine();

                        if (posibleContrasenia == contraseniaMensaje)
                        {
                            string mensajeDescifrado = DescifrarMensaje(cadenaDescodificadaCifrada);

                            Console.WriteLine($"Mensaje: {mensajeDescifrado}");

                            Console.WriteLine("Presiona cualquir tecla para continuar...");
                            Console.ReadKey();

                            memoryStream.Close();
                            repetir = false
                        }
                        else
                        {
                            Console.WriteLine("Contraseña incorrecta. Intenta de nuevo.");

                            Console.WriteLine("Presiona cualquir tecla para continuar...");
                            Console.ReadKey();
                        }
                        break;
                    case 3:
                        repetir = false;
                        break;
                    default:
                        break;
                }
            }
            while (repetir);
        }

        static string CifrarMensaje(string mensajeCifrarParam)
        {
            string mensajeCifrado;

            mensajeCifrado = mensajeCifrarParam;
            mensajeCifrado = mensajeCifrado.Replace('a', '1');
            mensajeCifrado = mensajeCifrado.Replace('e', '2');
            mensajeCifrado = mensajeCifrado.Replace('i', '3');
            mensajeCifrado = mensajeCifrado.Replace('o', '4');
            mensajeCifrado = mensajeCifrado.Replace('u', '5');

            return mensajeCifrado;
        }

        static string DescifrarMensaje(string mensajeCifradoParam)
        {
            string mensajeDescifrado;

            mensajeDescifrado = mensajeCifradoParam;
            mensajeDescifrado = mensajeDescifrado.Replace('1', 'a');
            mensajeDescifrado = mensajeDescifrado.Replace('2', 'e');
            mensajeDescifrado = mensajeDescifrado.Replace('3', 'i');
            mensajeDescifrado = mensajeDescifrado.Replace('4', 'o');
            mensajeDescifrado = mensajeDescifrado.Replace('5', 'u');

            return mensajeDescifrado;
        }
    }
}
