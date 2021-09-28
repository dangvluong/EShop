USE EShop;
GO
-------------------------STORED PROCEDURE-------------------------
--DROP PROC AddProduct
GO
CREATE PROC AddProduct(	
	@ProductId SMALLINT OUT,
	@ProductName NVARCHAR(64),
	@Sku VARCHAR(32),
	@Price INT,
	@PriceSaleOff INT,
	@Material NVARCHAR(64),
	@Description NVARCHAR(MAX)
)
AS
BEGIN	
	INSERT INTO Product(ProductName, Sku,Price, PriceSaleOff ,Material, Description) VALUES (@ProductName, @Sku ,@Price, @PriceSaleOff,@Material, @Description);		
	SET @ProductId = SCOPE_IDENTITY();
END
GO

CREATE PROC EditProduct(
	@ProductId SMALLINT,
	@ProductName NVARCHAR(64),
	@Sku VARCHAR(32),
	@Price INT,
	@PriceSaleOff INT,
	@Material NVARCHAR(64),
	@Description NVARCHAR(MAX)
)
AS
	UPDATE Product SET ProductName = @ProductName, Sku = @Sku, Price = @Price, PriceSaleOff = @PriceSaleOff, Material = @Material, Description = @Description
		WHERE ProductId = @ProductId; 
GO

--DROP PROC AddColor;
GO
CREATE PROC AddColor(
	@ColorCode VARCHAR(20),
	@IconUrl VARCHAR(32)
)
AS
	INSERT INTO Color(ColorCode, IconUrl) VALUES(@ColorCode, @IconUrl);
GO

--DROP PROC GetColors;
GO
--CREATE PROC GetColors(
--	@Id INT,
--	@Size INT,
--	@Total INT OUT
--)
--AS	
--BEGIN
--	SELECT * FROM Color WHERE IsDeleted = 0 ORDER BY ColorId
--		OFFSET (@Id - 1) * @Size ROWS FETCH NEXT @Size ROWS ONLY;
--	SELECT @Total = COUNT(*) FROM Color WHERE IsDeleted = 0;
--END

--DROP PROC DeleteColor;
GO
CREATE PROC DeleteColor(@Id SMALLINT)
AS
	UPDATE Color SET IsDeleted = 1 WHERE ColorId = @Id;
GO

