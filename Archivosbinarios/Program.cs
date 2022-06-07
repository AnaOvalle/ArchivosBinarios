using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivosbinarios
{
    class Program
    {
        class ArchivosBinariosEmpleados
        {
            // Declaración de flujos
            BinaryWriter bw = null; // flujo de salida de datos - escritura de datos
            BinaryReader br = null; // flujo de entrada - lectura de datos

            // campos de la clase
            public string Nombre, Dirección;
            public long Tel;
            public int NumEmp, DiasTrabajados;
            public float SalarioDiario;

            public void CrearArchivos (string Archivo)
            {
                // variable local método
                char resp;

                try
                {
                    // creaión del flujo para escribir datos al archivo
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                    // captura de datos
                    do
                    {
                        Console.Clear();
                        Console.Write("Número de empleado: ");
                        NumEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del empleado: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Dirección del empleado: ");
                        Dirección = Console.ReadLine();
                        Console.Write("Teléfono del empleado: ");
                        Tel = long.Parse(Console.ReadLine());
                        Console.Write("Dias trabajados del empleado: ");
                        DiasTrabajados = Int32.Parse(Console.ReadLine());
                        Console.Write("Salario diario de empleado: ");
                        SalarioDiario = float.Parse(Console.ReadLine());

                        // escribe los datos al archivo
                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Dirección);
                        bw.Write(Tel);
                        bw.Write(DiasTrabajados);
                        bw.Write(SalarioDiario);

                        Console.Write("\n\n ¿Deseas almacenar otro registro (s/n)?: ");

                        resp = char.Parse(Console.ReadLine());

                    } while ((resp == 's') || (resp == 's'));

                } 
                
                catch (IOException e)
                {
                    Console.WriteLine("\nError: " + e.Message);
                    Console.WriteLine("\nError: " + e.StackTrace);
                }

                finally
                {
                    if (bw != null) bw.Close(); // cierra el flujo - escritura
                    Console.Write("\nPresione ENTER para terminar la Escritura de Datos y regresar el Menú");
                    Console.ReadKey();

                }
            }

            public void MostrarArchivos (string Archivo)
            {
                try
                {
                    // verifica si existe el archivo
                    if (File.Exists(Archivo)) 
                
                    {
                        // creación del flujo para leer los datos del archivo
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        // Despliegue de datos en pantalla
                        Console.Clear();

                        do
                        {
                            // Lectura de registros mientas no llegue a EndorFile
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Dirección = br.ReadString();
                            Tel = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();

                            // muestra de datos
                            Console.WriteLine("Número de empleado: " + NumEmp);
                            Console.WriteLine("Nombre del empleado: " + Nombre);
                            Console.WriteLine("Dirección del empleado: " + Dirección);
                            Console.WriteLine("Teléfono del empleado: " + Tel);
                            Console.WriteLine("Dias trabajados del empleado: " + DiasTrabajados);
                            Console.WriteLine("Salario diario de empleado: " + SalarioDiario);

                            Console.WriteLine("SUELDO TOTAL del empleado: {0:C}", DiasTrabajados * SalarioDiario);
                            Console.WriteLine("\r");
                        } while (true);
                    }


                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl Archivo " + Archivo + "No existe en el disco!!");
                        Console.Write("\nPresiona ENTER para continuar...");
                        Console.ReadKey();

                    }
                
                }  
                
                catch (EndOfStreamException)
                {
                    Console.WriteLine("\n\nPin del listado de Empleado");
                    Console.Write("\nPresione ENTER para continuar...");
                    Console.ReadKey();
                }

                finally
                {
                    if (br != null) br.Close(); // cierre del flujo
                    Console.Write("\nPresione ENTER para terminar la lectura de datos y regresar al menú");
                    Console.ReadKey();
                }
            }
            
        }
        static void Main(string[] args)
        {
            // Declaración de variables 
            string Arch = null;
            int opcion;

            // Creación del objeto
            ArchivosBinariosEmpleados A1 = new ArchivosBinariosEmpleados();

            // Menú de opciones

            do
            {
                Console.Clear();
                Console.WriteLine("\n---ARCHIVO NINARIO EMPLEADOS---");
                Console.WriteLine("1. Creación de un archivo.");
                Console.WriteLine("2. Lectura de un Archivo.");
                Console.WriteLine("3. Salida del Programa");
                Console.Write("\nElija una opcion: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a crear: ");
                            Arch = Console.ReadLine();

                            //Verifica si existe el archivo
                            char reps = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl archivo existe!!, Deseas Sobreescribirlo (s/n)? ");
                                reps = char.Parse(Console.ReadLine());
                            }
                            if ((reps == 's') || (reps == 's'))
                            {
                                A1.CrearArchivos(Arch);
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError :" + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //Bloque lectura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas leer: ");
                            Arch = Console.ReadLine();
                            A1.MostrarArchivos(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError :" + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para salir del programa.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Write("\nEsa Opcion no EXISTE!! , pRESION <enter> para continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);


        }
        }
    }

