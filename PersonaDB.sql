USE [PersonaDB]
GO
/****** Object:  Table [dbo].[People]    Script Date: 14/03/2024 8:35:45 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Identification] [int] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[TypeId] [smallint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[IdTypeId] [varchar](50) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Identification] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14/03/2024 8:35:45 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserApp] [varchar](50) NOT NULL,
	[Pass] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserApp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[people_add]    Script Date: 14/03/2024 8:35:45 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[people_add]
	@Name varchar(50),
	@LastName varchar(50),
	@Identification int,
	@Email varchar(50),
	@TypeId int	
	
AS
BEGIN
	
SET NOCOUNT ON;
DECLARE @HasErrors INT;
DECLARE @IdPeople int;

SET @HasErrors = 0;
set @IdPeople = 0;

BEGIN TRY    
	BEGIN TRAN people_add

    insert into dbo.People
	(		
		Name,
		LastName,
		Identification,
		Email,
		TypeId,
		CreationDate,
		IdTypeId,
		FullName
	)
	values
	(		
		@Name,
		@LastName,
		@Identification,
		@Email,
		@TypeId,
		GETDATE(),
		CONVERT(VARCHAR(255), @Identification) + ' - ' + CONVERT(VARCHAR(10), @TypeId),
		@Name + ' - ' + @LastName
	)


	SET @IdPeople = (SELECT scope_identity());
	
	
	IF (@IdPeople <> 0)
	BEGIN
		SELECT	Id,
				@HasErrors HasErrors
		FROM	dbo.People
		WHERE	Id = @IdPeople
	END
	ELSE
	BEGIN
		SET @HasErrors = 1;
		SELECT @HasErrors HasErrors
	end

  COMMIT TRAN people_add
  
 END TRY  
 BEGIN CATCH
  ROLLBACK TRAN people_add

  SET	@HasErrors = 1;
  select @HasErrors HasErrors
  
 END CATCH  
    
END
GO
/****** Object:  StoredProcedure [dbo].[people_get]    Script Date: 14/03/2024 8:35:45 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[people_get]
		
AS
BEGIN
	
SET NOCOUNT ON;
DECLARE @HasErrors INT;

SET @HasErrors = 0;

BEGIN TRY    
	BEGIN TRAN people_get
	
	SELECT	FullName,
			case TypeId
				when 1 then 'Tarjeta de identidad'
				when 2 then 'Cedula'
				when 3 then 'Pasaporte'
			else
				'Sin identificar'
			end TypeId,
			Identification,
			Email,
			CONVERT(VARCHAR(10), CreationDate, 23) CreationDate,
			@HasErrors HasErrors
	FROM	dbo.People


  COMMIT TRAN people_get
  
 END TRY  
 BEGIN CATCH
  ROLLBACK TRAN people_get

  SET	@HasErrors = 1;
  select @HasErrors HasErrors
  
 END CATCH  
    
END
GO
/****** Object:  StoredProcedure [dbo].[Users_add]    Script Date: 14/03/2024 8:35:45 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Users_add]
	@UserApp varchar(50),
	@Pass varchar(50)
	
AS
BEGIN
	
SET NOCOUNT ON;
DECLARE @HasErrors INT;
DECLARE @IdUser int;

SET @HasErrors = 0;
set @IdUser = 0;

BEGIN TRY    
	BEGIN TRAN Users_add

    insert into dbo.Users
	(		
		UserApp,
		Pass,
		CreationDate
	)
	values
	(		
		LOWER(@UserApp),
		@Pass,
		GETDATE()
	)

	SET @IdUser = (SELECT scope_identity());	
	
	IF (@IdUser <> 0)
	BEGIN
		SELECT	Id,
				@HasErrors HasErrors
		FROM	dbo.Users
		WHERE	Id = @IdUser
	END
	ELSE
	BEGIN
		SET @HasErrors = 1;
		SELECT @HasErrors HasErrors
	end

  COMMIT TRAN Users_add
  
 END TRY  
 BEGIN CATCH
  ROLLBACK TRAN Users_add

  SET	@HasErrors = 1;
  select @HasErrors HasErrors
  
 END CATCH  
    
END
GO
/****** Object:  StoredProcedure [dbo].[users_get]    Script Date: 14/03/2024 8:35:45 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[users_get]
		
AS
BEGIN
	
SET NOCOUNT ON;
DECLARE @HasErrors INT;

SET @HasErrors = 0;

BEGIN TRY    
	BEGIN TRAN users_get
	
	SELECT	UserApp,
			CONVERT(VARCHAR(10), CreationDate, 23) CreationDate,
			@HasErrors HasErrors
	FROM	dbo.Users


  COMMIT TRAN users_get
  
 END TRY  
 BEGIN CATCH
  ROLLBACK TRAN users_get

  SET	@HasErrors = 1;
  select @HasErrors HasErrors
  
 END CATCH  
    
END
GO
/****** Object:  StoredProcedure [dbo].[Users_validate]    Script Date: 14/03/2024 8:35:45 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Users_validate]
	@UserApp varchar(50),
	@Pass varchar(50)
	
AS
BEGIN
	
SET NOCOUNT ON;
DECLARE @HasErrors INT;

SET @HasErrors = 0;

BEGIN TRY    
	BEGIN TRAN Users_validate
	
	IF EXISTS(SELECT 1 from dbo.Users where UserApp = @UserApp and Pass = @Pass)
	BEGIN
		SELECT	UserApp,
				@HasErrors HasErrors
		FROM	dbo.Users		
	END
	ELSE
	BEGIN
		SET @HasErrors = 1;
		SELECT @HasErrors HasErrors
	end

  COMMIT TRAN Users_validate
  
 END TRY  
 BEGIN CATCH
  ROLLBACK TRAN Users_validate

  SET	@HasErrors = 1;
  select @HasErrors HasErrors
  
 END CATCH  
    
END
GO
