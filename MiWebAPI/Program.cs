using System;
using System.Text.Json;
using System.IO;
using CamposApi;

HttpClient client = new HttpClient();

//Hago una solicitud GET a la URL y veirfico que la respuesta sea exitosa
HttpResponseMessage respuesta = await client.GetAsync("https://brasilapi.com.br/api/feriados/v1/2024");
respuesta.EnsureSuccessStatusCode();

//Leo y deserealizo la respuesta
string respuestaCuerpo = await respuesta.Content.ReadAsStringAsync();
List<datos> ListaDeDatos = JsonSerializer.Deserialize<List<datos>>(respuestaCuerpo) ?? new List<datos>();


Console.WriteLine("LISTA DE FERIADOS EN BRASIL:");
Console.WriteLine("-------------------------------------------------------");
foreach (var dato in ListaDeDatos)
{
    Console.WriteLine("Fecha del Feriado:" + dato.date);
    Console.WriteLine("Nombre del Feriado:" + dato.name);
    Console.WriteLine("Tipo del Feriado:" + dato.type);
    Console.WriteLine("-------------------------------------------------------");
}

//Creo un archivo .json
string ruta = @"D:\Taller de Lenguajes\tl1-tp10-2025-JuanMartinFeliu\MiWebAPI\MiWebApi.json";

//Serializo la lista de tareas a formato JSON
string JsonString = JsonSerializer.Serialize(ListaDeDatos, new JsonSerializerOptions{WriteIndented = true});

//Creo el directorio si no existe
Directory.CreateDirectory(Path.GetDirectoryName(ruta)!);

//Uso await para escribir el archivo json
await File.WriteAllTextAsync(ruta, JsonString);

Console.WriteLine($"Archivo generado en:{ruta}");