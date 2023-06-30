namespace WarehouseApp.Entities;

public class Pallet
{
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
    public int Weight { get => Boxes.Sum(b => b.Weight) + 30; }
    public List<Box> Boxes { get; } = new List<Box>();
    
    public Pallet(int width, int height, int depth)
    {
        Id = Guid.NewGuid();
        Width = width;
        Height = height;
        Depth = depth;
    }
    
    public DateTime GetExpiryDate()
    {
        if (!Boxes.Any()) return DateTime.MinValue;
        var minExpiryDate = Boxes.Min(b => b.GetExpiryDate());
        return minExpiryDate;
    }
    
    public int GetTotalVolume()
    {
        return Boxes.Sum(b => b.GetVolume()) + Width * Height * Depth;
    }
    
    public DateTime GetMaxBoxExpiryDate()
    {
        if (Boxes.Any())
        {
            var maxExpiryDate = Boxes.Max(b => b.GetExpiryDate());
            return maxExpiryDate;
        }

        return DateTime.MinValue;
    }
    
    public void AddBox(Box box)
    {
        if (box.Width > Width || box.Depth > Depth)
        {
            throw new ArgumentException("Box dimensions exceed pallet size.");
        }

        Boxes.Add(box);
    }
}