# NBE Banking Management System

An ASP.NET Web Forms application that provides comprehensive banking services for the National Bank of Egypt. The system features role-based access control with separate interfaces for clients and administrators, enabling secure banking operations and user management.

## What is NBE Banking System?

The NBE Banking Management System is a web-based platform that simulates real banking operations including:
- User registration and authentication with admin approval workflow
- Account management (Current and Savings accounts)
- Money transfers between accounts
- Balance inquiries and transaction history
- Administrative oversight with comprehensive logging
- Group payment functionality for multiple recipients

## Features

### **Client Features:**
- **Account Management:**
  - Registration with admin approval process
  - Multiple account types (Current & Savings)
  - Account creation after initial approval
  
- **Banking Operations:**
  - Money transfers between accounts
  - Balance checking across all accounts
  - Transaction history and bank statements
  - Group payments to multiple recipients
  
- **Security:**
  - Secure password hashing
  - Session management
  - Activity logging for all operations

### **Admin Features:**
- **User Management:**
  - Approve or reject user registrations
  - View pending user applications
  - Manage user account statuses
  
- **System Monitoring:**
  - Comprehensive audit logs
  - User activity tracking
  - Transaction monitoring
  
- **Administrative Controls:**
  - View all system activities
  - Monitor user sessions
  - Track IP addresses and timestamps

## User Authentication Flow

The system implements a secure multi-step authentication process:

1. **Role Selection** - Users choose between Client or Admin access
2. **Authentication** - Email and password verification with hashed passwords
3. **Account Status Check** - Validates user approval status (Pending/Approved/Rejected)
4. **Session Management** - Secure session handling with automatic redirects
5. **Activity Logging** - All actions logged with timestamps and IP tracking

## Database Architecture

The system uses SQL Server with the following key tables:

- **Users**: User information and account status
- **Accounts**: Banking accounts with balances and types
- **Transactions**: All financial operations
- **Logs**: Comprehensive audit trail
- **UserTypes**: Role definitions (Client/Admin)
- **AccountStatus**: User approval states
- **Branch**: Bank branch information
- **AccountType**: Current and Savings account types

## Technologies Used

- **Framework:** ASP.NET Web Forms (.NET Framework 4.7.2)
- **Database:** Microsoft SQL Server
- **Frontend:** HTML5, CSS3, JavaScript
- **Backend:** C#
- **Security:** Password hashing, Session management
- **Data Access:** ADO.NET with SqlConnection



### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.7.2 or higher
- SQL Server 2016 or later
- IIS Express (included with Visual Studio)

### Setup

1. **Clone the repository:**
git clone https://github.com/yourusername/NBE-Banking-System.git
cd NBE-Banking-System
2. **Database Setup:**
-- Create database
CREATE DATABASE NBEBankingDB;

-- Update connection string in web.config
<connectionStrings>
    <add name="NBEConnectionString" 
         connectionString="Server=.;Database=NBEBankingDB;Integrated Security=true;" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
3. **Configure web.config:**
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.web>
    <compilation debug="true" targetFramework="4.7.2" />
    <httpRuntime targetFramework="4.7.2" />
  </system.web>
  
  <connectionStrings>
    <add name="NBEConnectionString" 
         connectionString="[Your SQL Server connection string]" 
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
4. **Build and Run:**
   - Open solution in Visual Studio
   - Build the project (**Build** ? **Rebuild Solution**)
   - Run with **F5** or **Ctrl+F5**

## Usage

### Running the Application

### For Clients

#### Registration Process
1. Select "Client" role on homepage
2. Click "Register" and fill out registration form
3. Wait for admin approval
4. Login once approved
5. Complete account creation process

#### Banking Operations
1. **Transfer Money:**
   - Select source account
   - Enter recipient phone number
   - Choose recipient account
   - Specify amount and transfer

2. **Check Balance:**
   - View total balance across all accounts
   - See individual account details
   - Monitor account types and balances

3. **View Statements:**
   - Select specific account
   - View transaction history
   - See credits, debits, and counterparties

### For Administrators

#### User Management
1. Login with admin credentials
2. View pending user registrations
3. Approve or reject users
4. Monitor user activities

#### System Monitoring
1. Access comprehensive logs
2. Track user sessions
3. Monitor all system activities
4. View detailed audit trails

