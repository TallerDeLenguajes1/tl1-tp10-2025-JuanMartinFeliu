using System;
using System.Text.Json;
using System.IO;
using Tareas;

//Creo una nueva instancia
HttpClient client = new HttpClient(); 

//Hago una solicitud GET a la URL y veirfico que la respuesta sea exitosa
HttpResponseMessage respuesta = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/");
respuesta.EnsureSuccessStatusCode();

//Leo y deserealizo la respuesta
string respuestaCuerpo = await respuesta.Content.ReadAsStringAsync();
List<Tarea> ListaDeTareas = JsonSerializer.Deserialize<List<Tarea>>(respuestaCuerpo) ?? new List<Tarea>();


Console.WriteLine("TAREAS PENDIENTES:");
foreach (var tareitas in ListaDeTareas)
{
    if (tareitas.completed == false)
    {
        Console.WriteLine("Titulo:" + tareitas.title + "---Estado:" + tareitas.completed);
    }
}

Console.WriteLine("TAREAS REALIZADAS:");
foreach (var tareitas in ListaDeTareas)
{
    if (tareitas.completed == true)
    {
        Console.WriteLine("Titulo:" + tareitas.title + "---Estado:" + tareitas.completed);
    }
}

//Serializo de nuevo la lista de tareas a formato JSON
string JsonString = JsonSerializer.Serialize(ListaDeTareas,new JsonSerializerOptions{WriteIndented = true});

//Creo un archivo Json
string nombreArchivo = @"D:\Taller de Lenguajes\tl1-tp10-2025-JuanMartinFeliu\Tareas\tareas.json";

Directory.CreateDirectory(Path.GetDirectoryName(nombreArchivo)!);

//Uso await para escribir el archivo json
await File.WriteAllTextAsync(nombreArchivo, JsonString);

Console.WriteLine($"Archivo generado en:{nombreArchivo}");



