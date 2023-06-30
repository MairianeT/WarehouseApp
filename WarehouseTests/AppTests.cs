using NUnit.Framework;
using WarehouseApp.Entities;

namespace WarehouseTests;

public class AppTests
{
    [SetUp]
    public void Setup()
    {
        List<Pallet> pallets = new List<Pallet>();
    }

    [Test]
    public void AddBox_ValidBox_BoxAddedToPallet()
    {
        Pallet pallet = new Pallet(20, 20, 20);

        Box box = new Box(10, 10, 10, 30, DateTime.Now);
        pallet.AddBox(box);
        
        Assert.AreEqual(1, pallet.Boxes.Count);
        Assert.IsTrue(pallet.Boxes.Contains(box));
    }

    [Test]
    public void AddBox_BoxTooLarge_ExceptionThrown()
    {
        Pallet pallet = new Pallet(20, 20, 20);
        Box box = new Box(100, 100, 100, 300, DateTime.Now);
        Assert.Throws<ArgumentException>(() => pallet.AddBox(box));
    }
    
    [Test]
    public void GetPalletExpiryDate_NoBoxes_ReturnsMinDate()
    {
        Pallet pallet = new Pallet(20, 20, 20);
        var expiryDate = pallet.GetExpiryDate();
        Assert.AreEqual(DateTime.MinValue, expiryDate);
    }

    [Test]
    public void GetPalletExpiryDate_WithBoxes_ReturnsMinBoxExpiryDate()
    {
        Pallet pallet = new Pallet(20, 20, 20);

        DateTime boxExpiryDate1 = new DateTime(2023, 1, 1);
        Box box1 = new Box( 10,10, 10, 10, boxExpiryDate1);
        pallet.AddBox(box1);

        DateTime boxExpiryDate2 = new DateTime(2022, 12, 31);
        Box box2 = new Box(10, 10, 10, 10, boxExpiryDate2);
        pallet.AddBox(box2);

        var expiryDate = pallet.GetExpiryDate();
        Assert.AreEqual(boxExpiryDate2.AddDays(100), expiryDate);
    }
    
    [Test]
    public void Constructor_NegativeWidth_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Box( -10, 10, 10,10, DateTime.Now));
    }

    [Test]
    public void Constructor_NegativeHeight_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Box(10, -10, 10,10, DateTime.Now));
    }

    [Test]
    public void Constructor_NegativeDepth_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Box(10, 10, -10, 10, DateTime.Now));
    }

    [Test]
    public void Constructor_NegativeWeight_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Box(10, 10, 10, -10, DateTime.Now));
    }
}