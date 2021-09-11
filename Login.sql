--DROP TABLE Member
GO
CREATE TABLE Member(
	MemberId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,	
	Username VARCHAR(32) UNIQUE NOT NULL,
	Password VARBINARY(64) NOT NULL,
	Email VARCHAR(32) NOT NULL,
	Gender BIT NOT NULL,
	JoinDate DATETIME NOT NULL,
	DefaultContact INT DEFAULT NULL,
	IsBanned BIT DEFAULT 0
)
GO

--DROP TABLE Role;
GO
CREATE TABLE Role(
	RoleId TINYINT PRIMARY KEY NOT NULL,
	RoleName VARCHAR(64) NOT NULL
)
GO
--DROP TABLE MemberInRole
GO
CREATE TABLE MemberInRole(
	MemberId UNIQUEIDENTIFIER NOT NULL REFERENCES Member(MemberId),
	RoleId TINYINT NOT NULL REFERENCES Role(RoleId),
	IsDeleted BIT NOT NULL DEFAULT 0,
	PRIMARY KEY(MemberId, RoleId)
)
GO
--DROP PROC AddRole;
GO
CREATE PROC AddRole(@RoleId TINYINT,@RoleName VARCHAR(64))
AS
	INSERT INTO Role(RoleId,RoleName) VALUES(@RoleId,@RoleName);
GO

EXEC AddRole @RoleId=1, @RoleName = 'Customer';
EXEC AddRole @RoleId=2, @RoleName = 'Staff';
EXEC AddRole @RoleId=3, @RoleName = 'Manager';


--DROP PROC AddMember
GO
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

SELECT * FROM Member;
SELECT * FROM MemberInRole;
--DROP PROC GetRolesByMember;
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


EXEC GetRolesByMember @MemberId = '51322F65-FC2B-4408-9CE1-5107792EE703';
GO

CREATE PROC UpdateAccountStatus(@MemberId UNIQUEIDENTIFIER)
AS
	UPDATE Member SET IsBanned = ~IsBanned WHERE MemberId =@MemberId;
GO

SELECT * FROM Member;
SELECT * FROM District;


--DROP TABLE ContactOfMember;
GO
--DROP TABLE MemberInRole;
GO
--DROP TABLE Invoice;
GO
--DROP TABLE InvoiceDetail;
GO
