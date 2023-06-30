namespace WarehouseApp.Entities;

public class Box
{
    private int _weight;
    private int _width;
    private int _height;
    private int _depth;
    public Guid Id { get; }
    public int Width
    {
        get => _width;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Width cannot be negative.");
            }
            _width = value;
        } 
    }
    public int Height { get => _height;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Height cannot be negative.");
            }
            _height = value;
        }
    }
    public int Depth { get => _depth;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Depth cannot be negative.");
            }
            _depth = value;
        }
    }
    public int Weight { get => _weight;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Weight cannot be negative.");
            }
            _weight = value;
        }
    }
    public DateTime ProductionDate { get; }
    
    public Box(int width, int height, int depth, int weight, DateTime productionDate)
    {
        Id = Guid.NewGuid();
        Width = width;
        Height = height;
        Depth = depth;
        ProductionDate = productionDate;
        Weight = weight;
    }
    
    public DateTime GetExpiryDate()
    {
        return ProductionDate.AddDays(100);
    }
    
    public int GetVolume()
    {
        return Width * Height * Depth;
    }
}