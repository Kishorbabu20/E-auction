### **Description**
The **E-Auction Plan** is a web-based application that facilitates secure online auctions. It enables users to register, list products for auction, and place bids. The platform ensures data security by encrypting bid amounts using cryptography, providing privacy and integrity during the bidding process. Built using **ASP.NET Core** and **SQL Server**, it focuses on real-time data handling and robust backend performance.

---

### **Overview**
The project consists of three primary entities: **Users**, **Products**, and **Bids**.  
- **Users** register on the platform to list products for auction or participate as bidders.  
- **Products** are listed by users, including auction start and end times.  
- **Bids** are placed on products, encrypted using data protection techniques to maintain confidentiality.

This system uses **Entity Framework Core** for database management and cryptographic techniques for bid amount encryption. The backend API supports basic CRUD operations and ensures secure data flow between the application and database.

---

### **Key Features**
1. **User Management**:  
   - Secure user registration with hashed passwords.  
   - Unique email validation.

2. **Product Listing**:  
   - Owners can list products for auction with detailed descriptions.  
   - Specify auction start and end times.

3. **Secure Bidding**:  
   - Bid amounts are encrypted and stored securely in the database.  
   - Decrypt and display bid amounts only when retrieved.

4. **Database Integration**:  
   - Uses **SQL Server** for data storage.  
   - Supports relational integrity with foreign key relationships.

5. **Cryptography**:  
   - Bid amounts are encrypted using **ASP.NET Data Protection**.  
   - Ensures confidentiality and prevents unauthorized access.

6. **API Endpoints**:  
   - Place and retrieve bids via RESTful API.

---

### **Usage**
1. **User Registration and Login**:  
   - Register as a new user with a username, password, and email.
   - Authenticate using hashed credentials (extendable with JWT for token-based authentication).

2. **List Products**:  
   - Add products to the platform with details like name, description, start price, and auction duration.

3. **Place Bids**:  
   - Submit encrypted bids for a product.
   - Ensure bid amounts remain confidential.

4. **Retrieve Bids**:  
   - View bid details by decrypting the encrypted bid amounts stored in the database.

---

### **Configuration**
#### **1. Prerequisites**
- **ASP.NET Core 6.0 or later**
- **SQL Server** (Local or Remote)
- **Entity Framework Core** (EF Core)

#### **2. Database Setup**
- Use the provided SQL script to create the database and tables.
- Update the connection string in `appsettings.json`:
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=E_Auction;Trusted_Connection=True;"
    }
  }
  ```

#### **3. Installing Dependencies**
- Install required NuGet packages:
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  dotnet add package Microsoft.EntityFrameworkCore.Tools
  ```

#### **4. Running the Application**
- Build and run the application:
  ```bash
  dotnet run
  ```
- Use **Postman** or any API testing tool to test the endpoints:
  - `POST /api/bids/place`
  - `GET /api/bids/get/{bidId}`

---

### **Future Enhancements**
1. **Real-Time Bidding Updates**:  
   Use SignalR for live auction updates.
   
2. **Bid Validation**:  
   Implement validation rules to ensure each bid is higher than the previous bid.

3. **Frontend Integration**:  
   Develop a user-friendly frontend using **React**, **Angular**, or **Blazor**.

4. **Authentication**:  
   Add **JWT-based authentication** for secure login sessions.
