using System.Dynamic;

namespace APBD_4.Models;

public class Animal
{
    public int IdAnimal { get; private set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Mass { get; set; }
    public string FurColor { get; set; }

    public void SetId(int id)
    {
        IdAnimal = id;
    }
    
}