namespace EjercicioArchivosDirectorios
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string directorio;

            do
            {
                Console.Write("Por favor, ingrese la ruta del directorio: ");
                directorio = Console.ReadLine();

                if (!Directory.Exists(directorio))
                {
                    Console.WriteLine("La ruta especificada no existe. Por favor, ingrese una ruta válida.");
                }
            }
            while (!Directory.Exists(directorio));

            ExplorarDirectorio(directorio);
        }

        static void ExplorarDirectorio(string directorioParam)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine($"Contenido de: {directorioParam}\n");

                string[] archivosSubdirectorios = Directory.GetFileSystemEntries(directorioParam);

                MostrarTabla(archivosSubdirectorios);

                Console.WriteLine("Ingresa el número de la opción que deseas explorar (o 'a' para ir hacia atras en la ruta, 'n' para ingresar una nueva ruta o 's' para salir): ");

                string opcion = Console.ReadLine();

                if (opcion.ToLower() == "s")
                {
                    continuar = false;
                }
                else if (opcion.ToLower() == "a")
                {
                    directorioParam = Path.GetDirectoryName(directorioParam);
                }
                else if (opcion.ToLower() == "n")
                {
                    Console.Clear();

                    Console.Write("Ingresa la nueva ruta: ");
                    string nuevaRuta = Console.ReadLine();

                    if (Directory.Exists(nuevaRuta))
                    {
                        directorioParam = nuevaRuta;
                    }
                    else
                    {
                        Console.WriteLine("Ingresa una ruta válida.");
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                else if (Convert.ToInt32(opcion) >= 0 && Convert.ToInt32(opcion) < archivosSubdirectorios.Length)
                {
                    int opcionEscogida = Convert.ToInt32(opcion);

                    if (Directory.Exists(archivosSubdirectorios[opcionEscogida]))
                    {
                        directorioParam = archivosSubdirectorios[opcionEscogida];
                    }
                    else
                    {
                        OperacionesArchivos(archivosSubdirectorios[opcionEscogida]);
                    }
                }
                else
                {
                    Console.WriteLine("Ingresa una opción válida.");
                }
            }
        }

        static void MostrarTabla(string[] archivosSubdirectoriosParam)
        {
            Console.WriteLine($"{"Índice", -8}{"Nombre", -50}{"Tipo", -13}");

            String guiones = new String('-', 71);

            Console.WriteLine(guiones);

            string nombre, tipo;

            for (int i = 0; i < archivosSubdirectoriosParam.Length; i++)
            {
                nombre = Path.GetFileName(archivosSubdirectoriosParam[i]);

                if (Directory.Exists(archivosSubdirectoriosParam[i]))
                {
                    tipo = "Subdirectorio";
                }
                else
                {
                    tipo = Path.GetExtension(archivosSubdirectoriosParam[i]);
                }

                Console.WriteLine($"{i,-8}{nombre,-50}{tipo,-13}");
            }
        }

        static void OperacionesArchivos(string rutaArchivoParam)
        {
            string rutaCopiarArchivo, rutaMoverArchivo, destinoArchivo, respuestaReemplazo, respuestaEliminar, respuestRenombrar, nuevoNombreArchivo, rutaArchivoRenombrado;

            string nombreArchivo = Path.GetFileName(rutaArchivoParam);

            Console.WriteLine($"\n\n¿Qué quieres hacer con el archivo [{nombreArchivo}]?");

            Console.WriteLine("1. Copiar");
            Console.WriteLine("2. Mover");
            Console.WriteLine("3. Eliminar");
            Console.WriteLine("4. Renombrar");  

            Console.Write("Selecciona una opción: ");
            int opcionArchivo = Convert.ToInt32(Console.ReadLine());

            switch (opcionArchivo)
            {
                case 1:
                    Console.Write("\nIngrese la ruta en donde quiere copiar el archivo: ");
                    rutaCopiarArchivo = Console.ReadLine();

                    if (Directory.Exists(rutaCopiarArchivo))
                    {
                        destinoArchivo = Path.Combine(rutaCopiarArchivo, nombreArchivo);

                        if (!File.Exists(destinoArchivo))
                        {
                            File.Copy(rutaArchivoParam, destinoArchivo);
                            MensajeRealizadoConExito("copiado");
                        }
                        else
                        {
                            Console.Write($"\nEl archivo [{nombreArchivo}] ya existe en la ruta de destino, ¿desea reemplazarlo? (s/n): ");
                            respuestaReemplazo = Console.ReadLine();

                            if (respuestaReemplazo.ToLower() == "s")
                            {
                                File.Copy(rutaArchivoParam, destinoArchivo, true);
                                MensajeRealizadoConExito("copiado");
                            }
                            else
                            {
                                MensajeOperacionCancelada();
                            }
                        }
                    }
                    else
                    {
                        MensajeRutaNoValida();   
                    }
                    break;
                case 2:
                    Console.Write("\nIngrese la ruta en la que quiere mover el archivo: ");
                    rutaMoverArchivo = Console.ReadLine();

                    if (Directory.Exists(rutaMoverArchivo))
                    {
                        destinoArchivo = Path.Combine(rutaMoverArchivo, nombreArchivo);

                        if (!File.Exists(destinoArchivo))
                        {
                            File.Move(rutaArchivoParam, destinoArchivo);
                            MensajeRealizadoConExito("movido");
                        }
                        else
                        {
                            Console.Write($"\nEl archivo [{nombreArchivo}] ya existe en la ruta de destino, ¿desea reemplazarlo? (s/n): ");
                            respuestaReemplazo = Console.ReadLine();

                            if (respuestaReemplazo.ToLower() == "s")
                            {
                                File.Delete(rutaArchivoParam);

                                File.Move(rutaArchivoParam, destinoArchivo);

                                MensajeRealizadoConExito("movido");
                            }
                            else
                            {
                                MensajeOperacionCancelada();
                            }
                        }
                    }
                    else
                    {
                        MensajeRutaNoValida();
                    }
                    break;
                case 3:
                    Console.Write($"\n¿Está seguro de que desea eliminar el archivo [{nombreArchivo}]? (s/n): ");
                    respuestaEliminar = Console.ReadLine();

                    if (respuestaEliminar.ToLower() == "s")
                    {
                        File.Delete(rutaArchivoParam);
                        MensajeRealizadoConExito("eliminado");
                    }
                    else
                    {
                        MensajeOperacionCancelada();
                    }
                    break;
                case 4:
                    Console.Write("\nIngresa el nuevo nombre para el archivo (incluye la extensión): ");
                    nuevoNombreArchivo = Console.ReadLine();

                    Console.Write($"El nuevo nombre de [{nombreArchivo}] será: [{nuevoNombreArchivo}], ¿es correcto? (s/n): ");
                    respuestRenombrar = Console.ReadLine();

                    if (respuestRenombrar.ToLower() == "s")
                    {
                        rutaArchivoRenombrado = Path.Combine(Path.GetDirectoryName(rutaArchivoParam), nuevoNombreArchivo);
                        File.Move(rutaArchivoParam, rutaArchivoRenombrado);

                        MensajeRealizadoConExito("movido");
                    }
                    else if (respuestRenombrar.ToLower() == "n")
                    {
                        MensajeOperacionCancelada();
                    }
                    break;
                default:
                    Console.WriteLine("Escoge una opción válida.");
                    Console.Write("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }

        static void MensajeRutaNoValida()
        {
            Console.WriteLine("\nIngresa una ruta válida.");
            Console.Write("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void MensajeOperacionCancelada()
        {
            Console.WriteLine("\nOperación cancelada por el usuario.");
            Console.Write("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void MensajeRealizadoConExito(string tipoMovimientoParam)
        {
            Console.WriteLine($"\nEl archivo se ha {tipoMovimientoParam} con éxito.");
            Console.Write("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
