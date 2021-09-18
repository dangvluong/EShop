--DROP TABLE Cart;
GO
CREATE TABLE Cart(
	CartId UNIQUEIDENTIFIER NOT NULL,
	ProductId SMALLINT NOT NULL REFERENCES Product(ProductId),
	ColorId SMALLINT NOT NULL REFERENCES Color(ColorId),
	SizeId TINYINT NOT NULL REFERENCES Size(SizeId),
	Quantity SMALLINT NOT NULL,
	Price INT NOT NULL,
	PRIMARY KEY (CartId, ProductId,ColorId, SizeId)
)
GO
--DROP PROC AddCart;
GO
CREATE PROC AddCart(
	@CartId UNIQUEIDENTIFIER,
	@ProductId SMALLINT,
	@ColorId SMALLINT,
	@SizeId TINYINT,
	@Quantity SMALLINT,
	@Price INT
)
AS
BEGIN
	IF EXISTS (SELECT * FROM Cart WHERE CartId = @CartId AND ProductId = @ProductId AND ColorId = @ColorId AND SizeId = @SizeId)
		UPDATE Cart SET Quantity += @Quantity WHERE CartId = @CartId AND ProductId = @ProductId AND ColorId = @ColorId AND SizeId = @SizeId;
	ELSE
		INSERT INTO Cart(CartId, ProductId,ColorId,SizeId,Quantity, Price) VALUES(@CartId, @ProductId,@ColorId, @SizeId,@Quantity, @Price);
END
GO
--DROP PROC GetCarts;
GO
CREATE PROC GetCarts(@CartId UNIQUEIDENTIFIER)
AS
	SELECT Cart.*,ProductName, ColorCode, SizeCode,Price, PriceSaleOff
		FROM Cart JOIN Product ON Cart.ProductId = Product.ProductId
		JOIN Color ON Cart.ColorId = Color.ColorId
		JOIN Size ON Cart.SizeId = Size.SizeId
		WHERE CartId = @CartId;
GO
--DROP PROC DeleteCart;
GO
CREATE PROC DeleteCart(
	@CartId UNIQUEIDENTIFIER,
	@ProductId SMALLINT,
	@ColorId SMALLINT,
	@SizeId TINYINT
)
AS
	DELETE Cart WHERE CartId = @CartId AND ProductId = @ProductId AND ColorId = @ColorId AND SizeId = @SizeId;
GO


--DROP PROC EditCart;
GO
CREATE PROC EditCart(
	@CartId UNIQUEIDENTIFIER,
	@ProductId SMALLINT,
	@ColorId SMALLINT,
	@SizeId TINYINT,
	@Quantity SMALLINT
)
AS
	UPDATE Cart SET Quantity = @Quantity WHERE CartId = @CartId AND ProductId = @ProductId AND ColorId = @ColorId AND SizeId = @SizeId;
GO

SELECT * FROM Cart;
SELECT * FROM Product;

TRUNCATE TABLE Cart;

SELECT * FROM Cart;