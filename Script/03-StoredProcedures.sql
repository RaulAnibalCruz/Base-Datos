-- Active: 1727132803198@@127.0.0.1@3306@5to_bloody_roar_2
USE `5to_Bloody_Roar_2`;

#Para agregar algun personaje
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaPersonaje $$
CREATE PROCEDURE AltaPersonaje (OUT p_idPersonaje INT, IN p_Nombre VARCHAR(45), IN p_NombreBestia VARCHAR(45), IN p_ResistenciaBestia INT)
BEGIN
    INSERT INTO Personaje (Nombre, NombreBestia, ResistenciaBestia) 
    VALUES (p_Nombre, p_NombreBestia, p_ResistenciaBestia);

    SET p_idPersonaje = LAST_INSERT_ID();
    
END $$

#Para agregar un usuario
DELIMITER $$

DROP PROCEDURE IF EXISTS AltaUsuario $$
CREATE PROCEDURE AltaUsuario(
    OUT unIdUsuario INT,
    IN unNombre VARCHAR(45),
    IN unaContrasenia VARCHAR(64),
    IN unEmail VARCHAR(45)
)
BEGIN
    -- Verificar si el email ya existe
    IF EXISTS(SELECT 1 FROM Usuario WHERE Email = unEmail) THEN
        SET unIdUsuario = -1; -- Indica que el usuario ya existe
    ELSE
        INSERT INTO Usuario (Nombre, Contrasenia, Email)
        VALUES (unNombre, unaContrasenia, unEmail);

        SET unIdUsuario = LAST_INSERT_ID();
    END IF;
END$$

DELIMITER ;



#Para agregar un ataque
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaAtaque $$
CREATE PROCEDURE AltaAtaque(OUT unIdAtaque INT, IN unTipoAtaque VARCHAR(45), IN unDanio INT, IN unIdPersonaje INT)
BEGIN
    INSERT INTO Ataque (Tipo_Ataque, Danio, idPersonaje)
    VALUES (unTipoAtaque, unDanio, unIdPersonaje);
    SET unIdAtaque = LAST_INSERT_ID();
END $$

#Para agregar un combate
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCombate $$
CREATE PROCEDURE AltaCombate(OUT p_idCombate INT, IN p_idPersonaje INT, IN p_idUsuario INT, IN p_idModo_Juego INT, IN p_Duracion INT)
BEGIN
    INSERT INTO Combate (idPersonaje, idUsuario, idModo_Juego, Duracion)
    VALUES (p_idPersonaje, p_idUsuario, p_idModo_Juego, p_Duracion);
    SET p_idCombate = LAST_INSERT_ID();
END $$



#Para cambiar la duracion del combate
DELIMITER $$
DROP PROCEDURE IF EXISTS ActualizarDuracionCombate $$
CREATE PROCEDURE ActualizarDuracionCombate (IN p_idCombate INT, IN p_Duracion INT)
BEGIN
    UPDATE Combate
    SET Duracion = p_Duracion
    WHERE idCombate = p_idCombate;
END $$

#Para dar de alta un modo de juego

DELIMITER $$
DROP PROCEDURE IF EXISTS AltaModoJuego $$
CREATE PROCEDURE AltaModoJuego (OUT unIdModoJuego INT, IN unNombre VARCHAR(45))
BEGIN
    INSERT INTO modojuego(idModoJuego, Nombre)
    VALUES (unIdModoJuego, unNombre);
    SET unIdModoJuego = LAST_INSERT_ID();
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS AgregarModoJuego $$
CREATE PROCEDURE AgregarModoJuego (OUT unIdModoJuego INT, IN unNombre VARCHAR(45))
BEGIN
    UPDATE modojuego
    SET Nombre = unNombre
    WHERE idModoJuego = unIdModoJuego;
END $$

#Llama al ataque
DROP PROCEDURE IF EXISTS ObtenerAtaque $$
CREATE PROCEDURE ObtenerAtaque()
BEGIN
    SELECT 
        a.idAtaque,
        a.Tipo_Ataque,
        a.Danio,
        a.idPersonaje,
        p.Nombre AS NombrePersonaje
    FROM Ataque a
    INNER JOIN Personaje p ON a.idPersonaje = p.idPersonaje;
END $$

DELIMITER $$

DROP PROCEDURE IF EXISTS EliminarAtaque $$
CREATE PROCEDURE EliminarAtaque(IN idAtaque INT)
BEGIN
    DELETE FROM Ataque WHERE IdAtaque = idAtaque;
END $$

DELIMITER ;

-- Buscar usuario por email (devuelve 0/1 fila)
DELIMITER $$
DROP PROCEDURE IF EXISTS BuscarUsuarioPorEmail $$
CREATE PROCEDURE BuscarUsuarioPorEmail(IN emailBuscado VARCHAR(100))
BEGIN
    SELECT idUsuario, Nombre, Contrasenia, Email
    FROM Usuario
    WHERE Email = emailBuscado
    LIMIT 1;
END $$
DELIMITER ;
