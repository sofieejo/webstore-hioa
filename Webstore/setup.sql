CREATE TABLE [dbo].[city] (
    [zipcode] VARCHAR (4)  NOT NULL,
    [name]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([zipcode] ASC)
);

CREATE TABLE [dbo].[customer] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [firstname] VARCHAR (50)  NOT NULL,
    [lastname]  VARCHAR (50)  NOT NULL,
    [address]   VARCHAR (100) NOT NULL,
    [zipcode]   VARCHAR (4)   NOT NULL,
    [email]     VARCHAR (100) NOT NULL,
    [telephone] VARCHAR (8)   NOT NULL,
    [password]  VARBINARY (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_customer_city] FOREIGN KEY ([zipcode]) REFERENCES [dbo].[city] ([zipcode])
);

CREATE TABLE [dbo].[product] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (50)  NOT NULL,
    [price]       DECIMAL (18)  NOT NULL,
    [description] VARCHAR (MAX) DEFAULT ('no description') NOT NULL,
    [categoryID]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
);

CREATE TABLE [dbo].[order] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [date]       DATETIME NOT NULL,
    [customerID] INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_order_customer] FOREIGN KEY ([customerID]) REFERENCES [dbo].[customer] ([Id])
);

CREATE TABLE [dbo].[orderdetail] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [productID] INT NOT NULL,
    [quantity]  INT NOT NULL,
    [orderID]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_orderdetail_product] FOREIGN KEY ([productID]) REFERENCES [dbo].[product] ([Id]),
    CONSTRAINT [FK_orderdetail_order] FOREIGN KEY ([orderID]) REFERENCES [dbo].[order] ([Id])
);