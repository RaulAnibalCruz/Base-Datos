-- Active: 1727023960727@@127.0.0.1@3306
USE `5to_Bloody_Roar_2`;

# Este trigger establece la resistencia en 10 cuando se añade un personaje 
DELIMITER $$

DROP TRIGGER IF EXISTS Personaje_BEFORE_INSERT$$

CREATE TRIGGER Personaje_BEFORE_INSERT 
BEFORE INSERT ON Personaje 
FOR EACH ROW
BEGIN
    -- Actualizar la columna ResistenciaBestia del nuevo registro insertado
    SET NEW.ResistenciaBestia = 10;

END$$


# Verifica que la nueva contraseña no sea la misma que ya posea otro usuario
DELIMITER $$
DROP TRIGGER IF EXISTS Usuario_BEFORE_INSERT;
CREATE TRIGGER Usuario_BEFORE_INSERT BEFORE INSERT ON `Usuario`
FOR EACH ROW
BEGIN
	SET NEW.Contrasenia = SHA2(NEW.Contrasenia, 256);

    IF EXISTS(
        SELECT *
        FROM Usuario
        WHERE Contrasenia = NEW.Contrasenia
    )
    THEN
    SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La contraseña ya está en uso por otro usuario';
    END IF;

END $$

# Este trigger esta hecho para que al agregar un ataque el daño no sea menor o igual a cero
DELIMITER $$
DROP TRIGGER IF EXISTS Ataque_BEFORE_INSERT;
CREATE TRIGGER Ataque_BEFORE_INSERT BEFORE INSERT ON `Ataque`
FOR EACH ROW
BEGIN
	IF (NEW.Danio <= 0)
		then
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El daño del ataque no puede ser menor a 0';
    END IF;
END $$
