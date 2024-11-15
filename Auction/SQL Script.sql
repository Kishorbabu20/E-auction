CREATE DATABASE E_Auction;

USE E_Auction;

-- Users table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE
);

-- Products table
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    StartPrice DECIMAL(18, 2) NOT NULL,
    StartDateTime DATETIME NOT NULL,
    EndDateTime DATETIME NOT NULL,
    OwnerId INT FOREIGN KEY REFERENCES Users(UserId)
);

-- Bids table
CREATE TABLE Bids (
    BidId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT FOREIGN KEY REFERENCES Products(ProductId),
    BidderId INT FOREIGN KEY REFERENCES Users(UserId),
    EncryptedBidAmount VARBINARY(MAX) NOT NULL,
    Timestamp DATETIME DEFAULT GETDATE()
);

