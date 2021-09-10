CREATE TABLE Contact(
	ContactId SMALLINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	AddressHome NVARCHAR(100) NOT NULL,
	ProvinceId SMALLINT NOT NULL REFERENCES Province(ProvinceId),
	DistrictId SMALLINT NOT NULL REFERENCES District(DistrictId),
	WardId SMALLINT NOT NULL REFERENCES Ward(WardId),
	PhoneNumber VARCHAR(15) NOT NULL,
	FullName NVARCHAR(32)
)
GO
--DROP TABLE ContactOfMember;
GO
CREATE TABLE ContactOfMember(
	ContactId SMALLINT NOT NULL REFERENCES Contact(ContactId),
	MemberId UNIQUEIDENTIFIER NOT NULL REFERENCES Member(MemberId),
	PRIMARY KEY(ContactId, MemberId)
)
GO

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

CREATE PROC GetContacts
AS
	SELECT * FROM Contact;
GO
--DROP PROC GetContactsByMember;
GO
CREATE PROC GetContactsByMember(@MemberId UNIQUEIDENTIFIER)
AS
	SELECT * FROM Contact 
	JOIN ContactOfMember ON Contact.ContactId = ContactOfMember.ContactId 
	JOIN Province ON Contact.ProvinceId = Province.ProvinceId 
	JOIN District ON Contact.DistrictId = District.DistrictId
	JOIN Ward ON Contact.WardId = Ward.WardId	
	WHERE MemberId = @MemberId;
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

CREATE PROC DeleteContact(@ContactId SMALLINT)
AS
BEGIN
	DELETE FROM ContactOfMember WHERE ContactId = @ContactId;
	DELETE FROM Contact WHERE ContactId = @ContactId;
END


CREATE PROC UpdateDefaultContact(
	@MemberId UNIQUEIDENTIFIER, 
	@ContactId SMALLINT
)
AS
	UPDATE Member SET DefaultContact = @ContactId WHERE MemberId = @MemberId;
GO

SELECT * FROM Contact;
SELECT * FROM ContactOfMember;

TRUNCATE TABLE Contact;
TRUNCATE TABLE ContactOfMember;