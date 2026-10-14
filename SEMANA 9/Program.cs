using System;

using System.IO;



namespace SEMANA_9

{

    internal class Program

    {

        static int max_emp = 50;

        static string[] nombres = new string[max_emp];

        static string[] correo_emp = new string[max_emp];

        static int cant_emp = 0;



        static void Main(string[] args)

        {

            int opc = 0;

            while (opc != 8)

            {

                Console.Clear();

                Titulo();

                Console.WriteLine("[1] Registrar empleado");

                Console.WriteLine("[2] Buscar empleado");

                Console.WriteLine("[3] Ordenar alfabéticamente (A-Z)");

                Console.WriteLine("[4] Mostrar empleados");

                Console.WriteLine("[5] Guardar en archivo");

                Console.WriteLine("[6] Leer archivo en consola");

                Console.WriteLine("[7] Cargar datos de archivo a memoria");

                Console.WriteLine("[8] Salir");

                Console.Write("\nIngresar opción: ");



                if (!int.TryParse(Console.ReadLine(), out opc))

                {

                    Console.WriteLine("\nPor favor, ingrese un número válido.");

                    Pausar();

                    continue;

                }



                Console.WriteLine();

                switch (opc)

                {

                    case 1:

                        Registro_empleado();

                        break;

                    case 2:

                        Buscar_empleado();

                        break;

                    case 3:

                        Ordenar();

                        break;

                    case 4:

                        Mostrar();

                        break;

                    case 5:

                        Guardar_datos_Archivo();

                        break;

                    case 6:

                        Leer_datos_Archivo();

                        break;

                    case 7:

                        Cargar_datos_Archivo();

                        break;

                    case 8:

                        Console.WriteLine("Gracias por usar el sistema...!!");

                        break;

                    default:

                        Console.WriteLine("Opción incorrecta, elija del 1 al 8.");

                        break;

                }



                if (opc != 8)

                {

                    Pausar();

                }

            }

        }



        public static void Titulo()

        {

            Console.WriteLine("***************************");

            Console.WriteLine("  SISTEMA DE EMPLEADOS  ");

            Console.WriteLine("***************************");

        }



        public static void Pausar()

        {

            Console.WriteLine("\nPresione cualquier tecla para continuar...");

            Console.ReadKey();

        }



        public static void Registro_empleado()

        {

            Console.WriteLine("********REGISTRAR EMPLEADO*********");

            if (cant_emp >= max_emp)

            {

                Console.WriteLine("Alcanzó la capacidad máxima de datos.");

                return;

            }



            Console.Write("Ingresar nombres y apellidos: ");

            string nombres_emp = Console.ReadLine()?.Trim();



            if (string.IsNullOrEmpty(nombres_emp))

            {

                Console.WriteLine("Error. El nombre no puede estar vacío.");

                return;

            }



            string nombres_Mayus = nombres_emp.ToUpper();

            string[] partes = nombres_Mayus.Split(' ');

            string primer_nombre = partes.Length > 0 ? partes[0] : "usuario";





            string segundo_apellido = partes.Length >= 3 ? partes[2] : (partes.Length > 1 ? partes[1] : "emp");



            string correo = $"{primer_nombre.ToLower()}.{segundo_apellido.ToLower()}@upn.edu.pe";



            nombres[cant_emp] = nombres_Mayus;

            correo_emp[cant_emp] = correo;

            cant_emp++;



            Console.WriteLine($"\n¡Empleado registrado con éxito!");

            Console.WriteLine($"Nombre y Apellido: {nombres_Mayus}");

            Console.WriteLine($"Correo: {correo}");

        }



        public static void Buscar_empleado()