--DROP PROC AddSize
GO
CREATE PROC AddSize(
	@SizeCode VARCHAR(20)
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

--DROP PROC DeleteCategory;
GO
CREATE PROC DeleteCategory(@CategoryId SMALLINT)
AS
BEGIN
	DELETE FROM ProductInCategory WHERE CategoryId = @CategoryId;
	DELETE FROM Category WHERE CategoryId = @CategoryId;
END
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
	@SizeId SMALLINT
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

CREATE PROC AddImageOfProduct(
	@ProductId SMALLINT,
	@ColorId SMALLINT,
	@ImageUrl VARCHAR(100)
)
AS
	INSERT INTO ImageOfProduct(ProductId, ColorId, ImageUrl) VALUES(@ProductId, @ColorId, @ImageUrl);
GO

CREATE PROC GetImagesByProduct(
	@ProductId SMALLINT
)
AS
	SELECT * FROM ImageOfProduct WHERE ProductId = @ProductId;
GO

CREATE PROC GetProductById(@Id SMALLINT)
AS
	SELECT * FROM Product WHERE ProductId = @Id AND IsDeleted = 0;
GO
--DROP PROC GetColorByProduct;
GO
CREATE PROC GetColorsByProduct(@ProductId SMALLINT)
AS 
	SELECT * FROM ColorOfProduct JOIN Color ON ColorOfProduct.ColorId = Color.ColorId 
		WHERE ColorOfProduct.ProductId = @ProductId AND Color.IsDeleted = 0;
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

--DROP PROC GetProducts
GO

CREATE PROC GetProducts(
	@Page  INT,
	@Size INT,
	@Total INT OUT
)
AS
BEGIN
	SELECT * FROM Product WHERE IsDeleted = 0 ORDER BY ProductId
		OFFSET (@Page -1) * @Size ROWS FETCH NEXT @Size ROWS ONLY;
	SELECT @Total = COUNT(*) FROM Product WHERE IsDeleted = 0;	

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
	WHERE CategoryId = @CategoryId AND Product.IsDeleted = 0 ORDER BY Product.ProductId
	OFFSET (@Page-1) * @Size ROWS FETCH NEXT @Size ROWS ONLY;
	SELECT  @Total = COUNT(*) FROM Product JOIN ProductInCategory ON Product.ProductId = ProductInCategory.ProductId
	WHERE CategoryId = @CategoryId AND Product.IsDeleted = 0;

END

--DROP PROC GetProductsRelation;
GO
CREATE PROC GetProductsRelation(@ProductId SMALLINT)
AS
BEGIN
	SELECT DISTINCT TOP 12 Product.* FROM Product JOIN ProductInCategory
		ON Product.ProductId = ProductInCategory.ProductId
		JOIN (SELECT CategoryId FROM Product JOIN ProductInCategory ON Product.ProductId = ProductInCategory.ProductId WHERE Product.ProductId = @ProductId) AS Temp
		ON ProductInCategory.CategoryId = Temp.CategoryId
		WHERE Product.ProductId <> @ProductId AND Product.IsDeleted = 0;
END

--DROP PROC SearchProduct
GO
CREATE PROC SearchProduct(
	@Query NVARCHAR(100),
	@Page  INT,
	@Size INT,
	@Total INT OUT
)
AS
BEGIN	
	SELECT * FROM Product WHERE ProductName LIKE @Query AND Product.IsDeleted = 0 ORDER BY Product.ProductId
	OFFSET (@Page-1) * @Size ROWS FETCH NEXT @Size ROWS ONLY;
	SELECT  @Total = COUNT(*) FROM Product WHERE ProductName LIKE @Query AND Product.IsDeleted = 0;
END
--DROP PROC GetSizes;
GO
CREATE PROC GetSizes
AS
	SELECT * FROM Size WHERE IsDeleted = 0;
GO
--DROP PROC GetSizesByProduct;
GO
CREATE PROC GetSizesByProduct(@ProductId SMALLINT)
AS
	SELECT Size.* FROM SizeOfProduct JOIN Size ON SizeOfProduct.SizeId = Size.SizeId WHERE SizeOfProduct.ProductId = @ProductId AND Size.IsDeleted = 0;
GO

CREATE PROC GetInventoryQuantitiesByProduct(@ProductId SMALLINT)
AS
	SELECT * FROM InventoryQuantity WHERE InventoryQuantity.ProductId = @ProductId;
GO
--DROP PROC UpdateInventoryQuantity;
GO
CREATE PROC UpdateInventoryQuantityFromInvoice(@ProductId SMALLINT, @ColorId SMALLINT, @SizeId TINYINT, @Quantity SMALLINT)
AS
	UPDATE InventoryQuantity SET Quantity -= @Quantity WHERE ProductId = @ProductId AND ColorId = @ColorId AND SizeId = @SizeId;
GO
--DROP PROC UpdateInventoryQuantity;
GO
CREATE PROC UpdateInventoryQuantity(@ProductId SMALLINT, @ColorId SMALLINT, @SizeId TINYINT, @Quantity SMALLINT)
AS
	IF EXISTS(SELECT * FROM InventoryQuantity WHERE ProductId = @ProductId AND ColorId = @ColorId AND SizeId = @SizeId)
		UPDATE InventoryQuantity SET Quantity = @Quantity WHERE ProductId = @ProductId AND ColorId = @ColorId AND SizeId = @SizeId;
	ELSE
		INSERT INTO InventoryQuantity(ProductId, ColorId, SizeId, Quantity) VALUES(@ProductId, @ColorId, @SizeId, @Quantity);
GO
--DROP PROC GetGuides;
GO
CREATE PROC GetGuides
AS
	SELECT * FROM Guide WHERE IsDeleted = 0;
GO
--DROP PROC GetGuidesByProduct;
GO
CREATE PROC GetGuidesByProduct(@ProductId SMALLINT)
AS
	SELECT Guide.* FROM Guide JOIN GuideOfProduct ON Guide.GuideId = GuideOfProduct.GuideId WHERE GuideOfProduct.ProductId = @ProductId AND Guide.IsDeleted = 0;
GO

CREATE PROC GetRandom12Products
AS
	SELECT TOP(12) * FROM Product WHERE IsDeleted = 0 ORDER BY NEWID();
GO
----------------------------------------------------
CREATE PROC GetProvinces
AS
	SELECT * FROM Province;
GO

CREATE PROC GetProvinceById(@ProvinceId SMALLINT)
AS
	SELECT * FROM Province WHERE ProvinceId = @ProvinceId;
GO

CREATE PROC GetDistrictsByProvince(@ProvinceId SMALLINT)
AS 
	SELECT * FROM District WHERE ProvinceId =  @ProvinceId;
GO

CREATE PROC GetDistrictById(@DistrictId SMALLINT)
AS 
	SELECT * FROM District WHERE DistrictId = @DistrictId;
GO

CREATE PROC GetWardsByDistrict(@DistrictId SMALLINT)
AS
	SELECT * FROM Ward WHERE DistrictId = @DistrictId;
GO

CREATE PROC GetWardById(@WardId SMALLINT)
AS
	SELECT * FROM Ward WHERE WardId = @WardId;
GO

-------------------------------------------------------
--DROP PROC AddCart;
GO
CREATE PROC AddCart(
	@CartId UNIQUEIDENTIFIER,
	@ProductId SMALLINT,
	@ColorId SMALLINT,
	@SizeId SMALLINT,
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
	SELECT Cart.*,ProductName, ColorCode, SizeCode
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
-------------------------------------------------
--DROP PROC AddContact
GO
CREATE PROC AddContact(
	@AddressHome NVARCHAR(100),
	@ProvinceId SMALLINT,
	@DistrictId SMALLINT,
	@WardId SMALLINT,
	@PhoneNumber VARCHAR(15),
	@FullName NVARCHAR(32),
	@MemberId UNIQUEIDENTIFIER
)
AS
BEGIN
	INSERT INTO Contact(AddressHome, ProvinceId, DistrictId, WardId, PhoneNumber, FullName) 
		VALUES(@AddressHome, @ProvinceId, @DistrictId, @WardId, @PhoneNumber, @FullName);
	DECLARE @ContactId SMALLINT = SCOPE_IDENTITY();
	INSERT INTO ContactOfMember(ContactId, MemberId) VALUES(@ContactId, @MemberId);
	IF((SELECT COUNT(*) FROM ContactOfMember WHERE MemberId = @MemberId) = 1)
		UPDATE Member SET DefaultContact = @ContactId WHERE MemberId = @MemberId;
END
GO
--CREATE PROC GetContacts
--AS
--	SELECT * FROM Contact WHERE IsDeleted = 0;
--GO
--DROP PROC GetContactsByMember;
GO
CREATE PROC GetContactsByMember(@MemberId UNIQUEIDENTIFIER)
AS
	SELECT * FROM Contact 
		JOIN ContactOfMember ON Contact.ContactId = ContactOfMember.ContactId 
		JOIN Province ON Contact.ProvinceId = Province.ProvinceId 
		JOIN District ON Contact.DistrictId = District.DistrictId
		JOIN Ward ON Contact.WardId = Ward.WardId	
		WHERE MemberId = @MemberId AND Contact.IsDeleted = 0;
GO
--DROP PROC GetContactById;
GO
CREATE PROC GetContactById(@ContactId SMALLINT)
AS
	SELECT * FROM Contact 	
		JOIN Province ON Contact.ProvinceId = Province.ProvinceId 
		JOIN District ON Contact.DistrictId = District.DistrictId
		JOIN Ward ON Contact.WardId = Ward.WardId	
		WHERE ContactId = @ContactId;
GO

CREATE PROC UpdateContact(
	@AddressHome NVARCHAR(100),
	@ProvinceId SMALLINT,
	@DistrictId SMALLINT,
	@WardId SMALLINT,
	@PhoneNumber VARCHAR(15),
	@FullName NVARCHAR(32),
	@ContactId SMALLINT
)
AS	
	UPDATE Contact SET AddressHome = @AddressHome, ProvinceId = @ProvinceId, 
	DistrictId =@DistrictId, WardId = @WardId, PhoneNumber = @PhoneNumber,
	FullName = @FullName WHERE ContactId = @ContactId;
GO
--DROP PROC DeleteContact;
GO
CREATE PROC DeleteContact(@ContactId SMALLINT)
AS
BEGIN
	DECLARE @MemberId UNIQUEIDENTIFIER, @DefaultContact SMALLINT;
	SELECT @MemberId = MemberId FROM ContactOfMember WHERE ContactId = @ContactId;		
	SELECT @DefaultContact = DefaultContact FROM Member WHERE MemberId = @MemberId;
	IF @DefaultContact = @ContactId AND (SELECT COUNT(*) FROM ContactOfMember WHERE MemberId = @MemberId) >= 2
		UPDATE Member SET DefaultContact = 
			(SELECT TOP 1(ContactId) FROM ContactOfMember WHERE MemberId = @MemberId AND ContactId <> @ContactId);
	ELSE IF (SELECT COUNT(*) FROM ContactOfMember WHERE MemberId = @MemberId) = 1
		UPDATE Member SET DefaultContact = NULL WHERE MemberId = @MemberId;
	DELETE ContactOfMember WHERE ContactId = @ContactId;
	UPDATE Contact SET IsDeleted = 1 WHERE ContactId = @ContactId;
	
END
GO

CREATE PROC UpdateDefaultContact(
	@MemberId UNIQUEIDENTIFIER, 
	@ContactId SMALLINT
)
AS
	UPDATE Member SET DefaultContact = @ContactId WHERE MemberId = @MemberId;
GO
-----------------------------------------------
--DROP PROC AddInvoice;
GO
CREATE PROC AddInvoice(
	@InvoiceId UNIQUEIDENTIFIER,
	@MemberId UNIQUEIDENTIFIER,
	@ContactId SMALLINT,
	@StatusId TINYINT,
	@DateCreated DATETIME,
	@CartId UNIQUEIDENTIFIER,
	@ShipCost INT
)	
AS
BEGIN
		INSERT INTO Invoice(InvoiceId, MemberId, ContactId, StatusId,DateCreated, ShipCost) VALUES(@InvoiceId, @MemberId, @ContactId, @StatusId,@DateCreated, @ShipCost);
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

--DROP PROC UpdateInvoiceStatus;
GO
CREATE PROC UpdateInvoiceStatus(
	@InvoiceId UNIQUEIDENTIFIER,
	@StatusId TINYINT
)
AS
	UPDATE Invoice SET StatusId = @StatusId WHERE InvoiceId = @InvoiceId;		
GO

CREATE PROC GetInvoicesByMember(@MemberId UNIQUEIDENTIFIER)
AS
	SELECT * FROM Invoice WHERE MemberId = @MemberId;
GO

-----------------------------------------------
CREATE PROC AddMember(
	@Username VARCHAR(32),
	@Password VARBINARY(64),
	@Email VARCHAR(32),
	@Gender BIT,
	@JoinDate DATETIME
)
AS
BEGIN
	DECLARE @MemberId UNIQUEIDENTIFIER = NEWID();
	INSERT INTO Member(MemberId,Username, Password, Email, Gender, JoinDate) VALUES (@MemberId, @Username, @Password, @Email, @Gender, @Joindate);	
	INSERT INTO MemberInRole (MemberId, RoleId) VALUES(@MemberId, 1);
END

--DROP PROC AddMemberInRole
GO
CREATE PROC AddMemberInRole(
	@MemberId UNIQUEIDENTIFIER,
	@RoleId TINYINT
)
AS
BEGIN
	IF EXISTS(SELECT * FROM MemberInRole WHERE MemberId = @MemberId AND RoleId = @RoleId)
		UPDATE MemberInRole SET IsDeleted = ~IsDeleted WHERE MemberId = @MemberId AND RoleId = @RoleId;
	ELSE
		INSERT INTO MemberInRole(MemberId, RoleId) VALUES (@MemberId, @RoleId);
END
GO


--DROP PROC Login;
GO
CREATE PROC Login(
	@Username VARCHAR(32),
	@Password VARBINARY(64)
)
AS
	SELECT MemberId, Username, Email, Gender, JoinDate, IsBanned FROM Member WHERE Username = @Username AND Password = @Password;
GO

CREATE PROC UpdatePassword(@MemberId UNIQUEIDENTIFIER, @Password VARBINARY(64))
AS
	UPDATE Member SET Password = @Password WHERE MemberId = @MemberId;
GO

CREATE PROC GetRolesByMember(@MemberId UNIQUEIDENTIFIER)
AS
	SELECT Role.*, IIF(MemberInRole.MemberId IS NULL,0,1) AS Checked 
		FROM MemberInRole RIGHT JOIN Role ON MemberInRole.RoleId = Role.RoleId AND MemberId = @MemberId AND IsDeleted = 0;
GO

--DROP PROC GetMemberById
GO
CREATE PROC GetMemberById(@MemberId UNIQUEIDENTIFIER)
AS
	SELECT MemberId,Username, DefaultContact, Email, Gender, JoinDate,IsBanned  FROM Member WHERE MemberId = @MemberId;
GO


CREATE PROC UpdateAccountStatus(@MemberId UNIQUEIDENTIFIER)
AS
	UPDATE Member SET IsBanned = ~IsBanned WHERE MemberId =@MemberId;
GO
--DROP PROC ResetPassword;
GO
CREATE PROC ResetPassword(
	@Token VARCHAR(32),
	@NewPassword VARBINARY(64)
)
AS
BEGIN	
	UPDATE Member SET Password = @NewPassword, Token = NULL WHERE Token = @Token;	
END
-------------------------------------------------------
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
CREATE PROC GetBestSellingSizes
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
CREATE PROC GetBestSellingColors
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
		SELECT TOP 5 Product.ProductId AS Id, ProductName AS Name, SUM(Quantity) AS Total FROM Product JOIN InventoryQuantity ON Product.ProductId  = InventoryQuantity.ProductId
		GROUP BY Product.ProductId, ProductName;
END
GO
