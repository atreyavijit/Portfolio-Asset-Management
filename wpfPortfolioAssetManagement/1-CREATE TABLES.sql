DROP TABLE IF EXISTS Assets;
CREATE TYPE Assets AS TABLE(
	[Symbol] nvarchar(15) NOT NULL,
	[ExchangeName] nvarchar(100) NOT NULL,
    [ShortName] nvarchar(100) NOT NULL,
	[MarketPrice] float(24) NOT NULL,
	[ChangePercent] int NOT NULL,
    [QuoteType] nvarchar(50) NOT NULL,
    [Market] nvarchar(50) NOT NULL,
    [MarketTime] int NOT NULL,
    [MarketState] nvarchar(50) NOT NULL,
    [Language] nvarchar(10) NOT NULL
);
go

CREATE TABLE Assets(
	[Id] int IDENTITY(1,1) NOT NULL,
	[Symbol] nvarchar(15) NOT NULL,
	[ExchangeName] nvarchar(100) NOT NULL,
    [ShortName] nvarchar(100) NOT NULL,
	[MarketPrice] DECIMAL(7,2) NOT NULL,
	[ChangePercent] DECIMAL(5,2) NOT NULL,
    [QuoteType] nvarchar(50) NOT NULL,
    [Market] nvarchar(50) NOT NULL,
    [MarketTime] int NOT NULL,
    [MarketState] nvarchar(50) NOT NULL,
    [Language] nvarchar(10) NOT NULL,
    CONSTRAINT pk_Assets_Id PRIMARY KEY clustered (Id)
);
go

DROP TABLE IF EXISTS AssetHistory;

CREATE TABLE AssetHistory(
	[Symbol] nvarchar(15) NOT NULL,
	[ExchangeName] nvarchar(100) NOT NULL,
    [ShortName] nvarchar(100) NOT NULL,
	[MarketPrice] DECIMAL(7,2) NOT NULL,
	[ChangePercent] DECIMAL(5,2) NOT NULL,
    [QuoteType] nvarchar(50) NOT NULL,
    [Market] nvarchar(50) NOT NULL,
    [MarketTime] int NOT NULL,
    [MarketState] nvarchar(50) NOT NULL,
    [Language] nvarchar(10) NOT NULL,
    [DateTime] DateTime DEFAULT GETDATE() not null
);
go
DROP TABLE IF EXISTS PortfolioAssets;

CREATE TABLE PortfolioAssets
(
	[Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int not null,
    [Symbol] nvarchar(50) not null, -- Symbol will be Foreign Key from table Assets
    [BuyPrice] int null,
    [SellPrice] int null,
    [NumOfShares] int not null default(0),
	[ClosePrice] decimal(7,2) not null,
	[Date] DateTime DEFAULT GETDATE() not null,
    CONSTRAINT pk_PortfolioAssets_Id PRIMARY KEY (Id)
);
go

ALTER TABLE PortfolioAssets
    ADD CONSTRAINT fk_PortfolioAssets_Users FOREIGN KEY (UserId) REFERENCES Users(Id);

ALTER TABLE Assets
	ADD Source int not null default(1);
	go

DROP TABLE IF EXISTS StockSource;

CREATE TABLE StockSource(
	[Id] int IDENTITY(1,1) NOT NULL,
	[URL] nvarchar(200) null,
	[Name] nvarchar(100) not null,
	CONSTRAINT pk_StockSource_Id PRIMARY KEY (Id)
);
go

DROP TABLE AssetWatchList;

CREATE TABLE AssetWatchList(
		[Id] int IDENTITY(1,1) NOT NULL,
		[UserId] int not null,
		[Symbol] nvarchar(50) not null, -- Symbol will be Foreign Key from table Assets
		[Name] nvarchar(150) not null,
		[Date] DateTime DEFAULT GETDATE() not null,
		CONSTRAINT pk_AssetWatchList_Id PRIMARY KEY (Id)
);
go

CREATE TABLE AssetPortfolio(
		[Id] int IDENTITY(1,1) NOT NULL,
		[UserId] int not null,
		[Symbol] nvarchar(50) not null, -- Symbol will be Foreign Key from table Assets
		[NumOfShares] int not null,
		[BuyPrice] decimal(7,2) not null,
		[SellPrice] decimal(7,2) not null,
		[ClosePrice] decimal(7,2) not null,
		[Date] DateTime DEFAULT GETDATE() not null,
		CONSTRAINT pk_AssetPortfolio_Id PRIMARY KEY (Id)
);
go

ALTER TABLE AssetWatchList
    ADD CONSTRAINT fk_AssetWatchList_Users FOREIGN KEY (Id) REFERENCES Users(Id);

ALTER TABLE AssetHistory
	ADD CONSTRAINT fk_AssetHistory_StockSource FOREIGN KEY (Source) REFERENCES StockSource(Id);

ALTER TABLE [Assets] ALTER COLUMN ChangePercent DECIMAL(5,4);

ALTER TABLE AssetWatchList
	ADD CONSTRAINT fk_AssetWatchList_Users FOREIGN KEY (UserId) REFERENCES Users(Id);

ALTER TABLE AssetWatchList
	ADD CONSTRAINT fk_AssetWatchList_Assets FOREIGN KEY (Symbol) REFERENCES Assets(Symbol);

CREATE TABLE [Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_Id] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
