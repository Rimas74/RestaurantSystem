# RestaurantSystem

## Overview
RestaurantSystem is a student task for restaurant management application designed to streamline the operations of a restaurant. It includes features such as table management, order taking, and voucher generation.

## Features
- **Table Management**: Manage the occupancy and availability of tables in real-time.
- **Order Management**: Efficiently take customer orders and manage them.
- **Voucher Management**: Generate and manage vouchers for orders.
- **Email Notifications**: Send order details and vouchers via email to customers and the restaurant.

## Usage
To use the RestaurantSystem, run the application and follow the on-screen instructions to manage tables, take orders, and generate vouchers.

## Configuration
For email functionality, configure your SMTP settings in the `appsettings.json` file:
```json
{
  "EmailSettings": {
    "Username": "your-email@example.com",
    "Password": "your-password",
    "SmtpServer": "smtp-server-address",
    "SmtpPort": port-number
  }
}
