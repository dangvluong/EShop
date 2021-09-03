--DROP DATABASE EzShop;
GO
CREATE DATABASE EzShop;
GO
USE EzShop;
GO
--TABLES ENTITY
--DROP TABLE Product;
CREATE TABLE Product(
	ProductId SMALLINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	ProductName NVARCHAR(64) NOT NULL,
	Sku VARCHAR(20) NOT NULL,
	Price INT NOT NULL,
	PriceSaleOff INT,
	Material NVARCHAR(64),
	Description NVARCHAR(MAX)
)
GO
--DROP TABLE Color;
GO
CREATE TABLE Color(
	ColorId SMALLINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	ColorCode VARCHAR(16) NOT NULL,
	IconUrl VARCHAR(32)
)
GO
--DROP TABLE Size
GO
CREATE TABLE Size(
	SizeId TINYINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	SizeCode VARCHAR(5) NOT NULL
)
GO

CREATE TABLE Guide(
	GuideId SMALLINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	GuideDescription NVARCHAR(100) NOT NULL
)
GO

CREATE TABLE Category(
	CategoryId SMALLINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	CategoryName NVARCHAR(64) NOT NULL
)
GO
---TALBES RELATIONSHIP --
CREATE TABLE ProductInCategory(
	ProductId SMALLINT NOT NULL,
	CategoryId SMALLINT NOT NULL,
	PRIMARY KEY(ProductId, CategoryId)
)
GO

CREATE TABLE ColorOfProduct(
	ProductId SMALLINT NOT NULL,
	ColorId SMALLINT NOT NULL,
	PRIMARY KEY (ProductId, ColorId)
)
GO

CREATE TABLE SizeOfProduct(
	ProductId SMALLINT NOT NULL,
	SizeId TINYINT NOT NULL,
	PRIMARY KEY (ProductId, SizeId)
)
GO

CREATE TABLE GuideOfProduct(
	ProductId SMALLINT NOT NULL,
	GuideId SMALLINT NOT NULL,
	PRIMARY KEY (ProductId, GuideID)
)
GO
--DROP TABLE ProductImage
GO
CREATE TABLE ProductImage(
	ProductId SMALLINT NOT NULL,
	ColorId SMALLINT NOT NULL,
	ImageUrl VARCHAR(100),
	PRIMARY KEY (ProductId, ColorId,ImageUrl)
)
GO

CREATE TABLE InventoryStatus(
	ProductId SMALLINT NOT NULL,
	SizeId TINYINT NOT NULL,
	ColorId SMALLINT NOT NULL,
	Amount SMALLINT NOT NULL,
	PRIMARY KEY (ProductId, ColorId, SizeId)
)
GO

--STORES PROCEDURES
--DROP PROC AddProduct
GO
CREATE PROC AddProduct(	
	@ProductId SMALLINT OUT,
	@ProductName NVARCHAR(64),
	@Sku VARCHAR(20),
	@Price INT,
	@PriceSaleOff INT = NULL,
	@Material NVARCHAR(64) = NULL,
	@Description NVARCHAR(MAX) = NULL
)
AS
BEGIN	
	INSERT INTO Product(ProductName, Sku,Price, PriceSaleOff ,Material, Description) VALUES (@ProductName, @Sku ,@Price, @PriceSaleOff,@Material, @Description);		
	SET @ProductId = SCOPE_IDENTITY();
END
GO
--DROP PROC AddColor;
GO
CREATE PROC AddColor(
	@ColorCode VARCHAR(16),
	@IconUrl VARCHAR(32) = NULL
)
AS
	INSERT INTO Color(ColorCode, IconUrl) VALUES(@ColorCode, @IconUrl);
GO

--DROP PROC AddSize
GO
CREATE PROC AddSize(
	@SizeCode VARCHAR(5)
)
AS
	INSERT INTO Size(SizeCode) VALUES (@SizeCode);
GO

CREATE PROC AddGuide(
	@GuideDescription NVARCHAR(100)
)
AS 
	INSERT INTO Guide(GuideDescription) VALUES (@GuideDescription);
GO

CREATE PROC AddCategory(
	@CategoryName NVARCHAR(64) 
)
AS 
	INSERT INTO Category(CategoryName) VALUES(@CategoryName);
GO

CREATE PROC AddProductInCategory(
	@ProductId SMALLINT,
	@CategoryId SMALLINT
)
AS
	INSERT INTO ProductInCategory(ProductId, CategoryId) VALUES (@ProductId, @CategoryId);
GO

CREATE PROC AddColorOfProduct(
	@ProductId SMALLINT,
	@ColorId SMALLINT
)
AS
	INSERT INTO ColorOfProduct(ProductId, ColorId) VALUES(@ProductId, @ColorId);
GO

CREATE PROC AddSizeOfProduct(
	@ProductId SMALLINT,
	@SizeId TINYINT
)
AS
	INSERT INTO SizeOfProduct(ProductId, SizeId) VALUES(@ProductId, @SizeId);
GO

CREATE PROC AddGuideOfProduct(
	@ProductId SMALLINT,
	@GuideId SMALLINT
)
AS
	INSERT INTO GuideOfProduct(ProductId, GuideId) VALUES (@ProductId, @GuideId);
GO

