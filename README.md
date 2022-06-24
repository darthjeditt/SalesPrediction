### `INSTALLATION GUIDE`

### `SQLEXPRESS`

1. Download sqlexpress from: https://www.microsoft.com/en-au/sql-server/sql-server-downloads -> Express version
2. Go through the installation process until you have successfully created the sql server
3. Download https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16 -> free download and install it
4. Once you have installed SQL Server Manager, open it up and connect to your SQL server which should have the name `[name]\SQLEXPRESS`
5. Next create a new database by right-clicking Databases -> new database and name it `SalesHistoryDB`
6. Drop down your newly created database and right click tables -> new -> tables

### Tables

1. Column name = SalesId, Data type = int, allow nulls = unticked, right click and set primary key
2. Next column; Column name = Dos, Data Type = DateTime, allow nulls = unticked
3. Last columm; Column name = Sales, Data type = float

### insert data

1. insert some data into the tables by right clicking -> edit top 200 rows.


### `Visual Studios`
1. open `SalesPrediction` solution
2. run the solution using IIS Express
3. Go to view -> terminal
4. type cd SalesPredictionFrontEnd
5. type npm start