## Project Structure
NBE-Banking-System/
+-- Default.aspx              # Landing page with role selection
+-- Default.aspx.cs           # Role selection logic
+-- Login.aspx                # Authentication page
+-- Login.aspx.cs             # Login processing and validation
+-- Register.aspx             # User registration
+-- Register.aspx.cs          # Registration logic
+-- homepage.aspx             # Client dashboard
+-- homepage.aspx.cs          # Client homepage logic
+-- Adminpanel.aspx           # Admin dashboard
+-- Adminpanel.aspx.cs        # Admin panel functionality
+-- Transfer.aspx             # Money transfer interface
+-- Transfer.aspx.cs          # Transfer processing
+-- CheckBalance.aspx         # Balance inquiry
+-- CheckBalance.aspx.cs      # Balance checking logic
+-- Bankstatment.aspx         # Transaction history
+-- Bankstatment.aspx.cs      # Statement generation
+-- ViewLogs.aspx             # Admin log viewer
+-- ViewLogs.aspx.cs          # Log display logic
+-- CompleteRegistration.aspx # Account creation
+-- CompleteRegistration.aspx.cs # Account creation logic
+-- GroupPayment.aspx         # Multiple recipient transfers
+-- GroupPayment.aspx.cs      # Group payment processing
+-- web.config               # Application configuration
## Security Features

### Password Security
- Passwords are hashed using secure algorithms
- No plain text password storage
- Password verification during login

### Session Management
- Secure session handling
- Automatic session expiration
- Session validation on all pages

### Activity Logging
- Comprehensive audit trail
- IP address tracking
- Timestamp recording
- User action logging

### Access Control
- Role-based access (Client/Admin)
- Account status verification
- Session-based authentication

### Key Tables

**Users Table:**
- ID, Fname, Lname, Email, PhoneNumber, Addresses
- Passwords (hashed), UserTypeID, AccountStatusID

**Accounts Table:**
- AccountID, UserID, AccountTypeID, Balance, BranchID

**Transactions Table:**
- TransactionID, SenderAccountID, ReceiverAccountID
- Amount, Timestamp

**Logs Table:**
- LogID, SessionID, UserID, ActionType
- Timestamp, Details, IPAddress

**Branch Table:**
- BranchID, BranchName, Location

**AccountType Table:**
- AccountTypeID, AccountTypeName (Current/Savings)

## API Configuration

The application uses SQL Server for data persistence. Configure your connection string:

1. Update the connection string in `web.config`
2. Ensure SQL Server is running
3. Create the necessary database tables
4. Set up proper permissions for the application

## Sample Data

For testing purposes, you can create:
- Sample admin user with UserTypeID = 1
- Sample client users with UserTypeID = 2
- Different account types (Current = 1, Savings = 2)
- Sample branch data

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Educational Value

This project demonstrates:
- **Web Development:** ASP.NET Web Forms architecture
- **Database Design:** Relational database modeling
- **Security Implementation:** Authentication and authorization
- **Business Logic:** Banking operations and workflows
- **Audit Systems:** Comprehensive logging and monitoring
- **Session Management:** Secure user state handling



### Common Issues

1. **Database Connection Errors:**
<!-- Verify connection string in web.config -->
<add name="NBEConnectionString" 
     connectionString="Server=.;Database=NBEBankingDB;Integrated Security=true;" />
2. **Session Expired Errors:**
   - Check session timeout settings
   - Verify session state configuration
   - Ensure cookies are enabled

3. **Login Issues:**
   - Verify user account status (must be approved)
   - Check password hashing implementation
   - Confirm user exists in database

4. **404 Errors:**
   - Ensure web.config is properly configured
   - Verify Default.aspx is set as start page
   - Check IIS Express configuration

## Future Enhancements

- Mobile responsive design
- API integration for external services
- Advanced reporting features
- Multi-language support
- Enhanced security features (2FA)
- Real-time notifications
- Mobile application support

## Acknowledgments

- Microsoft for ASP.NET Web Forms framework
- SQL Server for robust data management
- Visual Studio for excellent development environment

## Author

**Nariman El-Azhary**
- GitHub: [@Nariman2005](https://github.com/Nariman2005)
- Project: [NBE-Banking-System](https://github.com/Nariman2005/NBE-Mini-Bank-System.git)

---

*This project was developed as part of an internship program, demonstrating practical applications of ASP.NET Web Forms in creating secure banking applications with comprehensive user management and audit capabilities.*



