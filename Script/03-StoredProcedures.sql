USE `5to_Bloody_Roar_2`;

DELIMITER $$

-- 1. AltaPersonaje
DROP PROCEDURE IF EXISTS AltaPersonaje $$
CREATE PROCEDURE AltaPersonaje (
    OUT p_idPersonaje INT, 
    IN p_Nombre VARCHAR(45), 
    IN p_NombreBestia VARCHAR(45), 
    IN p_ResistenciaBestia INT
)
BEGIN
    INSERT INTO Personaje (Nombre, NombreBestia, ResistenciaBestia) 
    VALUES (p_Nombre, p_NombreBestia, p_ResistenciaBestia);
    SET p_idPersonaje = LAST_INSERT_ID();
END $$

-- 2. AltaUsuario
DROP PROCEDURE IF EXISTS AltaUsuario $$
CREATE PROCEDURE AltaUsuario(
    OUT unIdUsuario INT,
    IN unNombre VARCHAR(45),
    IN unaContrasenia VARCHAR(64),
    IN unEmail VARCHAR(45)
)
BEGIN
    IF EXISTS(SELECT 1 FROM Usuario WHERE Email = unEmail) THEN
        SET unIdUsuario = -1;
    ELSE
        INSERT INTO Usuario (Nombre, Contrasenia, Email)
        VALUES (unNombre, unaContrasenia, unEmail);
        SET unIdUsuario = LAST_INSERT_ID();
    END IF;
END $$

-- 3. AltaAtaque
DROP PROCEDURE IF EXISTS AltaAtaque $$
CREATE PROCEDURE AltaAtaque(
    OUT unIdAtaque INT,
    IN unTipoAtaque VARCHAR(45),
    IN unDanio INT,
    IN unIdPersonaje INT
)
BEGIN
    INSERT INTO Ataque (Tipo_Ataque, Danio, idPersonaje)
    VALUES (unTipoAtaque, unDanio, unIdPersonaje);
    SET unIdAtaque = LAST_INSERT_ID();
END $$

-- 4. AltaCombate
DROP PROCEDURE IF EXISTS AltaCombate $$
CREATE PROCEDURE AltaCombate(
    OUT p_idCombate INT,
    IN p_idPersonaje INT,
    IN p_idUsuario INT,
    IN p_idModo_Juego INT,
    IN p_Duracion INT
)
BEGIN
    INSERT INTO Combate (idPersonaje, idUsuario, idModo_Juego, Duracion)
    VALUES (p_idPersonaje, p_idUsuario, p_idModo_Juego, p_Duracion);
    SET p_idCombate = LAST_INSERT_ID();
END $$

-- 5. ActualizarDuracionCombate
DROP PROCEDURE IF EXISTS ActualizarDuracionCombate $$
CREATE PROCEDURE ActualizarDuracionCombate (
    IN p_idCombate INT,
    IN p_Duracion INT
)
BEGIN
    UPDATE Combate SET Duracion = p_Duracion WHERE idCombate = p_idCombate;
END $$

-- 6. AltaModoJuego (CORREGIDO)
DROP PROCEDURE IF EXISTS AltaModoJuego $$
CREATE PROCEDURE AltaModoJuego (
    OUT unIdModoJuego INT,
    IN unNombre VARCHAR(45)
)
BEGIN
    INSERT INTO ModoJuego (Nombre) VALUES (unNombre);
    SET unIdModoJuego = LAST_INSERT_ID();
END $$

-- 7. EliminarAtaque
DROP PROCEDURE IF EXISTS EliminarAtaque $$
CREATE PROCEDURE EliminarAtaque(IN idAtaque INT)
BEGIN
    DELETE FROM Ataque WHERE idAtaque = idAtaque;
END $$

-- 8. BuscarUsuarioPorEmail
DROP PROCEDURE IF EXISTS BuscarUsuarioPorEmail $$
CREATE PROCEDURE BuscarUsuarioPorEmail(IN emailBuscado VARCHAR(100))
BEGIN
    SELECT idUsuario, Nombre, Contrasenia, Email
    FROM Usuario
    WHERE Email = emailBuscado
    LIMIT 1;
END $$

-- 9. ObtenerAtaque
DROP PROCEDURE IF EXISTS ObtenerAtaque $$
CREATE PROCEDURE ObtenerAtaque()
BEGIN
    SELECT 
        a.idAtaque,
        a.Tipo_Ataque AS TipoAtaque,
        a.Danio,
        a.idPersonaje,
        p.Nombre AS NombrePersonaje
    FROM Ataque a
    INNER JOIN Personaje p ON a.idPersonaje = p.idPersonaje;
END $$

-- NUEVOS SP PARA COMBATES CON NOMBRES
DROP PROCEDURE IF EXISTS ObtenerTodosCombatesConNombres $$
CREATE PROCEDURE ObtenerTodosCombatesConNombres()
BEGIN
    SELECT
        c.idCombate      AS IdCombate,
        c.Duracion       AS Duracion,
        p.Nombre         AS NombrePersonaje,
        u.Nombre         AS NombreUsuario,
        m.Nombre         AS NombreModoJuego
    FROM Combate c
    INNER JOIN Personaje p ON c.idPersonaje = p.idPersonaje
    INNER JOIN Usuario u   ON c.idUsuario = u.idUsuario
    INNER JOIN ModoJuego m ON c.idModo_Juego = m.idModoJuego
    ORDER BY c.idCombate DESC;
END $$

DROP PROCEDURE IF EXISTS ObtenerCombateConNombres $$
CREATE PROCEDURE ObtenerCombateConNombres(IN p_id INT)
BEGIN
    SELECT
        c.idCombate      AS IdCombate,
        c.Duracion       AS Duracion,
        p.Nombre         AS NombrePersonaje,
        u.Nombre         AS NombreUsuario,
        m.Nombre         AS NombreModoJuego
    FROM Combate c
    INNER JOIN Personaje p ON c.idPersonaje = p.idPersonaje
    INNER JOIN Usuario u   ON c.idUsuario = u.idUsuario
    INNER JOIN ModoJuego m ON c.idModo_Juego = m.idModoJuego
    WHERE c.idCombate = p_id;
END $$

DELIMITER ;