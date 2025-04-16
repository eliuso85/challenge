CREATE TABLE [dbo].[Usuarios]
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) UNIQUE NOT NULL,
    ContrasenaHash NVARCHAR(255) NOT NULL,  -- Almacenará el hash de la contraseña
   	Role varchar(50),
	FechaCreacion DATETIME DEFAULT GETDATE(),
	Activo BIT DEFAULT 1
);

select * from dbo.[Usuarios]
go 
 delete from dbo.[Usuarios]
 go

alter PROCEDURE dbo.sp_SaveUser
    @Nombre NVARCHAR(100),
    @Correo NVARCHAR(100),
    @ContrasenaHash NVARCHAR(255),
	@Role NVARCHAR(255)
AS
BEGIN
 SET NOCOUNT ON;
 DECLARE @Id INT;

    INSERT INTO Usuarios (Nombre, Correo, ContrasenaHash,Role)
    VALUES (@Nombre, @Correo, @ContrasenaHash,@role);

	SET @Id = SCOPE_IDENTITY();

	SELECT Id AS id,Nombre,Correo AS email,Role,FechaCreacion,Activo FROM dbo.Usuarios WHERE Id = @Id;

END
go 

Alter PROCEDURE dbo.sp_GetUserByEmail
    @Correo NVARCHAR(100)
AS
BEGIN
 SET NOCOUNT ON;

	SELECT Id,
		   ContrasenaHash as [Password],
		   Correo AS Email,
		   Role
	FROM dbo.Usuarios WHERE Correo = @Correo;

END