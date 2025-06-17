using System;
using System.Reflection.Metadata;

namespace Tareas
{
    public class Tarea
    {
        public int userID { get; set; }
        public int id { get; set; }
        public string? title { get; set; }
        public bool completed { get; set; }
    }    
}