CREATE PROC GetBestSellingProducts
AS
BEGIN
	SELECT TOP (5) Product.ProductId AS Id, Product.ProductName AS Name, SUM(InvoiceDetail.Quantity * InvoiceDetail.Price) AS Total 
		FROM Product JOIN InvoiceDetail ON Product.ProductId = InvoiceDetail.ProductId GROUP BY Product.ProductId, Product.ProductName;
END
GO

CREATE PROC GetBestSellingSize
AS
BEGIN
	SELECT TOP (5) Size.SizeId AS Id, Size.SizeCode AS Name, SUM(InvoiceDetail.Quantity * InvoiceDetail.Price) AS Total
		FROM Size JOIN InvoiceDetail ON Size.SizeId = InvoiceDetail.SizeId GROUP BY Size.SizeId, Size.SizeCode;
END
GO

CREATE PROC GetBestSellingColor
AS
BEGIN
	SELECT TOP (5) Color.ColorId AS Id, Color.ColorCode AS Name, SUM(InvoiceDetail.Quantity * InvoiceDetail.Price) AS Total
		FROM Color JOIN InvoiceDetail ON Color.ColorId = InvoiceDetail.ColorId GROUP BY Color.ColorId, Color.ColorCode;
END
GO
--DROP PROC GetRevenueByMonths;
GO
CREATE PROC GetRevenueByMonths(@Month SMALLINT)
AS
BEGIN
	SELECT  SUM(InvoiceDetail.Quantity * InvoiceDetail.Price) AS Total, MONTH(DateCreated) AS Id 
		FROM InvoiceDetail JOIN Invoice ON InvoiceDetail.InvoiceId = Invoice.InvoiceId 
		GROUP BY MONTH(DateCreated) ORDER BY MONTH(DateCreated);
END
GO

CREATE PROC GetHighestInventoryProducts
AS
BEGIN
		SELECT TOP 5 Product.ProductId AS Id, ProductName AS Name, SUM(Quantity) AS Total FROM Product JOIN InventoryStatus ON Product.ProductId  = InventoryStatus.ProductId GROUP BY Product.ProductId, ProductName;
END
GO
CREATE PROC GetDetailInventoryProduct (@ProductId SMALLINT)
AS
SELECT Product.ProductId AS Id, ProductName AS Name, SizeId AS Size, ColorId AS Color, SUM(Quantity) AS Total FROM Product JOIN InventoryStatus ON Product.ProductId  = InventoryStatus.ProductId AND Product.ProductId = 1 GROUP BY Product.ProductId, ProductName, SizeId, ColorId;
GO
SELECT SUM(Price *  Quantity) FROM InvoiceDetail;