CREATE PROC AddProductImage(
	@ProductId SMALLINT,
	@ColorId SMALLINT,
	@ImageUrl VARCHAR(100)
)
AS
	INSERT INTO ProductImage(ProductId, ColorId, ImageUrl) VALUES(@ProductId, @ColorId, @ImageUrl);
GO

CREATE PROC AddInventoryStatus(
	@ProductId SMALLINT,
	@SizeId TINYINT,
	@ColorId SMALLINT,
	@Amount SMALLINT	
)
AS
	INSERT INTO InventoryStatus(ProductId, SizeId, ColorId, Amount) VALUES(@ProductId, @SizeId, @ColorId, @Amount);
GO

CREATE PROC GetProductColorAndSizeId
AS
	SELECT Product.ProductId, ColorId, SizeId FROM Product JOIN ColorOfProduct ON Product.ProductId = ColorOfProduct.ProductId 
		JOIN SizeOfProduct ON Product.ProductId = SizeOfProduct.ProductId;
GO
--DROP PROC UpdateAmountInventoryStatus;
GO
--CREATE PROC AddInventoryStatus(
--	@ProductId UNIQUEIDENTIFIER,
--	@ColorId SMALLINT,
--	@SizeId TINYINT,
--	@Amount SMALLINT
--)
--AS
--	INSERT INTO InventoryStatus(ProductId, ColorId, SizeId, Amount) VALUES  (@ProductId, @ColorId, @SizeId, @Amount);
--GO

--DROP PROC ClearData
GO


CREATE PROC GetImagesByProduct(
	@ProductId SMALLINT
)
AS
	SELECT * FROM ProductImage WHERE ProductId = @ProductId;
GO

CREATE PROC GetProductById(@Id SMALLINT)
AS
	SELECT * FROM Product WHERE ProductId = @Id;
GO

CREATE PROC GetColorByProduct(@ProductId SMALLINT)
AS 
	SELECT * FROM ColorOfProduct JOIN Color ON ColorOfProduct.ColorId = Color.ColorId 
		WHERE ColorOfProduct.ProductId = @ProductId;
GO

CREATE PROC GetCategories
AS
	SELECT * FROM Category;
GO

--DROP PROC GetCategoriesByProduct;
GO
CREATE PROC GetCategoriesByProduct(@ProductId SMALLINT)
AS
	SELECT Category.* FROM Category JOIN ProductInCategory ON Category.CategoryId = ProductInCategory.CategoryId  WHERE ProductInCategory.ProductId = @ProductId;
GO

SELECT * FROM ProductInCategory;

--DROP PROC GetProducts
GO

CREATE PROC GetProducts(
	@Page  INT,
	@Size INT,
	@Total INT OUT
)
AS
BEGIN
	SELECT * FROM Product ORDER BY ProductId
		OFFSET (@Page -1) * @Size ROWS FETCH NEXT @Size ROWS ONLY;
	SELECT @Total = COUNT(*) FROM Product;	

END
--DROP PROC GetProductsByCategory;
GO
CREATE PROC GetProductsByCategory(
	@CategoryId SMALLINT,
	@Page  INT,
	@Size INT,
	@Total INT OUT
)
AS
BEGIN	
	SELECT * FROM Product JOIN ProductInCategory ON Product.ProductId = ProductInCategory.ProductId
	WHERE CategoryId = @CategoryId ORDER BY Product.ProductId
	OFFSET (@Page-1) * @Size ROWS FETCH NEXT @Size ROWS ONLY;
	SELECT  @Total = COUNT(*) FROM Product JOIN ProductInCategory ON Product.ProductId = ProductInCategory.ProductId
	WHERE CategoryId = @CategoryId ;

END
CREATE PROC ClearData
AS
BEGIN	
	TRUNCATE TABLE ColorOfProduct;	
	TRUNCATE TABLE GuideOfProduct;
	TRUNCATE TABLE InventoryStatus;
	TRUNCATE TABLE ProductImage;
	TRUNCATE TABLE ProductInCategory;	
	TRUNCATE TABLE SizeOfProduct;		
	TRUNCATE TABLE Color;
	TRUNCATE TABLE Guide;
	TRUNCATE TABLE Product;
	TRUNCATE TABLE Size;
	TRUNCATE TABLE Category;
END





SELECT * FROM Category;
SELECT * FROM Color;
SELECT * FROM ColorOfProduct;
SELECT * FROM Guide;
SELECT * FROM GuideOfProduct;
SELECT * FROM InventoryStatus;
SELECT * FROM Product;
SELECT * FROM ProductImage;
SELECT * FROM ProductInCategory;
SELECT * FROM Size;
SELECT * FROM SizeOfProduct;













CREATE PROC DeleteDat(@Id SMALLINT)
AS
BEGIN
	DELETE FROM Product WHERE ProductId = @Id;
	DELETE FROM ProductImage WHERE ProductId = @Id;
	DELETE FROM SizeOfProduct WHERE ProductId = @Id;
	DELETE FROM ColorOfProduct WHERE ProductId = @Id;
	DELETE FROM ProductInCategory WHERE ProductId = @Id;
	DELETE FROM InventoryStatus WHERE ProductId = @Id;
END
GO



EXEC DeleteDat @Id = 276;
