/*
Miguel Pérez
Actividad 1.2
 */

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace BlueGlobal
{
    class Program
    {
        // Usaré json para instanciar las clases de cargos simplificará el uso de recursos y funcionará de forma programatica.
        static List<Cargo> cargos()
        {
            using (StreamReader r = new StreamReader("cargos.json"))
            {
                string json = r.ReadToEnd();
                List<Cargo> cargos = JsonSerializer.Deserialize<List<Cargo>>(json);
                return cargos;
            }
        }

        static string uglyLinebreak = "=========================";

        static string getCargosOptions()
        {
            var s = "";
            int i = 0;
            foreach (Cargo cargo in cargos())
            {
                // Aprovecharé de usar ASCII para imprimir las letras.
                s += $"Para ser {cargo.nombreCargo} presione {Convert.ToChar(i + 65)}\n";
                i++;
            }
            return s;
        }
        
        static void MensajeIntro()
        {
            Console.WriteLine($"BLUEGLOBAL SPA\n" +
                $"{uglyLinebreak}\nBienvenido al programa que le permitirá \n" +
                $"seleccionar el tipo de trabajo que va a realizar\n{uglyLinebreak}\n" +
                $"{getCargosOptions()}{uglyLinebreak}\nPor favor digite una opción válida: ");
        }

        static void Main(string[] args)
        {
            while (true)
            {
                MensajeIntro();
                var respuesta = (int)Console.ReadKey().KeyChar;
                Console.ReadLine(); // Solo para prevenir que al ingresar la tecla la consola pase en banda.
                // respuesta debe estar entre A - Z o a - z
                if (respuesta > 64 && respuesta < 91 || respuesta > 95 && respuesta < 123) 
                {
                    // Normalizamos la respuesta.
                    respuesta = respuesta < 95 ? respuesta - 65 : respuesta - 97;
                    // Validamos la cantidad de cargos dispoinibles.
                    if(respuesta < cargos().Count)
                    {
                        Console.WriteLine($"\nOpción {cargos()[respuesta].nombreCargo}\n\n" +
                            $"COMPROBAR SUELDO ({cargos()[respuesta].nombreCargo})\n" +
                            $"{uglyLinebreak}\nPara mostrar sueldo presione s o S");
                        var confirmar = Console.ReadLine();
                        if (confirmar == "s" || confirmar == "S")
                        {
                            Console.WriteLine($"Sueldo de {cargos()[respuesta].nombreCargo} " +
                                $"es de {cargos()[respuesta].sueldo}");
                        }
                        else
                            Console.WriteLine("Usted no confirmó, este programa se reiniciará.");

                    }
                    else
                    {
                        Console.WriteLine("No hay tantos cargos disponibles.");
                        continue;
                    }

                }
                Console.WriteLine("Para continuar presione una tecla: ");
                Console.ReadLine();
            }
            
        }
    }
}
