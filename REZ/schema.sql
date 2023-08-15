CREATE TABLE [dbo].Users
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(50) NOT NULL, 
    [cpf_number] NVARCHAR(50) NULL, 
    [total_bill] MONEY NULL,
);
CREATE TABLE [dbo].Menu
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [category] NVARCHAR(50) NOT NULL, 
    [item_name] NVARCHAR(50) NOT NULL, 
    [description] NVARCHAR(50) NULL, 
    [price] MONEY NOT NULL,
);
CREATE TABLE [dbo].ShoppingCart
(
	[order_id] INT NOT NULL PRIMARY KEY, 
    [user_id] INT NOT NULL, 
    [dish_id] INT NULL, 
	[item_name] NVARCHAR(50) NULL, 
    [total_price] MONEY NULL,
	CONSTRAINT user_id FOREIGN KEY (user_id) REFERENCES Users(Id),
	CONSTRAINT dish_id FOREIGN KEY (dish_id) REFERENCES Menu(Id)
);