        {

            Console.WriteLine("********BÚSQUEDA DE EMPLEADO******");

            if (cant_emp == 0)

            {

                Console.WriteLine("No hay registros de empleados.");

                return;

            }



            Console.Write("Ingrese nombre o apellido a buscar: ");

            string buscar = Console.ReadLine()?.Trim().ToUpper();



            if (string.IsNullOrEmpty(buscar))

            {

                Console.WriteLine("La búsqueda no puede estar vacía.");

                return;

            }



            bool encontrado = false;

            for (int i = 0; i < cant_emp; i++)

            {

                if (nombres[i].Contains(buscar))

                {

                    Console.WriteLine($"\n[Encontrado] Nombre: {nombres[i]} | Correo: {correo_emp[i]}");

                    encontrado = true;

                }

            }



            if (!encontrado)

            {

                Console.WriteLine("No se encontraron coincidencias para la búsqueda.");

            }

        }



        public static void Ordenar()

        {

            Console.WriteLine("********ORDENAR EMPLEADO*******");

            if (cant_emp <= 1)

            {

                Console.WriteLine("No hay suficientes registros para ordenar.");

                return;

            }



            for (int i = 0; i < cant_emp - 1; i++)

            {

                for (int j = 0; j < cant_emp - 1 - i; j++)

                {

                    if (string.Compare(nombres[j], nombres[j + 1], StringComparison.Ordinal) > 0)

                    {

                        string temp_nom = nombres[j];

                        nombres[j] = nombres[j + 1];

                        nombres[j + 1] = temp_nom;



                        string temp_correo = correo_emp[j];

                        correo_emp[j] = correo_emp[j + 1];

                        correo_emp[j + 1] = temp_correo;

                    }

                }

            }



            Console.WriteLine("Empleados ordenados alfabéticamente correctamente.");

        }



        public static void Mostrar()

        {

            Console.WriteLine("*********LISTA DE EMPLEADOS*********");

            if (cant_emp == 0)

            {

                Console.WriteLine("No hay empleados registrados.");

                return;

            }



            for (int i = 0; i < cant_emp; i++)

            {

                Console.WriteLine($"[{i + 1}] Nombre: {nombres[i]} | Correo: {correo_emp[i]}");

            }

        }



        public static void Guardar_datos_Archivo()

        {

            Console.WriteLine("********GUARDAR DATOS EN ARCHIVO********");

            if (cant_emp == 0)

            {

                Console.WriteLine("No hay datos en memoria para guardar.");

                return;

            }



            using (StreamWriter escribir = new StreamWriter("Empleados.txt"))

            {

                for (int i = 0; i < cant_emp; i++)

                {

                    escribir.WriteLine(nombres[i] + "," + correo_emp[i]);

                }

            }



            Console.WriteLine(cant_emp + " empleados guardados en el archivo correctamente.");

        }



        public static void Leer_datos_Archivo()

        {

            Console.WriteLine("*******LEER DATOS DE ARCHIVO********");

            Console.WriteLine("***********************************");



            if (!File.Exists("Empleados.txt"))

            {

                Console.WriteLine("No existe el archivo de empleados.");

                return;

            }



            using (StreamReader lector = new StreamReader("Empleados.txt"))

            {

                string linea;

                int contador = 1;

                while ((linea = lector.ReadLine()) != null)

                {

                    string[] datos = linea.Split(',');

                    if (datos.Length == 2)

                    {

                        Console.WriteLine($"[{contador}] Nombre: {datos[0]} | Correo: {datos[1]}");

                        contador++;

                    }

                }

            }

        }



        public static void Cargar_datos_Archivo()

        {

            Console.WriteLine("*******CARGAR DATOS A MEMORIA********");

            if (!File.Exists("Empleados.txt"))

            {

                Console.WriteLine("No existe el archivo de empleados.");

                return;

            }



            cant_emp = 0;

            using (StreamReader lector = new StreamReader("Empleados.txt"))

            {

                string linea;

                while ((linea = lector.ReadLine()) != null && cant_emp < max_emp)

                {

                    string[] datos = linea.Split(',');

                    if (datos.Length == 2)

                    {

                        nombres[cant_emp] = datos[0];

                        correo_emp[cant_emp] = datos[1];

                        cant_emp++;

                    }

                }

            }



            Console.WriteLine($"{cant_emp} empleados cargados a memoria exitosamente.");

        }

    }

}