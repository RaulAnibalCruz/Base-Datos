USE `5to_Bloody_Roar_2`;

DELIMITER $$

-- Trigger: Si la resistencia es <= 0, la fuerza a 10. Si es mayor, la deja como está.
DROP TRIGGER IF EXISTS Personaje_BEFORE_INSERT $$
CREATE TRIGGER Personaje_BEFORE_INSERT
BEFORE INSERT ON Personaje
FOR EACH ROW
BEGIN
    IF NEW.ResistenciaBestia <= 0 THEN
        SET NEW.ResistenciaBestia = 10;
    END IF;
END $$

-- Trigger: Hashea la contraseña con SHA256 antes de insertar
DROP TRIGGER IF EXISTS Usuario_BEFORE_INSERT $$
CREATE TRIGGER Usuario_BEFORE_INSERT
BEFORE INSERT ON Usuario
FOR EACH ROW
BEGIN
    SET NEW.Contrasenia = SHA2(NEW.Contrasenia, 256);
END $$

-- Trigger: No permite daño <= 0 en Ataque
DROP TRIGGER IF EXISTS Ataque_BEFORE_INSERT $$
CREATE TRIGGER Ataque_BEFORE_INSERT
BEFORE INSERT ON Ataque
FOR EACH ROW
BEGIN
    IF NEW.Danio <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El daño del ataque no puede ser menor o igual a 0';
    END IF;
END $$

-- Trigger: Asigna automáticamente fecha y hora al crear un combate
DROP TRIGGER IF EXISTS trg_combate_before_insert $$
CREATE TRIGGER trg_combate_before_insert
BEFORE INSERT ON Combate
FOR EACH ROW
BEGIN
    SET NEW.fecha_hora = NOW();
END $$

DELIMITER ;