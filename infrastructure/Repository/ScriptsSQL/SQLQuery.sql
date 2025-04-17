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

CREATE TABLE dbo.SesionesActivas (
    id INT PRIMARY KEY,
    Token NVARCHAR(MAX),
    FechaExpiracion DATETIME
)


select * from dbo.SesionesActivas
GO 


select GETDATE()

CREATE PROCEDURE dbo.sp_GetSesionesActivas
@id INT
AS
BEGIN
 SET NOCOUNT ON;

	SELECT id,
		   Token,
		   FechaExpiracion 
    FROM dbo.SesionesActivas
	WHERE id = @id AND FechaExpiracion > select GETDATE()

END;

CREATE PROCEDURE dbo.sp_PostSesionesActivas
@id INT,
@Token NVARCHAR(MAX),
@FechaExpiracion DATETIME
AS
BEGIN
 SET NOCOUNT ON;

	INSERT INTO dbo.SesionesActivas (id, Token, FechaExpiracion)
    VALUES (@id, @Token, @FechaExpiracion);

END;

CREATE TABLE dbo.Sesiones (
    Id UNIQUEIDENTIFIER PRIMARY KEY,             -- Identificador único de la sesión
    UserId Int NOT NULL, 	-- Identificador del usuario que inició la sesión
	RecursoId INT NOT NULL,  -- Id del recurso 
    StartTime DATETIME NOT NULL DEFAULT GETDATE(),  -- Fecha y hora de inicio de la sesión
    EndTime DATETIME NULL,                       -- Fecha y hora de finalización de la sesión (si se aplica)
    CantidadRequerida INT NOT NULL,              -- Cantidad de recurso que está utilizando esta sesión
    Estado NVARCHAR(50) NOT NULL DEFAULT 'activa', -- Estado de la sesión: 'activa', 'finalizada', etc.
    CONSTRAINT FK_User_Sesiones FOREIGN KEY (UserId) REFERENCES dbo.Usuarios(Id) -- Relación con la tabla de usuarios
);
go;

ALTER PROCEDURE dbo.sp_IniciarSesion
    @UserId INT,
	@RecursoId INT,
    @CantidadRequerida INT
AS
BEGIN
    -- Insertar una nueva sesión
    INSERT INTO dbo.Sesiones (Id, UserId,RecursoId,CantidadRequerida)
    VALUES (NEWID(), @UserId,@RecursoId,  @CantidadRequerida);
END;
Go;

alter PROCEDURE dbo.sp_GetCapacidadOcupada
	@RecursoId INT
AS
BEGIN
    -- Seleccionar la suma de la capacidad ocupada por todas las sesiones activas
   SELECT COALESCE(SUM(CantidadRequerida), 0) AS [CantidadRequerida]
    FROM dbo.Sesiones
    WHERE Estado = 'activa' AND RecursoId = @RecursoId;
END;

select * from dbo.Sesiones


alter PROCEDURE dbo.sp_FinalizarSesion
    @SessionId UNIQUEIDENTIFIER,
	@RecursoId INT
AS
BEGIN
    -- Actualizar el estado de la sesión y poner la fecha de finalización
    UPDATE dbo.Sesiones
    SET Estado = 'finalizada',
        EndTime = GETUTCDATE()
    WHERE Id = @SessionId AND RecursoId = @RecursoId
END;


CREATE PROCEDURE dbo.sp_ObtenerSesionesActivas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        Id, 
        UserId, 
		RecursoId,
        StartTime, 
        CantidadRequerida, 
        Estado
    FROM dbo.Sesiones
    WHERE Estado = 'activa'
	Order by StartTime asc;
END;


CREATE TABLE dbo.Recursos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    CapacidadMaxima INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE() 
);

 select * from dbo.Recursos

 CREATE PROCEDURE dbo.sp_GetListaRecursos
    @id Int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Nombre,
        Descripcion,
        CapacidadMaxima,
        Activo,
        FechaCreacion
    FROM dbo.Recursos
    WHERE id=@id
    ORDER BY Nombre;
END;

-- INSERT INTO dbo.Recursos (Nombre, Descripcion, CapacidadMaxima)
--VALUES
--('Servidor102', 'Servidor equipado para sesiones de pruebas', 100);