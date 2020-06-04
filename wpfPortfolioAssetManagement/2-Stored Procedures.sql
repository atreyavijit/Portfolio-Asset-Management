drop procedure usp_InsertMarketAsset;

create procedure usp_InsertMarketAsset(@marketassets Assets readonly)
as
begin
   insert into [dbo].[Assets] select [Symbol],[ExchangeName],[ShortName],[MarketPrice],[ChangePercent],[QuoteType],[Market],[MarketTime],[MarketState],[Language] from @marketassets
end
go

IF OBJECT_ID ('AssetInsertTr', 'TR') IS NOT NULL
   DROP TRIGGER AssetInsertTr;
GO
CREATE TRIGGER AssetInsertTr
ON Assets
FOR INSERT
as
	begin
      DECLARE @lastID INT;

      SET @lastID = (SELECT max(Id)-14 FROM Assets);

		INSERT INTO AssetHistory
		(
			Symbol,
         ExchangeName,
         ShortName,
         MarketPrice,
         ChangePercent,
         QuoteType,
         Market,
         MarketTime,
         MarketState,
         Language
		)
		SELECT
			del.Symbol,
			del.ExchangeName,
			del.ShortName,
			del.MarketPrice,
			del.ChangePercent,
			del.QuoteType,
			del.Market,
			del.MarketTime,
			del.MarketState,
			del.Language
		from deleted as del
		union
		SELECT
			ins.Symbol,
			ins.ExchangeName,
			ins.ShortName,
			ins.MarketPrice,
			ins.ChangePercent,
			ins.QuoteType,
			ins.Market,
			ins.MarketTime,
			ins.MarketState,
			ins.Language
		from inserted as ins
      DELETE FROM Assets Where Id<@lastID;
	end
;
go
