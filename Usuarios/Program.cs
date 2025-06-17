using System;
using System.Text.Json;
using System.IO;
using Usuarios;

HttpClient client = new HttpClient(); 

//Hago una solicitud GET a la URL y veirfico que la respuesta sea exitosa
HttpResponseMessage respuesta = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
respuesta.EnsureSuccessStatusCode();

//Leo y deserealizo la respuesta
string respuestaCuerpo = await respuesta.Content.ReadAsStringAsync();
List<Usuario> ListaDeUsuarios = JsonSerializer.Deserialize<List<Usuario>>(respuestaCuerpo) ?? new List<Usuario>();


Console.WriteLine("LISTA DE USUARIOS:");
foreach (var usu in ListaDeUsuarios)
{
    if (usu.id <= 5)
    {
        Console.WriteLine("Nombre:" + usu.name + "---Email:" + usu.email + "---Domicilio:" + usu.address.street);
    }
}

//Creo un archivo .json
string ruta = @"D:\Taller de Lenguajes\tl1-tp10-2025-JuanMartinFeliu\Usuarios\Usuarios.json";

//Serializo la lista de tareas a formato JSON
string JsonString = JsonSerializer.Serialize(ListaDeUsuarios, new JsonSerializerOptions{WriteIndented = true});

//Creo el directorio si no existe
Directory.CreateDirectory(Path.GetDirectoryName(ruta)!);

//Uso await para escribir el archivo json
await File.WriteAllTextAsync(ruta, JsonString);

Console.WriteLine($"Archivo generado en:{ruta}");