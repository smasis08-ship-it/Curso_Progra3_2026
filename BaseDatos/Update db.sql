USE [AlquilerVehiculos]
GO
/****** Object:  StoredProcedure [dbo].[PR_ObtenerVehiculos]    Script Date: 15/10/2026 18:27:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_ObtenerVehiculosDisponibles]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdVehiculo,
        M.Marca 'Marca',
        Modelo,
        Placa,
        V.Annio 'Año',
        cv.Nombre 'Clasificación',
        PrecioDia,
        V.Activo,
        V.FechaCreacion,
        V.FechaModificacion
        
    FROM Vehiculos v
    inner join Marcas M on m.IdMarca = v.MarcaId
    inner join ClasificacionesVehiculos cv on cv.IdClasificacion = v.IdClasificacion
    ORDER BY IdVehiculo DESC;
END;


USE [AlquilerVehiculos]
GO
/****** Object:  StoredProcedure [dbo].[PR_EliminarVehiculo]    Script Date: 15/10/2026 18:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_EliminarUsuario]
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuarios
    SET
        Activo = 0,
        FechaModificacion = GETDATE()
    WHERE IdUsuario = @IdUsuario;

    SELECT @@ROWCOUNT AS RegistrosActualizados;
END;


CREATE PROCEDURE PR_ObtenerVehiculoPorId
    @IdVehiculo INT
AS
BEGIN
    SET NOCOUNT ON;

   Select * from Vehiculos
   WHERE IdVehiculo = @IdVehiculo

END;
GO

USE [AlquilerVehiculos]
GO
/****** Object:  StoredProcedure [dbo].[PR_ActualizarRol]    Script Date: 15/10/2026 18:38:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[PR_ActualizarRol]
    @IdRol INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(250),
    @Estado BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Roles
    SET NombreRol = @Nombre,
        Descripcion = @Descripcion,
        FechaModificacion = GETDATE()
    WHERE IdRol = @IdRol;
END;



ALTER TABLE Vehiculos
ADD CONSTRAINT FK_Vehiculos_Marca
FOREIGN KEY (MarcaId)
REFERENCES Marcas(IdMarca);
GO

ALTER TABLE Vehiculos
ADD CONSTRAINT FK_Vehiculos_Clasificaciones
FOREIGN KEY (IdClasificacion)
REFERENCES ClasificacionesVehiculos(IdClasificacion);
GO


USE [AlquilerVehiculos]
GO
/****** Object:  StoredProcedure [dbo].[PR_InsertarVehiculo]    Script Date: 15/10/2026 19:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[PR_InsertarVehiculo]
    @MarcaId INT,
    @Modelo VARCHAR(100),
    @Placa VARCHAR(20),
    @PrecioDia DECIMAL(18,2),
    @Annio int = null,
    @IdClasificacion int = null,
    @Estado bit = 1
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Vehiculos
    (
        MarcaId,
        Modelo,
        Placa,
        PrecioDia,
        Activo,
        IdClasificacion,
        Annio
    )
    VALUES
    (
        @MarcaId,
        @Modelo,
        @Placa,
        @PrecioDia,
        @Estado,
        @IdClasificacion,
        @Annio
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS IdVehiculo;
END;
