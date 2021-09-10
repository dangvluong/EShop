--DROP TABLE Invoice;
GO

CREATE TABLE Invoice(
	InvoiceId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	MemberId UNIQUEIDENTIFIER NOT NULL REFERENCES Member(MemberId),
	ContactId SMALLINT NOT NULL REFERENCES Contact(ContactId),
	StatusId TINYINT NOT NULL
)
GO
--DROP TABLE InvoiceDetail;
GO
CREATE TABLE InvoiceDetail(
	InvoiceId UNIQUEIDENTIFIER NOT NULL REFERENCES Invoice(InvoiceId),
	ProductId SMALLINT NOT NULL REFERENCES Product(ProductId),
	ColorId SMALLINT NOT NULL REFERENCES Color(ColorId),
	SizeId TINYINT NOT NULL REFERENCES Size(SizeId),
	Quantity SMALLINT NOT NULL,
	Price INT NOT NULL,
	PRIMARY KEY(InvoiceId, ProductId)
)
GO

--DROP PROC AddInvoice;
GO
CREATE PROC AddInvoice(
	@InvoiceId UNIQUEIDENTIFIER,
	@MemberId UNIQUEIDENTIFIER,
	@ContactId SMALLINT,
	@StatusId TINYINT,
	@CartId UNIQUEIDENTIFIER
)	
AS
BEGIN
		INSERT INTO Invoice(InvoiceId, MemberId, ContactId, StatusId) VALUES(@InvoiceId, @MemberId, @ContactId, @StatusId);
		INSERT INTO InvoiceDetail(InvoiceId, ProductId,ColorId, SizeId, Quantity, Price)
			SELECT @InvoiceId, Cart.ProductId,ColorId, SizeId, Quantity, Cart.Price 
				FROM Cart JOIN Product ON Cart.ProductId = Product.ProductId WHERE CartId = @CartId;
		DELETE Cart WHERE CartId =@CartId;
END
GO

CREATE PROC GetInvoiceById(@InvoiceId UNIQUEIDENTIFIER)
AS
	SELECT * FROM Invoice WHERE InvoiceId = @InvoiceId;
GO
--DROP PROC GetInvoiceDetailByInvoiceId;
GO
CREATE PROC GetInvoiceDetailByInvoiceId(@InvoiceId UNIQUEIDENTIFIER)
AS 
	SELECT InvoiceDetail.*, ProductName, ColorCode, SizeCode FROM InvoiceDetail 
		JOIN Product ON InvoiceDetail.ProductId = Product.ProductId
		JOIN Color ON InvoiceDetail.ColorId= Color.ColorId
		JOIN Size ON InvoiceDetail.SizeId = Size.SizeId		
		WHERE InvoiceId = @InvoiceId;
GO

SELECT * FROM Invoice;
SELECT * FROM InvoiceDetail;
SELECT * FROM Contact;