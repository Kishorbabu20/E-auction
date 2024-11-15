using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDataProtection();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<CryptographyHelper>();

var app = builder.Build();

app.MapControllers();

app.Run();


using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Bid> Bids { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}


public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
}


public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal StartPrice { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public int OwnerId { get; set; }
    public User Owner { get; set; }
}


public class Bid
{
    public int BidId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int BidderId { get; set; }
    public User Bidder { get; set; }
    public byte[] EncryptedBidAmount { get; set; }
    public DateTime Timestamp { get; set; }
}


using Microsoft.AspNetCore.DataProtection;

public class CryptographyHelper
{
    private readonly IDataProtector _protector;

    public CryptographyHelper(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("E_Auction_Key");
    }

    public byte[] Encrypt(decimal bidAmount)
    {
        var plainText = bidAmount.ToString("F2");
        return System.Text.Encoding.UTF8.GetBytes(_protector.Protect(plainText));
    }

    public decimal Decrypt(byte[] encryptedBidAmount)
    {
        var decryptedText = _protector.Unprotect(System.Text.Encoding.UTF8.GetString(encryptedBidAmount));
        return decimal.Parse(decryptedText);
    }
}


using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BidsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CryptographyHelper _cryptoHelper;

    public BidsController(AppDbContext context, CryptographyHelper cryptoHelper)
    {
        _context = context;
        _cryptoHelper = cryptoHelper;
    }

    [HttpPost("place")]
    public IActionResult PlaceBid(int productId, int bidderId, decimal bidAmount)
    {
        var encryptedBid = _cryptoHelper.Encrypt(bidAmount);

        var bid = new Bid
        {
            ProductId = productId,
            BidderId = bidderId,
            EncryptedBidAmount = encryptedBid,
            Timestamp = DateTime.Now
        };

        _context.Bids.Add(bid);
        _context.SaveChanges();

        return Ok("Bid placed successfully.");
    }

    [HttpGet("get/{bidId}")]
    public IActionResult GetBid(int bidId)
    {
        var bid = _context.Bids.Find(bidId);
        if (bid == null) return NotFound();

        var bidAmount = _cryptoHelper.Decrypt(bid.EncryptedBidAmount);
        return Ok(new { BidId = bid.BidId, BidAmount = bidAmount });
    }
}


