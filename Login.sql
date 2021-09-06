--DROP TABLE Member
GO
CREATE TABLE Member(
	MemberId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,	
	Username VARCHAR(32) NOT NULL,
	Password VARBINARY(64) NOT NULL,
	Email VARCHAR(32) NOT NULL,
	Gender BIT NOT NULL,
	JoinDate DATETIME NOT NULL,
	AddressDefault INT DEFAULT NULL
)
GO

CREATE TABLE Role(
	RoleId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL DEFAULT NEWID(),
	RoleName VARCHAR(64) NOT NULL
)
GO
--DROP TABLE MemberInRole
GO
CREATE TABLE MemberInRole(
	MemberId UNIQUEIDENTIFIER NOT NULL REFERENCES Member(MemberId),
	RoleId UNIQUEIDENTIFIER NOT NULL REFERENCES Role(RoleId),
	IsDeleted BIT NOT NULL DEFAULT 0,
	PRIMARY KEY(MemberId, RoleId)
)
GO

CREATE PROC AddRole(@RoleName VARCHAR(64))
AS
	INSERT INTO Role(RoleName) VALUES(@RoleName);
GO

EXEC AddRole @RoleName = 'Customer';
EXEC AddRole @RoleName = 'Staff';
EXEC AddRole @RoleName = 'Manager';



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
	INSERT INTO MemberInRole (MemberId, RoleId) VALUES(@MemberId, '78ab6af0-3726-46c6-81c3-418744f1bf9a');
END

CREATE PROC Login(
	@Username VARCHAR(32),
	@Password VARBINARY(64)
)
AS
	SELECT MemberId, Username, Email, Gender, JoinDate FROM Member WHERE Username = @Username AND Password = @Password;
GO

SELECT * FROM Member;
SELECT * FROM MemberInRole;
--DROP PROC GetRoleByMember;
GO
CREATE PROC GetRolesNameByMember(@MemberId UNIQUEIDENTIFIER)
AS
	SELECT RoleName FROM MemberInRole JOIN Role ON MemberInRole.RoleId = Role.RoleId WHERE MemberId = @MemberId;
GO
