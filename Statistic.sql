--DROP PROC GetBestSellingProducts;
GO
CREATE PROC GetBestSellingProducts
AS
BEGIN
	SELECT TOP (5) Product.ProductId AS Id, Product.ProductName AS Name, SUM(InvoiceDetail.Quantity * InvoiceDetail.Price) AS Total 
		FROM Product JOIN InvoiceDetail ON Product.ProductId = InvoiceDetail.ProductId 
		JOIN Invoice ON InvoiceDetail.InvoiceId = Invoice.InvoiceId 
		WHERE Invoice.StatusId = 4
		GROUP BY Product.ProductId, Product.ProductName ORDER BY Total DESC;
END
GO
--DROP PROC GetBestSellingSize;
GO
CREATE PROC GetBestSellingSize
AS
BEGIN
	SELECT TOP (5) Size.SizeId AS Id, Size.SizeCode AS Name, SUM(InvoiceDetail.Quantity * InvoiceDetail.Price) AS Total
		FROM Size JOIN InvoiceDetail ON Size.SizeId = InvoiceDetail.SizeId 
		JOIN Invoice ON InvoiceDetail.InvoiceId = Invoice.InvoiceId 
		WHERE Invoice.StatusId = 4
		GROUP BY Size.SizeId, Size.SizeCode ORDER BY Total DESC;
END
GO
--DROP PROC GetBestSellingColor;
GO
CREATE PROC GetBestSellingColor
AS
BEGIN
	SELECT TOP (5) Color.ColorId AS Id, Color.ColorCode AS Name, SUM(InvoiceDetail.Quantity * InvoiceDetail.Price) AS Total
		FROM Color JOIN InvoiceDetail ON Color.ColorId = InvoiceDetail.ColorId 
		JOIN Invoice ON InvoiceDetail.InvoiceId = Invoice.InvoiceId 
		WHERE Invoice.StatusId = 4
		GROUP BY Color.ColorId, Color.ColorCode ORDER BY Total DESC;
END
GO

--DROP PROC GetRevenueByMonths;
GO
CREATE PROC GetRevenueByMonths
AS
BEGIN
	SELECT  SUM(InvoiceDetail.Quantity * InvoiceDetail.Price) AS Total, MONTH(DateCreated) AS Id 
		FROM InvoiceDetail JOIN Invoice ON InvoiceDetail.InvoiceId = Invoice.InvoiceId 		
		WHERE Invoice.StatusId = 4
		GROUP BY MONTH(DateCreated) ORDER BY MONTH(DateCreated);
END
GO

CREATE PROC GetHighestInventoryProducts
AS
BEGIN
		SELECT TOP 5 Product.ProductId AS Id, ProductName AS Name, SUM(Quantity) AS Total FROM Product JOIN InventoryStatus ON Product.ProductId  = InventoryStatus.ProductId GROUP BY Product.ProductId, ProductName;
END
GO

