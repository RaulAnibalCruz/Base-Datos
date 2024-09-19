USE `5to_Bloody_Roar_2`;

#Para agregar algun personaje
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaPersonaje;
CREATE PROCEDURE AltaPersonaje (IN p_idPersonaje INT, IN p_Nombre VARCHAR(45), IN p_NombreBestia VARCHAR(45), IN p_ResistenciaBestia INT)
BEGIN
    INSERT INTO Personaje (idPersonaje, Nombre, NombreBestia, ResistenciaBestia) 
    VALUES (p_idPersonaje, p_Nombre, p_NombreBestia, p_ResistenciaBestia);
    
END $$

#Para agregar un usuario
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaUsuario $$
CREATE PROCEDURE AltaUsuario(IN unIdUsuario INT, IN unNombre VARCHAR(45), IN unaContrasenia VARCHAR(64), IN unEmail VARCHAR(45))
BEGIN
    INSERT INTO Usuario (IdUsuario, Nombre, Contrasenia, Email) 
    VALUES (unIdUsuario, unNombre, unaContrasenia, unEmail);
END$$


#Para agregar un ataque
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaAtaque$$
CREATE PROCEDURE AltaAtaque(IN unIdAtaque INT, IN unTipoAtaque VARCHAR(45), IN unDanio INT)
BEGIN
    INSERT INTO Ataque (IdAtaque, Tipo_Ataque, Danio) 
    VALUES (unIdAtaque, unTipoAtaque, unDanio);
END$$

#Para agregar un combate
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCombate;
CREATE PROCEDURE AltaCombate(IN p_idCombate INT, IN p_idPersonaje INT, IN p_idJugador INT, IN p_idModo_Juego INT, IN p_Duracion INT
)
BEGIN
    INSERT INTO Combate (idCombate, idPersonaje, idJugador, idModo_Juego, Duracion)
    VALUES (p_idCombate, p_idPersonaje, p_idJugador, p_idModo_Juego, p_Duracion);
END $$



#Para cambiar la duracion del combate
DELIMITER $$
DROP PROCEDURE IF EXISTS ActualizarDuracionCombate;
CREATE PROCEDURE ActualizarDuracionCombate (IN p_idCombate INT, IN p_Duracion INT)
BEGIN
    UPDATE Combate
    SET Duracion = p_Duracion
    WHERE idCombate = p_idCombate;
END $$
